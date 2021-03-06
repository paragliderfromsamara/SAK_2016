﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeraMicroMeasure.XmlObjects;
using NormaMeasure.Devices;
using NormaMeasure.Devices.XmlObjects;
using NormaMeasure.DBControl.Tables;

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
        int tryMeasureStartCounter;
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
                SetTitle();
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
        private ServerXmlState serverState;
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
            InitializeComponent();
            clientID = client_id;
            IsOnline = isCurrentPCClient = clientID == SettingsControl.GetClientId();
            //////////////////////////////////////////////////////////////
            ResetMeasureField();
            SetTitle();
            InitPanels();
            MeasureState = MeasureXMLState.GetDefault();
            SetDeviceCaptureStatus(DeviceCaptureStatus.DISCONNECTED);
            SetCapturedDeviceTypeId();
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
            selectDevicePanel.Visible = measurePanel.Enabled = isCurrentPCClient;
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

        private void SetTitle()
        {
            string t;
            if (clientID == 0) t = "Сервер";
            else if (clientID > 0) t = $"Испытательная линия {clientID}";
            else t = "Испытательная линия без номера";
            if (isCurrentPCClient) t += " (Этот компьютер)";
            else
            {
                t += (IsOnline) ? " (В сети)" : " (Нет связи)";
            }
            this.Text = t;
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
                SetTitle();
            }
        }

        private void fillInputsFromClientState()
        {
            //if (!HasClientState) return;
            SetMeasureTypeFromXMLState();
            SetVoltageFromXmlState();
            SetCableIDFromXmlState();
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
            if (measureState.MeasuredCableID < 0 || measureState.MeasuredCableID > cableComboBox.Items.Count)
            {
                measureState.MeasuredCableID = 0;
            }
            cableComboBox.SelectedIndex = measureState.MeasuredCableID;
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

            cableComboBox.SelectedValueChanged += MeasuredCableComboBox_SelectedValueChanged;
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
            measureState.MeasuredCableID = cb.SelectedIndex;
            cableLengthNumericUpDown.ValueChanged += CableLengthNumericUpDown_ValueChanged;
            MeasureStateOnFormChanged();
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
                if (i == availableDevices.SelectedIndex)
                {
                    if (d.ClientId == -1 || d.ClientId == clientID)
                    {
                        serial = d.Serial;
                    }else
                    {
                        MessageBox.Show($"{d.TypeNameFull} {d.Serial} занят другой измерительной линией!", "Устройство занято", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    break;
                }
                i++;
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
                    tryMeasureStartCounter = 3;
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
