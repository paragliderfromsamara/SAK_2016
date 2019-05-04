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

namespace NormaMeasure.Devices.SAC
{
    public partial class TableMap : Form
    {
        int ModulWidth = 40;
        int ModulHeight = 120;
        public TableMap()
        {
            InitializeComponent(); 
            DrawCommUnits();
            //Paint += TableMap_Paint;
            
        }

        private void DrawCommUnits()
        {
            for(int i = 0; i<26; i++)
            {
                InitPanel(i);
            }
        }

        private void InitPanel(int idx)
        {
            Panel p = new Panel();
            p.Name = $"moudule_{idx}";
            p.Parent = this;
            p.Paint += new PaintEventHandler(TableMap_Paint1);
            bool isDK = idx > 12;
            int xPos = isDK ? (idx - 13) * (ModulWidth + 4) : idx * (ModulWidth+4);
            int yPos = isDK ? 0 : ModulHeight + 30;
            xPos += 10;
            yPos += 10;
            Debug.WriteLine($"InitPanel_{ idx } xPos = {xPos} yPos = {yPos}");
            p.Size = new Size(ModulWidth, ModulHeight);
            p.Location = new Point(xPos, yPos);
            p.BackColor = Color.Aquamarine;

            p.Click += P_Click;
            //p.Visible = true;
            //p.BackColor = Color.Aquamarine;

        }

        private void P_Click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Panel).Name);
        }

        private void TableMap_Paint1(object sender, PaintEventArgs e)
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.DrawRectangle(myPen, new Rectangle(0, 0, (sender as Panel).Width, (sender as Panel).Height));
            myPen.Dispose();
            formGraphics.Dispose();
        }

        private void TableMap_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.DrawRectangle(myPen, new Rectangle(33, 33, 200, 300));
            myPen.Dispose();
            formGraphics.Dispose();
        }
    }
}
