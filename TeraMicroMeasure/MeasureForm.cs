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
        private ClientXmlState clientState;
        private ServerXmlState serverState;
        public bool HasClientState => clientState != null;
        public ClientXmlState ClientState
        {
            set
            {
                RemoveHandlersFromInputs();
                clientState = new ClientXmlState(value.InnerXml);
                fillInputsFromClientState();
                if (clientState.WasChanged && isCurrentPCClient) ClientStateOnFormChanged();
                AddHandlersToInputs();
            }
            get
            {
                return clientState;
            }
        }

        public EventHandler OnClientStateChanged;
        public MeasureForm(int client_id)
        {
            int curCLientId = SettingsControl.GetClientId();
            InitializeComponent();
            clientID = client_id;
            isCurrentPCClient = clientID == SettingsControl.GetClientId();
            //////////////////////////////////////////////////////////////
            SetTitle();
            InitPanels();
        }

        /// <summary>
        /// Инициализация для клиента 
        /// </summary>
        /// <param name="client_state"></param>
        public MeasureForm(ClientXmlState client_state) : this(client_state.ClientID)
        {
            ClientState = client_state;
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
            this.Text = t;
        }

        public void SetStates(ServerXmlState server_state, ClientXmlState client_state)
        {
            RefreshClientState(client_state);
            RefreshServerState(server_state);
        }

        private void RefreshServerState(ServerXmlState server_state)
        {
            this.serverState = new ServerXmlState(server_state.InnerXml);
        }

        private void RefreshClientState(ClientXmlState client_state)
        {
            this.ClientState = client_state;
        }

        private void fillInputsFromClientState()
        {
            if (!HasClientState) return;
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
            if (clientState.AveragingTimes < 0 || clientState.AveragingTimes > averagingCounter.Items.Count)
            {
                clientState.AveragingTimes = 0;
            }
            averagingCounter.SelectedIndex = Convert.ToInt16(clientState.AveragingTimes);
        }

        private void SetAfterMeasureDelayFromXMLState()
        {
            afterMeasureDelayUpDown.Value = clientState.AfterMeasureDelay;
        }

        private void SetBeforeMeasureDelayFromXMLState()
        {
            beforeMeasureDelayUpDown.Value = clientState.BeforeMeasureDelay;
        }

        private void SetCableLengthFromXmlState()
        {
            if (clientState.MeasuredCableLength < 1 || clientState.MeasuredCableLength > 10000)
            {
                clientState.MeasuredCableLength = 1000;
            }
            cableLengthNumericUpDown.Value = clientState.MeasuredCableLength;
        }

        private void SetCableIDFromXmlState()
        {
            if (clientState.MeasuredCableID < 0 || clientState.MeasuredCableID > cableComboBox.Items.Count)
            {
                clientState.MeasuredCableID = 0;
            }
            cableComboBox.SelectedIndex = clientState.MeasuredCableID;
        }

        private void SetVoltageFromXmlState()
        {
            switch (clientState.MeasureVoltage)
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
                    clientState.MeasureVoltage = 10;
                    v10_RadioButton.Checked = true;
                    break;
            }
        }

        private void SetMeasureTypeFromXMLState()
        {
            switch(clientState.MeasureTypeId)
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
                clientState.MeasureVoltage = v;
                //MessageBox.Show(v.ToString());
            }
            ClientStateOnFormChanged();
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
                clientState.MeasureTypeId = mId;
            }
            ClientStateOnFormChanged();
        }


        private void AddHandlersToInputs()
        {
            if (!HasClientState) return;
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
            clientState.AveragingTimes = Convert.ToUInt16(cb.SelectedIndex);
            ClientStateOnFormChanged();
        }

        private void AfterMeasureDelayUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown ud = sender as NumericUpDown;
            uint v = 0;
            uint.TryParse(ud.Value.ToString(), out v);
            clientState.AfterMeasureDelay = v;
            ClientStateOnFormChanged();
        }

        private void BeforeMeasureDelayUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown ud = sender as NumericUpDown;
            uint v = 0;
            uint.TryParse(ud.Value.ToString(), out v);
            clientState.BeforeMeasureDelay = v;
            ClientStateOnFormChanged();
        }

        private void MeasuredCableComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            clientState.MeasuredCableID = cb.SelectedIndex;
            cableLengthNumericUpDown.ValueChanged += CableLengthNumericUpDown_ValueChanged;
            ClientStateOnFormChanged();
        }

        private void RemoveHandlersFromInputs()
        {
            if (!HasClientState) return;
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
            clientState.MeasuredCableLength = v;
            ClientStateOnFormChanged();
        }

        private void ClientStateOnFormChanged()
        {
            if (!HasClientState) return;
            OnClientStateChanged?.Invoke(clientState, new EventArgs());
            richTextBox1.Text = clientState.InnerXml;
        }


    }
}
