﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaMeasure.Devices.SAC;
using NormaMeasure.MeasureControl.SAC;

namespace NormaMeasure.MeasureControl.SACMeasureForms
{
    internal class SACMeasureResultField : Panel 
    {
        System.Drawing.Color FontColor = System.Drawing.Color.FromArgb(25, 49, 66);
        public SACMeasureResultField(Panel parentPanel) : base()
        {
            this.Parent = parentPanel;
            this.Location = new System.Drawing.Point(0, 0);
            this.Size = parentPanel.Size;
            InitElements();
        }

        public void RefreshFields(SACMeasurePoint point, SACMeasure measure)
        {
            ResultValue_Label.Text = $"{Math.Round(point.ConvertedResult, 2) } {point.parameterType.Measure}";
            ResultValue_Label.Location = new System.Drawing.Point((this.Width/2)-(ResultValue_Label.Width/2), ResultValue_Label.Location.Y);
            MeasureCycleCounter_Label.Text = $"Измерение {measure.CycleNumber+1}";
            MeasureParameterType_Label.Text = $"Параметр {point.parameterType.ParameterName}";
        }

        #region Инициализация элементов панели
        /// <summary>
        /// Инициализация элементов панели
        /// </summary>
        private void InitElements()
        {
            InitSelfStyle();
            InitResultLabel();
            InitCycleCounterLabel();
            InitMeasuredParameterTypeLabel();
        }

        private void InitMeasuredParameterTypeLabel()
        {
            MeasureParameterType_Label = new Label();
            MeasureParameterType_Label.Parent = this;
            MeasureParameterType_Label.Text = "";
            MeasureParameterType_Label.AutoSize = true;
            MeasureParameterType_Label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            MeasureParameterType_Label.Name = "MeasureParameterType_Label";
            MeasureParameterType_Label.Size = new System.Drawing.Size(170, 58);
            MeasureParameterType_Label.Location = new System.Drawing.Point(10, 10);
            MeasureParameterType_Label.TabIndex = 4;
            MeasureParameterType_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));// = AnchorStyles.
        }

        private void InitCycleCounterLabel()
        {
            MeasureCycleCounter_Label = new Label();
            MeasureCycleCounter_Label.Parent = this;
            MeasureCycleCounter_Label.Text = "";
            MeasureCycleCounter_Label.AutoSize = true;
            MeasureCycleCounter_Label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            MeasureCycleCounter_Label.Name = "MeasureCycleCounter";
            MeasureCycleCounter_Label.Size = new System.Drawing.Size(170, 58);
            MeasureCycleCounter_Label.Location = new System.Drawing.Point(10, this.Height - 10- MeasureCycleCounter_Label.Height);
            MeasureCycleCounter_Label.TabIndex = 4;
            MeasureCycleCounter_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));// = AnchorStyles.
        }

        private void InitResultLabel()
        {
            ResultValue_Label = new Label();
            ResultValue_Label.Parent = this;
            ResultValue_Label.Text = "";
            ResultValue_Label.AutoSize = true;
            ResultValue_Label.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            ResultValue_Label.Name = "measureResultField";
            ResultValue_Label.Size = new System.Drawing.Size(170, 58);
            ResultValue_Label.Location = new System.Drawing.Point(this.Width / 2 - ResultValue_Label.Width/2, this.Height/2 - ResultValue_Label.Height/2);
            ResultValue_Label.TabIndex = 4;
            ResultValue_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));// = AnchorStyles.

        }

        private void InitSelfStyle()
        {
            this.BackColor = System.Drawing.Color.FromArgb(254, 255, 252);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.ForeColor = FontColor;

        }

        Label ResultValue_Label;
        Label MeasureCycleCounter_Label;
        Label MeasureParameterType_Label;
        Label CommutationMode_Label;
        #endregion

    }
}