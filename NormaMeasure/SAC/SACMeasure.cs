using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Devices.SAC;
using NormaMeasure.Devices.SAC.CPSUnits;
using System.Threading;



namespace NormaMeasure.MeasureControl.SAC
{
    public delegate void SACMeasure_Handler(SACMeasure measure, SACMeasurePoint curPoint);
    public class SACMeasure : MeasureBase
    {
        public SACMeasure(SAC_Device _sac)
        {
            SACDevice = _sac;
            OnMeasureThread_Finished += SACMeasure_OnMeasureThread_Finished;
        }

        private void SACMeasure_OnMeasureThread_Finished(object _measure)
        {
            SACDevice.SetDefaultSACState();
        }

        protected void Rleads_AND_CEK_Measure()
        {
            CPSMeasureUnit unit = SACDevice.SetMeasurePoint(currentMeasurePoint);
            if (unit == null)
            {
                Status = MeasureCycleStatus.WillFinished;
            }else
            {
                do
                {
                    unit.MakeMeasure(ref currentMeasurePoint);
                    Result_Gotten?.Invoke(this, currentMeasurePoint);
                    Thread.Sleep(PauseBetweenMeasure);
                    cycleNumber++;
                } while (WillMeasureContinue());

            }
        }


        public event SACMeasure_Handler Result_Gotten;
        protected SAC_Device SACDevice;
        protected SACMeasurePoint currentMeasurePoint;
    }
}
