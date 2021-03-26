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

        private DBEntityTable Cables;
        private DBEntityTable MeasuredParameterTypes;
        private DBEntityTable IsolMaterialCoeffsTable;
        private DBEntityTable LeadMaterials;
        private Cable currentCable;
        private CableStructure currentStructure;

        private DeviceCaptureStatus device_capture_status;
        public DeviceCaptureStatus DeviceCaptureStatus => device_capture_status;

        private MeasureStatus measureStatus = MeasureStatus.STOPPED;
        private DeviceXMLState current_device_state;
        private CableStructureMeasuredParameterData[] CurrentMeasuredParameterDataCollection;
        private CableStructureMeasuredParameterData CurrentParameterData => CurrentMeasuredParameterDataCollection.Length > 0 ? CurrentMeasuredParameterDataCollection[0] : null;
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
            testFile = new CableTestIni();
            clientID = client_id;
            IsOnline = isCurrentPCClient = clientID == SettingsControl.GetClientId();
            //////////////////////////////////////////////////////////////
            ResetMeasureField();
            LoadMeasuredParameterTypes();
            InitPanels();
            MeasureState = MeasureXMLState.GetDefault();
            SetDeviceCaptureStatus(DeviceCaptureStatus.DISCONNECTED);
            SetCapturedDeviceTypeId();
            LoadMaterials();
            LoadCables();
            InitMeasureDraft();
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
            if (testFile.CableID == 0 || !testFile.IsMeasureStart)
                SetCurrentCable(Cables.Rows[0] as Cable);
            else
            {
                DataRow[] dRows = Cables.Select($"{Cable.CableId_ColumnName} = {testFile.CableID}");
                if (dRows.Length > 0)
                {
                    if (QuestionTestNotSaved() == DialogResult.Yes)
                    {
                        ResetMeasureDraft();
                        SetCurrentCable(Cables.Rows[0] as Cable);
                    }else
                    {
                        SetCurrentCable(dRows[0] as Cable);
                    }
                }
            }
        }

        private void ResetMeasureDraft()
        {
            if (testFile != null) testFile.ResetFile();
            testFile = new CableTestIni();
        }

        private DialogResult QuestionTestNotSaved()
        {
            return MessageBox.Show("Предыдущее испытание не сохранено.\nНажмите \"Да\" чтобы его продолжить, \"Нет\" - начать новое испытание, не сохраняя данных предыдущено испытания", "Предыдущее испытание не сохранено!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void SetCurrentCable(Cable cable)
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
                    cableComboBox.SelectedValueChanged += MeasuredCableComboBox_SelectedValueChanged;

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
                    neasureResultPanel.Dock = DockStyle.Fill;
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
            captured_device_type = GetDeviceTypeByRadioBox();
            //this.Text = captured_device_type.ToString();
            // RleadRadioButton.Checked = true;
            // MeasureTypeRadioButton_CheckedChanged(RleadRadioButton, new EventArgs());
            // RefreshDeviceList();
            // captured_device_type = DeviceType.Microohmmeter;

        }

        private void InitPanels()
        {
            voltagesGroupBox.Visible = false;
            InitAverageCountComboBox();
            measuredParameterCB.DisplayMember = $"{MeasuredParameterType.ParameterName_ColumnName}";
            measuredParameterCB.ValueMember = $"{MeasuredParameterType.ParameterTypeId_ColumnName}";
            DisableParamtersCB();
            //selectDevicePanel.Visible = measurePanel.Enabled = isCurrentPCClient;
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
            if (CapturedDeviceType == DeviceType.Unknown) captured_device_type = GetDeviceTypeByRadioBox();
            availableDevices.Items.Clear();
            foreach(var d in remoteDevices.Values)
            {
                string status;
                string title = $"{d.TypeNameShort} {d.Serial}";
                if (d.ClientId > 0)
                {
                    status = $"(Линия {d.ClientId})";
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

        private DeviceType GetDeviceTypeByRadioBox()
        {
            if (measuredParameterCB.SelectedValue == null) return DeviceType.Unknown;
            switch((uint)measuredParameterCB.SelectedValue)
            {
                case MeasuredParameterType.Risol1:
                case MeasuredParameterType.Risol2:
                    return DeviceType.Teraohmmeter;
                case MeasuredParameterType.Rleads:
                    return DeviceType.Microohmmeter;
                default:
                    return DeviceType.Unknown;
            }
            //if (RleadRadioButton.Checked) return DeviceType.Microohmmeter;
            //else if (RizolRadioButton.Checked) return DeviceType.Teraohmmeter;
            //else return DeviceType.Unknown;
        }

        private void InitAverageCountComboBox()
        {
            averagingCounter.Items.Add("Без усреднения");
            averagingCounter.DropDownStyle = ComboBoxStyle.DropDownList;
            for(int i=1; i<=100; i++)
            {
                averagingCounter.Items.Add(i);
            }
            averagingCounter.SelectedIndex = 0;
        }

        public void RefreshMeasureState(MeasureXMLState measure_state)
        {
            MeasureState = measure_state;
            SetConnectionStatus(true);
            richTextBox1.Text = measure_state.InnerXml;
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
            //if (!HasClientState) return;
            //SetMeasureTypeFromXMLState();
            SetVoltageFromXmlState();
            //SetCableIDFromXmlState();
            SetCableLengthFromXmlState();
            SetBeforeMeasureDelayFromXMLState();
            SetAfterMeasureDelayFromXMLState();
            SetAveragingTimesFromXmlState();
            SetCapturedDeviceFromXmlState();
        }

        private void SetCapturedDeviceFromXmlState()
        {
            CapturedDeviceSerial = measureState.CapturedDeviceSerial;
            CapturedDeviceType = (DeviceType)measureState.CapturedDeviceTypeId;
        }

        private void SetAveragingTimesFromXmlState()
        {
            if (measureState.AveragingTimes < 0 || measureState.AveragingTimes > averagingCounter.Items.Count)
            {
                measureState.AveragingTimes = 0;
            }
            averagingCounter.SelectedIndex = Convert.ToInt16(measureState.AveragingTimes);
        }

        private void SetAfterMeasureDelayFromXMLState()
        {
            afterMeasureDelayUpDown.Value = measureState.AfterMeasureDelay;
        }

        private void SetBeforeMeasureDelayFromXMLState()
        {
            beforeMeasureDelayUpDown.Value = measureState.BeforeMeasureDelay;
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

        private void SetVoltageFromXmlState()
        {
            switch (measureState.MeasureVoltage)
            {
                case 10:
                    v10_RadioButton.Checked = true;
                    break;
                case 100:
                    v100_RadioButton.Checked = true;
                    break;
                case 500:
                    v500_RadioButton.Checked = true;
                    break;
                case 1000:
                    v1000_RadioButton.Checked = true;
                    break;
                default:
                    measureState.MeasureVoltage = 10;
                    v10_RadioButton.Checked = true;
                    break;
            }
        }
        /*
        private void SetMeasureTypeFromXMLState()
        {
            switch(measureState.MeasureTypeId)
            {
                case MeasuredParameterType.Rleads:
                    RleadRadioButton.Checked = true;
                    break;
                case MeasuredParameterType.Risol1:
                    RizolRadioButton.Checked = true;
                    break;
            }
        }*/

        private void MeasuredVoltageRadioButton_CheckedChanged(object sender, EventArgs e)
        {

            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                uint v = 0;
                if (rb == v10_RadioButton) v = 10;
                else if (rb == v100_RadioButton) v = 100;
                else if (rb == v500_RadioButton) v = 500;
                else if (rb == v1000_RadioButton) v = 1000;
                measureState.MeasureVoltage = v;
                //MessageBox.Show(v.ToString());
            }
            MeasureStateOnFormChanged();
        }

        //private void MeasureTypeRadioButton_CheckedChanged_Common(object sender, EventArgs e)
        //{
        //    voltagesGroupBox.Visible = RizolRadioButton.Checked;
        //    polarDelayLbl.Text = RizolRadioButton.Checked ? "Выдержка, мин" : "Выдержка, мс";
       //     depolTimeLbl.Text = RizolRadioButton.Checked ? "Разряд, с" : "Пауза, c";
       // }
        /*
        private void MeasureTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                uint mId = 0;
                if (rb == RizolRadioButton)
                {
                    mId = MeasuredParameterType.Risol1;
                }
                else if (rb == RleadRadioButton)
                {
                    mId = MeasuredParameterType.Rleads;
                }
                measuredParameterCB.Visible = mId == MeasuredParameterType.Risol1;
                CapturedDeviceType = GetDeviceTypeByRadioBox();
                measureState.MeasureTypeId = mId;
            }
            RefreshMeasureControl();
            MeasureStateOnFormChanged();
        }
        */

        private void AddHandlersToInputs()
        {
            //if (!HasClientState) return;
           // RleadRadioButton.CheckedChanged += MeasureTypeRadioButton_CheckedChanged;
           // RizolRadioButton.CheckedChanged += MeasureTypeRadioButton_CheckedChanged;

            v10_RadioButton.CheckedChanged += MeasuredVoltageRadioButton_CheckedChanged;
            v100_RadioButton.CheckedChanged += MeasuredVoltageRadioButton_CheckedChanged;
            v500_RadioButton.CheckedChanged += MeasuredVoltageRadioButton_CheckedChanged;
            v1000_RadioButton.CheckedChanged += MeasuredVoltageRadioButton_CheckedChanged;

            cableLengthNumericUpDown.ValueChanged += CableLengthNumericUpDown_ValueChanged;

            beforeMeasureDelayUpDown.ValueChanged += BeforeMeasureDelayUpDown_ValueChanged;
            afterMeasureDelayUpDown.ValueChanged += AfterMeasureDelayUpDown_ValueChanged;
            averagingCounter.SelectedIndexChanged += AveragingCounter_SelectedIndexChanged;

        }

        private void AveragingCounter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            measureState.AveragingTimes = Convert.ToUInt16(cb.SelectedIndex);
            MeasureStateOnFormChanged();
        }

        private void AfterMeasureDelayUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown ud = sender as NumericUpDown;
            uint v = 0;
            uint.TryParse(ud.Value.ToString(), out v);
            measureState.AfterMeasureDelay = v;
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
                currentCable = (Cable)(Cables.Select($"{Cable.CableId_ColumnName} = {cb.SelectedValue}")[0]);
                cableStructureCB.Items.Clear();
                foreach (CableStructure cs in currentCable.CableStructures.Rows)
                {
                    cableStructureCB.Items.Add(cs.StructureTitle);
                }
                cableLengthNumericUpDown.ValueChanged += CableLengthNumericUpDown_ValueChanged;
                cableStructureCB.SelectedIndex = 0;
                cableStructureCB.Enabled = currentCable.CableStructures.Rows.Count > 1;
                measureState.MeasuredCableID = (int)currentCable.CableId;
                FillMeasuredParameterTypesCB();
                RecalculateMaterialCoeff();
                testFile.CableID = (int)currentCable.CableId;
                MeasureStateOnFormChanged();
            }
            catch (EvaluateException ex)
            {
                //
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка при загрузке структур", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // string s = $"{Cable.CableId_ColumnName} = {cableComboBox.SelectedValue}";
            //foreach (DataColumn c in Cables.Columns) s += $"{c.ColumnName}\n";
            // MessageBox.Show(s);
            //currentCable = (Cable)(Cables.Select($"{Cable.CableId_ColumnName} = {cableComboBox.SelectedValue}")[0]);

            /*
            currentCable = (Cable)(Cables.Select($"{Cable.CableId_ColumnName} = {cableComboBox.SelectedValue}")[0]);
            cableStructureCB.Items.Clear();
            foreach(CableStructure cs in currentCable.CableStructures.Rows)
            {
                cableStructureCB.Items.Add(cs.StructureTitle);
            }
            //MessageBox.Show("Ха!");
        */
        }

        private void FillMeasuredParameterTypesCB()
        {
            //measuredParameterCB.Items.Clear();
            if (currentStructure != null)
            {
                if (currentStructure.MeasuredParameters.Rows.Count > 0)
                {
                    DBEntityTable t = new DBEntityTable(typeof(MeasuredParameterType));
                    measuredParameterCB.DataSource = MeasuredParameterTypes.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} IN ({string.Join(",", currentStructure.MeasuredParameterTypes_ids)})").CopyToDataTable();
                    
                    measuredParameterCB.DropDownStyle = ComboBoxStyle.DropDownList;
                    measuredParameterCB.Enabled = true;
                }
                else
                {
                    DisableParamtersCB();
                }
            }
        }

       private void DisableParamtersCB()
        {
            measuredParameterCB.Text = "Параметры отсутсвуют";
            measuredParameterCB.DropDownStyle = ComboBoxStyle.DropDown;
            measuredParameterCB.Enabled = false;
        }

        private void RemoveHandlersFromInputs()
        {
            //if (!HasClientState) return;
           // RleadRadioButton.CheckedChanged -= MeasureTypeRadioButton_CheckedChanged;
           // RizolRadioButton.CheckedChanged -= MeasureTypeRadioButton_CheckedChanged;

            v10_RadioButton.CheckedChanged -= MeasuredVoltageRadioButton_CheckedChanged;
            v100_RadioButton.CheckedChanged -= MeasuredVoltageRadioButton_CheckedChanged;
            v500_RadioButton.CheckedChanged -= MeasuredVoltageRadioButton_CheckedChanged;
            v1000_RadioButton.CheckedChanged -= MeasuredVoltageRadioButton_CheckedChanged;

            cableComboBox.SelectedValueChanged -= MeasuredCableComboBox_SelectedValueChanged;
            cableLengthNumericUpDown.ValueChanged -= CableLengthNumericUpDown_ValueChanged;

            beforeMeasureDelayUpDown.ValueChanged -= BeforeMeasureDelayUpDown_ValueChanged;
            afterMeasureDelayUpDown.ValueChanged -= AfterMeasureDelayUpDown_ValueChanged;
            averagingCounter.SelectedIndexChanged -= AveragingCounter_SelectedIndexChanged;
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
           // if (!HasClientState) return;
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
            resultField.Text = "0.0";
            measureTimerLabel.Text = "";
            normaLabel.Text = "";
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
                    StopMeasureTimer();
                    break;
                case MeasureStatus.WILL_START:
                    startMeasureButton.Text = "Запускается...";
                    startMeasureButton.BackColor = NormaUIColors.OrangeColor;
                    startMeasureButton.Enabled = false;
                    deviceControlButton.Enabled = false;
                    DisableMeasurePointControl();
                    StopMeasureTimer();
                    break;
                case MeasureStatus.STARTED:
                    startMeasureButton.Text = "Остановить измерение";
                    startMeasureButton.BackColor = NormaUIColors.RedColor;
                    startMeasureButton.Enabled = true;
                    deviceControlButton.Enabled = false;
                    MeasureIsStartedOnDevice = true;
                    DisableMeasurePointControl();
                    StartMeasureTimer();
                    break;
                case MeasureStatus.WILL_STOPPED:
                    startMeasureButton.Text = "Останавливается...";
                    startMeasureButton.BackColor = NormaUIColors.OrangeColor;
                    startMeasureButton.Enabled = false;
                    deviceControlButton.Enabled = false;
                    DisableMeasurePointControl();
                    StopMeasureTimer();
                    break;
            }
            measureStatus = status;
        }

        #region MeasureTimerControl

        private MeasureTimer measureTimer;
        private void StartMeasureTimer()
        {
            if (measureTimer == null) StopMeasureTimer();
            if (!MeasuredParameterType.IsItIsolationaResistance(measureState.MeasureTypeId)) return;
            measureTimerLabel.Text = "00:00";
            measureTimer = new MeasureTimer(currentStructure.RizolTimeLimit == null ? 60 : (int)currentStructure.RizolTimeLimit.MaxValue, MeasureTimer_Tick, MeasureTimerFinished);
            measureTimer.Start();
        }

        private void MeasureTimer_Tick(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(MeasureTimer_Tick), new object[] { sender, e });
            }else
            {
                measureTimerLabel.Text = measureTimer.WatchDisplay;
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
                measureTimer.Stop();
                measureTimer.Dispose();
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
                    measuredParameterSelect.Enabled = true;
                    break;
                case DeviceCaptureStatus.CONNECTED:
                    availableDevices.Visible = false;
                    panelMeasurePointControl.Visible = true; //startMeasureButton.Visible = true;
                    deviceControlButton.Text = "Отключить";
                    deviceControlButton.Visible = true;
                    measuredParameterSelect.Enabled = false;
                    break;
                case DeviceCaptureStatus.WAITING_FOR_CONNECTION:
                    availableDevices.Visible = true;
                    availableDevices.Enabled = false;
                    panelMeasurePointControl.Visible = false; //startMeasureButton.Visible = false;
                    deviceControlButton.Text = "Прервать";
                    deviceControlButton.Visible = true;
                    measuredParameterSelect.Enabled = false;
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
                }
                else if (xml_device.WorkStatusId == (int)DeviceWorkStatus.IDLE)
                {
                    SetMeasureStatus(MeasureStatus.STOPPED);
                }
                MeasureStateOnFormChanged();
            }

        }

        private void processMeasureCycleStatusBothSideDisconnected(DeviceXMLState xml_device)
        {
            SetMeasureStatus(MeasureStatus.STOPPED);
        }

        private void processMeasureCycleStatusOnCaseServerSideConnection(DeviceXMLState xml_device)
        {
            
        }

        private void processMeasureCycleStatusOnCaseClientOnlyConnection(DeviceXMLState xml_device)
        {
            switch (measureStatus)
            {
                case MeasureStatus.WILL_START:
                case MeasureStatus.STARTED:
                    if (xml_device.WorkStatusId == (int)DeviceWorkStatus.DEPOLARIZATION)
                    {
                        SetMeasureStatus(MeasureStatus.WILL_STOPPED);
                    }
                    else if (xml_device.WorkStatusId == (int)DeviceWorkStatus.IDLE)
                    {
                        SetMeasureStatus(MeasureStatus.STOPPED);
                    }
                    break;
            }
            measureState.MeasureStartFlag = false;
            MeasureStateOnFormChanged();
        }

        private void processMeasureCycleStatusOnCaseBothSideConnection(DeviceXMLState xml_device)
        {
            switch (measureStatus)
            {
                case MeasureStatus.STARTED:
                    if (xml_device.WorkStatusId == (int)DeviceWorkStatus.DEPOLARIZATION)
                    {
                        SetMeasureStatus(MeasureStatus.WILL_STOPPED);
                    }
                    break;
                case MeasureStatus.WILL_START:
                    if (xml_device.WorkStatusId == (int)DeviceWorkStatus.MEASURE || (xml_device.WorkStatusId == (int)DeviceWorkStatus.POLARIZATION))
                    {
                        SetMeasureStatus(MeasureStatus.STARTED);
                    }
                    else
                    {
                        SetMeasureStatus(MeasureStatus.STOPPED);
                        if (measureState.MeasureStartFlag)
                        {
                            measureState.MeasureStartFlag = false;
                            MeasureStateOnFormChanged();
                        }
                    }
                    break;

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
            resultField.Text = GetResultFieldText(xml_device);
        }

        private string GetResultFieldText(DeviceXMLState xml_device)
        {
            if (xml_device.MeasureStatusId == (uint)DeviceMeasureStatus.SUCCESS)
            {
                //return $"{xml_device.RawResult}";
                MeasureResultConverter c = new MeasureResultConverter(xml_device.RawResult, CurrentParameterData, (Single)cableLengthNumericUpDown.Value, MaterialCoeff);

                return ProcessResult(c);//return $"{Math.Round(xml_device.ConvertedResult, 3, MidpointRounding.AwayFromZero)}";
            }else if (xml_device.MeasureStatusId == 0)
            {
                return "";
            }
            else
            {
                return xml_device.MeasureStatusText;
            }
        }

        private string ProcessResult(MeasureResultConverter c)
        {
            bool addValueToProtocol = true;
            double valueToProtocol = c.RawValue;
            double valueToTable = c.ConvertedValueRounded;
            string stringVal = c.ConvertedValueLabel;
            switch (measureState.MeasureTypeId)
            {
                case MeasuredParameterType.Risol2:
                    MeasureResultConverter cr = new MeasureResultConverter(c.RawValue, currentStructure.RizolNormaValue, (Single)cableLengthNumericUpDown.Value, MaterialCoeff);
                    NormDeterminant d = new NormDeterminant(currentStructure.RizolNormaValue, cr.ConvertedValueRounded);
                    stringVal = cr.ConvertedValueLabel;
                    if (addValueToProtocol = d.IsOnNorma)
                    {
                        valueToProtocol = valueToTable = measureTimer.TimeInSeconds;
                        if (measureStatus == MeasureStatus.STARTED)startMeasureButton.PerformClick();
                    }
                    break;
                case MeasuredParameterType.Risol1:
                    break;
                case MeasuredParameterType.Rleads:
                    break;
            }
            if (addValueToProtocol)
            {
                testFile.SetMeasurePointValue((int)currentStructure.CableStructureId, (int)measureState.MeasureTypeId, measurePointMap.CurrentPoint, (float)valueToProtocol, (float)temperatureValue.Value);
                WriteValueToDataGridViewCell(valueToTable);
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
                    measureState.MeasureStartFlag = true;
                    SetMeasureStatus(MeasureStatus.WILL_START);
                    MeasureStateOnFormChanged();
                    MeasureIsStartedOnDevice = false;
                    break;
                case MeasureStatus.STARTED:
                    measureState.MeasureStartFlag = false;
                    SetMeasureStatus(MeasureStatus.WILL_STOPPED);
                    MeasureStateOnFormChanged();
                    break;
            }
        }



        private void cableStructureCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentStructure = currentCable.CableStructures.Rows[cableStructureCB.SelectedIndex] as CableStructure;
            //RefreshMeasureControl();
        }

        MeasurePointMap measurePointMap;
        bool pointChangedByClick = false;
        private void RefreshMeasureControl()
        {
            InitPointMapForCurrentStructureAndCurrentMeasureType();
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
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(measureResultDataGrid);
                r.Cells[ElementNumber.Index].Value = $"{currentStructure.StructureType.StructureTypeName} {i+1}";
                for(int j = 0; j < measurePointMap.MeasurePointsPerElement; j++)
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
                rowsForAdd.Add(r); 
            }
            measureResultDataGrid.Rows.AddRange(rowsForAdd.ToArray());
            RefreshMeasurePointLabel();
            SelectCurrentMeasurePointOnResultGrid();
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
            labelPointNumber.Text = (measurePointMap == null) ? String.Empty : $"{measurePointMap.CurrentElementTitle} {measurePointMap.CurrentMeasureTitle}";
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
            measurePointMap = new MeasurePointMap(currentStructure, measureState.MeasureTypeId);
            measurePointMap.OnMeasurePointChanged += OnMeasurePointChanged_Handler;
            RefreshMeasurePointControlButtons();
        }

        private void measureResultDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.ColumnIndex > ElementNumber.Index && e.ColumnIndex <= SubElement_4.Index && measureStatus == MeasureStatus.STOPPED)
            {
                pointChangedByClick = true;
                measurePointMap.SetMeasurePoint(e.RowIndex, e.ColumnIndex - ElementNumber.Index - 1);
            }
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
            CapturedDeviceType = GetDeviceTypeByRadioBox();
            measureState.MeasureTypeId = (uint)rb.SelectedValue;
            SetCurrentMeasureParameterData();
            MeasureStateOnFormChanged();
        }

        private void SetCurrentMeasureParameterData()
        {
            if (currentStructure == null) return;
            if (currentStructure.HasMeasuredParameters)
            {
                CurrentMeasuredParameterDataCollection = (CableStructureMeasuredParameterData[])currentStructure.MeasuredParameters.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {measureState.MeasureTypeId}");
                RefreshMeasureControl();
            }
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
}
