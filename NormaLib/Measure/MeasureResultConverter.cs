using System;
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
        float bringing_length = 0;
        
        public double ConvertedValue;
        public double ConvertedValueRounded => RoundValue(ConvertedValue);// Math.Round(ConvertedValue, GetDecimalPartLengthByValue(ConvertedValue), MidpointRounding.AwayFromZero);
        public string ConvertedValueRoundedLabel
        {
            get
            {
                if (MeasuredParameterType.IsItIsolationResistance(parameterData.ParameterTypeId))
                {
                    return ConvertedValueRounded > 10000 ? ConvertedValueRounded.ToString("E2") : ConvertedValueRounded.ToString();  
                }
                else
                {
                    return ConvertedValueRounded.ToString();
                }
            }
        }
        string main_result_measure = String.Empty;
        public string ConvertedValueLabel => $"{ConvertedValueRoundedLabel} {main_result_measure}";
        public double RawValue => raw_value;
        CableStructureMeasuredParameterData parameterData;
        public CableStructureMeasuredParameterData ParameterData => parameterData;

        private static int GetDecimalPartLengthByValue(double val)
        {
            if (val < 10) return 3;
            else if (val < 100) return 2;
            else if (val < 1000) return 1;
            else return 0;
        }

        public static double RoundValue(double value, int decimal_part)
        {
            return Math.Round(value, decimal_part, MidpointRounding.AwayFromZero);
        }
        public static double RoundValue(double value)
        {
            return RoundValue(value, GetDecimalPartLengthByValue(value));
        }

        public MeasureResultConverter(double _raw_value, CableStructureMeasuredParameterData _parameter_data, float _cable_length = 1000f, float _material_coeff = 1.0f)
        {
            ConvertedValue = raw_value = _raw_value;
            parameter_type_id = _parameter_data.ParameterTypeId;
            cable_length = _cable_length;
            bringing_length = (_parameter_data.LengthBringingTypeId == LengthBringingType.NoBringing) ? 0 : _parameter_data.LengthBringing;
            material_coeff = _material_coeff;
            parameterData = _parameter_data;
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
            main_result_measure = parameterData.ResultMeasure;
        }

        private void setMeasureLabel_Rizol()
        {
            main_result_measure = parameterData.ResultMeasure;
        }

        private void calculate_Rleads()
        {
            ConvertedValue = (bringing_length == 0) ? raw_value / material_coeff : ((raw_value * bringing_length) / cable_length) / material_coeff;
        }

        private void calculate_Rizol()
        {
            ConvertedValue = (bringing_length == 0) ? raw_value * material_coeff : (raw_value * cable_length * material_coeff) / bringing_length;

        }
    }
}
