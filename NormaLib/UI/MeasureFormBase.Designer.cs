namespace NormaLib.UI
{
    partial class MeasureFormBase
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
            this.measureResultPanel = new System.Windows.Forms.Panel();
            this.normaLabel = new System.Windows.Forms.Label();
            this.measureTimerLabel = new System.Windows.Forms.Label();
            this.resultFieldLabel = new System.Windows.Forms.Label();
            this.labelPointNumber = new System.Windows.Forms.Label();
            this.measureControlPanel = new System.Windows.Forms.Panel();
            this.panelMeasurePointControl = new System.Windows.Forms.Panel();
            this.buttonNextElement = new System.Windows.Forms.Button();
            this.buttonNextPoint = new System.Windows.Forms.Button();
            this.startMeasureButton = new System.Windows.Forms.Button();
            this.buttonPrevPoint = new System.Windows.Forms.Button();
            this.buttonPrevElement = new System.Windows.Forms.Button();
            this.measureResultPanel.SuspendLayout();
            this.measureControlPanel.SuspendLayout();
            this.panelMeasurePointControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // measureResultPanel
            // 
            this.measureResultPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(104)))), ((int)(((byte)(169)))));
            this.measureResultPanel.Controls.Add(this.normaLabel);
            this.measureResultPanel.Controls.Add(this.measureTimerLabel);
            this.measureResultPanel.Controls.Add(this.resultFieldLabel);
            this.measureResultPanel.Controls.Add(this.labelPointNumber);
            this.measureResultPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.measureResultPanel.ForeColor = System.Drawing.Color.Gainsboro;
            this.measureResultPanel.Location = new System.Drawing.Point(0, 0);
            this.measureResultPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.measureResultPanel.Name = "measureResultPanel";
            this.measureResultPanel.Size = new System.Drawing.Size(1065, 150);
            this.measureResultPanel.TabIndex = 33;
            // 
            // normaLabel
            // 
            this.normaLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.normaLabel.Location = new System.Drawing.Point(12, 5);
            this.normaLabel.Name = "normaLabel";
            this.normaLabel.Size = new System.Drawing.Size(879, 32);
            this.normaLabel.TabIndex = 22;
            this.normaLabel.Text = "норма: 10 МОм";
            this.normaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // measureTimerLabel
            // 
            this.measureTimerLabel.AutoSize = true;
            this.measureTimerLabel.Location = new System.Drawing.Point(201, 118);
            this.measureTimerLabel.Name = "measureTimerLabel";
            this.measureTimerLabel.Size = new System.Drawing.Size(34, 13);
            this.measureTimerLabel.TabIndex = 21;
            this.measureTimerLabel.Text = "00:00";
            // 
            // resultFieldLabel
            // 
            this.resultFieldLabel.BackColor = System.Drawing.Color.Transparent;
            this.resultFieldLabel.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultFieldLabel.Location = new System.Drawing.Point(12, 45);
            this.resultFieldLabel.Name = "resultFieldLabel";
            this.resultFieldLabel.Size = new System.Drawing.Size(572, 46);
            this.resultFieldLabel.TabIndex = 1;
            this.resultFieldLabel.Text = "106.56 ТОм⋅м";
            this.resultFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPointNumber
            // 
            this.labelPointNumber.AutoSize = true;
            this.labelPointNumber.Location = new System.Drawing.Point(12, 118);
            this.labelPointNumber.Name = "labelPointNumber";
            this.labelPointNumber.Size = new System.Drawing.Size(13, 13);
            this.labelPointNumber.TabIndex = 20;
            this.labelPointNumber.Text = "0";
            // 
            // measureControlPanel
            // 
            this.measureControlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(104)))), ((int)(((byte)(169)))));
            this.measureControlPanel.Controls.Add(this.panelMeasurePointControl);
            this.measureControlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.measureControlPanel.ForeColor = System.Drawing.Color.Gainsboro;
            this.measureControlPanel.Location = new System.Drawing.Point(0, 150);
            this.measureControlPanel.Name = "measureControlPanel";
            this.measureControlPanel.Size = new System.Drawing.Size(1065, 50);
            this.measureControlPanel.TabIndex = 44;
            // 
            // panelMeasurePointControl
            // 
            this.panelMeasurePointControl.Controls.Add(this.buttonNextElement);
            this.panelMeasurePointControl.Controls.Add(this.buttonNextPoint);
            this.panelMeasurePointControl.Controls.Add(this.startMeasureButton);
            this.panelMeasurePointControl.Controls.Add(this.buttonPrevPoint);
            this.panelMeasurePointControl.Controls.Add(this.buttonPrevElement);
            this.panelMeasurePointControl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelMeasurePointControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelMeasurePointControl.Location = new System.Drawing.Point(718, 0);
            this.panelMeasurePointControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMeasurePointControl.Name = "panelMeasurePointControl";
            this.panelMeasurePointControl.Size = new System.Drawing.Size(347, 50);
            this.panelMeasurePointControl.TabIndex = 41;
            // 
            // buttonNextElement
            // 
            this.buttonNextElement.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonNextElement.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonNextElement.FlatAppearance.BorderSize = 0;
            this.buttonNextElement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNextElement.Location = new System.Drawing.Point(302, 0);
            this.buttonNextElement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNextElement.Name = "buttonNextElement";
            this.buttonNextElement.Size = new System.Drawing.Size(44, 50);
            this.buttonNextElement.TabIndex = 39;
            this.buttonNextElement.Text = ">>";
            this.buttonNextElement.UseVisualStyleBackColor = false;
            this.buttonNextElement.Click += new System.EventHandler(this.buttonNextElement_Click);
            // 
            // buttonNextPoint
            // 
            this.buttonNextPoint.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonNextPoint.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonNextPoint.FlatAppearance.BorderSize = 0;
            this.buttonNextPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNextPoint.Location = new System.Drawing.Point(262, 0);
            this.buttonNextPoint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNextPoint.Name = "buttonNextPoint";
            this.buttonNextPoint.Size = new System.Drawing.Size(40, 50);
            this.buttonNextPoint.TabIndex = 38;
            this.buttonNextPoint.Text = ">";
            this.buttonNextPoint.UseVisualStyleBackColor = false;
            this.buttonNextPoint.Click += new System.EventHandler(this.buttonNextPoint_Click);
            // 
            // startMeasureButton
            // 
            this.startMeasureButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(206)))), ((int)(((byte)(31)))));
            this.startMeasureButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startMeasureButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.startMeasureButton.FlatAppearance.BorderSize = 0;
            this.startMeasureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startMeasureButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.startMeasureButton.Location = new System.Drawing.Point(84, 0);
            this.startMeasureButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startMeasureButton.Name = "startMeasureButton";
            this.startMeasureButton.Size = new System.Drawing.Size(178, 50);
            this.startMeasureButton.TabIndex = 11;
            this.startMeasureButton.Text = "Пуск измерения";
            this.startMeasureButton.UseVisualStyleBackColor = false;
            this.startMeasureButton.Click += new System.EventHandler(this.startMeasureButton_Click);
            // 
            // buttonPrevPoint
            // 
            this.buttonPrevPoint.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonPrevPoint.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPrevPoint.FlatAppearance.BorderSize = 0;
            this.buttonPrevPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevPoint.Location = new System.Drawing.Point(44, 0);
            this.buttonPrevPoint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPrevPoint.Name = "buttonPrevPoint";
            this.buttonPrevPoint.Size = new System.Drawing.Size(40, 50);
            this.buttonPrevPoint.TabIndex = 37;
            this.buttonPrevPoint.Text = "<";
            this.buttonPrevPoint.UseVisualStyleBackColor = false;
            this.buttonPrevPoint.Click += new System.EventHandler(this.buttonPrevPoint_Click);
            // 
            // buttonPrevElement
            // 
            this.buttonPrevElement.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonPrevElement.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPrevElement.FlatAppearance.BorderSize = 0;
            this.buttonPrevElement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevElement.Location = new System.Drawing.Point(0, 0);
            this.buttonPrevElement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPrevElement.Name = "buttonPrevElement";
            this.buttonPrevElement.Size = new System.Drawing.Size(44, 50);
            this.buttonPrevElement.TabIndex = 40;
            this.buttonPrevElement.Text = "<<";
            this.buttonPrevElement.UseVisualStyleBackColor = false;
            this.buttonPrevElement.Click += new System.EventHandler(this.buttonPrevElement_Click);
            // 
            // MeasureFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 716);
            this.Controls.Add(this.measureControlPanel);
            this.Controls.Add(this.measureResultPanel);
            this.Name = "MeasureFormBase";
            this.Text = "MeasureFormBase";
            this.measureResultPanel.ResumeLayout(false);
            this.measureResultPanel.PerformLayout();
            this.measureControlPanel.ResumeLayout(false);
            this.panelMeasurePointControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel measureResultPanel;
        private System.Windows.Forms.Label normaLabel;
        private System.Windows.Forms.Label measureTimerLabel;
        private System.Windows.Forms.Label resultFieldLabel;
        private System.Windows.Forms.Label labelPointNumber;
        private System.Windows.Forms.Panel measureControlPanel;
        private System.Windows.Forms.Panel panelMeasurePointControl;
        private System.Windows.Forms.Button buttonNextElement;
        private System.Windows.Forms.Button buttonNextPoint;
        private System.Windows.Forms.Button startMeasureButton;
        private System.Windows.Forms.Button buttonPrevPoint;
        private System.Windows.Forms.Button buttonPrevElement;
    }
}