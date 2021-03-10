namespace NormaMeasure.UI
{
    partial class DBTableContolForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataPanel = new System.Windows.Forms.Panel();
            this.dgEntities = new System.Windows.Forms.DataGridView();
            this.contextTableMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editEntityTooStripButton = new System.Windows.Forms.ToolStripMenuItem();
            this.removeEntityToolStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewRecordFormInit = new System.Windows.Forms.Button();
            this.loadStatusLabel = new System.Windows.Forms.Label();
            this.loadStatusLabelTimer = new System.Windows.Forms.Timer(this.components);
            this.entitiesDataSet = new System.Data.DataSet();
            this.dataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEntities)).BeginInit();
            this.contextTableMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entitiesDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataPanel
            // 
            this.dataPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPanel.Controls.Add(this.dgEntities);
            this.dataPanel.Location = new System.Drawing.Point(0, 63);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(1360, 841);
            this.dataPanel.TabIndex = 0;
            // 
            // dgEntities
            // 
            this.dgEntities.AllowUserToAddRows = false;
            this.dgEntities.AllowUserToDeleteRows = false;
            this.dgEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgEntities.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgEntities.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgEntities.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(54)))), ((int)(((byte)(87)))));
            this.dgEntities.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgEntities.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgEntities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEntities.ContextMenuStrip = this.contextTableMenu;
            this.dgEntities.Location = new System.Drawing.Point(12, 15);
            this.dgEntities.Name = "dgEntities";
            this.dgEntities.ReadOnly = true;
            this.dgEntities.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.NullValue = "-";
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgEntities.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgEntities.RowTemplate.Height = 30;
            this.dgEntities.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgEntities.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEntities.Size = new System.Drawing.Size(1337, 813);
            this.dgEntities.TabIndex = 0;
            this.dgEntities.TabStop = false;
            // 
            // contextTableMenu
            // 
            this.contextTableMenu.AccessibleRole = System.Windows.Forms.AccessibleRole.Row;
            this.contextTableMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.contextTableMenu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contextTableMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editEntityTooStripButton,
            this.removeEntityToolStripItem});
            this.contextTableMenu.Name = "contextTableMenu";
            this.contextTableMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextTableMenu.Size = new System.Drawing.Size(135, 48);
            this.contextTableMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextTableMenu_Opening);
            // 
            // editEntityTooStripButton
            // 
            this.editEntityTooStripButton.Name = "editEntityTooStripButton";
            this.editEntityTooStripButton.Size = new System.Drawing.Size(134, 22);
            this.editEntityTooStripButton.Text = "Изменить";
            this.editEntityTooStripButton.Click += new System.EventHandler(this.editEntityTooStripButton_Click);
            // 
            // removeEntityToolStripItem
            // 
            this.removeEntityToolStripItem.Name = "removeEntityToolStripItem";
            this.removeEntityToolStripItem.Size = new System.Drawing.Size(134, 22);
            this.removeEntityToolStripItem.Text = "Удалить";
            this.removeEntityToolStripItem.Click += new System.EventHandler(this.removeEntityToolStripItem_Click);
            // 
            // btnNewRecordFormInit
            // 
            this.btnNewRecordFormInit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(245)))), ((int)(((byte)(5)))));
            this.btnNewRecordFormInit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnNewRecordFormInit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewRecordFormInit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewRecordFormInit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnNewRecordFormInit.Location = new System.Drawing.Point(12, 12);
            this.btnNewRecordFormInit.Name = "btnNewRecordFormInit";
            this.btnNewRecordFormInit.Size = new System.Drawing.Size(150, 45);
            this.btnNewRecordFormInit.TabIndex = 1;
            this.btnNewRecordFormInit.Text = "Новая запись";
            this.btnNewRecordFormInit.UseVisualStyleBackColor = false;
            this.btnNewRecordFormInit.Click += new System.EventHandler(this.btnNewRecordFormInit_Click);
            // 
            // loadStatusLabel
            // 
            this.loadStatusLabel.AutoSize = true;
            this.loadStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.loadStatusLabel.Location = new System.Drawing.Point(12, 23);
            this.loadStatusLabel.Name = "loadStatusLabel";
            this.loadStatusLabel.Size = new System.Drawing.Size(223, 37);
            this.loadStatusLabel.TabIndex = 2;
            this.loadStatusLabel.Text = "Идёт загрузка";
            this.loadStatusLabel.Visible = false;
            // 
            // loadStatusLabelTimer
            // 
            this.loadStatusLabelTimer.Interval = 500;
            this.loadStatusLabelTimer.Tick += new System.EventHandler(this.loadStatusLabelTimer_Tick);
            // 
            // entitiesDataSet
            // 
            this.entitiesDataSet.DataSetName = "NewDataSet";
            // 
            // DBTableContolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1361, 903);
            this.Controls.Add(this.loadStatusLabel);
            this.Controls.Add(this.btnNewRecordFormInit);
            this.Controls.Add(this.dataPanel);
            this.Name = "DBTableContolForm";
            this.Text = "DBTableContolForm";
            this.Load += new System.EventHandler(this.DBTableContolForm_Load);
            this.dataPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgEntities)).EndInit();
            this.contextTableMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entitiesDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Panel dataPanel;
        public System.Windows.Forms.DataGridView dgEntities;
        private System.Windows.Forms.Label loadStatusLabel;
        private System.Windows.Forms.Timer loadStatusLabelTimer;
        private System.Windows.Forms.ToolStripMenuItem editEntityTooStripButton;
        private System.Windows.Forms.ToolStripMenuItem removeEntityToolStripItem;
        protected System.Windows.Forms.ContextMenuStrip contextTableMenu;
        protected System.Windows.Forms.Button btnNewRecordFormInit;
        protected System.Data.DataSet entitiesDataSet;
    }
}