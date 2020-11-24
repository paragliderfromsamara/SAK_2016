namespace NormaMeasure.SocketControl.TCPControlLib
{
    partial class TCPSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.localIPComboBox = new System.Windows.Forms.ComboBox();
            this.localPortInput = new System.Windows.Forms.NumericUpDown();
            this.localSettings = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.serverSettingsPanel = new System.Windows.Forms.GroupBox();
            this.remotePortInput = new System.Windows.Forms.NumericUpDown();
            this.remoteIPInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.localPortInput)).BeginInit();
            this.localSettings.SuspendLayout();
            this.serverSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.remotePortInput)).BeginInit();
            this.SuspendLayout();
            // 
            // localIPComboBox
            // 
            this.localIPComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.localIPComboBox.FormattingEnabled = true;
            this.localIPComboBox.Location = new System.Drawing.Point(22, 49);
            this.localIPComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.localIPComboBox.Name = "localIPComboBox";
            this.localIPComboBox.Size = new System.Drawing.Size(201, 24);
            this.localIPComboBox.TabIndex = 0;
            // 
            // localPortInput
            // 
            this.localPortInput.Location = new System.Drawing.Point(265, 49);
            this.localPortInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.localPortInput.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.localPortInput.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.localPortInput.Name = "localPortInput";
            this.localPortInput.Size = new System.Drawing.Size(119, 23);
            this.localPortInput.TabIndex = 1;
            this.localPortInput.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // localSettings
            // 
            this.localSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.localSettings.Controls.Add(this.label2);
            this.localSettings.Controls.Add(this.label1);
            this.localSettings.Controls.Add(this.localIPComboBox);
            this.localSettings.Controls.Add(this.localPortInput);
            this.localSettings.Location = new System.Drawing.Point(12, 13);
            this.localSettings.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.localSettings.Name = "localSettings";
            this.localSettings.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.localSettings.Size = new System.Drawing.Size(414, 95);
            this.localSettings.TabIndex = 2;
            this.localSettings.TabStop = false;
            this.localSettings.Text = "Локальные настройки";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Порт";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP Адрес";
            // 
            // serverSettingsPanel
            // 
            this.serverSettingsPanel.Controls.Add(this.remotePortInput);
            this.serverSettingsPanel.Controls.Add(this.remoteIPInput);
            this.serverSettingsPanel.Controls.Add(this.label3);
            this.serverSettingsPanel.Controls.Add(this.label4);
            this.serverSettingsPanel.Location = new System.Drawing.Point(12, 125);
            this.serverSettingsPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.serverSettingsPanel.Name = "serverSettingsPanel";
            this.serverSettingsPanel.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.serverSettingsPanel.Size = new System.Drawing.Size(414, 95);
            this.serverSettingsPanel.TabIndex = 3;
            this.serverSettingsPanel.TabStop = false;
            this.serverSettingsPanel.Text = "Настройки сервера";
            // 
            // remotePortInput
            // 
            this.remotePortInput.Location = new System.Drawing.Point(265, 48);
            this.remotePortInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.remotePortInput.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.remotePortInput.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.remotePortInput.Name = "remotePortInput";
            this.remotePortInput.Size = new System.Drawing.Size(119, 23);
            this.remotePortInput.TabIndex = 5;
            this.remotePortInput.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // remoteIPInput
            // 
            this.remoteIPInput.Location = new System.Drawing.Point(22, 48);
            this.remoteIPInput.Name = "remoteIPInput";
            this.remoteIPInput.Size = new System.Drawing.Size(201, 23);
            this.remoteIPInput.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Порт";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "IP Адрес";
            // 
            // saveButton
            // 
            this.saveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(12, 242);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(189, 37);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(237, 242);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(189, 37);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // TCPSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 306);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.serverSettingsPanel);
            this.Controls.Add(this.localSettings);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TCPSettingsForm";
            this.Text = "Настройки TCP";
            ((System.ComponentModel.ISupportInitialize)(this.localPortInput)).EndInit();
            this.localSettings.ResumeLayout(false);
            this.localSettings.PerformLayout();
            this.serverSettingsPanel.ResumeLayout(false);
            this.serverSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.remotePortInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox localIPComboBox;
        private System.Windows.Forms.NumericUpDown localPortInput;
        private System.Windows.Forms.GroupBox localSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox serverSettingsPanel;
        private System.Windows.Forms.NumericUpDown remotePortInput;
        private System.Windows.Forms.TextBox remoteIPInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
}