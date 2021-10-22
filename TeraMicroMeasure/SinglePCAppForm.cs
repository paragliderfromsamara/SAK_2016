using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaLib.UI;
using TeraMicroMeasure;
using NormaLib.DBControl;
using NormaLib.DBControl.DBNormaMeasure;
using System.Threading;
using TeraMicroMeasure.Forms;
using NormaLib.SessionControl;
using NormaLib.Devices;


namespace NormaMeasure
{
    public partial class SinglePCAppForm : UIMainForm
    {
        LoadAppForm LoadAppForm;
        public static DevicesDispatcher DeviceDispatcher;
        public static List<DeviceControlForm> DeviceControlForms = new List<DeviceControlForm>();
        public SinglePCAppForm()
        {
            //clientTitle.Text = "NormaMeasure";
            //titleLabel_1.Text = "";
            //titleLabel_2.Text = "";
            LoadAppForm = new LoadAppForm();
            LoadAppForm.Show();
            LoadAppForm.Refresh();
            bool flag = InitDataBase();
            LoadAppForm.Close();
            if (!flag)
            {
                MessageBox.Show("Невозможно запустить приложение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }else
            {
                InitSessionForm();
                InitDeviceFinder();
            }
        }

        private void InitDeviceFinder()
        {
            DeviceDispatcher = new DevicesDispatcher(new DeviceCommandProtocol(), OnDeviceFound_EventHandler);
        }

        private void InitSessionForm()
        {
            SessionControlForm scf = new SessionControlForm();
            scf.IsServerApp = true;
            scf.Shown += (o, s) => { panelMenu.Visible = false; };
            scf.OnUserSignedIn += (o, s) =>
            {
                panelMenu.VisibleChanged += (s1, o1) =>
                {
                    if (!panelMenu.Visible) return;
                    SetActiveForm(GetMeasureForm(), btnMeasure);
                    userNameLabel.Text = SessionControl.CurrentUser.FullNameShort;
                    roleTitleLabel.Text = SessionControl.CurrentUserRole.UserRoleName;
                };
                panelMenu.Visible = SessionControl.SignedIn;

            };
            SetActiveForm(scf);
        }

        protected override void InitializeDesign()
        {
            base.InitializeDesign();
            InitializeComponent();
            ReorederMenuItems();
            FormClosing += ThisForm_FormClosing;
        }

        private void ThisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MeasureIsActive())
            {
                StopDeviceFinder();
            }
            else
            {
                MessageBox.Show("Невозможно перейти в другое меню или закрыть приложение при запущенном измерении!", "Измерение не завершено!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Cancel = true;
            }
        }

        private bool MeasureIsActive()
        {
            return false;
            //MeasureForm mf = FindOpened_MeasureForm();
            //if (mf == null) return false;
            //return mf.MeasureState.MeasureStartFlag;
        }

        private void StopDeviceFinder()
        {
            if (DeviceDispatcher != null) DeviceDispatcher.Dispose();
        }

        private void ReorederMenuItems()
        {
            this.panelMenu.Controls.Clear();
            this.panelMenu.Controls.Add(this.devicesListPanel);
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Controls.Add(this.btnDataBase);
            this.panelMenu.Controls.Add(this.btnMeasure);
            this.panelMenu.Controls.Add(this.panelHeader);
            //this.panelMenu.Controls.Add(this.connectButPanel);
            this.panelMenu.Controls.Add(this.sessionPanel);
            //connectButPanel.Visible = !IsServerApp;
        }

        private bool InitDataBase()
        {
            retry:
            try
            {
                DBNormaMeasureTablesMigration dbnm = new DBNormaMeasureTablesMigration();
                dbnm.OnStepChanged += (o, s) =>
                {
                    LoadAppForm.SetTaskLabelsValue(dbnm.CurrentStep, dbnm.CurrentSubStep);
                    Thread.Sleep(750);
                };
                dbnm.InitDataBase();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Не удалось подключиться к Базе данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DataBaseSettingsForm dbfm = new DataBaseSettingsForm();
                dbfm.StartPosition = FormStartPosition.CenterScreen;
                dbfm.cancelButton.Visible = true;
                DialogResult dr = dbfm.ShowDialog();
                if (dr == DialogResult.Retry) goto retry;
                else return false;
            }
        }

        private void OnDeviceFound_EventHandler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnDeviceFound_EventHandler), new object[] { sender, e });
            }
            else
            {
                DeviceBase d = sender as DeviceBase;
                AddButtonToDevicesControlPanel(d);
            }

        }

        private void AddButtonToDevicesControlPanel(DeviceBase d)
        {
            
            string butName = $"device_{d.TypeId}_{d.SerialYear}_{d.SerialNumber}_Button";
            bool f = deviceButtonsContainerPanel.Controls.ContainsKey(butName);
            if (!f)
            {
                Button b = BuildDeviceButton();
                b.Name = butName;
                b.Text = $"{d.TypeNameFull}\nЗав.№ {d.Serial}";
                b.Click += (o, s) => { OpenDeviceControlForm(d); };
                b.TabIndex = deviceButtonsContainerPanel.Controls.Count + 1;
                d.OnDisconnected += (o, s) => { RemoveButtonFromButtonsContainer(b, new EventArgs()); };
                deviceButtonsContainerPanel.Controls.Add(b);
            }
            
        }

        private void RemoveButtonFromButtonsContainer(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(RemoveButtonFromButtonsContainer), new object[] { sender, e });
            }
            else
            {
                Button b = sender as Button;
                deviceButtonsContainerPanel.Controls.Remove(b);
            }
        }

        private void OpenDeviceControlForm(DeviceBase device)
        {
            string formName = $"Device_{device.TypeId}_{device.Serial}_{device.SerialYear}_Form";

            DeviceControlForm f = null;

            foreach (DeviceControlForm c in DeviceControlForms)
            {
                if (c.Name == formName)
                {
                    f = c as DeviceControlForm;
                    break;
                }
            }
            if (f == null)
            {
                f = new DeviceControlForm(device);
                f.Name = formName;
                DeviceControlForms.Add(f);
                f.FormClosed += (o, s) => { DeviceControlForms.Remove(f); };
                f.StartPosition = FormStartPosition.CenterScreen;
                f.Show();

            }
            else
            {
                if (f.WindowState == FormWindowState.Minimized) f.WindowState = FormWindowState.Normal;

            }
        }

        private Button BuildDeviceButton()
        {
            Button b = new Button();
            b.Dock = System.Windows.Forms.DockStyle.Top;
            b.FlatAppearance.BorderSize = 0;
            b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            b.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            b.ForeColor = System.Drawing.Color.Gainsboro;
            //b.Image = ((System.Drawing.Image)(resources.GetObject("btnDevices.Image")));
            //b.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            b.Location = new System.Drawing.Point(0, 260);
            b.Name = "btnDevices";
            b.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            b.Size = new System.Drawing.Size(240, 60);
            b.TabIndex = 1;
            b.Text = "Прибор";
            b.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            b.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            b.UseVisualStyleBackColor = true;
            return b;
        }

        private void signoutButton_Click(object sender, EventArgs e)
        {
            currentForm.FormClosed += (s, a) =>
            {
                SessionControl.SignOut();
            };
            InitSessionForm();
        }

    }
}
