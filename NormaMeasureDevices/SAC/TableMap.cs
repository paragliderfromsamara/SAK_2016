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
        int ModulsAmount = 26;

        int ModulRightPadding = 4;
        int ModulWidth = 60;
        int ModulHeight = 200;
        public TableMap()
        {
            InitializeComponent(); 
            DrawCommUnits();
            //Paint += TableMap_Paint;
            
        }

        private void DrawCommUnits()
        {
            for(int i = 0; i< ModulsAmount; i++)
            {
                Panel p = InitPanel(i);
            }
            this.Size = new Size((ModulsAmount/2)*(ModulWidth + ModulRightPadding+3), ModulHeight*2+35+60);
        }

        private Panel InitPanel(int idx)
        {
            Panel p = new Panel();
            p.Name = $"moudule_{idx}";
            p.Parent = this;

            bool isDK = idx > ModulsAmount/2 - 1;
            int xPos = isDK ? (idx - 13) * (ModulWidth + ModulRightPadding) : idx * (ModulWidth+ ModulRightPadding);
            int yPos = isDK ? 0 : ModulHeight + 30;
            xPos += 10;
            yPos += 10;
            //Debug.WriteLine($"InitPanel_{ idx } xPos = {xPos} yPos = {yPos}");
            p.Size = new Size(ModulWidth, ModulHeight);

            p.BackColor = Color.FromArgb(216, 232, 255);
            p.Click += P_Click;
            p.Location = new Point(xPos, yPos);
            p.Paint += new PaintEventHandler(TableMap_Paint1);
            p.Refresh();
            InitPairsOnModule(p, idx);
            return p;

            //p.Visible = true;
            //p.BackColor = Color.Aquamarine;

        }

        protected void InitPairsOnModule(Panel p, int moduleId)
        {
            for(int i = 0; i<4; i++)
            {
                InitPair(p, i);
            }
        }

        protected void InitPair(Panel p, int pairIdx)
        {
            int pairHeight = 30;
            int marginY = (p.Height - pairHeight*4)/5;
            int locY = (pairHeight+marginY)*pairIdx+marginY;
            int locX = 5;
            Panel pairPanel = new Panel();
            pairPanel.Parent = p;
            pairPanel.Width = p.Width - 10;
            pairPanel.Height = pairHeight;
            pairPanel.Location = new Point(locX, locY);
            pairPanel.BackColor = Color.Black;
        }

        private void P_Click(object sender, EventArgs e)
        {
            MessageBox.Show((sender as Panel).Name);
        }

        private void TableMap_Paint1(object sender, PaintEventArgs e)
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = (sender as Panel).CreateGraphics();
            formGraphics.DrawRectangle(myPen, new Rectangle( 0, 0, (sender as Panel).Width - 2, (sender as Panel).Height - 2));
            //formGraphics.DrawRectangle(myPen, new Rectangle((sender as Panel).Location.X-1, (sender as Panel).Location.Y-1, (sender as Panel).Width+2, (sender as Panel).Height+2));
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
