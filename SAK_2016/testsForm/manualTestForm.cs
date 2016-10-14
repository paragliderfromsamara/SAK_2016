using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SAK_2016
{
    public partial class manualTestForm : Form
    {

        delegate void testResultLblTextDelegate(int result);

        public mainForm mainForm = null;
        public measureMain mMain = new measureMain();
        private manualTestThread mThread;

        private string defaultWindowTitle = "Ручные испытания"; 
        public manualTestForm(mainForm f)
        {
            this.mainForm = f;
            InitializeComponent();
            makeParamsList();
            initPairList();
            initFreqList();
 
        }

        private void initMeasTable()
        {

            //measureTable.
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.mainForm.switchMenuStripItems(true);
            this.mainForm.manualTestForm = null;
            this.Close();
            this.Dispose();
        }

        private void makeParamsList()
        {
            for (int i=0; i < mMain.measuredParameters.Length; i++)
            {
                measuredParamsList.Items.Add(mMain.measuredParameters[i]);
                measuredParamsList.SelectedItem = mMain.measuredParameters[0];
            }
        }

        private void initPairList()
        {
            int divider = (tableMode.Checked) ? 1 : 2; //для сдвоенного стола 1 для раздвоенного 0
            pair1.Items.Clear();
            pair2.Items.Clear();
            for (int i = 0; i < Properties.Settings.Default.numberOfComUnits * 4 / divider; i++)
            {
                pair1.Items.Add(Convert.ToString(i+1));
                pair2.Items.Add(Convert.ToString(i + 1));
                pair1.SelectedIndex = 0;
                pair2.SelectedIndex = 0;
            }
        }

        

        private void startPair_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeEndPair();
        }


        private void changeEndPair()
        {
            if (pair1.SelectedIndex > pair2.SelectedIndex) pair2.SelectedIndex = pair1.SelectedIndex;
        }

        private void endPair_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeStartPair();
        }

        private void changeStartPair()
        {
            if (pair1.SelectedIndex > pair2.SelectedIndex) pair1.SelectedIndex = pair2.SelectedIndex;
        }

        private void measuredParamsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (measuredParamsList.SelectedItem.ToString() == "Rж")         mMain = new measureResLeads();
            else if (measuredParamsList.SelectedItem.ToString() == "C0")    mMain = new measureCapacityZero();
            else if (measuredParamsList.SelectedItem.ToString() == "A0")    mMain = new measureTransitiveAttenuationNearSide();
            else if (measuredParamsList.SelectedItem.ToString() == "Aз")    mMain = new measureTransitiveAttenuationFarSide();
            else if (measuredParamsList.SelectedItem.ToString() == "al")    mMain = new measureWorkAttenuation();
            else if (measuredParamsList.SelectedItem.ToString() == "Rиз")   mMain = new measureResIsolation();
            else if (measuredParamsList.SelectedItem.ToString() == "ВсВИ")  mMain = new measureHightVoltageTest();
            else mMain = new measureMain();
            updForm();
        }

        private void updForm()
        {
            leadList.Visible = leadNumberLabel.Visible = mMain.showLeadSelectList;
            tableMode.Visible = mMain.isNotOnlySplitTable;
            freqGroupBox.Visible = mMain.isNeedFreqMenu;
            pair1Lbl.Text = this.mMain.pair1Label;
            pair2Lbl.Text = this.mMain.pair2Label;
            pair2.Visible = pair2Lbl.Visible = mMain.showPair2SelectList;
            
            this.Text = this.defaultWindowTitle + ": " + mMain.fullMeasureModeName;
            this.quantitativeMeasureResult.Text = mMain.quantitativeMeasure;
        }

        private void tableMode_CheckedChanged(object sender, EventArgs e)
        {
            initPairList();
        }

        public void updateResultField(int result) //Для обновления поля результата из другого потока в котором проходит испытание
        {
            if (InvokeRequired)
            {
                BeginInvoke(new testResultLblTextDelegate(updateResultField), new object[] { result });
                return;
            }
            else
            {
                double cResult = mMain.convertResult(result);
                this.mResultLabel.Text = String.Format("{0}", cResult);
                //testResultLbl.Text = string.Format("{0} {1}", cResult, quantitativeMeasureLbl.Text);
                //rawResultField.Text = String.Format("{0}; {1}", Convert.ToString(result, 16), result);
                //updateMeasMemory(cResult);
                //measGraphic.drawMeasGraphic();

            }
        }

        private void startStopMeasureBut_Click(object sender, EventArgs e)
        {
            if (this.mThread == null)
            {
                updateMeasureParams();
                this.mThread = new manualTestThread(this);
                this.startStopMeasureBut.Text = "остановить измерение";
            }
            else
            {
                this.closeThread();
                this.startStopMeasureBut.Text = "начать измерение";
            }
        }

        private void updateMeasureParams()
        {
            this.mMain.startPair = this.pair1.SelectedIndex + 1;
            this.mMain.endPair = this.pair2.SelectedIndex + 1;
            this.mMain.leadNumber = this.leadList.SelectedIndex + 1;
            if (this.mMain.isNeedFreqMenu)
            {
                this.mMain.startFreq = this.mMain.freqRange[this.sFreqComboBox.SelectedIndex];
                this.mMain.endFreq = this.mMain.freqRange[this.eFreqComboBox.SelectedIndex];
                this.mMain.stepFreq = (this.mMain.startFreq == this.mMain.endFreq)? 0 : Convert.ToInt16(this.freqStepComboBox.SelectedText);
            }
        }

        public void closeThread()
        {
            if (this.mThread != null)
            {
                this.mThread.thread.Abort();
                this.mThread = null;
            }
        }

        private void manualTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.closeThread();
        }

        private void initFreqList()
        {

            sFreqComboBox.Items.Clear();
            eFreqComboBox.Items.Clear();
            for (int i = 0; i < this.mMain.freqRange.Length; i++)
            {
                sFreqComboBox.Items.Add(String.Format("{0}", this.mMain.freqRange[i]));
                eFreqComboBox.Items.Add(String.Format("{0}", this.mMain.freqRange[i]));
            }
            sFreqComboBox.SelectedIndex = 0;
            eFreqComboBox.SelectedIndex = 0;
            makeFreqStepList();

        }

        private void makeFreqStepList()
        {
            int stepFreq = this.mMain.minFreqStep;
            //double sFreq = this.mMain.freqRange[sFreqComboBox.SelectedIndex];
            freqStepComboBox.Items.Clear();
            if (sFreqComboBox.SelectedIndex == eFreqComboBox.SelectedIndex) freqStepComboBox.Enabled = false;
            else
            {
                freqStepComboBox.Enabled = true;
                while (stepFreq <= Convert.ToDouble(eFreqComboBox.SelectedItem)/2)
                {
                    freqStepComboBox.Items.Add(stepFreq);
                    stepFreq += this.mMain.minFreqStep;
                } 
                freqStepComboBox.SelectedIndex = 0;
            }
        }

        private void sFreqComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sFreqComboBox.SelectedIndex > eFreqComboBox.SelectedIndex) eFreqComboBox.SelectedIndex = sFreqComboBox.SelectedIndex;
            makeFreqStepList();
        }

        private void eFreqComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sFreqComboBox.SelectedIndex > eFreqComboBox.SelectedIndex) sFreqComboBox.SelectedIndex = eFreqComboBox.SelectedIndex;
            makeFreqStepList();
        }
    }
}
