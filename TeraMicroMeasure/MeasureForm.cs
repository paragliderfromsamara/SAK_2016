using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeraMicroMeasure.XmlObjects;
using NormaLib.Devices;
using NormaLib.Devices.XmlObjects;
using NormaLib.DBControl.Tables;
using NormaLib.DBControl;
using NormaLib.Measure;
using NormaLib.UI;
using System.Diagnostics;
using NormaLib.SessionControl;
using NormaLib.ProtocolBuilders;


namespace TeraMicroMeasure
{
    internal delegate void XmlDeviceListDelegate(Dictionary<string, DeviceXMLState> xml_device_list);
    internal delegate void XMLDeviceDelegate(DeviceXMLState xml_device);
    internal delegate void WithoutAnAguments();
    public partial class MeasureForm : Form
    {
        bool IsOnline = false;
        int clientID;
        bool MeasureIsStartedOnDevice;
        private CableTestStat ? TestStats;


        private DBEntityTable BarabanTypes;
        private DBEntityTable LeadStatuses;
        private DBEntityTable Cables;
        private DBEntityTable MeasuredParameterTypes;
        private DBEntityTable IsolMaterialCoeffsTable;
        private DBEntityTable LeadMaterials;
        private DBEntityTable MeasuredParametersDataTable;
        

        private Cable cur_cable;
        private Cable currentCable
        {
            set
            {
               // if (cur_cable == value) return;
                cur_cable = value;
                cableStructureCB.Items.Clear();
                foreach (CableStructure cs in cur_cable.CableStructures.Rows)
                {
                    cableStructureCB.Items.Add(cs.StructureTitle);
                }
                cableLengthNumericUpDown.ValueChanged += CableLengthNumericUpDown_ValueChanged;
                cableStructureCB.SelectedIndex = -1;
                cableStructureCB.SelectedIndex = 0;
                cableStructureCB.Enabled = cur_cable.CableStructures.Rows.Count > 1;
                measureState.MeasuredCableID = (int)cur_cable.CableId;
                FillMeasuredParameterTypesCB();
                RecalculateMaterialCoeff();
                MeasureStateOnFormChanged();
            }
            get
            {
                return cur_cable;
            }
        }

        private CableStructure cur_struct;
        private CableStructure currentStructure
        {
            get
            {
                return cur_struct;
            }
            set
            {
                cur_struct = value;
                measureState.MeasureVoltage = (uint)cur_struct.IsolationResistanceVoltage;
                
                RefreshMeasureParameterDataTabs();
                RefreshMeasureControl();
                RefreshTestStatsLabelForCurrentMode();
                //MessageBox.Show(string.Join(", ", cur_struct.AffectedElements.Keys));
            }
        }

        private DeviceCaptureStatus device_capture_status;
        public DeviceCaptureStatus DeviceCaptureStatus => device_capture_status;

        private MeasureStatus measureStatus = MeasureStatus.STOPPED;
        private DeviceXMLState current_device_state;
        private CableStructureMeasuredParameterData[] CurrentMeasuredParameterDataCollection;
        private CableStructureMeasuredParameterData CurrentParameterData => measuredParameterDataTabs.TabPages.Count > 0 ? CurrentMeasuredParameterDataCollection[measuredParameterDataTabs.SelectedIndex] : null;
        private float MaterialCoeff = 1.0f;
        private MaterialCoeffCalculator materialCoeffCalculator;


        public int ClientID
        {
            get
            {
                return clientID;
            }set
            {
                clientID = value;
            }
        }
        private DeviceType captured_device_type = DeviceType.Unknown;
        public DeviceType CapturedDeviceType
        {
            get
            {
                return captured_device_type;
            }set
            {
                captured_device_type = value;
                RefreshDeviceSettingsControls();
                RefreshDeviceList();
            }   
        }



        private string captured_device_serial = String.Empty;
        public string CapturedDeviceSerial
        {
            get
            {
                return captured_device_serial;
            }
            set
            {
                captured_device_serial = value;
                
            }   
        }

        bool isCurrentPCClient;
        private MeasureXMLState measureState;
        private CableTestIni testFile;
        public bool HasState => measureState != null;
        private Dictionary<string, DeviceXMLState> remoteDevices = new Dictionary<string, DeviceXMLState>();

        public MeasureXMLState MeasureState
        {
            set
            {
                RemoveHandlersFromInputs();
                measureState = new MeasureXMLState(value.InnerXml);
                fillInputsFromClientState();
                if (measureState.WasChanged && isCurrentPCClient) MeasureStateOnFormChanged();
                AddHandlersToInputs();
            }
            get
            {
                return measureState;
            }
        }
        
        public EventHandler OnMeasureStateChanged;

        public MeasureForm(int client_id)
        {
            int curCLientId = SettingsControl.GetClientId();
            InitDesign();
            
            clientID = client_id;
            IsOnline = isCurrentPCClient = clientID == SettingsControl.GetClientId();
            //////////////////////////////////////////////////////////////
            ResetMeasureField();
            LoadLeadStatuses();
            LoadMeasuredParameterTypes();
            LoadMeasuredParametersData();
            LoadBarabanTypes();
            InitPanels();
            MeasureState = MeasureXMLState.GetDefault();
            SetDeviceCaptureStatus(DeviceCaptureStatus.DISCONNECTED);
            SetCapturedDeviceTypeId();
            LoadMaterials();
            LoadCables();
            InitMeasureDraft();
        }

        private void LoadBarabanTypes()
        {
            KeyValuePair<uint, string> zeroVal = new KeyValuePair<uint, string>(0, "Не выбран");
            BarabanTypes = BarabanType.get_all_active_as_table();
            if (BarabanTypes.Rows.Count > 0)
            {
                barabanTypeCB.ValueMember = "key";
                barabanTypeCB.DisplayMember = "value";
                barabanTypeCB.Items.Add(zeroVal);
                foreach (BarabanType bt in BarabanTypes.Rows) barabanTypeCB.Items.Add(new KeyValuePair<uint, string>(bt.TypeId, bt.TypeName));
                barabanTypeCB.SelectedIndex = 0;
            } else
            {
                barabanTypeCB.DropDownStyle = ComboBoxStyle.DropDown;
                barabanTypeCB.Text = "Список пуст";
                barabanSelectorPanel.Enabled = false;
            }

        }

        ToolStripMenuItem ClearCurrentPointValueToolStripItem;
        private void LoadLeadStatuses()
        {
            LeadStatuses = LeadTestStatus.get_all_as_table();
            leadStatusContextMenu.Items.Clear();
            ToolStripLabel label = new ToolStripLabel("Статус жилы");
            label.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label.ForeColor = Color.DarkBlue;

            ClearCurrentPointValueToolStripItem = new ToolStripMenuItem("Сбросить");
            ClearCurrentPointValueToolStripItem.Click += (s, q) => {
                ClearValueOfCurrentPoint();
            };
            leadStatusContextMenu.Items.Add(ClearCurrentPointValueToolStripItem);
            leadStatusContextMenu.Items.Add(label);
            foreach (LeadTestStatus sts in LeadStatuses.Rows)
            {
                ToolStripMenuItem i = new ToolStripMenuItem(sts.StatusTitle);
                i.Tag = sts;
                i.Click += (s, q) => 
                {
                    ToolStripMenuItem item = s as ToolStripMenuItem;
                    LeadTestStatus status = item.Tag as LeadTestStatus;
                    SetLeadStatusForCurrentPointOnTestFile(status);
                    leadStatusContextMenu.Close();
                };
                leadStatusContextMenu.Items.Add(i);
            }

        }



        private void SetLeadStatusForCurrentPointOnTestFile(LeadTestStatus status)
        {
            if (status.StatusId == GetLeadStatusByElementNumber(measurePointMap.CurrentElementNumber).StatusId) return;
            CableMeasurePoint p = measurePointMap.CurrentPoint;
            if (status.StatusId == LeadTestStatus.Ok)
            {
                CableMeasurePointMap map = new CableMeasurePointMap(currentStructure, MeasuredParameterType.Calling);
                map.SetMeasurePoint(p.ElementIndex, 0);
                do
                {
                    testFile.SetLeadStatusOfPoint(map.CurrentPoint, (int)status.StatusId);
                } while (map.TryGetNextPoint() && map.CurrentElementMeasurePointIndex > 0);
                if (currentStructure.AffectedElements.ContainsKey(measurePointMap.CurrentElementNumber)) currentStructure.AffectedElements.Remove(measurePointMap.CurrentElementNumber);
                RefreshMeasureControl();
                measurePointMap.SetMeasurePoint(p);
            }
            else
            {
                testFile.SetLeadStatusOfPoint(measurePointMap.CurrentPoint, (int)status.StatusId);
                for (int i = 0; i < measurePointMap.MeasurePointsPerElement; i++)
                {
                    measureResultDataGrid.Rows[measurePointMap.CurrentPoint.ElementIndex].Cells[ElementNumber.Index + 1 + i].Value = status.StatusTitle;
                }
                if (currentStructure.AffectedElements.ContainsKey(measurePointMap.CurrentElementNumber)) currentStructure.AffectedElements[measurePointMap.CurrentElementNumber] = status.StatusId;
                else currentStructure.AffectedElements.Add(measurePointMap.CurrentElementNumber, status.StatusId);
            }
            if (testFile.TestStatus == CableTestStatus.NotStarted)
            {
                SetSourceCableToTest(CableTestStatus.StopedByOperator);
            }
            RefreshTestStats();
        }

        private void LoadMeasuredParametersData()
        {
            MeasuredParametersDataTable = MeasuredParameterData.get_all();
        }

        private void LoadMaterials()
        {
            LeadMaterials = LeadMaterial.get_all_as_table();
            IsolMaterialCoeffsTable = IsolMaterialCoeffs.get_all_as_table();
            materialCoeffCalculator = new MaterialCoeffCalculator(IsolMaterialCoeffsTable, LeadMaterials);
        }

        private void LoadMeasuredParameterTypes()
        {
            MeasuredParameterTypes = MeasuredParameterType.get_all_as_table_for_cable_structure_form();
        }

        private void InitDesign()
        {
            InitializeComponent();
            measureResultDataGrid.ColumnHeadersDefaultCellStyle = NormaUIDataGridStyles.BuildElementsHeaderStyle();
            ElementNumber.DefaultCellStyle = NormaUIDataGridStyles.BuildElementCellStyle();
            SubElement_4.DefaultCellStyle = SubElement_3.DefaultCellStyle = SubElement_2.DefaultCellStyle = SubElement_1.DefaultCellStyle = NormaUIDataGridStyles.BuildResultStyle();
            startMeasureButton.BackColor = NormaUIColors.GreenColor;
        }

        private void InitMeasureDraft()
        {
            if (Cables.Rows.Count == 0) return;
            uint measureModeOnState = measureState.MeasureTypeId;
            testFile = new CableTestIni(ClientID);
            testFile.OnDraftLocked += (s, a)=> { SetDraftIsLock(); };
            if (!testFile.IsLockDraft)
            {
                testDraftControlPanel.Enabled = false;
                cableLengthNumericUpDown.Enabled = cableComboBox.Enabled = true;
                if (currentCable != null)
                {
                    Cable[] rows = (Cable[])Cables.Select($"{Cable.CableId_ColumnName} = {currentCable.CableId}");
                    if (rows.Length == 0) currentCable = Cables.Rows[0] as Cable;
                    else currentCable = rows[0];
                }else currentCable = Cables.Rows[0] as Cable;
                SetCurrentCableOnComboBox(currentCable);
                cableComboBox.SelectedValueChanged += MeasuredCableComboBox_SelectedValueChanged;
            }
            else
            {
                Cable cable = GetCableFromDraft();
                if (cable != null)
                {
                    SetDraftIsLock();
                    currentCable = cable;//SetCurrentCable(tested_cable);
                    SetCurrentCableOnComboBox(currentCable);
                }
                else
                {
                    ForceResetMeasureDraft();
                    currentCable = Cables.Rows[0] as Cable;
                    SetCurrentCableOnComboBox(currentCable);
                    cableComboBox.SelectedValueChanged += MeasuredCableComboBox_SelectedValueChanged;
                }
            }
            barabanTypeCB.SelectedValue = testFile.BarabanTypeId;
            barabanNameCB.Text = testFile.BarabanNumber;
            RefreshTestStats();
            
            //cableStatusLable.Text = testFile.TestedCableIsOnNorma ? "Кабель годен" : "Кабель не годен";
            //if (measureState.MeasureTypeId != measureModeOnState && currentCable.MeasuredParameterTypes_IDs.Contains(measureModeOnState)) SetMeasureModeComboBoxValue(measureModeOnState);
        }

        private void RefreshTestStats()
        {
            TestStats = testFile.GetCablleTestStat();
            RefreshTestStatsLabelForCurrentMode();
        }

        private void RefreshTestStatsLabelForCurrentMode()
        {
            if (TestStats == null) return;
            string text = $"Процент годности кабеля: {TestStats.Value.CableNormalPercent}%\n";
            double structPercent = TestStats.Value.StructuresNormalPercents.ContainsKey(currentStructure.StructureTypeId) ? TestStats.Value.StructuresNormalPercents[currentStructure.StructureTypeId].StructureNormalPercent : 0;
            double mpdPercent = structPercent == 0 ? 0 : TestStats.Value.StructuresNormalPercents[currentStructure.StructureTypeId].MeasuredParameterDataNormalPercents[CurrentParameterData.MeasuredParameterDataId];
            text += $"Процент годности структуры: {structPercent}%\n";
            text += $"Процент годности по текущему параметру: {mpdPercent}%";

            cableStatusLable.Text = text;
        }

        private void SetMeasureModeComboBoxValue(uint val)
        {
            if (MeasuredParameterType.IsItIsolationResistance(val))
            {
                measuredParameterCB.SelectedValue = MeasuredParameterType.Risol1;
                rIsolTypeSelectorCB.SelectedValue = val;
            }
            else
            {
                measuredParameterCB.SelectedValue = val;
            }
        }

        private Cable GetCableFromDraft()
        {
            Cable c = null;
            try
            {
                c = testFile.SourceCable;
                return c;
            }
            catch(Exception)
            {
                return null;
            }      
        }

        private void SetDraftIsLock()
        {
            cableComboBox.Enabled = false;
            cableLengthNumericUpDown.Enabled = false;
            testDraftControlPanel.Enabled = true;
            cableComboBox.SelectedValueChanged -= MeasuredCableComboBox_SelectedValueChanged;
        }

        private void ForceResetMeasureDraft()
        {
            if (testFile != null)
            {
                testFile.ResetFile();
                testFile = new CableTestIni(clientID);
                LoadCables();
                InitMeasureDraft();

            }
        }

        private void ResetMeasureDraft()
        {
            if (testFile != null)
            {
                DialogResult r = MessageBox.Show("При сбросе испытания полученные результаты будут безвозвратно утеряны!\n\nСбросить?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    ForceResetMeasureDraft();
                }
            }
        }


        private DialogResult QuestionTestNotSaved()
        {
            return MessageBox.Show("Предыдущее испытание не сохранено.\nНажмите \"Да\" чтобы его продолжить, \"Нет\" - начать новое испытание, не сохраняя данных предыдущено испытания", "Предыдущее испытание не сохранено!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void SetCurrentCableOnComboBox(Cable cable)
        {
            cableComboBox.SelectedValue = cable.CableId;
        }

        private void LoadCables()
        {
           try
           {
                Cables = Cable.get_all_as_table();
                if (Cables.Rows.Count > 0)
                {
                    cableComboBox.DataSource = Cables;
                    cableComboBox.DisplayMember = Cable.FullCableName_ColumnName;
                    cableComboBox.ValueMember = Cable.CableId_ColumnName;
                }else
                {
                    cableComboBox.Text = "Список кабелей пуст";
                    cableComboBox.SelectedIndex = 0;
                    cableComboBox.Enabled = false;
                    cableStructureCB.Enabled = false;
                    labelPointNumber.Text = "";
                    measureResultDataGrid.Visible = false;
                    testParamsControlPanel.Dock = DockStyle.Fill;
                    lblNoMeasureData.Text = "Добавьте кабель, для того, чтобы начать испытания";
                    lblNoMeasureData.Dock = DockStyle.Fill;
                    lblNoMeasureData.TextAlign = ContentAlignment.MiddleCenter;
                    lblNoMeasureData.AutoSize = false;
                    measureControlPanel.Visible = false;
                    measureResultPanel.Visible = false;
                    measuredParameterDataTabs.Visible = false;
                    testParamsControlPanel.Visible = false;
                }
            }catch
            {
                MessageBox.Show("Не удалось загрузить список кабелей");
            }
        }

        /// <summary>
        /// Инициализация для клиента 
        /// </summary>
        /// <param name="client_state"></param>
        public MeasureForm(ClientXmlState client_state) : this(client_state.ClientID)
        {
            MeasureState = client_state.MeasureState;
        }

        private void SetCapturedDeviceTypeId()
        {
            captured_device_type = GetDeviceTypeBySelectedParameterType();
        }

        private void InitPanels()
        {
            rIsolTypeSelectorCB.DisplayMember = measuredParameterCB.DisplayMember = "value";
            rIsolTypeSelectorCB.ValueMember = measuredParameterCB.ValueMember = "key";
            DisableRisolSelector();
            DisableParamtersCB();
            InitOperatorData();  
            testDraftControlPanel.Enabled = false;
        }

        private void InitOperatorData()
        {
            operatorLabel.Text = $"Оператор: {SessionControl.CurrentUser.FullNameShort}";
            //throw new NotImplementedException();
        }

        public void SetXmlDeviceList(Dictionary<string, DeviceXMLState> xml_device_list)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new XmlDeviceListDelegate(SetXmlDeviceList), new object[] { xml_device_list });
            }else
            {
                remoteDevices = xml_device_list;
                RefreshDeviceList();
            }
        }

        private void RefreshDeviceList()
        {
            int idx = 0;
            int i = -1;
            if (CapturedDeviceType == DeviceType.Unknown) captured_device_type = GetDeviceTypeBySelectedParameterType();
            availableDevices.Items.Clear();
            foreach(var d in remoteDevices.Values)
            {
                string status;
                string title = $"{d.TypeNameShort} {d.Serial}";
                if (d.ClientId > 0)
                {
                    status = $"(Поле {d.ClientId})";
                }else if (d.ClientId == 0)
                {
                    status = $"(Сервер)";
                } else
                {
                    status = $"(Не занят)";
                }

                if (d.TypeId == (int)CapturedDeviceType)
                {
                    ++i;
                    availableDevices.Items.Add($"{title} {status}");
                    if (clientID == d.ClientId) idx = i;
                }
            }
            if (i>-1)
            {
                availableDevices.SelectedIndex = idx;
                availableDevices.Enabled = true;
                availableDevices.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else
            {
                availableDevices.DropDownStyle = ComboBoxStyle.DropDown;
                availableDevices.Enabled = false;
                availableDevices.Text = "Устройства отсутсвуют";
            }
        }

        private DeviceType GetDeviceTypeBySelectedParameterType()
        {
            if (measuredParameterCB.SelectedItem == null) return DeviceType.Unknown;
            switch(((KeyValuePair<uint, string>)measuredParameterCB.SelectedItem).Key)
            {
                case MeasuredParameterType.Risol1:
                case MeasuredParameterType.Risol2:
                    return DeviceType.Teraohmmeter;
                case MeasuredParameterType.Rleads:
                    return DeviceType.Microohmmeter;
                default:
                    return DeviceType.Unknown;
            }
        }

        private void RefreshDeviceSettingsControls()
        {
            measureDelayUpDown.Visible = measureDelayLabel.Visible = captured_device_type == DeviceType.Microohmmeter || captured_device_type == DeviceType.Teraohmmeter;
            if (captured_device_type == DeviceType.Teraohmmeter)
            {
                measureDelayLabel.Text = "Разряд, с";
                measureDelayUpDown.Minimum = 10;
                measureDelayUpDown.Maximum = 250;
                measureDelayUpDown.Value = 10;
            }
            else if (captured_device_type == DeviceType.Microohmmeter)
            {
                measureDelayLabel.Text = "Выдержка, мс";
                measureDelayUpDown.Minimum = 200;
                measureDelayUpDown.Maximum = 1000;
                measureDelayUpDown.Value = 200;
            }
        }

        public void RefreshMeasureState(MeasureXMLState measure_state)
        {
            MeasureState = measure_state;
            SetConnectionStatus(true);
            //richTextBox1.Text = measure_state.InnerXml;
        }

        public void SetConnectionStatus(bool is_online)
        {
            if (is_online != IsOnline)
            {
                IsOnline = is_online;
            }
        }

        private void fillInputsFromClientState()
        {
            SetCableLengthFromXmlState();
            SetAfterMeasureDelayFromXMLState();
            SetCapturedDeviceFromXmlState();
        }

        private void SetCapturedDeviceFromXmlState()
        {
            CapturedDeviceSerial = measureState.CapturedDeviceSerial;
            CapturedDeviceType = (DeviceType)measureState.CapturedDeviceTypeId;
        }



        private void SetAfterMeasureDelayFromXMLState()
        {
            measureDelayUpDown.Value = measureState.AfterMeasureDelay;
        }

        private void SetCableLengthFromXmlState()
        {
            if (measureState.MeasuredCableLength < 1 || measureState.MeasuredCableLength > 10000)
            {
                measureState.MeasuredCableLength = 1000;
            }
            cableLengthNumericUpDown.Value = (decimal)measureState.MeasuredCableLength;
        }

        private void SetCableIDFromXmlState()
        {
            if (Cables.Rows.Count > 0)
            {
                if (measureState.MeasuredCableID < 0 || measureState.MeasuredCableID > cableComboBox.Items.Count)
                {
                    measureState.MeasuredCableID = 0;
                }
                cableComboBox.SelectedIndex = measureState.MeasuredCableID;
            }

        }


        private void AddHandlersToInputs()
        {
            cableLengthNumericUpDown.ValueChanged += CableLengthNumericUpDown_ValueChanged;
            measureDelayUpDown.ValueChanged += MeasureDelayUpDown_ValueChanged;
        }

        private void AveragingCounter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            measureState.AveragingTimes = Convert.ToUInt16(cb.SelectedIndex);
            MeasureStateOnFormChanged();
        }

        private void MeasureDelayUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown ud = sender as NumericUpDown;
            uint v = 0;
            uint.TryParse(ud.Value.ToString(), out v);
            if (captured_device_type == DeviceType.Teraohmmeter)
            {
                measureState.BeforeMeasureDelay = 0;
                measureState.AfterMeasureDelay = v == 0 ? 10 : v;
            }
            else if (captured_device_type == DeviceType.Microohmmeter)
            {
                measureState.BeforeMeasureDelay = v == 0 ? 200 : v;
                measureState.AfterMeasureDelay = 2;
            }
            MeasureStateOnFormChanged();
        }

        private void BeforeMeasureDelayUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown ud = sender as NumericUpDown;
            uint v = 0;
            uint.TryParse(ud.Value.ToString(), out v);
            measureState.BeforeMeasureDelay = v;
            MeasureStateOnFormChanged();
        }



        private void MeasuredCableComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            try
            {
                if ((uint)cb.SelectedValue != currentCable.CableId)
                {
                    currentCable = ((Cable)(Cables.Select($"{Cable.CableId_ColumnName} = {cb.SelectedValue}")[0]));
                }
            }
            catch (EvaluateException)
            {
                //
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка при загрузке структур", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void FillMeasuredParameterTypesCB()
        {
            //measuredParameterCB.Items.Clear();
            if (currentCable != null)
            {
                measuredParameterCB.Items.Clear();
                
                if (currentCable.MeasuredParameterTypes_IDs.Length > 0)
                {
                    MeasuredParameterType[] pTypes = (MeasuredParameterType[])MeasuredParameterTypes.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} IN ({string.Join(",", currentCable.MeasuredParameterTypes_IDs)})");
                    bool hasRisol = false;
                    foreach (MeasuredParameterType pType in pTypes)
                    {
                        string item_value;
                        uint item_key;
                        if (pType.IsIsolationResistance)
                        {
                            if (hasRisol) continue;
                            item_value = "Rиз";
                            item_key = pType.ParameterTypeId;
                            hasRisol = true;
                            FillRisolSelectorCB();
                        }else
                        {
                            item_value = pType.ParameterName;
                            item_key = pType.ParameterTypeId;
                        }
                        measuredParameterCB.Items.Add(new KeyValuePair<uint, string>(item_key, item_value));
                    }
                    measuredParameterCB.DropDownStyle = ComboBoxStyle.DropDownList;
                    measuredParameterCB.Enabled = true;
                    measuredParameterCB.SelectedIndex = 0;
                }
                else
                {
                    DisableParamtersCB();
                }
            }
        }

        private void FillRisolSelectorCB()
        {
            rIsolTypeSelectorCB.Items.Clear();
            MeasuredParameterType[] pTypes = (MeasuredParameterType[])MeasuredParameterTypes.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} IN ({string.Join(",", MeasuredParameterType.RisolParametersIDs)})");
            foreach(MeasuredParameterType t in pTypes)
            {
                rIsolTypeSelectorCB.Items.Add(new KeyValuePair<uint, string>(t.ParameterTypeId, t.ParameterName));
            }
            rIsolTypeSelectorCB.SelectedIndex = 0;
        }
        private void DisableParamtersCB()
        {
            measuredParameterCB.Text = "Параметры отсутсвуют";
            measuredParameterCB.DropDownStyle = ComboBoxStyle.DropDown;
            measuredParameterCB.Enabled = false;
        }

        private void RemoveHandlersFromInputs()
        {
            cableComboBox.SelectedValueChanged -= MeasuredCableComboBox_SelectedValueChanged;
            cableLengthNumericUpDown.ValueChanged -= CableLengthNumericUpDown_ValueChanged;

            measureDelayUpDown.ValueChanged -= MeasureDelayUpDown_ValueChanged;
        }

        private void CableLengthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown ud = sender as NumericUpDown;
            uint v = 1000;
            uint.TryParse(ud.Value.ToString(), out v);
            measureState.MeasuredCableLength = v;
            MeasureStateOnFormChanged();
        }

        private void MeasureStateOnFormChanged()
        {
            OnMeasureStateChanged?.Invoke(measureState, new EventArgs());
            richTextBox1.Text = measureState.InnerXml;
        }

        private void connectToDevice_Click(object sender, EventArgs e)
        {
            switch(device_capture_status)
            {
                case DeviceCaptureStatus.DISCONNECTED:
                    CaptureSelectedDevice();
                    if (!String.IsNullOrWhiteSpace(captured_device_serial))SetDeviceCaptureStatus(DeviceCaptureStatus.WAITING_FOR_CONNECTION);
                    break;
                case DeviceCaptureStatus.CONNECTED:
                    DisconnectSelectedDevice();
                    SetDeviceCaptureStatus(DeviceCaptureStatus.DISCONNECTED);
                    break;
                case DeviceCaptureStatus.WAITING_FOR_CONNECTION:
                    DisconnectSelectedDevice();
                    SetDeviceCaptureStatus(DeviceCaptureStatus.DISCONNECTED);
                    break;
            }
        }

        private void DisconnectSelectedDevice()
        {
            captured_device_serial = measureState.CapturedDeviceSerial = String.Empty;
            measureState.CapturedDeviceTypeId = (int)DeviceType.Unknown;
            SetMeasureStatus(MeasureStatus.STOPPED);
            measureState.MeasureStartFlag = false;
            ResetMeasureField();
            MeasureStateOnFormChanged();
            current_device_state = null;
        }

        private void ResetMeasureField()
        {
            deviceInfo.Text = "Измеритель не выбран";
            resultFieldLabel.Text = "0.0";
            measureTimerLabel.Text = "";
            SetResultFieldStyle(ResultFieldStyle.SIMPLE_RESULT);
        }

        private void CaptureSelectedDevice()
        {
            string serial = String.Empty;
            int i = 0;
            foreach (var d in remoteDevices.Values)
            {
                if (d.TypeId != (int)CapturedDeviceType) continue;
                if (i++ == availableDevices.SelectedIndex)
                {
                    richTextBox1.Text = $"FormClientId = {clientID}\nDeviceClientId = {d.ClientId}\nDeviceName = {d.TypeNameFull}";
                    if (d.ClientId == -1 || d.ClientId == clientID)
                    {
                        serial = d.Serial;
                    }else
                    {
                        MessageBox.Show($"{d.TypeNameFull} {d.Serial} занят другой измерительной линией!", "Устройство занято", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    break;
                }
                //i++;
            }
            if (!String.IsNullOrWhiteSpace(serial))
            {
                measureState.CapturedDeviceTypeId = (int)CapturedDeviceType;
                captured_device_serial = measureState.CapturedDeviceSerial = serial;
                MeasureStateOnFormChanged();
            }
        }

        private void SetMeasureStatus(MeasureStatus status)
        {
            switch(status)
            {
                case MeasureStatus.STOPPED:
                    startMeasureButton.Text = "Пуск измерения";
                    startMeasureButton.BackColor = NormaUIColors.GreenColor;
                    startMeasureButton.Enabled = true;
                    deviceControlButton.Enabled = true;
                    MeasureIsStartedOnDevice = false;
                    EnableMeasurePointControl();
                    rIsolTypeSelectorCB.Enabled = true;
                    measureDelayUpDown.Enabled = true;
                    testParamsControlPanel.Enabled = true;
                    measuredParameterDataTabs.Enabled = true;
                    StopMeasureTimer();
                    StopCountDownTimer();
                    break;
                case MeasureStatus.WILL_START:
                    startMeasureButton.Text = "Запускается...";
                    startMeasureButton.BackColor = NormaUIColors.OrangeColor;
                    startMeasureButton.Enabled = false;
                    deviceControlButton.Enabled = false;
                    rIsolTypeSelectorCB.Enabled = false;
                    measureDelayUpDown.Enabled = false;
                    testParamsControlPanel.Enabled = false;
                    measuredParameterDataTabs.Enabled = false;
                    SetSourceCableToTest();
                    DisableMeasurePointControl();
                    StopMeasureTimer();
                    break;
                case MeasureStatus.STARTED:
                    startMeasureButton.Text = "Остановить измерение";
                    startMeasureButton.BackColor = NormaUIColors.RedColor;
                    startMeasureButton.Enabled = true;
                    deviceControlButton.Enabled = false;
                    rIsolTypeSelectorCB.Enabled = false;
                    measureDelayUpDown.Enabled = false;
                    MeasureIsStartedOnDevice = true;
                    testParamsControlPanel.Enabled = false;
                    measuredParameterDataTabs.Enabled = false;
                    DisableMeasurePointControl();
                    StartMeasureTimer();
                    break;
                case MeasureStatus.WILL_STOPPED:
                    startMeasureButton.Text = "Останавливается...";
                    startMeasureButton.BackColor = NormaUIColors.OrangeColor;
                    startMeasureButton.Enabled = false;
                    deviceControlButton.Enabled = false;
                    rIsolTypeSelectorCB.Enabled = false;
                    testParamsControlPanel.Enabled = false;
                    measuredParameterDataTabs.Enabled = false;
                    DisableMeasurePointControl();
                    testFile.TestStatus = CableTestStatus.StopedByOperator;
                    StopMeasureTimer();
                    break;
            }
            measureStatus = status;
        }

        private void SetSourceCableToTest(uint status_id = CableTestStatus.Started)
        {
            if (testFile.SourceCable == null) testFile.SourceCable = currentCable;
            testFile.TestStatus = status_id;
        }

        #region MeasureTimerControl

        private MeasureTimer measureTimer;
        private void StartMeasureTimer()
        {
            if (measureTimer != null) StopMeasureTimer();
            if (!MeasuredParameterType.IsItIsolationResistance(measureState.MeasureTypeId)) return;
            int time = (MeasuredParameterType.IsItIsolationResistanceTime(measureState.MeasureTypeId)) ? currentStructure.GetRisolTimeLimit(CurrentParameterData) * 2 : currentStructure.GetRisolTimeLimit(CurrentParameterData);
            measureTimerLabel.Text = "00:00";

            measureTimer = new MeasureTimer(time, MeasureTimer_Tick, MeasureTimerFinished);
            measureTimer.Start();
        }

        private void MeasureTimer_Tick(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(MeasureTimer_Tick), new object[] { sender, e });
            }else
            {
                MeasureTimer t = sender as MeasureTimer;
                Debug.WriteLine(t.WatchDisplay);
                measureTimerLabel.Text = t.WatchDisplay;
            }

        }

        private void MeasureTimerFinished(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(MeasureTimerFinished), new object[] { sender, e });
            }else
            {
                if (measureStatus == MeasureStatus.STARTED) startMeasureButton.PerformClick();
            }

        }

        private void StopMeasureTimer()
        {
            if (measureTimer != null)
            {
                Debug.WriteLine("StopMeasureTimer()");
                measureTimer.Stop();
                measureTimer.Dispose();
            }
        }
        private MeasureTimer CountDownTimer = null;

        private void StartCountDownTimer()
        {
            if (CountDownTimer != null) StopCountDownTimer();
            if (measureTimer != null) StopMeasureTimer();
            if (!MeasuredParameterType.IsItIsolationResistance(measureState.MeasureTypeId)) return;
            int time = (int)measureDelayUpDown.Value;
            measureTimerLabel.Text = $"00:00";
            if (time > 0)
            {
                CountDownTimer = new MeasureTimer(time, MeasureTimer_Tick);
                CountDownTimer.Start(true);
            }

        }

        private void StopCountDownTimer()
        {
            if (CountDownTimer != null)
            {
                measureTimerLabel.Text = $"00:00";
                CountDownTimer.Stop();
                CountDownTimer.Dispose();
            }
        }

        #endregion

        private void DisableMeasurePointControl()
        {
            buttonNextElement.Enabled = buttonNextPoint.Enabled = buttonPrevElement.Enabled = buttonPrevPoint.Enabled = false;
            measureResultDataGrid.Enabled = false;
        }

        private void EnableMeasurePointControl()
        {
            measureResultDataGrid.Enabled = true;
            RefreshMeasurePointControlButtons();
        }

        private void SetDeviceCaptureStatus(DeviceCaptureStatus status)
        {
            switch(status)
            {
                case DeviceCaptureStatus.DISCONNECTED:
                    availableDevices.Visible = true;
                    availableDevices.Enabled = true;
                    panelMeasurePointControl.Visible = false; //startMeasureButton.Visible = false;
                    deviceControlButton.Text = "Подключить";
                    deviceControlButton.Visible = true;
                    deviceControlButton.Enabled = true;
                    measuredParameterCB.Enabled = true;
                    break;
                case DeviceCaptureStatus.CONNECTED:
                    availableDevices.Visible = false;
                    panelMeasurePointControl.Visible = true; //startMeasureButton.Visible = true;
                    deviceControlButton.Text = "Отключить";
                    deviceControlButton.Visible = true;
                    measuredParameterCB.Enabled = false;
                    break;
                case DeviceCaptureStatus.WAITING_FOR_CONNECTION:
                    availableDevices.Visible = true;
                    availableDevices.Enabled = false;
                    panelMeasurePointControl.Visible = false; //startMeasureButton.Visible = false;
                    deviceControlButton.Text = "Прервать";
                    deviceControlButton.Visible = true;
                    measuredParameterCB.Enabled = false;
                    break;
            }
            device_capture_status = status;
        }
        public void RefreshCapturedXmlDevice(DeviceXMLState xml_device)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new XMLDeviceDelegate(RefreshCapturedXmlDevice), new object[] { xml_device });
            }else
            {
                if (isCurrentPCClient)
                {
                    if (CheckDeviceCaptureStatus(xml_device))
                    {
                        CheckMeasureStatus(xml_device);
                        RefreshMeasureField(xml_device);
                        current_device_state = new DeviceXMLState(xml_device.InnerXml);
                    }
                }
                else
                {
                    RefreshMeasureField(xml_device);
                }
            }
        }

        private void CheckMeasureStatus(DeviceXMLState xml_device)
        {
            bool devMeasFlag = xml_device.WorkStatusId == (int)DeviceWorkStatus.MEASURE || xml_device.WorkStatusId == (int)DeviceWorkStatus.POLARIZATION;
            bool clMeasFlag = MeasureIsStartedOnDevice;
            bool stMeasFlag = measureState.MeasureStartFlag;

            if (!devMeasFlag && stMeasFlag && !clMeasFlag)
            {
                //запуск измерения

            }else if (devMeasFlag && stMeasFlag && !clMeasFlag)
            {
                //измерение запущено
                SetMeasureStatus(MeasureStatus.STARTED);
                
            }
            else if (devMeasFlag && stMeasFlag && clMeasFlag)
            {
                //измерение идёт
            }
            else if (devMeasFlag && !stMeasFlag && clMeasFlag)
            {
                //Остановка измерения
            }
            else if (!devMeasFlag && !stMeasFlag && clMeasFlag)
            {
                //Измерение остановлено
                if (xml_device.WorkStatusId == (int)DeviceWorkStatus.DEPOLARIZATION)
                {
                    SetMeasureStatus(MeasureStatus.WILL_STOPPED);
                    StartCountDownTimer();
                }
                else if (xml_device.WorkStatusId == (int)DeviceWorkStatus.IDLE)
                {
            
                    SetMeasureStatus(MeasureStatus.STOPPED);
                }
            }
            else if (!devMeasFlag && stMeasFlag && clMeasFlag)
            {
                //Измерение остановлено на устройстве
                measureState.MeasureStartFlag = false;

                if (xml_device.WorkStatusId == (int)DeviceWorkStatus.DEPOLARIZATION)
                {
                    SetMeasureStatus(MeasureStatus.WILL_STOPPED);
                    StartCountDownTimer();
                }
                else if (xml_device.WorkStatusId == (int)DeviceWorkStatus.IDLE)
                {
                    SetMeasureStatus(MeasureStatus.STOPPED);
                }
                MeasureStateOnFormChanged();
            }

        }

        private bool CheckDeviceCaptureStatus(DeviceXMLState device_state)
        {
            if (device_capture_status == DeviceCaptureStatus.WAITING_FOR_CONNECTION)
            {
                if (device_state.ClientId == clientID && device_state.IsOnPCMode)
                {
                    SetDeviceCaptureStatus(DeviceCaptureStatus.CONNECTED);
                    RefreshMeasureField(device_state);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (device_capture_status == DeviceCaptureStatus.CONNECTED)
            {
                if (device_state.ClientId != clientID)
                {
                    DisconnectDeviceFromServerSide();
                    measureState.MeasureStartFlag = false;
                    MeasureStateOnFormChanged();
                    return false;
                }
                else
                {
                    RefreshMeasureField(device_state);
                    return true;
                }
            }
            else return false;
        }

        private void RefreshMeasureField(DeviceXMLState xml_device)
        {
            deviceInfo.Text = $"{xml_device.TypeNameFull} {xml_device.Serial}\n{xml_device.WorkStatusText}";
            if (xml_device.WorkStatusId == (uint)DeviceWorkStatus.MEASURE)
            {
                resultFieldLabel.Text = GetResultFieldText(xml_device);
            }
        }

        private string GetResultFieldText(DeviceXMLState xml_device)
        {
            if (xml_device.MeasureStatusId == (uint)DeviceMeasureStatus.SUCCESS)
            {
                //return $"{xml_device.RawResult}";
                MeasureResultConverter c = new MeasureResultConverter(xml_device.RawResult, CurrentParameterData, (Single)cableLengthNumericUpDown.Value, MaterialCoeff);
                return ProcessResult(c);//return $"{Math.Round(xml_device.ConvertedResult, 3, MidpointRounding.AwayFromZero)}";
            }else if (xml_device.MeasureStatusId == 0 || xml_device.MeasureStatusId == (uint)DeviceMeasureStatus.IN_WORK)
            {
                return "";
            }
            else
            {
                SetResultFieldStyle(ResultFieldStyle.DEVICE_ERROR_MESSAGE);
                return xml_device.MeasureStatusText;
            }
        }

        private string ProcessResult(MeasureResultConverter c)
        {
            bool addValueToProtocol = true;
            double valueToProtocol = c.RawValue;
            double valueToTable = c.ConvertedValueRounded;
            string stringVal = c.ConvertedValueLabel;
            NormDeterminant d;
            MeasureResultConverter cr;
            switch (measureState.MeasureTypeId)
            {
                case MeasuredParameterType.Risol2:
                    CableStructureMeasuredParameterData RisolNormaValue = CurrentParameterData.GetRisolNormaValue();
                    cr = new MeasureResultConverter(c.RawValue, RisolNormaValue, (float)cableLengthNumericUpDown.Value, MaterialCoeff);
                    d = new NormDeterminant(RisolNormaValue, cr.ConvertedValueRounded);
                    stringVal = cr.ConvertedValueLabel;
                    if (addValueToProtocol = d.IsOnNorma || measureTimer.TimeInSeconds > CurrentParameterData.MaxValue)
                    {
                        valueToProtocol = valueToTable = measureTimer.TimeInSeconds;
                        if (measureStatus == MeasureStatus.STARTED) startMeasureButton.PerformClick();
                    }
                    break;
                default:
                    cr = new MeasureResultConverter(c.RawValue, CurrentParameterData, (float)cableLengthNumericUpDown.Value, MaterialCoeff);
                    d = new NormDeterminant(CurrentParameterData, cr.ConvertedValueRounded);
                    SetResultFieldStyle((d.IsOnNorma ? ResultFieldStyle.IN_NORMA_RESULT : ResultFieldStyle.OUT_NORMA_RESULT));
                    break;
            }
            if (addValueToProtocol)
            {
                testFile.SetMeasurePointValue(measurePointMap.CurrentPoint, (float)valueToProtocol, (float)temperatureValue.Value);
                WriteValueToDataGridViewCell(valueToTable);
                RefreshTestStats();
            }

            return stringVal;
        }

        private void WriteValueToDataGridViewCell(double value)
        {
            WriteValueToDataGridViewCell(value, measurePointMap.CurrentElementIndex, measurePointMap.CurrentElementMeasurePointIndex);
        }
        private void WriteValueToDataGridViewCell(double value, int element_index, int measure_per_element_index)
        {
            DataGridViewRow row = measureResultDataGrid.Rows[element_index];
            WriteValueToDataGridViewCell(value, row, measure_per_element_index);
        }

        private void WriteValueToDataGridViewCell(double value, DataGridViewRow row, int measure_per_element_index)
        {
            try
            {
                if (!float.IsNaN((float)value))
                {
                    row.Cells[measure_per_element_index + SubElement_1.Index].Value = value;
                }
                else
                {
                   row.Cells[measure_per_element_index + SubElement_1.Index].Value = "-";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ClearValueOfCurrentPoint()
        {
            testFile.ClearMeasurePointValue(measurePointMap.CurrentPoint);
            WriteValueToDataGridViewCell(double.NaN, measurePointMap.CurrentElementIndex, measurePointMap.CurrentElementMeasurePointIndex);
            RefreshTestStats();
        }

        public void DisconnectDeviceFromServerSide()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new WithoutAnAguments(DisconnectDeviceFromServerSide), new object[] { });
            }else
            {
                DisconnectSelectedDevice();
                SetDeviceCaptureStatus(DeviceCaptureStatus.DISCONNECTED);
            }
        }

        private void startMeasureButton_Click(object sender, EventArgs e)
        {
            switch(measureStatus)
            {
                case MeasureStatus.STOPPED:
                    if (GetAgreementForStartMeasure())
                    {
                        measureState.MeasureStartFlag = true;
                        SetMeasureStatus(MeasureStatus.WILL_START);
                        MeasureStateOnFormChanged();
                        MeasureIsStartedOnDevice = false;
                    }
                    break;
                case MeasureStatus.STARTED:
                    measureState.MeasureStartFlag = false;
                    SetMeasureStatus(MeasureStatus.WILL_STOPPED);
                    MeasureStateOnFormChanged();
                    break;
            }
        }

        private bool GetAgreementForStartMeasure()
        {
            bool flag = true;
            LeadTestStatus currentElementStatus = GetLeadStatusByElementNumber(measurePointMap.CurrentElementNumber);
            if (currentElementStatus.StatusId != LeadTestStatus.Ok)
            {
                MessageBox.Show($"Выбранный элемент кабеля забракован, измерения невозможны.", "Элемент забракован", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;

            }
            if (MeasuredParameterType.IsItIsolationResistance(measureState.MeasureTypeId))
            {
                if (measureState.MeasureVoltage > 10)
                {
                    return (MessageBox.Show($"На измерительную цепь будет подано напряжение {measureState.MeasureVoltage} Вольт.\nНажмите \"Да\", если Вы убедились, что операторы с других испытательных линий закончили работу с измерительной цепью. ", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                }
            }
            return flag;
        }

        private void cableStructureCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentStructure = currentCable.CableStructures.Rows[cableStructureCB.SelectedIndex] as CableStructure;
          
        }

        CableMeasurePointMap measurePointMap;
        bool pointChangedByClick = false;
        private void RefreshMeasureControl()
        {
            if (currentStructure == null)
            {
                SetNoMeasuredParameterData_State();
            }
            if (currentStructure.MeasuredParameterTypes_ids.Contains(measureState.MeasureTypeId))
            {
                InitPointMapForCurrentStructureAndCurrentMeasureType();
                SetHasMeasuredParameterData_State();
                //int subElsCounter = MeasuredParameterType.MeasurePointNumberPerStructureElement(measureState.MeasureTypeId, currentStructure.StructureType.StructureLeadsAmount);
                int[] valueColumns = new int[] { SubElement_1.Index, SubElement_2.Index, SubElement_3.Index, SubElement_4.Index };
                List<DataGridViewRow> rowsForAdd = new List<DataGridViewRow>();
                measureResultDataGrid.Rows.Clear();
                SubElement_1.Visible = true;
                SubElement_2.Visible = measurePointMap.MeasurePointsPerElement > 1;
                SubElement_3.Visible = measurePointMap.MeasurePointsPerElement > 2;
                SubElement_4.Visible = measurePointMap.MeasurePointsPerElement > 3;

                SubElement_1.HeaderText = measurePointMap.MeasurePointsPerElement > 1 ? "Жила 1" : "Результат";
                SubElement_2.HeaderText = "Жила 2";
                SubElement_3.HeaderText = "Жила 3";
                SubElement_4.HeaderText = "Жила 4";

                for (int i = 0; i < currentStructure.RealAmount; i++)
                {
                    //measurePointMap.SetMeasurePoint(i, 0);
             
                    LeadTestStatus sts = GetLeadStatusByElementNumber(i+1);
                    DataGridViewRow r = new DataGridViewRow();
                    r.CreateCells(measureResultDataGrid);
                    r.Cells[ElementNumber.Index].Value = $"{currentStructure.StructureType.StructureTypeName} {i + 1}";
                    for (int j = 0; j < measurePointMap.MeasurePointsPerElement; j++)
                    {
                        if (sts.StatusId != LeadTestStatus.Ok)
                        {
                            r.Cells[ElementNumber.Index + 1 + j].Value = sts.StatusTitle;
                        }
                        else
                        {
                            int pointIdx = measurePointMap.GetPointIndex(i, j);
                            double val = testFile.GetMeasurePointValue((int)currentStructure.CableStructureId, (int)measureState.MeasureTypeId, pointIdx);
                            if (!double.IsNaN(val))
                            {
                                float temp = testFile.GetMeasurePointTemperature((int)currentStructure.CableStructureId, (int)measureState.MeasureTypeId, pointIdx);
                                MeasureResultConverter c = new MeasureResultConverter(val, CurrentParameterData, (float)cableLengthNumericUpDown.Value, materialCoeffCalculator.CalculateCoeff(measureState.MeasureTypeId, currentStructure, temp));
                                val = c.ConvertedValueRounded;
                            }
                            WriteValueToDataGridViewCell(val, r, j);
                        }

                    }
                    rowsForAdd.Add(r);
                }
                measureResultDataGrid.Rows.AddRange(rowsForAdd.ToArray());
                RefreshMeasurePointLabel();
                SelectCurrentMeasurePointOnResultGrid();
            }
            else
            {
                SetNoMeasuredParameterData_State();
            }
        }

        private LeadTestStatus GetLeadStatusByElementNumber(int el_number)
        {
            if (currentStructure.AffectedElements.ContainsKey(el_number))
            {
                LeadTestStatus[] sts = (LeadTestStatus[])LeadStatuses.Select($"{LeadTestStatus.StatusId_ColumnName} = {currentStructure.AffectedElements[el_number]}");
                if (sts.Length > 0) return sts[0];
                else return (LeadTestStatus)LeadStatuses.Rows[0];
            }
            else return (LeadTestStatus)LeadStatuses.Rows[0];
        }

        private void SetNoMeasuredParameterData_State()
        {
            startMeasureButton.Enabled = false;
            DisableMeasurePointControl();
            measuredParameterDataTabs.TabPages.Clear();
            normaLabel.Text = string.Empty;
            measuredParameterDataTabs.Visible = false;
            lblNoMeasureData.Text = currentStructure == null ? "Не выбрана структура для измерения" : $"Для структуры {currentStructure.StructureTitle} отсутсвуют нормы\nдля выбранного параметра";
            lblNoMeasureData.Visible = true;
        }

        private void SetHasMeasuredParameterData_State()
        {
            startMeasureButton.Enabled = true;
            EnableMeasurePointControl();

            lblNoMeasureData.Visible = false;
            measuredParameterDataTabs.Visible = true;
        }

        private void RefreshMeasureParameterDataTabs()
        {
            if (currentStructure == null) return;
            if (currentStructure.HasMeasuredParameters)
            {
                measuredParameterDataTabs.TabPages.Clear();

                CurrentMeasuredParameterDataCollection = (CableStructureMeasuredParameterData[])currentStructure.MeasuredParameters.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {measureState.MeasureTypeId}");
                foreach(CableStructureMeasuredParameterData td in CurrentMeasuredParameterDataCollection)
                {
                    TabPage tp = new TabPage(td.GetFullTitle());
                    measuredParameterDataTabs.TabPages.Add(tp);
                }
                ReplaceResultGridToPage();
                RefreshMeasureControl();
            }
            //throw new NotImplementedException();
        }

        private void SelectCurrentMeasurePointOnResultGrid()
        {
            measureResultDataGrid.ClearSelection();
            measureResultDataGrid.Rows[measurePointMap.CurrentElementIndex].Cells[ElementNumber.Index + measurePointMap.CurrentMeasurePointNumber].Selected = true;
            if (!pointChangedByClick)
            {
                ScrollResultTableToSelectedElement();
            }
            else pointChangedByClick = false;
        }

        private void ScrollResultTableToSelectedElement()
        {
            if (measureResultDataGrid.DisplayedRowCount(false) > 0) measureResultDataGrid.FirstDisplayedScrollingRowIndex = (measurePointMap.CurrentElementIndex / measureResultDataGrid.DisplayedRowCount(false)) * measureResultDataGrid.DisplayedRowCount(false);
        }

        private void OnMeasurePointChanged_Handler(object sender, EventArgs e)
        {
            RefreshMeasurePointLabel();
            SelectCurrentMeasurePointOnResultGrid();
            RefreshMeasurePointControlButtons();
        }

        private void RefreshMeasurePointLabel()
        {
            labelPointNumber.Text = (measurePointMap == null) ? String.Empty : $"{measurePointMap.CurrentPoint.ElementTitle} {measurePointMap.CurrentPoint.MeasureTitle}";
        }

        private void buttonNextPoint_Click(object sender, EventArgs e)
        {
            measurePointMap.SetNextPoint();
        }

        private void buttonPrevPoint_Click(object sender, EventArgs e)
        {
            measurePointMap.SetPrevPoint();
        }

        private void buttonNextElement_Click(object sender, EventArgs e)
        {
            measurePointMap.SetNextElement();
        }

        private void buttonPrevElement_Click(object sender, EventArgs e)
        {
            measurePointMap.SetPrevElement();
        }

        private void RefreshMeasurePointControlButtons()
        {
            buttonPrevElement.Enabled = measurePointMap.PrevElementEnabled;
            buttonPrevPoint.Enabled = measurePointMap.PrevPointEnabled;
            buttonNextPoint.Enabled = measurePointMap.NextPointEnabled;
            buttonNextElement.Enabled = measurePointMap.NextElementEnabled;
        }

        private void InitPointMapForCurrentStructureAndCurrentMeasureType()
        {
            measurePointMap = new CableMeasurePointMap(currentStructure, measureState.MeasureTypeId);
            measurePointMap.OnMeasurePointChanged += OnMeasurePointChanged_Handler;
            RefreshMeasurePointControlButtons();
        }


        void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData & Keys.KeyCode)
            {
                case Keys.Up:
                    buttonNextElement.PerformClick();
                    break;
                case Keys.Right:
                    buttonNextPoint.PerformClick();
                    break;
                case Keys.Down:
                    buttonPrevElement.PerformClick();
                    break;
                case Keys.Left:
                    buttonPrevPoint.PerformClick();
                    break;
            }
            switch (e.KeyData & Keys.KeyCode)
            {
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                case Keys.Left:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
            }

        }



        private void measuredParameterCB_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox rb = sender as ComboBox;
            int selIdx = rb.SelectedIndex;
            uint parameterTypeId = rb.SelectedIndex == -1 ? 0 : ((KeyValuePair<uint, string>)rb.SelectedItem).Key;
            uint parameterTypeWas = measureState.MeasureTypeId;
            if (DeviceCaptureStatus == DeviceCaptureStatus.CONNECTED && rb.Name != rIsolTypeSelectorCB.Name)
            {
                try
                {
                    bool isIsolResistance = MeasuredParameterType.IsItIsolationResistance(parameterTypeWas);
                    uint pTypeId = isIsolResistance ? MeasuredParameterType.Risol1 : parameterTypeWas;
                    if (currentCable.MeasuredParameterTypes_IDs.Contains(pTypeId))
                    {
                        measuredParameterCB.SelectedIndexChanged -= measuredParameterCB_SelectedIndexChanged;
                        for (int i = 0; i < measuredParameterCB.Items.Count; i++)
                        {
                            KeyValuePair<uint, string> v = (KeyValuePair<uint, string>)measuredParameterCB.Items[i];
                            if (v.Key == pTypeId)
                            {
                                measuredParameterCB.SelectedIndex = i;
                                break;
                            }
                        }
                        measuredParameterCB.SelectedIndexChanged += measuredParameterCB_SelectedIndexChanged;
                        measuredParameterCB.Enabled = false;
                        if (MeasuredParameterType.IsItIsolationResistance(pTypeId))
                        {
                            rIsolTypeSelectorCB.SelectedIndexChanged -= measuredParameterCB_SelectedIndexChanged;
                            rIsolTypeSelectorCB.SelectedIndex = 0;
                            for (int i = 0; i < rIsolTypeSelectorCB.Items.Count; i++)
                            {
                                KeyValuePair<uint, string> v = (KeyValuePair<uint, string>)rIsolTypeSelectorCB.Items[i];
                                if (v.Key == parameterTypeWas)
                                {
                                    rIsolTypeSelectorCB.SelectedIndex = i;
                                    break;
                                }
                            }
                            parameterTypeId = (rIsolTypeSelectorCB.SelectedIndex == 0) ? pTypeId : parameterTypeWas;
                            rIsolTypeSelectorCB.SelectedIndexChanged += measuredParameterCB_SelectedIndexChanged;
                            rIsolTypeSelectorCB.Enabled = true;
                        }
                        else parameterTypeId = parameterTypeWas;
                    }
                    else deviceControlButton.PerformClick();
                }
                catch(Exception)
                {
                    deviceControlButton.PerformClick();
                    rb.SelectedIndexChanged -= measuredParameterCB_SelectedIndexChanged;
                    rb.SelectedIndex = selIdx;
                    rb.SelectedIndexChanged += measuredParameterCB_SelectedIndexChanged;
                }
            }
            if (MeasuredParameterType.IsItIsolationResistance(parameterTypeId) && !MeasuredParameterType.IsItIsolationResistance(parameterTypeWas))
                EnableRisolSelector();
            else if (!MeasuredParameterType.IsItIsolationResistance(parameterTypeId) && MeasuredParameterType.IsItIsolationResistance(parameterTypeWas))
                DisableRisolSelector();
            CapturedDeviceType = GetDeviceTypeBySelectedParameterType();
            measureState.MeasureTypeId = parameterTypeId;
            RefreshMeasureParameterDataTabs();
            MeasureStateOnFormChanged();
            //normaLabel.Text = string.Empty;
        }

        private void EnableRisolSelector()
        {
            rIsolTypeSelectorCB.Visible = true;
            rIsolTypeSelectorCB.SelectedIndexChanged += measuredParameterCB_SelectedIndexChanged;
        }
        private void DisableRisolSelector()
        {
            rIsolTypeSelectorCB.Visible = false;
            rIsolTypeSelectorCB.SelectedIndexChanged -= measuredParameterCB_SelectedIndexChanged;
        }



        private void temperature_ValueChanged(object sender, EventArgs e)
        {
            RecalculateMaterialCoeff();
        }

        private void RecalculateMaterialCoeff()
        {
            float mat_coeff = 1.0f;
            if (currentStructure != null && measuredParameterCB.SelectedValue != null)
            {
                mat_coeff = materialCoeffCalculator.CalculateCoeff((uint)measuredParameterCB.SelectedValue, currentStructure, (float)temperatureValue.Value);
            }
            MaterialCoeff = mat_coeff;
        }

        private void measuredParameterDataTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReplaceResultGridToPage();
        }

        private void ReplaceResultGridToPage()
        {
            if (measuredParameterDataTabs.SelectedIndex >= 0)
            {
                string norma;
                measuredParameterDataTabs.TabPages[measuredParameterDataTabs.SelectedIndex].Controls.Add(panelResultMeasure);
                norma = CurrentParameterData.GetFullTitle();
                normaLabel.Text = string.IsNullOrWhiteSpace(norma) ? "" : $"Норма: {norma}";
                resetCurrentResultsButton.Text = $"Очистить результаты {CurrentParameterData.ParameterName}";
                RefreshTestStatsLabelForCurrentMode();
            }
        }

        private void MeasureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (measureStatus != MeasureStatus.STOPPED)
            {
                MessageBox.Show("Невозможно перейти в другое меню или закрыть приложение при запущенном измерении!", "Измерение не завершено!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Cancel = true;
            }else
            {
                if (DeviceCaptureStatus == DeviceCaptureStatus.CONNECTED)
                {
                    DisconnectSelectedDevice();
                }
            }
        }

        private void resetTestButton_Click(object sender, EventArgs e)
        {
            ResetMeasureDraft();
        }

        private void saveResultButton_Click(object sender, EventArgs e)
        {
            if (BarabanTypes.Rows.Count > 0)
            {
                if (((KeyValuePair<uint, string>)barabanTypeCB.SelectedItem).Key == 0)
                {
                    MessageBox.Show("Не выбран тип барабана!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(barabanNameCB.Text))
                {
                    MessageBox.Show("Введите серийный номер барабана!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                BarabanType bType = ((BarabanType[])BarabanTypes.Select($"{BarabanType.TypeId_ColumnName} = {((KeyValuePair<uint, string>)barabanTypeCB.SelectedItem).Key}"))[0];
                testFile.BarabanTypeWeight = bType.BarabanWeight;
                testFile.BarabanTypeId = bType.TypeId;
                testFile.BarabanNumber = barabanNameCB.Text;
            }
            testFile.OperatorID = SessionControl.CurrentUser.UserId;
            testFile.TestedCableLength = (uint)cableLengthNumericUpDown.Value;
            testFile.TestLineNumber = ClientID;
            testFile.Temperature = (float)temperatureValue.Value;

            CableTest test;
            if (testFile.SaveTest(out test))
            {
                ProtocolViewer v = new ProtocolViewer(new ProtocolPathBuilder(test, NormaExportType.MSWORD));
                ForceResetMeasureDraft();
            } 
        }

        private void measureResultDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.ColumnIndex > ElementNumber.Index && e.ColumnIndex <= SubElement_4.Index && measureStatus == MeasureStatus.STOPPED)
            {
                pointChangedByClick = true;
                measurePointMap.SetMeasurePoint(e.RowIndex, e.ColumnIndex - ElementNumber.Index - 1);
                if (e.Button == MouseButtons.Right)
                {
                    Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                    LeadTestStatus sts = GetLeadStatusByElementNumber(measurePointMap.CurrentPoint.ElementNumber);
                    ClearCurrentPointValueToolStripItem.Enabled = false;
                    foreach (object item in leadStatusContextMenu.Items)
                    {
                        if (typeof(ToolStripMenuItem) != item.GetType()) continue;
                        ToolStripMenuItem titem = item as ToolStripMenuItem;
                        if (titem.Tag != null)
                        {
                            LeadTestStatus s = titem.Tag as LeadTestStatus;
                            titem.Checked = s.StatusId == sts.StatusId;
                            if (titem.Checked) ClearCurrentPointValueToolStripItem.Enabled |= s.StatusId == LeadTestStatus.Ok;
                        } 
                    }
                    ClearCurrentPointValueToolStripItem.Visible = testFile.HasMeasurePointValue(measurePointMap.CurrentPoint);
                    leadStatusContextMenu.Show(p);
                }
            }
        }

        private void resetCurrentResultsButton_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show($"Вы уверены, что хотите очистить все результаты параметра {CurrentParameterData.ParameterName} ({CurrentParameterData.ParameterDescription})?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;
            testFile.ClearParameterDataResults(CurrentParameterData);
            RefreshMeasureControl();
            RefreshTestStats();
        }

        private void SetResultFieldStyle(ResultFieldStyle style)
        {
            switch(style)
            {
                case ResultFieldStyle.SIMPLE_RESULT:
                    resultFieldLabel.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    resultFieldLabel.ForeColor = Color.White;
                    break;
                case ResultFieldStyle.IN_NORMA_RESULT:
                    resultFieldLabel.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    resultFieldLabel.ForeColor = Color.FromArgb(142, 255, 142);
                    break;
                case ResultFieldStyle.OUT_NORMA_RESULT:
                    resultFieldLabel.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    resultFieldLabel.ForeColor = Color.FromArgb(255, 142, 142);
                    break;
                case ResultFieldStyle.DEVICE_ERROR_MESSAGE:
                    resultFieldLabel.Font = new System.Drawing.Font("Tahoma", 18.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    resultFieldLabel.ForeColor = Color.FromArgb(255, 142, 142);
                    break;
            }
        }
    }


    public enum DeviceCaptureStatus : byte
    {
        DISCONNECTED,
        WAITING_FOR_CONNECTION,
        CONNECTED
    }

    enum MeasureStatus
    {
        STOPPED,
        WILL_START,
        STARTED,
        WILL_STOPPED
    }

    enum ResultFieldStyle
    {
        SIMPLE_RESULT, 
        IN_NORMA_RESULT,
        OUT_NORMA_RESULT,
        DEVICE_ERROR_MESSAGE
    }
}
