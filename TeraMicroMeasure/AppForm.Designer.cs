namespace TeraMicroMeasure
{
    partial class AppForm
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
            this.clientTitle = new System.Windows.Forms.Label();
            this.titleLabel_2 = new System.Windows.Forms.Label();
            this.titleLabel_1 = new System.Windows.Forms.Label();
            this.connectToServerButton = new System.Windows.Forms.Button();
            this.connectButPanel = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.connectButPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.connectButPanel);
            this.panelMenu.Controls.SetChildIndex(this.btnSettings, 0);
            this.panelMenu.Controls.SetChildIndex(this.btnDataBase, 0);
            this.panelMenu.Controls.SetChildIndex(this.btnMeasure, 0);
            this.panelMenu.Controls.SetChildIndex(this.panelHeader, 0);
            this.panelMenu.Controls.SetChildIndex(this.connectButPanel, 0);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.titleLabel_1);
            this.panelHeader.Controls.Add(this.titleLabel_2);
            this.panelHeader.Controls.Add(this.clientTitle);
            this.panelHeader.TabIndex = 1;
            // 
            // btnMeasure
            // 
            this.btnMeasure.FlatAppearance.BorderSize = 0;
            this.btnMeasure.TabIndex = 2;
            // 
            // btnSettings
            // 
            this.btnSettings.FlatAppearance.BorderSize = 0;
            // 
            // btnDataBase
            // 
            this.btnDataBase.FlatAppearance.BorderSize = 0;
            // 
            // clientTitle
            // 
            this.clientTitle.AutoSize = true;
            this.clientTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientTitle.ForeColor = System.Drawing.Color.White;
            this.clientTitle.Location = new System.Drawing.Point(12, 3);
            this.clientTitle.Name = "clientTitle";
            this.clientTitle.Size = new System.Drawing.Size(108, 31);
            this.clientTitle.TabIndex = 0;
            this.clientTitle.Text = "Сервер";
            this.clientTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleLabel_2
            // 
            this.titleLabel_2.AutoSize = true;
            this.titleLabel_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleLabel_2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.titleLabel_2.Location = new System.Drawing.Point(15, 57);
            this.titleLabel_2.Name = "titleLabel_2";
            this.titleLabel_2.Size = new System.Drawing.Size(181, 15);
            this.titleLabel_2.TabIndex = 1;
            this.titleLabel_2.Text = "IP 192.168.100.232 Порт: 4999";
            // 
            // titleLabel_1
            // 
            this.titleLabel_1.AutoSize = true;
            this.titleLabel_1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.titleLabel_1.Location = new System.Drawing.Point(15, 40);
            this.titleLabel_1.Name = "titleLabel_1";
            this.titleLabel_1.Size = new System.Drawing.Size(55, 13);
            this.titleLabel_1.TabIndex = 2;
            this.titleLabel_1.Text = "Клиентов";
            // 
            // connectToServerButton
            // 
            this.connectToServerButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.connectToServerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectToServerButton.FlatAppearance.BorderSize = 0;
            this.connectToServerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connectToServerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectToServerButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.connectToServerButton.Image = global::TeraMicroMeasure.Properties.Resources.disconnect_white;
            this.connectToServerButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.connectToServerButton.Location = new System.Drawing.Point(0, 0);
            this.connectToServerButton.Name = "connectToServerButton";
            this.connectToServerButton.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.connectToServerButton.Size = new System.Drawing.Size(240, 60);
            this.connectToServerButton.TabIndex = 4;
            this.connectToServerButton.Text = "  Подключен";
            this.connectToServerButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.connectToServerButton.UseVisualStyleBackColor = true;
            // 
            // connectButPanel
            // 
            this.connectButPanel.Controls.Add(this.connectToServerButton);
            this.connectButPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.connectButPanel.Location = new System.Drawing.Point(0, 824);
            this.connectButPanel.Name = "connectButPanel";
            this.connectButPanel.Size = new System.Drawing.Size(240, 60);
            this.connectButPanel.TabIndex = 5;
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 884);
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(1170, 900);
            this.Name = "AppForm";
            this.Text = "AppForm";
            this.panelMenu.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.connectButPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label clientTitle;
        private System.Windows.Forms.Label titleLabel_1;
        private System.Windows.Forms.Label titleLabel_2;
        private System.Windows.Forms.Button connectToServerButton;
        private System.Windows.Forms.Panel connectButPanel;
    }
}