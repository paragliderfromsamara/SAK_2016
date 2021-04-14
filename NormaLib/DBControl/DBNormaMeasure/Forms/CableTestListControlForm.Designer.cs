namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    partial class CableTestListControlForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CableTestListControlForm));
            this.filterPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.dateToValueInput = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateFromInput = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cableMarkComboBox = new System.Windows.Forms.ComboBox();
            this.filterButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.entitiesDataSet)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.filterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataPanel
            // 
            this.dataPanel.Location = new System.Drawing.Point(0, 103);
            this.dataPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataPanel.Size = new System.Drawing.Size(1448, 1134);
            // 
            // btnNewRecordFormInit
            // 
            this.btnNewRecordFormInit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnNewRecordFormInit.Location = new System.Drawing.Point(18, 35);
            this.btnNewRecordFormInit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNewRecordFormInit.Size = new System.Drawing.Size(262, 46);
            // 
            // headerPanel
            // 
            this.headerPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.headerPanel.Size = new System.Drawing.Size(2042, 118);
            // 
            // filterPanel
            // 
            this.filterPanel.Controls.Add(this.refreshButton);
            this.filterPanel.Controls.Add(this.label3);
            this.filterPanel.Controls.Add(this.dateToValueInput);
            this.filterPanel.Controls.Add(this.label2);
            this.filterPanel.Controls.Add(this.dateFromInput);
            this.filterPanel.Controls.Add(this.label1);
            this.filterPanel.Controls.Add(this.cableMarkComboBox);
            this.filterPanel.Controls.Add(this.filterButton);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Location = new System.Drawing.Point(0, 0);
            this.filterPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(1448, 103);
            this.filterPanel.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(459, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 18);
            this.label3.TabIndex = 32;
            this.label3.Text = "Конечная дата";
            // 
            // dateToValueInput
            // 
            this.dateToValueInput.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateToValueInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateToValueInput.Location = new System.Drawing.Point(462, 49);
            this.dateToValueInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateToValueInput.Name = "dateToValueInput";
            this.dateToValueInput.Size = new System.Drawing.Size(195, 24);
            this.dateToValueInput.TabIndex = 31;
            this.dateToValueInput.ValueChanged += new System.EventHandler(this.dateToValueChanged_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(247, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 18);
            this.label2.TabIndex = 30;
            this.label2.Text = "Начальная дата";
            // 
            // dateFromInput
            // 
            this.dateFromInput.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateFromInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateFromInput.Location = new System.Drawing.Point(250, 49);
            this.dateFromInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateFromInput.Name = "dateFromInput";
            this.dateFromInput.Size = new System.Drawing.Size(195, 24);
            this.dateFromInput.TabIndex = 29;
            this.dateFromInput.ValueChanged += new System.EventHandler(this.dateFromInput_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 18);
            this.label1.TabIndex = 28;
            this.label1.Text = "Марка кабеля";
            // 
            // cableMarkComboBox
            // 
            this.cableMarkComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cableMarkComboBox.FormattingEnabled = true;
            this.cableMarkComboBox.Location = new System.Drawing.Point(12, 47);
            this.cableMarkComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cableMarkComboBox.Name = "cableMarkComboBox";
            this.cableMarkComboBox.Size = new System.Drawing.Size(216, 26);
            this.cableMarkComboBox.TabIndex = 27;
            // 
            // filterButton
            // 
            this.filterButton.BackColor = System.Drawing.Color.Navy;
            this.filterButton.FlatAppearance.BorderSize = 0;
            this.filterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filterButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.filterButton.Image = ((System.Drawing.Image)(resources.GetObject("filterButton.Image")));
            this.filterButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.filterButton.Location = new System.Drawing.Point(678, 33);
            this.filterButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.filterButton.Name = "filterButton";
            this.filterButton.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.filterButton.Size = new System.Drawing.Size(124, 40);
            this.filterButton.TabIndex = 26;
            this.filterButton.Text = " Поиск";
            this.filterButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.filterButton.UseVisualStyleBackColor = false;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.Color.LightSeaGreen;
            this.refreshButton.FlatAppearance.BorderSize = 0;
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.refreshButton.ForeColor = System.Drawing.Color.White;
            this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
            this.refreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.refreshButton.Location = new System.Drawing.Point(799, 33);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.refreshButton.Size = new System.Drawing.Size(145, 40);
            this.refreshButton.TabIndex = 33;
            this.refreshButton.Text = " Обновить";
            this.refreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // CableTestListControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1448, 1237);
            this.Controls.Add(this.filterPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CableTestListControlForm";
            this.Text = "CableTestListControlForm";
            this.Controls.SetChildIndex(this.filterPanel, 0);
            this.Controls.SetChildIndex(this.dataPanel, 0);
            this.Controls.SetChildIndex(this.headerPanel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.entitiesDataSet)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateToValueInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateFromInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cableMarkComboBox;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button refreshButton;
    }
}