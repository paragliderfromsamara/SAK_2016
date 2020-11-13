using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeraMicroMeasure.XmlObjects;

namespace TeraMicroMeasure
{
    class MeasurePanel : Panel
    {
        ClientXmlState state;
        ComboBox cableComboBox;
        ComboBox measureModeComboBox;
        NumericUpDown cableLength;
        public MeasurePanel(ClientXmlState _state) : base()
        {
            state = _state;
            initPanel();
        }

        private void initPanel()
        {
            initCableComboBox();
            initCableLengthInput();
            initMeasureModeComboBox();
            ////////////////////
            setElementsPosition();
        }


        private void setElementsPosition()
        {
            int x = 10, y = 20, xOffset = 30;
            cableComboBox.Location = new System.Drawing.Point(x, y);
            x += cableComboBox.Width + xOffset;
            cableLength.Location = new System.Drawing.Point(x, y);
            x += cableLength.Width + xOffset;
            measureModeComboBox.Location = new System.Drawing.Point(x, y);
        }

        private void initCableComboBox()
        {
            cableComboBox = new ComboBox();
            cableComboBox.Name = "cable_combo_box";
            cableComboBox.Items.Add("Кабель 1");
            cableComboBox.Items.Add("Кабель 2");
            cableComboBox.Items.Add("Кабель 3");
            cableComboBox.SelectedIndex = state.MeasureState.CableId;
            cableComboBox.Parent = this;
            cableComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void initCableLengthInput()
        {
            cableLength = new NumericUpDown();
            cableLength.Minimum = 1;
            cableLength.Maximum = 10000;
            cableLength.Value = state.MeasureState.CableLength;
            cableLength.Parent = this;
        }

        private void initMeasureModeComboBox()
        {
            measureModeComboBox = new ComboBox();
            measureModeComboBox.Parent = this;
            measureModeComboBox.Name = "measure_mode_combobox";
            measureModeComboBox.Items.Add("Сопротивление жил");
            measureModeComboBox.Items.Add("Сопротивление изоляции");
            measureModeComboBox.SelectedIndex = state.MeasureState.MeasureType;
            measureModeComboBox.Width = 150;
            measureModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
