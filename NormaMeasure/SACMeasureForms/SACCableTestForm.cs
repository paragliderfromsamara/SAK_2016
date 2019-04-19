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
    public delegate void SACCableTestForm_Handler();
    public partial class SACCableTestForm : Form
    {
        
        public SACCableTestForm()
        {
            InitializeComponent();
            InitTestProgamGroupBox();
            if (!LoadDataFromDB())
            {
                this.Close();
                return;
            }
            InitInputs();
            InitMeasure();
            CurrentTest = CableTest.GetLastOrCreateNew();
        }

        private void InitInputs()
        {
            RefreshTableElementsList();
            RefreshCableMeasuredParams();
            RefreshRisolSelector();
        }

        /// <summary>
        /// Обновляем список измеряемых Сопротивлений изоляции
        /// </summary>
        private void RefreshRisolSelector()
        {
            RizolSelector_CB.Items.Clear();
            RizolSelector_CB.ValueMember = MeasuredParameterType.ParameterTypeId_ColumnName;
            RizolSelector_CB.DisplayMember = MeasuredParameterType.ParameterName_ColumnName;
            foreach(MeasuredParameterType pType in SelectedCable.MeasuredParameterTypes.Rows)
            {
                if (MeasuredParameterType.IsItIsolationaResistance(pType.ParameterTypeId))
                {
               
                    RizolSelector_CB.Items.Add( pType.ParameterName);

                }
            }
            RizolSelector_CB.Enabled = RizolSelector_CB.Items.Count > 0;
            if (RizolSelector_CB.Items.Count > 0) RizolSelector_CB.SelectedIndex = 0;
        }

        private void RefreshCableMeasuredParams()
        {

            foreach(MeasuredParameterType mpt in MeasuredParametersTable.Rows)
            {
                Control[] cbArr = testProgram_GroupBox.Controls.Find($"{mpt.Table.TableName}_{mpt.ParameterTypeId}", false);
                if (cbArr.Length == 0) continue;
                CheckBox cb = cbArr[0] as CheckBox;
                if (mpt.ParameterTypeId == MeasuredParameterType.Calling) cb.Enabled = true;
                else if(MeasuredParameterType.IsItIsolationaResistance(mpt.ParameterTypeId))
                {
                    cb.Enabled = SelectedCable.MeasuredParameterTypes_IDs.Contains(MeasuredParameterType.Risol1) || SelectedCable.MeasuredParameterTypes_IDs.Contains(MeasuredParameterType.Risol2) || SelectedCable.MeasuredParameterTypes_IDs.Contains(MeasuredParameterType.Risol3) || SelectedCable.MeasuredParameterTypes_IDs.Contains(MeasuredParameterType.Risol4);
                }else
                {
                    cb.Enabled = SelectedCable.MeasuredParameterTypes_IDs.Contains(mpt.ParameterTypeId);
                }
                cb.Checked = cb.Enabled;
            }

        }

        /// <summary>
        /// Заполняет CheckBox ы панели программа испытаний
        /// </summary>
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
            MeasuredParametersTable = MeasuredParameterType.get_for_a_program_test();
            foreach(MeasuredParameterType pType in MeasuredParametersTable.Rows)
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

                cb.Name = $"{pType.Table.TableName}_{pType.ParameterTypeId}";
                cb.Width = (pType.ParameterTypeId == MeasuredParameterType.Calling) ? cbWidth : cbWidth*2/3;
                cb.Height = cbHeight;
                cb.Location = new Point(tmpHorOffset, tmpVertOffset);

                x++;
                tmpHorOffset += (cbWidth + horOffset);
                

            }
            testProgram_GroupBox.Width = onRow * (horOffset + cbWidth) + horOffset;
            testProgram_GroupBox.Height = (y+1) * (vertOffset + cbHeight) + vertOffset + startVertOffset;
            this.Width = testProgram_GroupBox.Width + 40;
            this.Height += testProgram_GroupBox.Height;
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
        private bool LoadDataFromDB()
        {
            LoadOperators();
            LoadCables();
            LoadBarabanTypes();

            return CheckTest_Availability();
        }

        /// <summary>
        /// Проверка наличия необходимых для испытания данных
        /// </summary>
        private bool CheckTest_Availability()
        {
            List < string > errors = new List<string>();
            if (CablesTable.Rows.Count == 0) errors.Add("В базе данных нет ни одного кабеля!");
            if (OperatorsTable.Rows.Count == 0) errors.Add("В базе данных нет ни одного оператора!");
            if (BarabanTypesTable.Rows.Count == 0) errors.Add("В базе данных нет ни одного типа барабана!");

            if(errors.Count > 0)
            {
                string t = "Невозможно произвести испытания кабеля: \n";
                for(int i=0; i<errors.Count; i++)
                {
                    t += $"{i+1}) {errors[i]}\n";
                }
                MessageBox.Show(t, "Отсутсвуют данные", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return errors.Count == 0;
        }

        private void LoadBarabanTypes()
        {
            BarabanTypesTable = BarabanType.get_all_as_table();
            CableTestFormDataSet.Tables.Add(BarabanTypesTable);
            barabanTypes_CB.DataSource = BarabanTypesTable;
            barabanTypes_CB.DisplayMember = BarabanType.TypeName_ColumnName;
            barabanTypes_CB.ValueMember = BarabanType.TypeId_ColumnName;
        }

        private void LoadCables()
        {
            CablesTable = Cable.get_all_as_table();
            CableTestFormDataSet.Tables.Add(CablesTable);
            SelectedCableChanged += SACCableTestForm_SelectedCableChanged;
            cableForTest_CB.DataSource = CablesTable;
            cableForTest_CB.DisplayMember = Cable.FullCableName_ColumnName;
            cableForTest_CB.ValueMember = Cable.CableId_ColumnName;
        }

        private void SACCableTestForm_SelectedCableChanged()
        {

            try
            {
                SelectedCable = (Cable)(CablesTable.Select($"{Cable.CableId_ColumnName} = {cableForTest_CB.SelectedValue}")[0]);
                RefreshCableMeasuredParams();
                RefreshRisolSelector();
            }
            catch (System.Data.EvaluateException) { }
        }

        private void LoadOperators()
        {
            OperatorsTable = User.get_allowed_for_cable_test();
            CableTestFormDataSet.Tables.Add(OperatorsTable);
            operatorsList.DataSource = OperatorsTable;
            operatorsList.DisplayMember = User.FullName_ColumnName;
            operatorsList.ValueMember = User.UserId_ColumnName;
        }
        #endregion


        private void tableMode_RadioBatton_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTableElementsList();
        }

        private void RefreshTableElementsList()
        {
            connectedFromTableElement_ComboBox.Items.Clear();
            int maxRows = (doubleTable_RadioBatton.Checked) ? 104 : 52;
            for (int i=0; i<maxRows; i++)
            {
                connectedFromTableElement_ComboBox.Items.Add(i+1);
            }
            connectedFromTableElement_ComboBox.SelectedIndex = 0;
        }




        private Cable SelectedCable;
        private CableTest CurrentTest;

        private DBEntityTable CablesTable;
        private DBEntityTable OperatorsTable;
        private DBEntityTable BarabanTypesTable;
        private DBEntityTable MeasuredParametersTable;

        public event SACCableTestForm_Handler SelectedCableChanged;

        private void cableForTest_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCableChanged();

        }
    }
}
