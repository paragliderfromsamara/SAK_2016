using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Devices.SAC;
using NormaMeasure.Devices.SAC.SACUnits;
using System.Threading;
using System.Diagnostics;


namespace NormaMeasure.MeasureControl.SAC
{
    public delegate void SACMeasure_Handler(SACMeasure measure, SACMeasurePoint curPoint);
    public class SACMeasure : MeasureBase
    {
        public SACMeasure(SAC_Device _sac)
        {
            SACDevice = _sac;
            OnMeasureThread_Finished += SACMeasure_OnMeasureThread_Finished;
            resultCollections = new Dictionary<uint, List<SACMeasurePoint>>();
            Result_Gotten += SACMeasure_Result_Gotten;
        }

        private void SACMeasure_Result_Gotten(SACMeasure measure, SACMeasurePoint curPoint)
        {
            uint key =curPoint.ParameterType.ParameterTypeId;
            if (!resultCollections.ContainsKey(key)) resultCollections.Add(key, new List<SACMeasurePoint>());
            resultCollections[key].Add(curPoint);
        }

        private void SACMeasure_OnMeasureThread_Finished(object _measure)
        {
            SACDevice.SetDefaultSACState();
        }

        protected void Rizol_Measure()
        {
            CPSMeasureUnit unit = SACDevice.SetMeasurePoint(currentMeasurePoint);
            SACDevice.table.SetTableForMeasurePoint(currentMeasurePoint);
            Thread.Sleep(150);
            if (unit == null)
            {
                Status = MeasureCycleStatus.DeviceNotFound;
            }
            else
            {
                ((U130)unit).SwitchOffVoltageSource();
                unit.SetUnitStateByMeasurePoint(currentMeasurePoint);
                unit.SetMeasureMode();
                do
                {
                    unit.MakeMeasure(ref currentMeasurePoint);
                    Result_Gotten?.Invoke(this, currentMeasurePoint);
                    cycleNumber++;
                } while (WillMeasureContinue());
                ((U130)unit).SwitchOffVoltageSource();
                SACDevice.CentralSysPult.Commutator.SetOnGroundState();
            }
        }

        protected void Rleads_AND_CEK_Measure()
        {
            CPSMeasureUnit unit = SACDevice.SetMeasurePoint(currentMeasurePoint);
            if (unit == null)
            {
                Status = MeasureCycleStatus.DeviceNotFound;
            }else
            {
                SACDevice.table.SetTableForMeasurePoint(currentMeasurePoint);
                Thread.Sleep(150);
                unit.SetUnitStateByMeasurePoint(currentMeasurePoint);
               // unit.SetMeasureMode();
                do
                {
                    unit.MakeMeasure(ref currentMeasurePoint);
                    Result_Gotten?.Invoke(this, currentMeasurePoint);
                    Thread.Sleep(250);
                    cycleNumber++;
                } while (WillMeasureContinue());
                SACDevice.CentralSysPult.Commutator.SetOnGroundState();
            }
        }

        protected void al_Measure()
        {
            CPSMeasureUnit unit = SACDevice.SetMeasurePoint(currentMeasurePoint);
            Dictionary<double, double> Values = new Dictionary<double, double>();
            if (unit == null)
            {
                Status = MeasureCycleStatus.DeviceNotFound;
            }
            else
            {
                //currentMeasurePoint.CurrentFrequency = currentMeasurePoint.FrequencyMin;

               // unit.SetUnitStateByMeasurePoint(currentMeasurePoint);
               // unit.SetMeasureMode();
                do
                {
                    double value = 0;
                    for (currentMeasurePoint.CurrentFrequency = currentMeasurePoint.FrequencyMin; currentMeasurePoint.CurrentFrequency <= currentMeasurePoint.FrequencyMax; currentMeasurePoint.CurrentFrequency += currentMeasurePoint.FrequencyStep)
                    {
                        unit.SetUnitStateByMeasurePoint(currentMeasurePoint);
                        SACDevice.table.SetTableForMeasurePoint(currentMeasurePoint);
                        unit.SetMeasureMode();
                        SACDevice.table.DDSGenerator.SetFrequency(currentMeasurePoint.CurrentFrequency);
                        unit.MakeMeasure(ref currentMeasurePoint);
                        if (currentMeasurePoint.ConvertedResult > value)
                        {
                            value = currentMeasurePoint.ConvertedResult;
                        }
                        Debug.WriteLine($"MeasureBase.MeasureMainFunction() CycleNumber = {CycleNumber}");
                        if (!WillMeasureContinue()) break;
                        //Thread.Sleep(200);
                    }
                    currentMeasurePoint.ConvertedResult = value;
                    //unit.MakeMeasure(ref currentMeasurePoint);
                    Result_Gotten?.Invoke(this, currentMeasurePoint);
                    cycleNumber++;
                    Debug.WriteLine($"MeasureBase.MeasureMainFunction() CycleNumber = {CycleNumber}");
                } while (WillMeasureContinue());
            }
        }

        public event SACMeasure_Handler Result_Gotten;
        protected SAC_Device SACDevice;
        protected SACMeasurePoint currentMeasurePoint;

        /// <summary>
        /// Коллекция результатов измерения
        /// Ключ - ID типа измеряемого параметра
        /// Значение - список значений соответствующий измеряемому параметру
        /// </summary>
        protected Dictionary<uint, List<SACMeasurePoint>> resultCollections;
    }
}
