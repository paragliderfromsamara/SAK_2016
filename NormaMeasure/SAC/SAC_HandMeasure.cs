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
    public class SAC_HandMeasure : SACMeasure
    {

        public double Result => _resultValue;

        private void RefreshResult(double r)
        {
            _resultValue = r;
        }

        public SAC_HandMeasure(SAC_Device _sac) : base(_sac)
        {

        }

        private void HandMeasureFunction()
        {
            double result = 0;
           // sacDevice.MeasureParameter(parameterType, ref result);
            RefreshResult(result);
            Thread.Sleep(500);
        }


        /// <summary>
        /// Запуск измерения для выбранных настроек
        /// </summary>
        /// <param name="point"></param>
        /// <param name="_cycleLimit"></param>
        public void StartMeasureForPoint(SACMeasurePoint point, int _cycleLimit = 1)
        {
            currentMeasurePoint = point;
            switch(point.ParameterType.ParameterTypeId)
            {
                case MeasuredParameterType.al:
                    MeasureBody = al_Measure;
                    break;
                case MeasuredParameterType.Ao:
                case MeasuredParameterType.Az:
                    break;
                case MeasuredParameterType.Risol1:
                case MeasuredParameterType.Risol2:
                case MeasuredParameterType.Risol3:
                case MeasuredParameterType.Risol4:
                    MeasureBody = Rizol_Measure;
                    break;
                case MeasuredParameterType.Calling:
                    break;
                default:
                    MeasureBody = Rleads_AND_CEK_Measure;
                    break;
            }
            Start(_cycleLimit);
        }


        private SAC_Device sacDevice;
        private double _resultValue;


        SACMeasurePoint _measurePoint;
    }

}
