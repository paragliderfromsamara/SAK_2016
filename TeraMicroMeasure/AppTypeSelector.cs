using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using NormaLib.SocketControl.TCPControlLib;
using NormaLib.DBControl;

namespace TeraMicroMeasure
{
    public partial class AppTypeSelector : Form
    {
        public AppTypeSelector()
        {
            InitializeComponent();
        }

        private void panel_MouseHover(object sender, EventArgs e)
        {
            Label l = sender as Label;
            l.BackColor = System.Drawing.SystemColors.Highlight;
            l.ForeColor = SystemColors.HighlightText;
        }



        private void panel_MouseLeave(object sender, EventArgs e)
        {
            Label l = sender as Label;
            l.BackColor = System.Drawing.SystemColors.HotTrack;
            l.ForeColor = Color.White;
        }


        private void label1_Click(object sender, EventArgs e)
        {
            // ok.PerformClick();
            DialogResult = DialogResult.OK;
            SettingsControl.SetClientId(0);
            TCPSettingsForm form = new TCPSettingsForm(new TCPSettingsController(true));
            form.StartPosition = FormStartPosition.CenterScreen;
            DialogResult = form.ShowDialog(this);
            //Properties.Settings.Default.IsServerApp = true;
            //Properties.Settings.Default.FirstRun = false;
            //Properties.Settings.Default.Save();
            this.Close();
            //this.Dispose();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            SettingsControl.SetClientId(-1);
            TCPSettingsForm form = new TCPSettingsForm(new TCPSettingsController(false));
            DBSettingsControl.IsEnabled = false;
            form.StartPosition = FormStartPosition.CenterScreen;
            DialogResult = form.ShowDialog(this);

            //Properties.Settings.Default.IsServerApp = false;
            //Properties.Settings.Default.FirstRun = false;
            //Properties.Settings.Default.Save();
            this.Close();
            //this.Dispose();
        }

        // private void panel1_MouseHover(object sender, EventArgs e)
        //       {
        //
        //}
    }
}
