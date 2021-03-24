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
        private Cable currentCable;
        private CableStructure currentStructure;

        private DeviceCaptureStatus device_capture_status;
        public DeviceCaptureStatus DeviceCaptureStatus => device_capture_status;

        private MeasureStatus measureStatus = MeasureStatus.STOPPED;
        private DeviceXMLState current_device_state;

      

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
            clientID = client_id;
            IsOnline = isCurrentPCClient = clientID == SettingsControl.GetClientId();
            //////////////////////////////////////////////////////////////
            ResetMeasureField();
            InitPanels();
            MeasureState = MeasureXMLState.GetDefault();
            SetDeviceCaptureStatus(DeviceCaptureStatus.DISCONNECTED);
            SetCapturedDeviceTypeId();
            LoadCables();
            InitMeasureDraft();
        }

        private void InitDesign()
        {
            InitializeComponent();
            measureResultDataGrid.ColumnHeadersDefaultCellStyle = BuildParameterNameCellStyle();
            ElementNumber.DefaultCellStyle = BuildParameterNameCellStyle();
        }

        private void InitMeasureDraft()
        {
            if (Cables.Rows.Count == 0) return;
            testFile = new CableTestIni();
            if (testFile.CableID == 0)
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
            if (RleadRadioButton.Checked) return DeviceType.Microohmmeter;
            else if (RizolRadioButton.Checked) return DeviceType.Teraohmmeter;
            else return DeviceType.Unknown;
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
            SetMeasureTypeFromXMLState();
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

        private void SetMeasureTypeFromXMLState()
        {
            switch(measureState.MeasureTypeId)
            {
                case MeasuredParameterType.Rleads:
                    RleadRadioButton.Checked = true;
                    break;
                case MeasuredParameterType.Risol2:
                    RizolRadioButton.Checked = true;
                    break;
            }
        }

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

        private void MeasureTypeRadioButton_CheckedChanged_Common(object sender, EventArgs e)
        {
            voltagesGroupBox.Visible = RizolRadioButton.Checked;
            polarDelayLbl.Text = RizolRadioButton.Checked ? "Выдержка, мин" : "Выдержка, мс";
            depolTimeLbl.Text = RizolRadioButton.Checked ? "Разряд, с" : "Пауза, c";
        }
        private void MeasureTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                uint mId = 0;
                if (rb == RizolRadioButton)
                {
                    mId = MeasuredParameterType.Risol2;
                }
                else if (rb == RleadRadioButton)
                {
                    mId = MeasuredParameterType.Rleads;
                }
                CapturedDeviceType = GetDeviceTypeByRadioBox();
                measureState.MeasureTypeId = mId;
            }
            MeasureStateOnFormChanged();
        }


        private void AddHandlersToInputs()
        {
            //if (!HasClientState) return;
            RleadRadioButton.CheckedChanged += MeasureTypeRadioButton_CheckedChanged;
            RizolRadioButton.CheckedChanged += MeasureTypeRadioButton_CheckedChanged;

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
                MeasureStateOnFormChanged();
            }
            catch (EvaluateException ex)
            {
                //
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка загрузки структур", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void RemoveHandlersFromInputs()
        {
            //if (!HasClientState) return;
            RleadRadioButton.CheckedChanged -= MeasureTypeRadioButton_CheckedChanged;
            RizolRadioButton.CheckedChanged -= MeasureTypeRadioButton_CheckedChanged;

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
                    startMeasureButton.Enabled = true;
                    deviceControlButton.Enabled = true;
                    MeasureIsStartedOnDevice = false;
                    break;
                case MeasureStatus.WILL_START:
                    startMeasureButton.Text = "Запускается...";
                    startMeasureButton.Enabled = false;
                    deviceControlButton.Enabled = false;
                    break;
                case MeasureStatus.STARTED:
                    startMeasureButton.Text = "Остановить измерение";
                    startMeasureButton.Enabled = true;
                    deviceControlButton.Enabled = false;
                    MeasureIsStartedOnDevice = true;
                    break;
                case MeasureStatus.WILL_STOPPED:
                    startMeasureButton.Text = "Останавливается...";
                    startMeasureButton.Enabled = false;
                    deviceControlButton.Enabled = false;
                    break;
            }
            measureStatus = status;
        }

        private void SetDeviceCaptureStatus(DeviceCaptureStatus status)
        {
            switch(status)
            {
                case DeviceCaptureStatus.DISCONNECTED:
                    availableDevices.Visible = true;
                    availableDevices.Enabled = true;
                    startMeasureButton.Visible = false;
                    deviceControlButton.Text = "Подключить";
                    deviceControlButton.Visible = true;
                    deviceControlButton.Enabled = true;
                    measuredParametersGroupBox.Enabled = true;
                    break;
                case DeviceCaptureStatus.CONNECTED:
                    availableDevices.Visible = false;
                    startMeasureButton.Visible = true;
                    deviceControlButton.Text = "Отключить";
                    deviceControlButton.Visible = true;
                    measuredParametersGroupBox.Enabled = false;
                    break;
                case DeviceCaptureStatus.WAITING_FOR_CONNECTION:
                    availableDevices.Visible = true;
                    availableDevices.Enabled = false;
                    startMeasureButton.Visible = false;
                    deviceControlButton.Text = "Прервать";
                    deviceControlButton.Visible = true;
                    measuredParametersGroupBox.Enabled = false;
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
               // return $"{xml_device.ConvertedResult}";
               return $"{Math.Round(xml_device.ConvertedResult, 3, MidpointRounding.AwayFromZero)}";
            }else if (xml_device.MeasureStatusId == 0)
            {
                return "";
            }
            else
            {
                return xml_device.MeasureStatusText;
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
            RefreshMeasureControl();
        }

        MeasurePointMap measurePointMap;
        bool pointChangedByClick = false;
        private void RefreshMeasureControl()
        {
            InitPointMapForCurrentStructureAndCurrentMeasureType();
            //int subElsCounter = MeasuredParameterType.MeasurePointNumberPerStructureElement(measureState.MeasureTypeId, currentStructure.StructureType.StructureLeadsAmount);
            int[] valueColumns = new int[] { SubElement_1.Index, SubElement_2.Index, SubElement_3.Index, SubElement_4.Index };
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
                    int pointIdx = i * measurePointMap.MeasurePointsPerElement + j;
                    r.Cells[valueColumns[j]].Value = pointIdx;
                }
                measureResultDataGrid.Rows.Add(r);
            }

            //measureResultDataGrid.ClearSelection();
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
            measureResultDataGrid.FirstDisplayedScrollingRowIndex = (measurePointMap.CurrentElementIndex / measureResultDataGrid.DisplayedRowCount(false)) * measureResultDataGrid.DisplayedRowCount(false);
        }

        private void OnMeasurePointChanged_Handler(object sender, EventArgs e)
        {
            labelPointNumber.Text = $"{measurePointMap.CurrentElementTitle} {measurePointMap.CurrentMeasureTitle}";
            SelectCurrentMeasurePointOnResultGrid();
            RefreshControlButtons();
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

        private void RefreshControlButtons()
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
            RefreshControlButtons();
        }

        private void measureResultDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > ElementNumber.Index && e.ColumnIndex <= SubElement_4.Index)
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

        private System.Windows.Forms.DataGridViewCellStyle BuildParameterNameCellStyle()
        {
            System.Windows.Forms.DataGridViewCellStyle parameterNameCellStyle = new DataGridViewCellStyle();
            parameterNameCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            parameterNameCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(109)))));
            parameterNameCellStyle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            parameterNameCellStyle.ForeColor = System.Drawing.Color.Gainsboro;
            parameterNameCellStyle.NullValue = "-";
            parameterNameCellStyle.Padding = new System.Windows.Forms.Padding(3);
            parameterNameCellStyle.SelectionBackColor = parameterNameCellStyle.BackColor;
            parameterNameCellStyle.SelectionForeColor = System.Drawing.Color.Gainsboro;
            parameterNameCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            return parameterNameCellStyle;
        }

        private System.Windows.Forms.DataGridViewCellStyle BuildParameterNameHeaderStyle()
        {
            System.Windows.Forms.DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(201)))), ((int)(((byte)(0)))));
            style.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            style.ForeColor = System.Drawing.Color.Gainsboro;
            style.NullValue = "-";
            style.Padding = new System.Windows.Forms.Padding(3);

            style.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            style.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            style.WrapMode = System.Windows.Forms.DataGridViewTriState.True;

            return style;
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
