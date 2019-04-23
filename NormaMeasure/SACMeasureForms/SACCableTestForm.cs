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
using NormaMeasure.MeasureControl.SAC;

namespace NormaMeasure.MeasureControl.SACMeasureForms
{
    public delegate void SACCableTestForm_Handler();
    public partial class SACCableTestForm : Form
    {
        
        public SACCableTestForm()
        {
            InitializeComponent();
            CurrentTest = CableTest.GetLastOrCreateNew();
            InitTestProgamGroupBox();
            if (!LoadDataFromDB())
            {
                this.Close();
                return;
            }else
            {
                
                InitCableTest();
                InitInputs();
            }



        }

        private void InitCableTest()
        {
            SwitchEnablingFields(CurrentTest.IsNotStarted);
            if(CurrentTest.IsNotStarted)
            {
                CurrentTest.SourceCable = (Cable)CablesTable.Rows[0];
              //  MessageBox.Show("is_not_started");
            }
        }

        private void SwitchEnablingFields(bool is_enabled)
        {
            cableForTest_CB.Enabled = is_enabled;
            cableLength_NumericUpDown.Enabled = is_enabled;
            //connectionType.Enabled = is_enabled;


        }

        private void InitInputs()
        {
            RefreshTableElementsList();
            RefreshCableMeasuredParams();
            RefreshRisolSelector();
            InitInputHandlers();
        }

        private void InitInputHandlers()
        {
            barabanTypes_CB.SelectedValueChanged += BarabanTypes_CB_SelectedValueChanged;
            barabanSerial_TextBox.TextChanged += BarabanSerial_TextBox_TextChanged;
            operatorsList.SelectedValueChanged += OperatorsList_SelectedValueChanged;
            temperature_NumericUpDown.ValueChanged += Temperature_NumericUpDown_ValueChanged;
            useTemperatureSensor_CheckBox.CheckedChanged += UseTemperatureSensor_CheckBox_CheckedChanged;
            cableLength_NumericUpDown.ValueChanged += CableLength_NumericUpDown_ValueChanged;
        }

        private void CableLength_NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CurrentTest.CableLength = (float)cableLength_NumericUpDown.Value;
        }

        private void UseTemperatureSensor_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CurrentTest.IsUseTermoDetector = useTemperatureSensor_CheckBox.Checked;
        }

        private void Temperature_NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CurrentTest.Temperature = (uint)temperature_NumericUpDown.Value;
        }

        private void OperatorsList_SelectedValueChanged(object sender, EventArgs e)
        {
            CurrentTest.OperatorId = (uint)operatorsList.SelectedValue;
        }

        

        private void BarabanSerial_TextBox_TextChanged(object sender, EventArgs e)
        {
            CurrentTest.BarabanSerial = barabanSerial_TextBox.Text;
        }

        private void BarabanTypes_CB_SelectedValueChanged(object sender, EventArgs e)
        {
            CurrentTest.BarabanTypeId = (uint)barabanTypes_CB.SelectedValue;
        }



        /// <summary>
        /// Обновляем список измеряемых Сопротивлений изоляции
        /// </summary>
        private void RefreshRisolSelector()
        {
            RizolSelector_CB.Items.Clear();
            RizolSelector_CB.ValueMember = MeasuredParameterType.ParameterTypeId_ColumnName;
            RizolSelector_CB.DisplayMember = MeasuredParameterType.ParameterName_ColumnName;
            foreach(MeasuredParameterType pType in CurrentTest.MeasuredParameterTypes)
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
                CheckBox cb = getParameterTypeCheckBox(mpt);
                if (cb == null) continue;
                if (mpt.ParameterTypeId == MeasuredParameterType.Calling) cb.Enabled = true;
                else if(MeasuredParameterType.IsItIsolationaResistance(mpt.ParameterTypeId))
                {
                    cb.Enabled = CurrentTest.MeasuredParameterTypes_IDs.Contains(MeasuredParameterType.Risol1) || CurrentTest.MeasuredParameterTypes_IDs.Contains(MeasuredParameterType.Risol2) || CurrentTest.MeasuredParameterTypes_IDs.Contains(MeasuredParameterType.Risol3) || CurrentTest.MeasuredParameterTypes_IDs.Contains(MeasuredParameterType.Risol4);
                }else
                {
                    cb.Enabled = CurrentTest.MeasuredParameterTypes_IDs.Contains(mpt.ParameterTypeId);
                }
                cb.Checked = cb.Enabled;
            }
        }

        private CheckBox getParameterTypeCheckBox(MeasuredParameterType p_type)
        {
            Control[] cbArr = testProgram_GroupBox.Controls.Find($"{p_type.Table.TableName}_{p_type.ParameterTypeId}", false);
            if (cbArr.Length == 0) return null;
            return cbArr[0] as CheckBox;
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

        private void InitMeasure(CableTest test)
        { 
            Measure = new CableTestMeasure();
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
            //Measure.Start();

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
                CurrentTest.SourceCable = (Cable)(CablesTable.Select($"{Cable.CableId_ColumnName} = {cableForTest_CB.SelectedValue}")[0]);
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
            isSplittedTable = doubleTable_RadioBatton.Checked;
            for (int i=0; i<maxRows; i++)
            {
                connectedFromTableElement_ComboBox.Items.Add(i+1);
            }
            connectedFromTableElement_ComboBox.SelectedIndex = 0;
        }

        private MeasuredParameterType[] getSelectedParameterTypes()
        {
            List<MeasuredParameterType> pTypes = new List<MeasuredParameterType>();
            foreach(MeasuredParameterType pType in MeasuredParametersTable.Rows)
            {
                CheckBox cb = getParameterTypeCheckBox(pType);
                if (cb == null) continue;
                if (cb.Checked) pTypes.Add(pType);
            }
            return pTypes.ToArray();
        }


        private CableTest CurrentTest;

        private DBEntityTable CablesTable;
        private DBEntityTable OperatorsTable;
        private DBEntityTable BarabanTypesTable;
        private DBEntityTable MeasuredParametersTable;
        private bool isSplittedTable;

        public event SACCableTestForm_Handler SelectedCableChanged;

        private CableTestMeasure Measure;


        private void cableForTest_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCableChanged();

        }

        private void measureControlButton_Click(object sender, EventArgs e)
        {
            string txt = String.Empty;
            txt += $"Барабан {CurrentTest.BarabanTypeId} {CurrentTest.BarabanSerial} \n";
            txt += $"Температура {CurrentTest.Temperature} {CurrentTest.IsUseTermoDetector} \n";
            txt += $"Оператор {CurrentTest.OperatorId} \n";
            txt += $"Кабель {CurrentTest.SourceCable.Name} {CurrentTest.CableLength}м \n";
            txt += $"Тип подключения с ДК? {CurrentTest.IsSplittedTable}; Подключен с ПУ {CurrentTest.ConnectedFrom}\n";
            MessageBox.Show(txt);
        }
    }
}
