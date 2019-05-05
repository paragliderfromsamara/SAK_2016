using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace NormaMeasure.Devices
{
    public delegate void TableMap_Module_Handler(int pairNumber);
    public delegate void TableMap_Pair_Handler(ModulePair pair);
    public delegate void TableMap_PairLead_Handler();
    public class TableMap : Panel
    {
        public Dictionary<int, ModulePair> TablePairs;
        public Dictionary<int, ModulePair> SelectedPairs;

        int DefaultWidth;
        int DefaultHeight;

        bool KeyPressed = false;


        public int ModulsAmount = 26;
        public int HalfTableModulsAmount => 26 / 2;

        public TableMap(Form form) : base()
        {
            SelectedPairs = new Dictionary<int, ModulePair>();
            Name = "table_map";
            BackColor = Color.FromArgb(92, 92, 92);
            CalculateDefaultSize();
            DrawCommUnits();
            form.KeyUp += Form_KeyUp;
            form.KeyDown += Form_KeyDown; //+= Form_KeyUp; 
            OnPair_Click += TableMap_OnPair_ClickNoControl;
        }


        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            KeyPressed = false;
            if (e.KeyCode == Keys.ControlKey)
            {
                OnPair_Click -= TableMap_OnPair_ClickWithACntrl;
                OnPair_Click += TableMap_OnPair_ClickNoControl;
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyPressed) return;
            if (e.KeyCode == Keys.ControlKey)
            {
                OnPair_Click -= TableMap_OnPair_ClickNoControl;
                OnPair_Click += TableMap_OnPair_ClickWithACntrl;
                KeyPressed = true;
            }
        }

        private void TableMap_OnPair_ClickNoControl(ModulePair pair)
        {
           // MessageBox.Show("Form_WithCtrl");
            ClearSelection();
            TogglePairSelection(pair);
        }

        /// <summary>
        /// Снятие выделения со всех реле
        /// </summary>
        private void ClearSelection()
        {
            foreach(ModulePair pair in SelectedPairs.Values)
            {
                pair.ToogleSelection();
            }
            SelectedPairs.Clear();
        }

        private void TableMap_OnPair_ClickWithACntrl(ModulePair pair)
        {
            //MessageBox.Show("Form_NoCtrl");
            TogglePairSelection(pair);

            //MessageBox.Show("Control");
        }


        private void CalculateDefaultSize()
        {
            DefaultHeight = TableModule.ModulHeight * 2 + TableModule.ModulVerticalOffset;
            DefaultWidth = TableModule.ModulWidth * HalfTableModulsAmount + TableModule.ModulRightPadding *(HalfTableModulsAmount);
            this.Size = new Size(DefaultWidth+30, DefaultHeight);
            AutoSize = true;
        }

        private void DrawCommUnits()
        {
            TablePairs = new Dictionary<int, ModulePair>();
            for (int i = 0; i < ModulsAmount; i++)
            {
                TableModule m = new TableModule(this, i);
            }
            //this.Size = new Size((ModulsAmount / 2) * (ModulWidth + ModulRightPadding + 3), ModulHeight * 2 + 35 + 60);
        }

        public void PairClicked(ModulePair pair)
        {
            OnPair_Click?.Invoke(pair);
        }

        

        public void TogglePairSelection(ModulePair pair)
        {
            pair.ToogleSelection();
            if (SelectedPairs.ContainsKey(pair.PairOnTableNumber))
            {
                SelectedPairs.Remove(pair.PairOnTableNumber);
                OnPair_LoseSelection?.Invoke(pair);
            }
            else
            {
                SelectedPairs[pair.PairOnTableNumber] = pair;
                OnPair_Selected?.Invoke(pair);
            }
            
        }

        public event TableMap_Pair_Handler OnPair_LoseSelection;
        public event TableMap_Pair_Handler OnPair_Selected;
        public event TableMap_Pair_Handler OnPair_Click;
    }


    public class TableModule : Panel
    {
        public TableMap tableMap;
        public int ModuleNumber;
        public const int ModulRightPadding = 4;
        public const int ModulWidth = 60;
        public const int ModulHeight = 200;
        public const int ModulVerticalOffset = 30;

        readonly Color DefaultModulColor = Color.FromArgb(214, 227, 255);


        public TableModule(TableMap map, int idx) : base()
        {
            bool isDK = idx > map.HalfTableModulsAmount - 1;
            int moduleXIdx = !isDK ? idx : idx - map.HalfTableModulsAmount;
            int xPos = moduleXIdx * (ModulWidth) + (moduleXIdx)* ModulRightPadding;
            int yPos = isDK ? 0 : TableModule.ModulHeight + 30;
            tableMap = map;
            ModuleNumber = idx;
            Name = $"moudule_{idx}";
            Parent = map;

            Size = new Size(ModulWidth, ModulHeight);

            BackColor = DefaultModulColor;
            Location = new Point(xPos, yPos);
            Refresh();
            InitPairs();
        }

        protected void InitPairs()
        {

            for (int i = 0; i < 4; i++)
            {
                ModulePair pair = new ModulePair(this, i);
                tableMap.TablePairs.Add(pair.PairOnTableNumber, pair); 
            }
        }
    }

    public class ModulePair : Panel
    {
        private int PairIdxOnTableModule;
        public int PairOnTableNumber;
        public TableModule tabModule;
        public Dictionary<int,PairLead> Leads;
        private ModulePairState pairState;

        private Color NotSelectedPairColor = Color.Empty;
        private Color SelectedBackColor = Color.FromArgb(160, 160, 160);
        

        public ModulePairState PairState
        {
            get
            {
                return pairState;
            }
            set
            {
                pairState = value;
                SetPairState(value);
            }
        }

        private void SetPairState(ModulePairState value)
        {
            switch(value)
            {
                case ModulePairState.NotSelected:
                    BackColor = NotSelectedPairColor;
                    break;
                case ModulePairState.Selected:
                    BackColor = SelectedBackColor;
                    break;
            }
        }

        readonly Color DefaultLeadColor =  Color.FromArgb(0, 96, 255);

        public ModulePair(TableModule module, int pairIdx)
        {
            PairIdxOnTableModule = pairIdx;
            tabModule = module;
            PairState = ModulePairState.NotSelected;
            InitPair();
            InitLeads();
        }

        public void ToogleSelection()
        {
            if (PairState == ModulePairState.NotSelected)
            {
                PairState = ModulePairState.Selected;
            }else if(pairState == ModulePairState.Selected)
            {
                PairState = ModulePairState.NotSelected;
            }
        }

        private void InitLeads()
        {
            Leads = new Dictionary<int, PairLead>();
            for (int i=0; i<2;i++)
            {
                PairLead lead = new PairLead(this, i);
                Leads.Add(i, lead);
            }
        }



        private void InitPair()
        {
            PairOnTableNumber = tabModule.ModuleNumber * 4 + PairIdxOnTableModule + 1;
            int pairHeight = tabModule.Height/4;
            //int marginY = pairHeight* PairIdxOnTableModule;
            int locY = pairHeight* PairIdxOnTableModule;
            int locX = 0;
            Parent = tabModule;
            Size = new Size(tabModule.Width, pairHeight);
            Location = new Point(locX, locY);
            BackColor = tabModule.BackColor;

            Label lbl = new Label();
            lbl.AutoSize = true;
            lbl.Text = $"{PairOnTableNumber}";
            lbl.Parent = this;
            lbl.Location = new Point(this.Width/2 - lbl.Width/2, 8);
            lbl.Cursor = this.Cursor = Cursors.Hand;
            lbl.Click += ModulePair_Click;
            Click += ModulePair_Click;
        }

        private void ModulePair_Click(object sender, EventArgs e)
        {
            this.tabModule.tableMap.PairClicked(this);
        }
    }

    public class PairLead : Panel
    {
        ModulePair pair;
        int LeadId;
        
        public PairLead(ModulePair _pair, int lead_id) : base()
        {
            pair = _pair;
            LeadId = lead_id;

            int leadHeight = (pair.Height * 2) / 4 - 2;
            int leadWidth = leadHeight;
            int locY = pair.Width - leadHeight - 17;
            int locX = lead_id * pair.Width / 2 + (pair.Width / 2 - leadWidth) / 2;
            Parent = pair;
            Size = new Size(leadWidth, leadHeight);
            Location = new Point(locX, locY);
            Name = lead_id == 0 ? "B" : "A";
            ForeColor = Color.Black;
            Paint += new PaintEventHandler(Pair_Paint);
        }

        private void Pair_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            System.Drawing.Graphics formGraphics;
            System.Drawing.Graphics textGraphics;
            textGraphics = (sender as Panel).CreateGraphics();
            formGraphics = (sender as Panel).CreateGraphics();
            formGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            formGraphics.FillEllipse(brush, new Rectangle(0, 0, (sender as Panel).Width - 2, (sender as Panel).Height - 2));

            string drawString = (sender as Panel).Name;
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            textGraphics.DrawString(drawString, drawFont, drawBrush, (sender as Panel).Width / 2 - 6, (sender as Panel).Height / 2 - 7, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            textGraphics.Dispose();

            brush.Dispose();
            formGraphics.Dispose();
        }

    }

    public enum ModulePairState
    {
        NotSelected,
        Selected
    }
}
