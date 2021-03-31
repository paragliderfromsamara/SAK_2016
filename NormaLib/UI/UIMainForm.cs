using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace NormaLib.UI
{
    public partial class UIMainForm : Form
    {
        protected Button currentButton;
        protected Form currentForm;
        Form nextForm;
        Button nextButton;
        //Constructor
        public UIMainForm()
        {
            InitializeDesign();
            InitCulture();
            SetThemeColors(panelMenu.BackColor);
            RefreshThemOnCurrentForm();
            this.Text = String.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            //CheckColors();
        }


        protected virtual void InitCulture()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("my");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        }

        protected virtual void InitializeDesign()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);



        //Methods

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = GetButtonColor(((Button)btnSender).Name);
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    SetThemeColors(color);
                    RefreshThemOnCurrentForm();
                    lblTitle.Text =  currentButton.Text.Trim().ToUpper();

    }
            }
        }

        private void SetThemeColors(Color color)
        {
            NormaUIColors.PrimaryColor = color;
            NormaUIColors.SecondaryColor = NormaUIColors.ChangeColorBrightness(color, 0.2);
            NormaUIColors.ThirdColor = NormaUIColors.ChangeColorBrightness(color, 0.3);

        }

        protected Color GetButtonColor(string btnName)
        {
            switch(btnName)
            {
                case "btnMeasure":
                    return ColorTranslator.FromHtml(NormaUIColors.ColorList[9]);
                case "btnSettings":
                    return ColorTranslator.FromHtml(NormaUIColors.ColorList[10]);
                case "btnDataBase":
                    return ColorTranslator.FromHtml(NormaUIColors.ColorList[15]);
                default:
                    return Color.White;
            }
            
        }

        private void DisableButton()
        {
            foreach(Control prevBtn in panelMenu.Controls)
            {
                if (prevBtn.GetType() == typeof(Button))
                {
                    prevBtn.BackColor = Color.FromArgb(3, 64, 114);
                    prevBtn.ForeColor = Color.Gainsboro;
                    prevBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    
                }
            }
        }

        private void RefreshThemOnCurrentForm()
        {
            panelTitleBar.BackColor = NormaUIColors.SecondaryColor;
            panelHeader.BackColor = NormaUIColors.ThirdColor;
        }

        //protected virtual Color SelectThemeColor()
        //{
        //
        //        }



        async private Task CheckColors()
        {
            await Task.Run(() => {
                for (int i = 0; i < NormaUIColors.ColorList.Count; i++)
                {
                    SetColor(i);

                    System.Threading.Thread.Sleep(2000);
                }
            });
        }
        void SetColor(int colorIdx)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new SetColorDelegate(SetColor), new object[] { colorIdx });
            }
            else
            {
                btnMeasure.BackColor = ColorTranslator.FromHtml(NormaUIColors.ColorList[colorIdx]);
                btnMeasure.Text = $"Цвет {colorIdx}";
            }
        }

        private void CloseAppButton_Click(object sender, EventArgs e)
        {
            if (currentForm == null)Close();
            else
            {
                currentForm.FormClosed -= CurrentForm_FormClosed;
                currentForm.FormClosed += (s, a) => { Close(); };
                currentForm.Close();
            }
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

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #region LeftPanelButtonClick Handlers
        private void btnDataBase_Click(object sender, EventArgs e)
        {
            SetActiveForm(GetDataBaseForm(), sender);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SetActiveForm(GetSettingsForm(), sender);
        }

        private void btnMeasure_Click(object sender, EventArgs e)
        {
            SetActiveForm(GetMeasureForm(), sender);
        }
        #endregion

        protected virtual Form GetSettingsForm()
        {
            return new ChildForms.NormaSettingsForm();
        }

        protected virtual Form GetDataBaseForm()
        {
            return new ChildForms.BlankForm();
        }

        protected virtual Form GetMeasureForm()
        {
            return new ChildForms.BlankForm();
        }


        protected virtual void SetActiveForm(Form next_form, object btnSender)
        {
            nextButton = (Button)btnSender;
            nextForm = next_form;
            if (currentForm != null)
            {
                currentForm.Close();
            }else
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
            this.childFormPanel.Controls.Add(currentForm);
            this.childFormPanel.Tag = currentForm;
            currentForm.BringToFront();
            currentForm.FormClosed += CurrentForm_FormClosed;
            lblTitle.Text = currentForm.Text.ToUpper();
            currentForm.Show();
        }

        private void CurrentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (nextForm != null) SetNextForm();
        }

        private void UIMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentForm != null)
            {
              
            }
        }
    }

    delegate void SetColorDelegate(int colorIdx);
}
