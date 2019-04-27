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
            if (!LoadBaseDataFromDB())
            {
                this.Close();
                return;
            }else
            {
                CurrentTest = CableTest.GetLastOrCreateNew();
                if (!CurrentTest.HasSourceCable) CurrentTest.SourceCable = GetCableFromCableComboBox();
                InitTestProgamGroupBox();
                InitCableTest();
                InitInputs();
                InitMeasure();
            }
        }

        private Cable GetCableFromCableComboBox()
        {
           return (Cable)(CablesTable.Select($"{Cable.CableId_ColumnName} = {cableForTest_CB.SelectedValue}")[0]);
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
            FillFromCableTest();
            RefreshTableElementsList();
            RefreshCableMeasuredParams();
            RefreshRisolSelector();
            InitInputHandlers();

        }

        private void FillFromCableTest()
        {
            FillOperatorId();
            FillBarabanInfo();
            FillTemperature();
            FillTableSettings();
        }

        private void FillRisolType()
        {
            if (CurrentTest.RisolTypeFavourTypeId == 0)
            {
                CurrentTest.RisolTypeFavourTypeId = (uint)RizolSelector_CB.SelectedValue;
            }
        }

        private void FillTableSettings()
        {
            mergedTable_RadioBatton.Checked = !CurrentTest.IsSplittedTable;
            RefreshTableElementsList();
        }

        private void FillTemperature()
        {
            temperature_NumericUpDown.Value = (uint)CurrentTest.Temperature;
            useTemperatureSensor_CheckBox.Checked = CurrentTest.IsUseTermoSensor;
        }

        private void FillBarabanInfo()
        {
            if(CurrentTest.BarabanTypeId > 0)
            {
                barabanTypes_CB.SelectedValue = CurrentTest.BarabanTypeId;
                barabanSerial_TextBox.Text = CurrentTest.BarabanSerial;
            }
            else
            {
                CurrentTest.BarabanTypeId = (uint)barabanTypes_CB.SelectedValue;
                CurrentTest.BarabanSerial = barabanSerial_TextBox.Text;
            }
        }

        private void FillOperatorId()
        {
            if(CurrentTest.HasOperator)
            {
                operatorsList.SelectedValue = CurrentTest.OperatorId;
            }else
            {
                CurrentTest.OperatorId = (uint)operatorsList.SelectedValue;
            }
        }

        private void InitInputHandlers()
        {
            barabanTypes_CB.SelectedValueChanged += BarabanTypes_CB_SelectedValueChanged;
            barabanSerial_TextBox.TextChanged += BarabanSerial_TextBox_TextChanged;
            operatorsList.SelectedValueChanged += OperatorsList_SelectedValueChanged;
            temperature_NumericUpDown.ValueChanged += Temperature_NumericUpDown_ValueChanged;
            useTemperatureSensor_CheckBox.CheckedChanged += UseTemperatureSensor_CheckBox_CheckedChanged;
            cableLength_NumericUpDown.ValueChanged += CableLength_NumericUpDown_ValueChanged;
            connectedFromTableElement_ComboBox.SelectedIndexChanged += ConnectedFromTableElement_ComboBox_SelectedIndexChanged;
            SelectedCableChanged += SACCableTestForm_SelectedCableChanged;
            this.cableForTest_CB.SelectedValueChanged += new System.EventHandler(this.cableForTest_CB_SelectedIndexChanged);
            RizolSelector_CB.SelectedValueChanged += RizolSelector_CB_SelectedValueChanged;
        }

        private void RizolSelector_CB_SelectedValueChanged(object sender, EventArgs e)
        {
            CurrentTest.RisolTypeFavourTypeId = (uint)RizolSelector_CB.SelectedValue;
        }

        private void ConnectedFromTableElement_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentTest.CableConnectedFrom = Convert.ToUInt16(connectedFromTableElement_ComboBox.Text);
        }

        private void CableLength_NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CurrentTest.CableLength = (float)cableLength_NumericUpDown.Value;
        }

        private void UseTemperatureSensor_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CurrentTest.IsUseTermoSensor = useTemperatureSensor_CheckBox.Checked;
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

            DBEntityTable t = new DBEntityTable(typeof(MeasuredParameterType));
            RizolSelector_CB.ValueMember = MeasuredParameterType.ParameterTypeId_ColumnName;
            RizolSelector_CB.DisplayMember = MeasuredParameterType.ParameterName_ColumnName;
            
            foreach(MeasuredParameterType pType in CurrentTest.AllowedMeasuredParameterTypes)
            {
                if (MeasuredParameterType.IsItIsolationaResistance(pType.ParameterTypeId))
                {
                    MeasuredParameterType type = (MeasuredParameterType)t.NewRow();
                    type.FillColsFromEntity(pType);
                    type.ParameterTypeId = pType.ParameterTypeId;
                    t.Rows.Add(type);
                }
            }
            RizolSelector_CB.DataSource = t;
            RizolSelector_CB.Enabled = t.Rows.Count > 0;
            if (RizolSelector_CB.Enabled)
            {
                if (t.Select($"{MeasuredParameterType.ParameterTypeId_ColumnName} = {CurrentTest.RisolTypeFavourTypeId}").Length > 0)
                {
                    RizolSelector_CB.SelectedValue = CurrentTest.RisolTypeFavourTypeId;
                }
                else
                {
                    RizolSelector_CB.SelectedIndex = 0;
                    CurrentTest.RisolTypeFavourTypeId = (uint)RizolSelector_CB.SelectedValue;
                }

            }

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
                cb.Checked = CurrentTest.IsOnTestProgram(mpt);
            }
        }

        private CheckBox getParameterTypeCheckBox(MeasuredParameterType p_type)
        {
            Control[] cbArr = testProgram_GroupBox.Controls.Find($"{p_type.RefText}", false);
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

                cb.Name = $"{pType.RefText}";
                cb.Width = (pType.ParameterTypeId == MeasuredParameterType.Calling) ? cbWidth : cbWidth*2/3;
                cb.Height = cbHeight;
                cb.Location = new Point(tmpHorOffset, tmpVertOffset);
                cb.CheckedChanged += Cb_CheckedChanged;
                x++;
                tmpHorOffset += (cbWidth + horOffset);
                

            }
            testProgram_GroupBox.Width = onRow * (horOffset + cbWidth) + horOffset;
            testProgram_GroupBox.Height = (y+1) * (vertOffset + cbHeight) + vertOffset + startVertOffset;
            this.Width = testProgram_GroupBox.Width + 40;
            this.Height += testProgram_GroupBox.Height;
        }

        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            CurrentTest.SetParameterTypeFlagInTestProgram(cb.Name, cb.Checked);
           // MessageBox.Show(cb.Name);
        }

        /// <summary>
        /// Инициализация функции измерения
        /// Здесь создаётся экземпляр измерения
        /// </summary>
        private void InitMeasure()
        {
            Measure = new CableTestMeasure(CurrentTest);
            Measure.OnMeasureStart += Measure_SwitchMeasureControlButton;
            Measure.OnMeasureStop += Measure_SwitchMeasureControlButton;
            Measure.OnOverallMeasureTimerTick += Measure_OnOverallMeasureTimerTick;
            Measure.Result_Gotten += Measure_Result_Gotten;
            Measure.MeasurePointChanged += Measure_MeasurePointChanged;
            measureControlButton.Click += startMeasure_Click;
            Measure.CableTestFinished += Measure_CableTestFinished;

        }

        private void Measure_CableTestFinished(CableTestMeasure measure)
        {
            CurrentTest.SetFinished();
            MessageBox.Show($"Испытание кабеля \"{CurrentTest.TestedCable.FullName}\" успешно завершено!", "Испытание завершено", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }


        #region Обработчики событий измерения
        private void Measure_MeasurePointChanged(CableTestMeasure measure)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new CableTestMeasure_Handler(Measure_MeasurePointChanged), new object[] { measure });
                return;
            }
            else
            {
                CurrentElement_Label.Text = $"Параметр: {measure.CurrentMeasurePoint.ParameterType.ParameterName}\n{measure.CurrentMeasurePoint.TestedCableStructure.StructureType.StructureTypeName}: {measure.CurrentMeasurePoint.ElementNumber}; Жила: {measure.CurrentMeasurePoint.MeasureNumber}"; 
            }
        }

        private void Measure_Result_Gotten(CableTestMeasure measure)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new CableTestMeasure_Handler(Measure_Result_Gotten), new object[] { measure });
                return;
            }
            else
            {
                measureResultField.Text = measure.LastResult.Result.ToString();
            }
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
                if (Measure.IsStart)
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
        #endregion


        private void startMeasure_Click(object sender, EventArgs e)
        {
            CurrentTest.SetStarted();
            Measure.Start();

        }

        private void stopMeasure_Click(object sender, EventArgs e)
        {
            Measure.Stop();
            CurrentTest.SetStoppedByOperator();
        }



        #region Загрузка необходимых данных из БД
        private bool LoadBaseDataFromDB()
        {
            LoadOperators();
            LoadCables();
            LoadBarabanTypes();
            LoadMeasuredParameterTypes();

            return CheckTest_Availability();
        }

        private void LoadMeasuredParameterTypes()
        {
            MeasuredParametersTable = MeasuredParameterType.get_for_a_program_test();
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
            cableForTest_CB.DataSource = CablesTable;
            cableForTest_CB.DisplayMember = Cable.FullCableName_ColumnName;
            cableForTest_CB.ValueMember = Cable.CableId_ColumnName;
        }

        private void SACCableTestForm_SelectedCableChanged()
        {
            try
            {
                CurrentTest.SourceCable = GetCableFromCableComboBox();
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
            int maxRows = (mergedTable_RadioBatton.Checked) ? 104 : 52;
            CurrentTest.IsSplittedTable = !mergedTable_RadioBatton.Checked;
            for (int i=0; i<maxRows; i++)
            {
                connectedFromTableElement_ComboBox.Items.Add(i+1);
            }
            connectedFromTableElement_ComboBox.SelectedIndex = (CurrentTest.CableConnectedFrom <= connectedFromTableElement_ComboBox.Items.Count) ? (int)(CurrentTest.CableConnectedFrom-1) : 0;
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

        public event SACCableTestForm_Handler SelectedCableChanged;

        private CableTestMeasure Measure;


        private void cableForTest_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
             SelectedCableChanged();
        }

        private void resetCurrentTest_Button_Click(object sender, EventArgs e)
        {
            CurrentTest.SetNotStarted();
        }

        /*
        private void measureControlButton_Click(object sender, EventArgs e)
        {
            /*
            string txt = String.Empty;
            
            txt += $"Барабан {CurrentTest.BarabanTypeId} {CurrentTest.BarabanSerial} \n";
            txt += $"Температура {CurrentTest.Temperature} {CurrentTest.IsUseTermoSensor} \n";
            txt += $"Оператор {CurrentTest.OperatorId} \n";
            txt += $"Кабель {CurrentTest.SourceCable.Name} {CurrentTest.CableLength}м \n";
            txt += $"Тип подключения с ДК? {CurrentTest.IsSplittedTable}; Подключен с ПУ {CurrentTest.CableConnectedFrom}\n";

            MessageBox.Show(txt);
            
        } */
    }
}
