using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;

namespace NormaLib.Measure
{
    public class NormDeterminant
    {
        private CableStructureMeasuredParameterData parameterData;
        private double resultValue;

        public CableStructureMeasuredParameterData ParameterData
        {
            set
            {
                parameterData = value;
            }
        }

        public double ResultValue
        {
            set
            {
                resultValue = value;
            }
        }

        public bool IsOnNorma => IsLargerOrEqualMin & IsLessOrEqualMax;
        public bool IsLessOrEqualMax => parameterData.HasMaxLimit ? resultValue <= parameterData.MaxValue : true;
        public bool IsLargerOrEqualMin => parameterData.HasMinLimit ? resultValue >= parameterData.MinValue : true;

        public string ConclusionMessage
        {
            get
            {
                if (!IsLargerOrEqualMin)
                    return "Результат меньше нормы!";
                else if (!IsLessOrEqualMax)
                    return "Результат меньше нормы!";
                else return "Результат соответствует норме!";
            }
        }


        public NormDeterminant()
        {

        }

        public NormDeterminant(CableStructureMeasuredParameterData parameter_data, double result)
        {
            parameterData = parameter_data;
            resultValue = result;
        }

    }
}
