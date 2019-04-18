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
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.MeasureControl.SACMeasureForms
{
    
    public partial class SACCableTestForm : Form
    {
        
        public SACCableTestForm()
        {
            InitializeComponent();
            LoadDataFromDB();
            InitMeasure();
            CurrentTest = CableTest.GetLastOrCreateNew();
        }




        private void InitMeasure()
        {
            Measure = new MeasureBase();
            Measure.OnOverallMeasureTimerTick += Measure_OnOverallMeasureTimerTick;
            Measure.OnMeasureStart += Measure_SwitchMeasureButton;
            Measure.OnMeasureStop += Measure_SwitchMeasureButton;
            Measure.OnMeasure += Measure_OnMeasure;
            Measure_SwitchMeasureButton(Measure);
        }

        private void Measure_OnMeasure(object _measure)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MeasureHandler(Measure_OnMeasure), new object[] { _measure });
                return;
            }
            else
            {
                MeasureBase meas = _measure as MeasureBase;
                label1.Text = meas.CycleNumber.ToString();
            }
        }

        private void Measure_SwitchMeasureButton(object _measure)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MeasureHandler(Measure_SwitchMeasureButton), new object[] { _measure });
                return;
            }
            else
            {
                MeasureBase meas = _measure as MeasureBase;
                if (meas.IsStart)
                {
                    measureControlButton.Text = "СТОП";
                    measureControlButton.Click -= startMeasure_Click;
                    measureControlButton.Click += stopMeasure_Click;
                }
                else
                {
                    measureControlButton.Text = "СТАРТ";
                    measureControlButton.Click -= stopMeasure_Click;
                    measureControlButton.Click += startMeasure_Click;
                }
            }
        }

        private void Measure_OnOverallMeasureTimerTick(object measObj)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MeasureHandler(Measure_OnOverallMeasureTimerTick), new object[] { measObj });
                return;
            }
            else
            {
                MeasureBase meas = measObj as MeasureBase;
                label2.Text = meas.OverallMeasTime.ToString();
            }
        }

        private void startMeasure_Click(object sender, EventArgs e)
        {
            Measure.Start();
        }

        private MeasureBase Measure;

        private void stopMeasure_Click(object sender, EventArgs e)
        {
            Measure.Stop();
        }

        #region Загрузка необходимых данных из БД
        private void LoadDataFromDB()
        {
            LoadOperators();
            LoadCables();
            LoadBarabanTypes();
        }

        private void LoadBarabanTypes()
        {
            DBEntityTable t = BarabanType.get_all_as_table();
            CableTestFormDataSet.Tables.Add(t);
            barabanTypes_CB.DataSource = t;
            barabanTypes_CB.DisplayMember = BarabanType.TypeName_ColumnName;
            barabanTypes_CB.ValueMember = BarabanType.TypeId_ColumnName;
        }

        private void LoadCables()
        {
            DBEntityTable t = Cable.get_all_as_table();
            CableTestFormDataSet.Tables.Add(t);
            cableForTest_CB.DataSource = t;
            cableForTest_CB.DisplayMember = Cable.CableName_ColumnName;
            cableForTest_CB.ValueMember = Cable.CableId_ColumnName;
        }

        private void LoadOperators()
        {
            DBEntityTable t = User.get_allowed_for_cable_test();
            CableTestFormDataSet.Tables.Add(t);
            operatorsList.DataSource = t;
            operatorsList.DisplayMember = User.FullName_ColumnName;
            operatorsList.ValueMember = User.UserId_ColumnName;
        }
        #endregion


        private CableTest CurrentTest;
    }
}
