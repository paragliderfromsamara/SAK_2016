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
        private MeasuredParameterType selectedParameterType;
        private SACMeasurePoint MeasurePoint;
        private SACMeasureResultField ResultField;
        public SACHandMeasureForm(SAC_Device sac)
        {
            InitializeComponent();
            ResultField = new SACMeasureResultField(measureResultPanel_Container);
            sac_device = sac;
            InitMeasure();
            LoadDataFromDB();
            InitInputs();
           
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
            ParameterTypes_CB_SelectedIndexChanged(ParameterTypes_CB, new EventArgs());
        }

        private void InitMeasure()
        {
            InitMeasurePoint();
            InitMeasureThread();
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

        private void InitMeasurePoint()
        {
            MeasurePoint = new SACMeasurePoint();
            MeasurePoint.CommutationType = SACCommutationType.Etalon;
            //MeasurePoint.StartElementPair = 1;
            //MeasurePoint.StartElementLead = 1;
            MeasurePoint.RawResult = 0;
            MeasurePoint.ConvertedResult = 0;
        }


        private void startMeasure_Click(object sender, EventArgs e)
        {
            if (MeasurePoint.CommutationType == SACCommutationType.Etalon)
            {
                HandMeasure.StartMeasureForPoint(MeasurePoint, (int)measureCycles_NumericUpDown.Value);
            }
            else
            {
                throw new NotImplementedException("Не реализовано ни одного метода подключения кроме Эталона");
            }
            
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
            selectedParameterType = (MeasuredParameterType)parameterTypes.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {ParameterTypes_CB.SelectedValue}")[0];
            this.Text = $"Ручные измерения: {selectedParameterType.ParameterName} ({selectedParameterType.Description})";
            freqParameters_Panel.Enabled = selectedParameterType.IsFreqParameter;
            MeasurePoint.parameterType = selectedParameterType;
        }

        private void CommutationMode_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Etalon_RadioButton.Checked)
            {
                MeasurePoint.CommutationType = SACCommutationType.Etalon;
                tableElementsPanel.Enabled = false;
            }else if (withDK_RadioButton.Checked)
            {
                MeasurePoint.CommutationType = SACCommutationType.SplittedTable;
                tableElementsPanel.Enabled = true;
            }
            else if (NoDK_RadioButton.Checked)
            {
                MeasurePoint.CommutationType = SACCommutationType.MergedTable;
                tableElementsPanel.Enabled = true;
            }
        }
    }
}
