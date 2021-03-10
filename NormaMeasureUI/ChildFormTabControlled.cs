using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormaMeasure.UI
{
    public partial class ChildFormTabControlled : Form
    {
        Form currentForm;
        int nextIndex = -1;
        public ChildFormTabControlled()
        {
            InitDesign();
            if (settingsTab.TabPages.Count > 0)InitDefaultTab();
        }

        protected virtual void InitDesign()
        {
            InitializeComponent();
        }

        protected virtual void InitDefaultTab()
        {
            SetNextForm(settingsTab.SelectedIndex);
        }



        protected virtual Form GetCurrentTabForm(int idx)
        {
            return new NormaMeasure.UI.ChildForms.BlankForm();
        }

        protected void settingsTab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            nextIndex = e.TabPageIndex;
            if (currentForm != null && nextIndex != settingsTab.SelectedIndex)
            {
                e.Cancel = true;
                currentForm.Close();
            }
        }

        private void SetNextForm(int idx)
        {
            currentForm = GetCurrentTabForm(idx);
            currentForm.TopLevel = false;
            currentForm.FormBorderStyle = FormBorderStyle.None;
            currentForm.Dock = DockStyle.Fill;
            settingsTab.TabPages[idx].Controls.Add(currentForm);
            settingsTab.TabPages[idx].Tag = currentForm;
            currentForm.BringToFront();
            currentForm.FormClosed += CurrentForm_FormClosed;
            currentForm.Show();
        }

        protected void CurrentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            currentForm = null;
            settingsTab.SelectedIndex = nextIndex;
        }


        protected void settingsTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetNextForm(((TabControl)sender).SelectedIndex);
        }
    }
}
