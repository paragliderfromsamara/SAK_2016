using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaMeasure.Devices.SAC;
using NormaMeasure.DBControl.Tables;
using NormaMeasure.MeasureControl.SAC;


namespace NormaMeasure.MeasureControl.SACMeasureForms
{
    public partial class SACHandMeasureForm : SACMeasureForm
    {
        private SAC_HandMeasure HandMeasure;
        public SACHandMeasureForm(SAC_Device sac) : base(sac)
        {
            InitializeComponent();
            InitMeasure();
        }

        private void InitMeasure()
        {
            HandMeasure = new SAC_HandMeasure(sac_device);
            HandMeasure.OnMeasureThread_Started += Measure_SwitchMeasureControlButton;
            HandMeasure.OnMeasureThread_Finished += Measure_SwitchMeasureControlButton;
            // HandMeasure.OnOverallMeasureTimerTick += Measure_OnOverallMeasureTimerTick;
            measureControlButton.Click += startMeasure_Click;
            HandMeasure.Result_Gotten += HandMeasure_Result_Gotten;

        }

        private void HandMeasure_Result_Gotten(SAC_HandMeasure measure)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new SAC_HandMeasure_Handler(HandMeasure_Result_Gotten), new object[] { measure });
                return;
            }
            else
            {
                resultLabel.Text = measure.Result.ToString();
            }
         }

        private void startMeasure_Click(object sender, EventArgs e)
        {
            HandMeasure.Start(1);
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
                }
                else
                {
                    measureControlButton.Click -= stopMeasure_Click;
                    measureControlButton.Click += startMeasure_Click;
                    measureControlButton.Text = "СТАРТ";
                }

            }
        }
    }
}
