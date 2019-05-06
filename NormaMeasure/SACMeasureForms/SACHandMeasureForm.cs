using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaMeasure.DBControl;
using NormaMeasure.Devices.SAC;
using NormaMeasure.DBControl.Tables;
using NormaMeasure.MeasureControl.SAC;


namespace NormaMeasure.MeasureControl.SACMeasureForms
{
    public partial class SACHandMeasureForm : Form
    {
        private SAC_HandMeasure HandMeasure;
        private SAC_Device sac_device;
        private DBEntityTable parameterTypes;
        private SACMeasurePoint measPoint;
        private SACMeasurePoint MeasurePoint
        {
            get
            {
                if (measPoint == null)
                {
                    measPoint = new SACMeasurePoint();
                    measPoint.CommutationType = SACCommutationType.NoFarEnd;
                    measPoint.LeadCommType = LeadCommutationType.A;
                    measPoint.PairCommutatorPosition_1 = 1;
                    //MeasurePoint.StartElementPair = 1;
                    //MeasurePoint.StartElementLead = 1;
                    measPoint.RawResult = 0;
                    measPoint.ConvertedResult = 0;
                }
                return measPoint;
            }
        }
        private SACMeasureResultField ResultField;
        public SACHandMeasureForm(SAC_Device sac)
        {
            InitializeComponent();
            ResultField = new SACMeasureResultField(measureResultPanel_Container);
            sac_device = sac;
            LoadDataFromDB();
            InitInputs();
            InitMeasure();

        }

        private void InitInputs()
        {
            InitCommModeGroupBoxes();
        }

        private void InitCommModeGroupBoxes()
        {
            Etalon_RadioButton.Checked = true;
        }

        private void LoadDataFromDB()
        {
            LoadParameterTypes();
        }

        private void LoadParameterTypes()
        {
            parameterTypes = MeasuredParameterType.for_a_manual_test_form();
            ParameterTypes_CB.DataSource = parameterTypes;
            ParameterTypes_CB.DisplayMember = MeasuredParameterType.ParameterName_ColumnName;
            ParameterTypes_CB.ValueMember = MeasuredParameterType.ParameterTypeId_ColumnName;
            ParameterTypes_CB.SelectedIndex = 0;
            MeasurePoint.ParameterType = SelectedParameterType;
            //ParameterTypes_CB_SelectedIndexChanged(ParameterTypes_CB, new EventArgs());
        }

        private void InitMeasure()
        {
            InitMeasureThread();
            measPoint.Changed += MeasurePoint_Changed;
        }

        private void InitMeasureThread()
        {
            HandMeasure = new SAC_HandMeasure(sac_device);
            HandMeasure.OnMeasureThread_Started += Measure_SwitchMeasureControlButton;
            HandMeasure.OnMeasureThread_Finished += Measure_SwitchMeasureControlButton;
            // HandMeasure.OnOverallMeasureTimerTick += Measure_OnOverallMeasureTimerTick;
            measureControlButton.Click += startMeasure_Click;
            HandMeasure.Result_Gotten += HandMeasure_Result_Gotten; 
        }

        private void HandMeasure_Result_Gotten(SACMeasure measure, SACMeasurePoint curPoint)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new SACMeasure_Handler(HandMeasure_Result_Gotten), new object[] { measure, curPoint });
                return;
            }
            else
            {
                ResultField.RefreshFields(curPoint, measure);
            }

        }


        private void MeasurePoint_Changed(SACMeasurePoint point)
        {
            ResultField.RefreshFields(point);
            sac_device.table.SetTableForMeasurePoint(point);
        }

        private void startMeasure_Click(object sender, EventArgs e)
        {
                HandMeasure.StartMeasureForPoint(MeasurePoint, (int)measureCycles_NumericUpDown.Value);

            
        }

        private void stopMeasure_Click(object sender, EventArgs e)
        {
            HandMeasure.Stop();
        }


        private void Measure_SwitchMeasureControlButton(object _measure)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MeasureHandler(Measure_SwitchMeasureControlButton), new object[] { _measure });
                return;
            }
            else
            {
                if (HandMeasure.IsStarted)
                {
                    measureControlButton.Click -= startMeasure_Click;
                    measureControlButton.Click += stopMeasure_Click;
                    measureControlButton.Text = "СТОП";
                    measureCycles_NumericUpDown.Enabled = false;
                }
                else
                {
                    measureControlButton.Click -= stopMeasure_Click;
                    measureControlButton.Click += startMeasure_Click;
                    measureControlButton.Text = "СТАРТ";
                    measureCycles_NumericUpDown.Enabled = true;
                }

            }
        }

        private void ParameterTypes_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = $"Ручные измерения: {SelectedParameterType.ParameterName} ({SelectedParameterType.Description})";
            freqParameters_Panel.Enabled = SelectedParameterType.IsFreqParameter;
            MeasurePoint.ParameterType = SelectedParameterType;
            MessageBox.Show(MeasurePoint.ParameterType.ParameterName);
        }

        private MeasuredParameterType SelectedParameterType
        {
            get
            {
                return (MeasuredParameterType)parameterTypes.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {ParameterTypes_CB.SelectedValue}")[0];
            }
        }

        /*
        private void CommutationMode_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Etalon_RadioButton.Checked)
            {
                MeasurePoint.CommutationType = SACCommutationType.Etalon;
                pairSelector_ComboBox_1.CommutationType = SACCommutationType.Etalon;
                tableElementsPanel.Enabled = false;
            }else if (withDK_RadioButton.Checked)
            {
                MeasurePoint.CommutationType = SACCommutationType.WithFarEnd;
                pairSelector_ComboBox_1.CommutationType = SACCommutationType.WithFarEnd;
                tableElementsPanel.Enabled = true;
            }
            else if (NoDK_RadioButton.Checked)
            {
                MeasurePoint.CommutationType = SACCommutationType.NoFarEnd;
                pairSelector_ComboBox_1.CommutationType = SACCommutationType.NoFarEnd;
                tableElementsPanel.Enabled = true;
            }
            
        }
        */
        private void pairSelector_ComboBox_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MeasurePoint.PairCommutatorPosition_1 = (byte)(pairSelector_ComboBox_1.SelectedIndex+1);
        }

        private void Etalon_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Etalon_RadioButton.Checked)
            {
                MeasurePoint.CommutationType = SACCommutationType.Etalon;
                pairSelector_ComboBox_1.CommutationType = SACCommutationType.Etalon;
                tableElementsPanel.Enabled = false;
            }
        }

        private void withDK_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (withDK_RadioButton.Checked)
            {
                MeasurePoint.CommutationType = SACCommutationType.WithFarEnd;
                pairSelector_ComboBox_1.CommutationType = SACCommutationType.WithFarEnd;
                tableElementsPanel.Enabled = true;
            }
        }

        private void NoDK_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (NoDK_RadioButton.Checked)
            {
                MeasurePoint.CommutationType = SACCommutationType.NoFarEnd;
                pairSelector_ComboBox_1.CommutationType = SACCommutationType.NoFarEnd;
                tableElementsPanel.Enabled = true;
            }

        }

        private void leadCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (leadCB.SelectedIndex == 0)
            {
                MeasurePoint.LeadCommType = LeadCommutationType.A;
            }else if (leadCB.SelectedIndex == 1)
            {
                MeasurePoint.LeadCommType = LeadCommutationType.B;
            }
            else if (leadCB.SelectedIndex == 2)
            {
                MeasurePoint.LeadCommType = LeadCommutationType.AB;
            }
        }
    }
}
