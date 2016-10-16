using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SAK_2016
{
    public partial class addCableForm : Form
    {
        public addCableForm()
        {
            InitializeComponent();
            /*
            getCableMarksList(); //Загружаем марки кабелей
            getNormaList(); //Загружаем нормативы
            getStructTypeList(); //Загружаем типы структур
            getLeadMaterialList(); //Загружаем материалы жил
            getLeadsDiameters(); //Загружаем диаметры жил
            getIzolMaterialList(); //Загружаем материалы изоляции
            getWaveResistanceList(); //Загружаем список ранее введенных волновых сопротивлений
            getDrFormuls(); //Загружаем список формул для вычисления омической ассиметрии
            */
        }

        private void closeBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addCableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
        /*
private void getCableMarksList() //Загружаем марки кабелей
{
DBControl mysql = new DBControl("bd_cable");
mysql.MyConn.Open();
string com = mysql.GetSQLCommand("ShowCableMarks");
CoreLab.MySql.MySqlDataAdapter da = new CoreLab.MySql.MySqlDataAdapter(com, mysql.MyConn);
da.Fill(cableMarksDataSet);
cableMarkComboBox.DataSource = cableMarksDataSet.Tables[0];
cableMarkComboBox.DisplayMember = "CabName";
cableMarkComboBox.ValueMember = "CabName";
mysql.MyConn.Close();
cableMarkComboBox.Update();
}

private void getNormaList() //Загружаем нормативы
{
DBControl mysql = new DBControl("bd_cable");
mysql.MyConn.Open();
string com = mysql.GetSQLCommand("ShowNormaDocs");
CoreLab.MySql.MySqlDataAdapter da = new CoreLab.MySql.MySqlDataAdapter(com, mysql.MyConn);
da.Fill(normDocsDataSet);
normDocComboBox.DataSource = normDocsDataSet.Tables[0];
normDocComboBox.DisplayMember = "DocNum";
normDocComboBox.ValueMember = "DocInd";
mysql.MyConn.Close();
normDocComboBox.Update();
}

private void getStructTypeList() //Загружаем типы структур
{
DBControl mysql = new DBControl("bd_cable");
mysql.MyConn.Open();
string com = mysql.GetSQLCommand("ShowStructTypes");
CoreLab.MySql.MySqlDataAdapter da = new CoreLab.MySql.MySqlDataAdapter(com, mysql.MyConn);
da.Fill(structTypesDataSet);
structTypeComboBox.DataSource = structTypesDataSet.Tables[0];
structTypeComboBox.DisplayMember = "StruktTip";
structTypeComboBox.ValueMember = "StruktNum";
mysql.MyConn.Close();
structTypeComboBox.Update();
}

private void getLeadMaterialList() //Загружаем материалы жил
{
DBControl mysql = new DBControl("bd_cable");
mysql.MyConn.Open();
string com = mysql.GetSQLCommand("ShowLeadsMaterial");
CoreLab.MySql.MySqlDataAdapter da = new CoreLab.MySql.MySqlDataAdapter(com, mysql.MyConn);
da.Fill(leadMaterDataSet);
leadMaterialComboBox.DataSource = leadMaterDataSet.Tables[0];
leadMaterialComboBox.DisplayMember = "MaterName";
leadMaterialComboBox.ValueMember = "MaterInd";
mysql.MyConn.Close();
leadMaterialComboBox.Update();
}
private void getLeadsDiameters() //Загружаем диаметры жил
{
DBControl mysql = new DBControl("bd_cable");
mysql.MyConn.Open();
string com = mysql.GetSQLCommand("ShowLeadsDiameters");
CoreLab.MySql.MySqlDataAdapter da = new CoreLab.MySql.MySqlDataAdapter(com, mysql.MyConn);
da.Fill(leadsDiameterDataSet);
leadDiametersComboBox.DataSource = leadsDiameterDataSet.Tables[0];
leadDiametersComboBox.DisplayMember = "DiamGil";
leadDiametersComboBox.ValueMember = "DiamGil";
mysql.MyConn.Close();
leadDiametersComboBox.Update();
}
private void getIzolMaterialList() //Загружаем материалы изоляции
{
DBControl mysql = new DBControl("bd_cable");
mysql.MyConn.Open();
string com = mysql.GetSQLCommand("ShowIsolationMaterial");
CoreLab.MySql.MySqlDataAdapter da = new CoreLab.MySql.MySqlDataAdapter(com, mysql.MyConn);
da.Fill(izolMaterialsDataSet);
izolMaterialComboBox.DataSource = izolMaterialsDataSet.Tables[0];
izolMaterialComboBox.DisplayMember = "MaterName";
izolMaterialComboBox.ValueMember = "MaterInd";
mysql.MyConn.Close();
izolMaterialComboBox.Update();
}
private void getWaveResistanceList() //Загружаем список ранее введенных волновых сопротивлений
{
DBControl mysql = new DBControl("bd_cable");
mysql.MyConn.Open();
string com = mysql.GetSQLCommand("ShowWaveResistance");
CoreLab.MySql.MySqlDataAdapter da = new CoreLab.MySql.MySqlDataAdapter(com, mysql.MyConn);
da.Fill(waveResistanceDataSet);
waveResistance.DataSource = waveResistanceDataSet.Tables[0];
waveResistance.DisplayMember = "Zwave";
waveResistance.ValueMember = "Zwave";
mysql.MyConn.Close();
waveResistance.Update();
}
private void getDrFormuls() //Загружаем список формул для вычисления омической ассиметрии
{
DBControl mysql = new DBControl("bd_cable");
mysql.MyConn.Open();
string com_dr_formuls = mysql.GetSQLCommand("ShowDrFormuls");
string com_dr_priv_formuls = mysql.GetSQLCommand("ShowDrPrivFormuls");
CoreLab.MySql.MySqlDataAdapter da_formuls = new CoreLab.MySql.MySqlDataAdapter(com_dr_formuls, mysql.MyConn);
CoreLab.MySql.MySqlDataAdapter da_priv_formuls = new CoreLab.MySql.MySqlDataAdapter(com_dr_priv_formuls, mysql.MyConn);
da_formuls.Fill(drFormulsDataSet);
da_priv_formuls.Fill(drPrivFormulsDataSet);
drFormulsComboBox.DataSource = drFormulsDataSet.Tables[0];
drFormulsComboBox.DisplayMember = "DROpis";
drFormulsComboBox.ValueMember = "DRInd";
drPrivFormulsComboBox.DataSource = drPrivFormulsDataSet.Tables[0];
drPrivFormulsComboBox.DisplayMember = "DRPrivOpis";
drPrivFormulsComboBox.ValueMember = "DRPrivInd";
mysql.MyConn.Close();
drPrivFormulsComboBox.Update();
drFormulsComboBox.Update();
}


private void normDocComboBox_SelectedIndexChanged(object sender, EventArgs e) //Подгружаем полное название документа
{
string sVal = normDocComboBox.SelectedValue.ToString();
for (int i = 0; i < normDocsDataSet.Tables[0].Rows.Count; i++)
{
string v = normDocsDataSet.Tables[0].Rows[i][0].ToString();
if (v == sVal)
{
  fullNameNormDoc.Text = normDocsDataSet.Tables[0].Rows[i][2].ToString();
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
catch (FormatException ex)
{
t.Text = "";
}
}

private void checkVoltageValue(TextBox t)
{
try
{
int val = Convert.ToInt32(t.Text);
if (val < 500) val = 500;
else if (val > 3000) val = 3000;
t.Text = Convert.ToInt32(val).ToString();
}
catch (FormatException ex)
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
checkDecimalValInTextBox(cableWeight);
}

private void testVoltageObol_KeyPress(object sender, KeyPressEventArgs e)
{
e.Handled = !DoubleCharChecker(e.KeyChar);
}

private void testVoltageObol_Leave(object sender, EventArgs e)
{
checkDecimalValInTextBox(testVoltageObol);
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
*/
    }
}
