using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Panel s = sender as Panel;
            s.BackColor = System.Drawing.SystemColors.HotTrack;
           
        }


        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            // ok.PerformClick();
            DialogResult = DialogResult.OK;
            Properties.Settings.Default.IsServerApp = true;
            Properties.Settings.Default.FirstRun = false;
            Properties.Settings.Default.Save();
            this.Close();
            //this.Dispose();
        }

        private void panel_MouseLeave(object sender, EventArgs e)
        {
            Panel s = sender as Panel;
            s.BackColor = System.Drawing.SystemColors.Highlight;
         
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Properties.Settings.Default.IsServerApp = false;
            Properties.Settings.Default.FirstRun = false;
            Properties.Settings.Default.Save();
            this.Close();
            //this.Dispose();
        }

        // private void panel1_MouseHover(object sender, EventArgs e)
        //       {
        //
        //}
    }
}
