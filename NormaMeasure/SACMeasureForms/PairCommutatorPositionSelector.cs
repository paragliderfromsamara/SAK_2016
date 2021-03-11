using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.Devices.SAC;

namespace NormaMeasure.MeasureControl.SACMeasureForms
{
    public class PairSelector_ComboBox : ComboBox
    {
        int maxElements = 104;
        SACCommutationType curType;

        public PairSelector_ComboBox() : base()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            CommutationType = SACCommutationType.NoFarEnd;

        }

        public PairSelector_ComboBox(SACCommutationType commType) : this()
        {
            CommutationType = commType;
        }


        public void RefreshList(int maxEls)
        {
            int curIndex = SelectedIndex > -1 ? SelectedIndex : 0;
            Items.Clear();
            for (int i = 1; i <= maxEls; i++)
            {
                Items.Add($"{i}");
            }

            if (Items.Count > 0)
            {
                SelectedIndex = 0;
                if (curIndex > Items.Count) SelectedIndex = 0;
                else SelectedIndex = curIndex;
            } else SelectedIndex = -1;
            Refresh();
        }

        public SACCommutationType CommutationType
        {
            set
            {
                //if (value == curType) return;
                curType = value;
                switch (value)
                {
                    case SACCommutationType.Etalon:
                        this.Enabled = false;
                        RefreshList(0);
                        break;
                    case SACCommutationType.NoFarEnd:
                        this.Enabled = true;
                        RefreshList(maxElements);
                        break;
                    case SACCommutationType.WithFarEnd:
                        this.Enabled = true;
                        RefreshList(maxElements/2);
                        break;
                }

            }
        }




    }
}
