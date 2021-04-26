namespace NormaLib.UI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataPanel = new System.Windows.Forms.Panel();
            this.emptyEntitiesList = new System.Windows.Forms.Label();
            this.dgEntities = new System.Windows.Forms.DataGridView();
            this.contextTableMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editEntityTooStripButton = new System.Windows.Forms.ToolStripMenuItem();
            this.removeEntityToolStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewRecordFormInit = new System.Windows.Forms.Button();
            this.loadStatusLabel = new System.Windows.Forms.Label();
            this.loadStatusLabelTimer = new System.Windows.Forms.Timer(this.components);
            this.entitiesDataSet = new System.Data.DataSet();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.dataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEntities)).BeginInit();
            this.contextTableMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entitiesDataSet)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataPanel
            // 
            this.dataPanel.Controls.Add(this.dgEntities);
            this.dataPanel.Controls.Add(this.emptyEntitiesList);
            this.dataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataPanel.Location = new System.Drawing.Point(0, 110);
            this.dataPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(2042, 1279);
            this.dataPanel.TabIndex = 0;
            // 
            // emptyEntitiesList
            // 
            this.emptyEntitiesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emptyEntitiesList.Location = new System.Drawing.Point(0, 0);
            this.emptyEntitiesList.Name = "emptyEntitiesList";
            this.emptyEntitiesList.Size = new System.Drawing.Size(2042, 1279);
            this.emptyEntitiesList.TabIndex = 1;
            this.emptyEntitiesList.Text = "Список пуст";
            this.emptyEntitiesList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.dgEntities.BackgroundColor = System.Drawing.Color.White;
            this.dgEntities.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgEntities.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgEntities.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(65)))), ((int)(((byte)(109)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.NullValue = "-";
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgEntities.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgEntities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEntities.ContextMenuStrip = this.contextTableMenu;
            this.dgEntities.EnableHeadersVisualStyles = false;
            this.dgEntities.Location = new System.Drawing.Point(13, 0);
            this.dgEntities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgEntities.Name = "dgEntities";
            this.dgEntities.ReadOnly = true;
            this.dgEntities.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgEntities.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.NullValue = "-";
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(108)))), ((int)(((byte)(205)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgEntities.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgEntities.RowTemplate.Height = 30;
            this.dgEntities.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgEntities.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEntities.Size = new System.Drawing.Size(2016, 1259);
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
            this.btnNewRecordFormInit.Location = new System.Drawing.Point(13, 33);
            this.btnNewRecordFormInit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNewRecordFormInit.Name = "btnNewRecordFormInit";
            this.btnNewRecordFormInit.Size = new System.Drawing.Size(262, 46);
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
            this.loadStatusLabel.Location = new System.Drawing.Point(25, 42);
            this.loadStatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.btnNewRecordFormInit);
            this.headerPanel.Controls.Add(this.loadStatusLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(2042, 110);
            this.headerPanel.TabIndex = 3;
            // 
            // DBTableContolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(2042, 1389);
            this.Controls.Add(this.dataPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DBTableContolForm";
            this.Text = "DBTableContolForm";
            this.Load += new System.EventHandler(this.DBTableContolForm_Load);
            this.dataPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgEntities)).EndInit();
            this.contextTableMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entitiesDataSet)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.DataGridViewCellStyle dataGridViewColumnHeadersStyle;
        protected System.Windows.Forms.Panel dataPanel;
        public System.Windows.Forms.DataGridView dgEntities;
        private System.Windows.Forms.Label loadStatusLabel;
        private System.Windows.Forms.Timer loadStatusLabelTimer;
        protected System.Windows.Forms.ToolStripMenuItem editEntityTooStripButton;
        protected System.Windows.Forms.ToolStripMenuItem removeEntityToolStripItem;
        protected System.Windows.Forms.ContextMenuStrip contextTableMenu;
        protected System.Windows.Forms.Button btnNewRecordFormInit;
        protected System.Data.DataSet entitiesDataSet;
        protected System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label emptyEntitiesList;
    }
}