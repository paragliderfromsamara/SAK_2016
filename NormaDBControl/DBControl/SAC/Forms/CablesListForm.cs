﻿using System;
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
    public partial class CablesListForm : Form
    {
        private List<CableForm> cableForms = new List<CableForm>();
        public CablesListForm()
        {
            InitializeComponent();
            FillCablesList();
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
            CableForm draftCableForm = new CableForm();
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
            CableForm cable_form = new CableForm(cable_id);
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

        private CableForm FindDraftCableFormInList()
        {
            foreach(CableForm cf in cableForms)
            {
                if (cf.IsNew) return cf;
            }
            return null;
        }

        private CableForm FindCableFormByCableId(uint cable_id)
        {
            foreach (CableForm cf in cableForms)
            {
                if (cf.Cable.CableId == cable_id) return cf;
            }
            return null;
        }

        private void cableForm_Closed(object sender, EventArgs e)
        {
            cableForms.Remove(sender as CableForm);
        }

        private void editCableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cablesList.SelectedRows.Count > 0)
            {
                uint cable_id = (uint)cablesList.SelectedRows[0].Cells["cable_id"].Value;
                CableForm cf = FindCableFormByCableId(cable_id);
                if (cf == null) CreatEditCableForm(cable_id);
                else doFocusToCableForm(cf);
            }
        }


    }
}
