using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.DBControl.Tables;


namespace NormaLib.DBControl.DBNormaMeasure.Forms
{

    public partial class BarabanTypeForm : Form
    {
        DBEntityTable barabanTypeTable;
        BarabanType barabanType;
        string nameWas;
        float weightWas;
        public BarabanTypeForm(BarabanType type, DBEntityTable table = null)
        {
            InitializeComponent();
            barabanType = type;
            this.Text = type.IsNewRecord() ? "Новый тип барабана" : "Изменение типа барабана";
            barabanTypeTable = table == null ? BarabanType.get_all_as_table() : table;
            tbBarabanName.Text = nameWas = barabanType.TypeName;
            weightWas = barabanType.BarabanWeight;
            tbBarabanWeight.Text = weightWas.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void tbBarabanWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !ServiceFunctions.DoubleCharChecker(e.KeyChar);
        }

        private void BarabanTypeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                float weight = 0.0f;
                barabanType.TypeName = tbBarabanName.Text;
                if (float.TryParse(tbBarabanWeight.Text, out weight))
                {
                    barabanType.BarabanWeight = weight;
                    if (barabanType.Save())
                    {
                        MessageBox.Show($"Тип барабана успешно {(!barabanType.IsNewRecord() ? "сохранен" : "добавлен") }", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }else
                    {
                        e.Cancel = true;
                        MessageBox.Show($"Не удалось {(barabanType.IsNewRecord() ? "добавить" : "сохранить") }\n\n{barabanType.ErrorsAsNumericList}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                }
                else
                {
                    e.Cancel = true;
                    MessageBox.Show("Некорректный формат значения веса барана.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (e.Cancel)
                {
                    barabanType.BarabanWeight = weightWas;
                    barabanType.TypeName = nameWas;
                }
            }
        }
    }
}
