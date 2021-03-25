﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;


namespace NormaLib.Measure
{
    public class MeasureResultConverter
    {
        float material_coeff = 1.0f;
        double raw_value;
        uint parameter_type_id;
        float cable_length;
        float bringing_length;
        public double ConvertedValue;
        string measure_bringing_length_label = String.Empty;
        string main_result_measure = String.Empty;
        public string ConvertedValueLabel => $"{Math.Round(ConvertedValue, 3, MidpointRounding.AwayFromZero)} {main_result_measure}{measure_bringing_length_label}";


        public MeasureResultConverter(double _raw_value, uint _parameter_type_id, float _cable_length = 1000f, float _bringing_length = 1000f, float _material_coeff = 1.0f)
        {
            ConvertedValue = raw_value = _raw_value;
            parameter_type_id = _parameter_type_id;
            cable_length = _cable_length;
            bringing_length = _bringing_length;
            material_coeff = _material_coeff;
            calculate();
        }

        public MeasureResultConverter(double _raw_value, CableStructureMeasuredParameterData _parameter_data, float _cable_length = 1000f, float _material_coeff = 1.0f)
        {
            ConvertedValue = raw_value = _raw_value;
            parameter_type_id = _parameter_data.ParameterTypeId;
            cable_length = _cable_length;
            bringing_length = _parameter_data.LengthBringing;
            material_coeff = _material_coeff;
            calculate();
        }

        private void calculate()
        {
            switch(parameter_type_id)
            {
                case MeasuredParameterType.Risol1:
                case MeasuredParameterType.Risol3:
                    calculate_Rizol();
                    setMeasureLabel_Rizol();
                    break;
                case MeasuredParameterType.Rleads:
                    calculate_Rleads();
                    setMeasureLabel_Rleads();
                    break;
            }
        }

        private void setMeasureLabel_Rleads()
        {
            measure_bringing_length_label = (bringing_length == 1000) ? "/км" : $"/{bringing_length}м";
            main_result_measure = "Ом";
        }

        private void setMeasureLabel_Rizol()
        {
            measure_bringing_length_label = (bringing_length == 1000) ? "⋅км" : $"⋅{bringing_length}м";
            main_result_measure = "МОм";
        }

        private void calculate_Rleads()
        {
            ConvertedValue = ((raw_value * bringing_length) / cable_length) / material_coeff;
        }

        private void calculate_Rizol()
        {
            ConvertedValue = (raw_value * cable_length * material_coeff) / bringing_length;
        }
    }
}