using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.DBControl.SAC.Forms
{
    public partial class BarabanTypesControlForm : DBEntityControlForm
    {
        private BarabanType barabanType;
        public BarabanTypesControlForm()
        {
            InitializeComponent();
            formDataSet = barabanTypesDS;
            InitNewBaraban();
            loadBarabanTypes();
        }

        private void InitNewBaraban()
        {
            barabanType = BarabanType.build();
            barabanName.Text = barabanType.TypeName;
            textBox1.Text = barabanType.BarabanWeight.ToString();
        }

        private void loadBarabanTypes()
        {
            DataTable bTypesTable = BarabanType.get_all_as_table();
            AddOrMergeTableToFormDataSet(bTypesTable);
            barabanTypeList.DataSource = bTypesTable;
            barabanTypeList.Refresh();
        }

        private void barabanName_TextChanged(object sender, EventArgs e)
        {
            barabanType.TypeName = (sender as TextBox).Text;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (sender as TextBox);
            float value = 0;
            bool isValid = float.TryParse(tb.Text, out value);
            barabanType.BarabanWeight = value;
            if (!isValid)
            {
                tb.Text = "0";
            }
            
        }

        private void addBarabanType_Click(object sender, EventArgs e)
        {
            if (barabanType.Save())
            {
                InitNewBaraban();
                loadBarabanTypes();
                //MessageBox.Show("Новый тип барабана успешно сохранён!", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }

        }

        private void closeForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
