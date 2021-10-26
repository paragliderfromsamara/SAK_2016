using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.Devices;
using System.Runtime.InteropServices;

namespace NormaLib.UI
{
    public partial class DeviceControlFormBase : NormaMeasureBaseForm
    {
        Form currentForm;
        protected Button currentButton;
        Form nextForm;
        Button nextButton;

        public DeviceControlFormBase() : base()
        {
        }

        public DeviceControlFormBase(DeviceBase _device) : this()
        {
            SetDevice(_device);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        protected virtual void SetDevice(DeviceBase _device)
        {
            this.label1.Text = this.Text = _device.SerialWithFullName;
            //
        }

        protected override void InitializeDesign()
        {
            base.InitializeDesign();
            InitializeComponent();
            panelTitleBar.BackColor = ColorTranslator.FromHtml(NormaUIColors.ColorList[9]);
            Color SecondaryColor = NormaUIColors.ChangeColorBrightness(panelTitleBar.BackColor, 0.2);
            Color ThirdColor = NormaUIColors.ChangeColorBrightness(panelTitleBar.BackColor, 0.3);
            CloseAppButton.FlatAppearance.MouseDownBackColor = panelTitleBar.BackColor;
            CloseAppButton.FlatAppearance.MouseOverBackColor = ThirdColor;
            MinimizeAppButton.FlatAppearance.MouseDownBackColor = panelTitleBar.BackColor;
            MinimizeAppButton.FlatAppearance.MouseOverBackColor = ThirdColor;
            MaximizeAppButton.FlatAppearance.MouseOverBackColor = ThirdColor;
            MaximizeAppButton.FlatAppearance.MouseDownBackColor = panelTitleBar.BackColor;
        }



        private void CloseAppButton_Click(object sender, EventArgs e)
        {
            if (currentForm == null) Close();
            else
            {
                currentForm.FormClosed -= CurrentForm_FormClosed;
                currentForm.FormClosed += (s, a) => { Close(); };
                currentForm.Close();
            }
        }

        private void CurrentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (nextForm != null) SetNextForm();
        }

        private void MaximizeAppButton_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void MinimizeAppButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        protected virtual void SetActiveForm(Form next_form, object btnSender = null)
        {
            nextButton = btnSender == null ? null : (Button)btnSender;
            nextForm = next_form;
            if (currentForm != null)
            {
                currentForm.Close();
            }
            else
            {
                SetNextForm();
            }
        }

        private void SetNextForm()
        {
            ActivateButton(nextButton);
            currentForm = nextForm;
            nextForm = null;
            currentForm.TopLevel = false;
            currentForm.FormBorderStyle = FormBorderStyle.None;
            currentForm.Dock = DockStyle.Fill;
            this.currentModePanel.Controls.Add(currentForm);
            this.currentModePanel.Tag = currentForm;
            currentForm.BringToFront();
            currentForm.FormClosed += CurrentForm_FormClosed;

            currentForm.Show();
        }



        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = NormaUIColors.ChangeColorBrightness(currentButton.BackColor, 0.3); 
                    currentButton.ForeColor = Color.DarkSlateGray; 
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    //SetThemeColors(color);
                    //RefreshThemOnCurrentForm();
                    //lblTitle.Text = currentButton.Text.Trim().ToUpper();

                }
            }
        }

        private void DisableButton()
        {
            foreach (Control prevBtn in deviceModesPanel.Controls)
            {
                if (prevBtn.GetType() == typeof(Button))
                {
                    prevBtn.BackColor = System.Drawing.Color.Turquoise;
                    prevBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
                    prevBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                }
            }
        }
    }
}
