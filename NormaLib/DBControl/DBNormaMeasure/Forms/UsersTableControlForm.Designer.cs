﻿namespace NormaLib.DBControl.DBNormaMeasure.Forms
{
    partial class UsersTableControlForm
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
            ((System.ComponentModel.ISupportInitialize)(this.entitiesDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNewRecordFormInit
            // 
            this.btnNewRecordFormInit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnNewRecordFormInit.Text = "Новый пользователь";
            // 
            // UsersTableControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1361, 903);
            this.Name = "UsersTableControlForm";
            this.Text = "UsersTableControlForm";
            this.Controls.SetChildIndex(this.dataPanel, 0);
            //this.Controls.SetChildIndex(this.btnNewRecordFormInit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.entitiesDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}