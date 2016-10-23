using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace SAK_2016
{
    public partial class addCableForm : Form
    {
        private DBControl mysql = new DBControl(Properties.dbSakQueries.Default.dbName);
        private Cable dbCable;
        public addCableForm()
        {
            InitializeComponent();
            fillCableDS();   
        }

        public addCableForm(long cable_id)
        {
            dbCable = new Cable(cable_id);
            InitializeComponent();
            fillCableDS();
            fillFieldsFromCable();
        }

        private void fillFieldsFromCable()
        {
            cableMarkComboBox.SelectedValue = dbCable.name;
            structDescTextBox.Text = dbCable.structName;
            buildLength.Text = dbCable.buildLength.ToString();
            linearMass.Text = dbCable.linearMass.ToString();
            code_okp.Text = dbCable.codeOkp;
            code_kch.Text = dbCable.codeKch;
            uCoverVoltage.Text = dbCable.uCover.ToString();
            minPleasure.Text = dbCable.pMin.ToString();
            maxPleasure.Text = dbCable.pMax.ToString();
            notes.Text = dbCable.notes;
            normDocComboBox.SelectedValue = dbCable.documentId Разобраться с установкой значения
        }


        /// <summary>
        /// Заполняет cableDS, обновляет связанные с ним элементы
        /// </summary>
        private void fillCableDS()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter();
                mysql.MyConn.Open();
                da = new MySqlDataAdapter(Properties.dbSakQueries.Default.selectDocuments, mysql.MyConn);
                da.Fill(cableDS.Tables["documents"]);
                da = new MySqlDataAdapter(Properties.dbSakQueries.Default.selectCableMarks, mysql.MyConn);
                da.Fill(cableDS.Tables["cable_marks"]);
                da = new MySqlDataAdapter(Properties.dbSakQueries.Default.selectLeadMaterials, mysql.MyConn);
                da.Fill(cableDS.Tables["lead_materials"]);
                da = new MySqlDataAdapter(Properties.dbSakQueries.Default.selectLeadDiameters, mysql.MyConn);
                da.Fill(cableDS.Tables["lead_diameters"]);
                da = new MySqlDataAdapter(Properties.dbSakQueries.Default.selectDrFormuls, mysql.MyConn);
                da.Fill(cableDS.Tables["dr_formuls"]);
                da = new MySqlDataAdapter(Properties.dbSakQueries.Default.selectDrBringingFormuls, mysql.MyConn);
                da.Fill(cableDS.Tables["dr_bringing_formuls"]);
                da = new MySqlDataAdapter(Properties.dbSakQueries.Default.selectIsolationMaterials, mysql.MyConn);
                da.Fill(cableDS.Tables["isolation_materials"]);
                da = new MySqlDataAdapter(Properties.dbSakQueries.Default.selectWaveResistance, mysql.MyConn);
                da.Fill(cableDS.Tables["wave_resistance"]);
                da = new MySqlDataAdapter(Properties.dbSakQueries.Default.selectCableStructureTypes, mysql.MyConn);
                da.Fill(cableDS.Tables["cable_structure_types"]);
                mysql.MyConn.Close();

                updDocFullNameField();
            }
            catch(Exception)
            {
                MessageBox.Show("Не удалось загрузить таблицы параметров кабеля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void closeBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addCableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

    private void normDocComboBox_SelectedIndexChanged(object sender, EventArgs e) //Подгружаем полное название документа
    {
            updDocFullNameField();
    }
        /// <summary>
        /// Обновляет поле полного названия документа
        /// </summary>
        private void updDocFullNameField()
        {
            string sVal = normDocComboBox.SelectedValue.ToString();
            foreach (DataRow r in cableDS.Tables["documents"].Rows)
            {
                if (sVal == r["id"].ToString())
                {
                    fullNameNormDoc.Text = r["full_name"].ToString();
                    break;
                }
            }
        }






    private bool DoubleCharChecker(char key) //проверка на ввод числа типа Double
    {
    return ((key >= Convert.ToChar(Keys.D0) && key <= Convert.ToChar(Keys.D9)) || key == Convert.ToChar(Keys.Separator) || key == Convert.ToChar(Keys.Delete) || key == Convert.ToChar(Keys.Back));

    //return false;
    }

    private bool IntCharChecker(char key) //проверка на ввод числа типа Integer
    {
    return ((key >= Convert.ToChar(Keys.D0) && key <= Convert.ToChar(Keys.D9)) || key == Convert.ToChar(Keys.Back));

    //return false;
    }

    private bool DoubleTextChecker(char key, string text)
    {
    return true;
    }

    private void buildLength_KeyPress(object sender, KeyPressEventArgs e)
    {
    e.Handled = !DoubleCharChecker(e.KeyChar);
    }

    private void buildLength_Leave(object sender, EventArgs e)
    {
    checkDecimalValInTextBox(buildLength);
    }

    private void checkDecimalValInTextBox(TextBox t)
    {
    try
    {
    t.Text = Convert.ToDecimal(t.Text).ToString();
    }
    catch (FormatException)
    {
    t.Text = "";
    }
    }

    private void checkVoltageValue(TextBox t)
    {
        try
        {
            int val = Convert.ToInt32(t.Text);
            if (val < 500 && val != 0)
            {
                val = 500;
            }
            else if (val > 3000) val = 3000;
            t.Text = Convert.ToInt32(val).ToString();
        }
        catch (FormatException)
        {
        t.Text = "";
        }
    }

    private void cableWeight_KeyPress(object sender, KeyPressEventArgs e)
    {
    e.Handled = !DoubleCharChecker(e.KeyChar);
    }

    private void cableWeight_Leave(object sender, EventArgs e)
    {
    checkDecimalValInTextBox(linearMass);
    }

    private void testVoltageObol_KeyPress(object sender, KeyPressEventArgs e)
    {
    e.Handled = !DoubleCharChecker(e.KeyChar);
    }

    private void testVoltageObol_Leave(object sender, EventArgs e)
    {
    checkDecimalValInTextBox(uCoverVoltage);
    }

    private void minPleasure_KeyPress(object sender, KeyPressEventArgs e)
    {
    e.Handled = !DoubleCharChecker(e.KeyChar);
    }

    private void minPleasure_Leave(object sender, EventArgs e)
    {
    checkDecimalValInTextBox(minPleasure);
    }

    private void maxPleasure_KeyPress(object sender, KeyPressEventArgs e)
    {
    e.Handled = !DoubleCharChecker(e.KeyChar);
    }

    private void maxPleasure_Leave(object sender, EventArgs e)
    {
    checkDecimalValInTextBox(maxPleasure);
    }

    private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
    {
    e.Handled = !IntCharChecker(e.KeyChar);
    }

    private void textBox3_Leave(object sender, EventArgs e)
    {
    checkVoltageValue(textBox3);
    }

    private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
    {
    e.Handled = !IntCharChecker(e.KeyChar);
    }

    private void textBox2_Leave(object sender, EventArgs e)
    {
    checkVoltageValue(textBox2); 
    }

    private void checkStructNumbers(string changedVal) //Проверка фактического и номинального значения элементов структуры в кабеле
    {
    decimal fact = factElNumb.Value;
    decimal nominal = nomElNumb.Value;
    if (changedVal == "f") //если изменено фактическое значение
    {
    if (nominal == 0)
      nomElNumb.Value = 1;
    else
      if (nominal > fact) nomElNumb.Value = fact;

    }
    else if (changedVal == "n") //если изменено номинальное значение
    {
    if (nominal > fact) nomElNumb.Value = fact;
    }

    }

    private void factElNumb_ValueChanged(object sender, EventArgs e)
    {
    checkStructNumbers("f");
    structNameGenerator();
    }

    private void nomElNumb_ValueChanged(object sender, EventArgs e)
    {
    checkStructNumbers("n");
    structNameGenerator();
    }

    private void switchGroupCapacityMeasure() //Выключает или включает возможность измерения емкости группы
    {
    string val = structTypeComboBox.SelectedValue.ToString();
    if (val == "1" || val == "3")
    {
    groupCapacityCheckBox.Checked = false;
    groupCapacityCheckBox.Enabled = false;
    }
    else
    {
    groupCapacityCheckBox.Enabled = true;
    }
    }

    private void structTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
    switchGroupCapacityMeasure();
    structNameGenerator();
    }

    private void structNameGenerator() //Формируем наименование структуры кабеля
    {
    try
    {
    int val = Convert.ToInt16(structTypeComboBox.SelectedValue.ToString());
    double leadDiameter = Convert.ToDouble(leadDiametersComboBox.SelectedValue.ToString());
    string elementsNumber = nomElNumb.Value.ToString();
    if (val == 1)
      structDescTextBox.Text = String.Format("{0}x{1}", elementsNumber, leadDiameter);
    else
    {
      val = val > 4 ? 4 : val;
      structDescTextBox.Text = String.Format("{0}x{1}x{2}", elementsNumber, val, leadDiameter);
    }
    }
    catch { }
    }

    private void leadDiametersComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
    structNameGenerator();
    }


    }
}
