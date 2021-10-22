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
using System.Diagnostics;


namespace TeraMicroMeasure.Forms
{
    public partial class DevicesForm : Form
    {
        public event EventHandler DevicePanelClick;
        private DevicesDispatcher dispatcher;
        public DevicesForm(DevicesDispatcher _dispatcher)
        {
            InitializeComponent();
            examplePanel.Dispose();
            dispatcher = _dispatcher;
            DrawConnectedDevices();
            dispatcher.OnDeviceFound += OnDeviceFound_EventHandler;
        }

        private DeviceFormPanel FindDevicePanelByName(string name)
        {
            DeviceFormPanel panel = null;
            foreach(Control c in devicePanelsContainer.Controls)
            {
                if (c.Name == name)
                {
                    panel = c as DeviceFormPanel;
                    break;
                }
            }
            return panel;
        }

        private void DrawConnectedDevices()
        {
            List<string> panels = new List<string>();
            foreach (KeyValuePair<string, DeviceBase> item in dispatcher.DeviceList)
            {
                string panelName = $"Device_{item.Value.TypeId}_{item.Value.SerialYear}_{item.Value.SerialNumber}_panel";
                DeviceFormPanel panel = FindDevicePanelByName(panelName);
                if (panel == null)
                {
                    panel = new DeviceFormPanel(item.Value);
                    panel.Name = panelName;
                    panel.Disposed += Panel_Disposed;
                    panel.Click += Panel_Click;
                    devicePanelsContainer.Controls.Add(panel);
                    
                }
            }
            RefreshPanelsPosition();
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            DeviceFormPanel panel = sender as DeviceFormPanel;

            DevicePanelClick?.Invoke(panel.AssignedDevice, e);
        }

        private void Panel_Disposed(object sender, EventArgs e)
        {
            RefreshPanelsPosition();
        }

        private void RefreshPanelsPosition()
        {
            if (devicePanelsContainer.Controls.Count == 0) return;
            int paddingY = 25;
            int colsNum = devicePanelsContainer.Width / (DeviceFormPanel.DefaultWidth);
            int paddingX = (devicePanelsContainer.Width - colsNum * DeviceFormPanel.DefaultWidth) / (colsNum + 1);
            devicePanelsContainer.Size = new Size(this.Width, ((devicePanelsContainer.Controls.Count / colsNum) + 1) * (DeviceFormPanel.DefaultHeight + paddingY) + paddingY);
            for (int i = 0; i < devicePanelsContainer.Controls.Count; i++)
            {
                int row = i / colsNum;
                int col = i % colsNum;
                Panel p = devicePanelsContainer.Controls[i] as Panel;
                p.Location = new Point(col * (DeviceFormPanel.DefaultWidth + paddingX) + paddingX, row * (DeviceFormPanel.DefaultHeight + paddingY) + paddingY);
            }

        }

        private void devicePanelsContainer_Resize(object sender, EventArgs e)
        {
            // this.PointToScreen(new Point(0, 0));
            RefreshPanelsPosition();
        }

        private void DevicesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dispatcher.OnDeviceFound -= OnDeviceFound_EventHandler;
        }

        private void OnDeviceFound_EventHandler(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnDeviceFound_EventHandler), new object[] { sender, e });
            }
            else
            {
                DrawConnectedDevices();
            }

        }


    }
    class DeviceFormPanel : Panel
    {
        public static int DefaultWidth = 300;
        public static int DefaultHeight = 180;
        //private Panel devicePanel;
        private Label deviceStatus;
        private Label deviceSerialLabel;
        private Label deviceNameLabel;
        private Label versionLabel;
        public DeviceBase AssignedDevice => device;
        private DeviceBase device;
        public DeviceFormPanel(DeviceBase _device) : base()
        {
            device = _device;
            InitPanel();
            SetLabelsText();
            this.SuspendLayout();
            device.OnWorkStatusChanged += WorkStatusChanged_Handler;
            foreach(Control c in this.Controls)
            {
                c.MouseEnter += C_MouseEnter;
            }
            deviceNameLabel.MouseLeave += DeviceNameLabel_MouseLeave;
            deviceStatus.MouseLeave += DeviceNameLabel_MouseLeave;
        }

        private void DeviceNameLabel_MouseLeave(object sender, EventArgs e)
        {
            MouseMove_Handler();
        }

        private void C_MouseEnter(object sender, EventArgs e)
        {
            MouseEnter_Handler();
        }

        private void InitPanel()
        {
           // this.devicePanel = new System.Windows.Forms.Panel();
            this.deviceNameLabel = new System.Windows.Forms.Label();
            this.deviceSerialLabel = new System.Windows.Forms.Label();
            this.deviceStatus = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            //this.devicePanel.SuspendLayout();
            this.SuspendLayout();


            this.Controls.Add(this.deviceSerialLabel);
            this.Controls.Add(this.deviceStatus);
            this.Controls.Add(this.deviceNameLabel);
            this.Location = new System.Drawing.Point(34, 47);
            //this.Name = "devicePanel";

            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(49)))), ((int)(((byte)(117)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.deviceNameLabel);
            this.Controls.Add(this.deviceSerialLabel);
            this.Controls.Add(this.deviceStatus);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Location = new System.Drawing.Point(12, 64);
            this.Size = new System.Drawing.Size(DefaultWidth, DefaultHeight);
            this.TabIndex = 0;
            this.MouseEnter += examplePanel_MouseHover;
            this.MouseLeave += examplePanel_MouseLeave;

            // 
            // deviceNameLabel
            // 
            this.deviceNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.deviceNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deviceNameLabel.Location = new System.Drawing.Point(0, 0);
            this.deviceNameLabel.Name = "deviceNameLabel";
            this.deviceNameLabel.Size = new System.Drawing.Size(296, 55);
            this.deviceNameLabel.TabIndex = 3;
            this.deviceNameLabel.Text = "Тераомметр ТОмМ-01";
            this.deviceNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deviceSerialLabel
            // 
            this.deviceSerialLabel.AutoSize = true;
            this.deviceSerialLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deviceSerialLabel.Location = new System.Drawing.Point(22, 64);
            this.deviceSerialLabel.Name = "deviceSerialLabel";
            this.deviceSerialLabel.Size = new System.Drawing.Size(187, 18);
            this.deviceSerialLabel.TabIndex = 1;
            this.deviceSerialLabel.Text = "Серийный номер: 2021-05";
            this.deviceSerialLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // deviceStatus
            // 
            this.deviceStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(200)))));
            this.deviceStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.deviceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deviceStatus.ForeColor = System.Drawing.SystemColors.Control;
            this.deviceStatus.Location = new System.Drawing.Point(0, 139);
            this.deviceStatus.Name = "deviceStatus";
            this.deviceStatus.Size = new System.Drawing.Size(296, 39);
            this.deviceStatus.TabIndex = 2;
            this.deviceStatus.Text = "Статус";
            this.deviceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //

            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(22, 102);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(97, 13);
            this.versionLabel.TabIndex = 4;
            this.versionLabel.Text = "Версия прошивки";

            this.PerformLayout();
           
        }

        private void SetLabelsText()
        {
            //DeviceNameLabel
            this.deviceNameLabel.Text = device.TypeNameFull;
            this.deviceSerialLabel.Text = $"Серийный номер: {device.Serial}";
            this.versionLabel.Text = $"Версия {device.ModelVersion}";
            this.deviceStatus.Text = device.WorkStatusText;
            //
        }

        private void WorkStatusChanged_Handler(object sender, EventArgs e)
        {

            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(WorkStatusChanged_Handler), new object[] { sender, e });
            }
            else
            {
                DeviceBase d = sender as DeviceBase;
                DeviceWorkStatusEventArgs a = e as DeviceWorkStatusEventArgs;
                // AddOrUpdateDeviceOnToolStrip(d);
                if (d.WorkStatus == DeviceWorkStatus.DISCONNECTED)
                {
                    this.Dispose();
                }
                else
                {
                    this.deviceStatus.Text = device.WorkStatusText;
                }

            }
        }

        private void examplePanel_MouseHover(object sender, EventArgs e)
        {
            MouseEnter_Handler();
        }

        private void examplePanel_MouseLeave(object sender, EventArgs e)
        {
            MouseMove_Handler();
        }

        private void MouseEnter_Handler()
        {
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(49)))), ((int)(((byte)(144)))));
        }

        private void MouseMove_Handler()
        {
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(49)))), ((int)(((byte)(117)))));
        }


    }
}
