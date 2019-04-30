using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Devices.SAC;
using NormaMeasure.DBControl.Tables;
using System.Threading;

namespace NormaMeasure.MeasureControl.SAC
{
    public delegate void SAC_HandMeasure_Handler(SAC_HandMeasure measure);
    public class SAC_HandMeasure : MeasureBase
    {

        public double Result => _resultValue;

        private void RefreshResult(double r)
        {
            _resultValue = r;
            Result_Gotten?.Invoke(this);
        }

        public SAC_HandMeasure(SAC_Device sac)
        {
            sacDevice = sac;
            parameterType = MeasuredParameterType.find_by_id(MeasuredParameterType.Co);
            MeasureBody = HandMeasureFunction;

        }

        private void HandMeasureFunction()
        {
            double result = 0;
            sacDevice.MeasureParameter(parameterType, ref result);
            RefreshResult(result);
            Thread.Sleep(500);

        }



        private SAC_Device sacDevice;
        private MeasuredParameterType parameterType;
        private double _resultValue;

        public event SAC_HandMeasure_Handler Result_Gotten;
    }
}
