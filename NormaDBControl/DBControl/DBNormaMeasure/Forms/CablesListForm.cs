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

namespace NormaMeasure.DBControl.DBNormaMeasure.Forms
{
    public partial class CablesListForm : Form
    {
        CABLE_FORM_TYPE cableFormType; 
        private List<CableForm> cableForms = new List<CableForm>();
        public CablesListForm(CABLE_FORM_TYPE cable_form_type)
        {
            cableFormType = cable_form_type;
            InitializeComponent();
            InitCablesList();
            FillCablesList();
        }

        private void InitCablesList()
        {
            cablesList.RowTemplate.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = cablesList.SelectedRows.Count == 0;
            createFromToolStripMenuItem.Enabled = cablesList.SelectedRows.Count == 1;
        }

        private void FillCablesList()
        {
            DBEntityTable t = Cable.get_all_including_docs();
            cablesList.DataSource = t;
            cablesList.Refresh();
        }

        private void newCableFormButton_Click(object sender, EventArgs e)
        {
            CableForm draftCableForm = FindDraftCableFormInList();
            if (draftCableForm == null) CreateNewCableForm();
            else doFocusToCableForm(draftCableForm);
        }

        private void doFocusToCableForm(CableForm cable_form)
        {
            if (cable_form.WindowState == FormWindowState.Minimized)
            {
                cable_form.WindowState = FormWindowState.Normal;
            }
            cable_form.Activate();
        }

        private void CreateNewCableForm()
        {
            CableForm draftCableForm = (cableFormType == CABLE_FORM_TYPE.SAK) ? (CableForm)(new CableFormSAC()) : (CableForm)(new CableFormTeraMicro());
            if (draftCableForm.Cable == null)
            {
                draftCableForm.Close();
            }
            else
            {
                draftCableForm.MdiParent = this.MdiParent;
                draftCableForm.Show();
                cableForms.Add(draftCableForm);
                draftCableForm.FormClosed += new FormClosedEventHandler(cableForm_Closed);
            }
        }

        private void CreatEditCableForm(uint cable_id)
        {
            CableFormSAC cable_form = new CableFormSAC(cable_id);
            if (cable_form.Cable == null)
            {
                cable_form.Close();
                MessageBox.Show($"Кабель с id = {cable_id} не найден");
            }else
            {
                cable_form.MdiParent = this.MdiParent;
                cable_form.Show();
                cableForms.Add(cable_form);
                cable_form.FormClosed += new FormClosedEventHandler(cableForm_Closed);
            }
        }

        private void CreateCopyFromCableForm(uint cable_id)
        {
            Cable copiedCable = Cable.find_by_cable_id(cable_id);
            if (copiedCable != null)
            {
                CableFormSAC cable_form = new CableFormSAC(copiedCable);

                if (cable_form.Cable == null)
                {
                    cable_form.Close();
                    MessageBox.Show($"Кабель с id = {cable_id} не найден");
                }
                else
                {
                    cable_form.MdiParent = this.MdiParent;
                    cable_form.Show();
                    cableForms.Add(cable_form);
                    cable_form.FormClosed += new FormClosedEventHandler(cableForm_Closed);
                }
            }


        }

        private CableForm FindDraftCableFormInList()
        {
            foreach(CableForm cf in cableForms)
            {
                if (cf.IsNew) return cf;
            }
            return null;
        }

        private CableFormSAC FindCableFormByCableId(uint cable_id)
        {
            foreach (CableFormSAC cf in cableForms)
            {
                if (cf.Cable.CableId == cable_id) return cf;
            }
            return null;
        }

        private void cableForm_Closed(object sender, EventArgs e)
        {
            CableForm f = sender as CableForm;
            uint cabId = f.Cable.CableId;
            cableForms.Remove(f);
            FillCablesList();
            selectRowWithCableId(cabId);
        }

        private void selectRowWithCableId(uint cabId)
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable));
            cablesList.ClearSelection();
            foreach (DataGridViewRow r in cablesList.Rows)
            {
                if((uint)r.Cells[t.PrimaryKey[0].ColumnName].Value == cabId)
                {
                    r.Selected = true;
                    return;
                }
            }
        }

        private void editCableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cablesList.SelectedRows.Count > 0)
            {
                uint cable_id = (uint)cablesList.SelectedRows[0].Cells["cable_id"].Value;
                CableFormSAC cf = FindCableFormByCableId(cable_id);
                if (cf == null) CreatEditCableForm(cable_id);
                else doFocusToCableForm(cf);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = cablesList.SelectedRows.Count == 1 ? "выбранный кабель" : "выбранные кабели";
            DBEntityTable t = new DBEntityTable(typeof(Cable));
            DialogResult r = MessageBox.Show($"Вы уверены, что хотите удалить {msg}?", "Вопрос...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                foreach(DataGridViewRow row in cablesList.SelectedRows)
                {
                    uint cabId = 0;
                    if (uint.TryParse(row.Cells[t.PrimaryKey[0].ColumnName].Value.ToString(), out cabId))
                    {
                        Cable cab = Cable.find_by_cable_id(cabId);
                        cab.Destroy();
                    }
                    
                }
            }
            FillCablesList();
            //if (r == DialogResult.OK) 

        }

        private void createFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBEntityTable t = new DBEntityTable(typeof(Cable));
            uint cableId = 0;

            if (uint.TryParse(cablesList.SelectedRows[0].Cells[t.PrimaryKey[0].ColumnName].Value.ToString(), out cableId))
            {
                CreateCopyFromCableForm(cableId);
            }
            

        }
    }

    public enum CABLE_FORM_TYPE
    {
        SAK, 
        TERA_MICRO
    }
}
