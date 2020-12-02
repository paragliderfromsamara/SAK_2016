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
using NormaMeasure.DBControl.Tables;

namespace TeraMicroMeasure
{
    public partial class MeasureForm : Form
    {
        bool IsOnline = false;
        int clientID;
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
        bool isCurrentPCClient;
        private MeasureXMLState measureState;
        private ServerXmlState serverState;
        public bool HasState => measureState != null;
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
            SetTitle();
            InitPanels();
            MeasureState = MeasureXMLState.GetDefault();
        }

        /// <summary>
        /// Инициализация для клиента 
        /// </summary>
        /// <param name="client_state"></param>
        public MeasureForm(ClientXmlState client_state) : this(client_state.ClientID)
        {
            MeasureState = client_state.MeasureState;
        }


        private void InitPanels()
        {
            voltagesGroupBox.Visible = false;
            InitAverageCountComboBox();
            measurePanel.Enabled = isCurrentPCClient;
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
            cableLengthNumericUpDown.Value = measureState.MeasuredCableLength;
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
        }
        private void MeasureTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                uint mId = 0;
                if (rb == RizolRadioButton) mId = MeasuredParameterType.Risol2;
                else if (rb == RleadRadioButton) mId = MeasuredParameterType.Rleads;
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


    }
}
