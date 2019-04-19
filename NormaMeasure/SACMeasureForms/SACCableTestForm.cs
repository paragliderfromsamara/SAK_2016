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
            InitTestProgamGroupBox();
            LoadDataFromDB();
            InitMeasure();
            CurrentTest = CableTest.GetLastOrCreateNew();
        }

        private void InitTestProgamGroupBox()
        {
            int onRow = 7;
            int x = 0;
            int y = 0;
            int vertOffset =10;
            int horOffset = 5;
            int cbWidth = 85;
            int cbHeight = 20;
            int startHorOffset = 30;
            int startVertOffset = 30;
            int tmpHorOffset = startHorOffset;
            int tmpVertOffset = startVertOffset;
            DBEntityTable t = MeasuredParameterType.get_for_a_program_test();
            foreach(MeasuredParameterType pType in t.Rows)
            {
                if (x == onRow)
                {
                    y++;
                    x = 0;
                    tmpHorOffset = startHorOffset;
                    tmpVertOffset += vertOffset + cbHeight;
                }
                CheckBox cb = new CheckBox();
                cb.Parent = testProgram_GroupBox;
                if (pType.ParameterTypeId == MeasuredParameterType.Risol1)
                {
                    cb.Text = (pType.ParameterTypeId == MeasuredParameterType.Risol1) ? "Rиз" : pType.ParameterName;
                }else if (pType.ParameterTypeId == MeasuredParameterType.Calling)
                {
                    cb.Text = "Прозвонка";
                }
                else
                {
                    cb.Text = pType.ParameterName;
                }

                cb.Name = $"MeasuredParameterType_{pType.ParameterTypeId}";
                cb.Width = (pType.ParameterTypeId == MeasuredParameterType.Calling) ? cbWidth : cbWidth*2/3;
                cb.Height = cbHeight;
                cb.Location = new Point(tmpHorOffset, tmpVertOffset);

                x++;
                tmpHorOffset += (cbWidth + horOffset);
                

            }
            testProgram_GroupBox.Width = onRow * (horOffset + cbWidth) + horOffset;
            testProgram_GroupBox.Height = (y+1) * (vertOffset + cbHeight) + vertOffset + startVertOffset;
            this.Width = testProgram_GroupBox.Width + 40;
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
            cableForTest_CB.DisplayMember = Cable.FullCableName_ColumnName;
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
