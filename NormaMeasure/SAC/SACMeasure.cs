﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Devices.SAC;
using NormaMeasure.Devices.SAC.SACUnits;
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
            SACDevice.table.SetTableForMeasurePoint(currentMeasurePoint);
            Thread.Sleep(150);
            if (unit == null)
            {
                Status = MeasureCycleStatus.DeviceNotFound;
            }else
            {
                do
                {
                    unit.MakeMeasure(ref currentMeasurePoint);
                    Result_Gotten?.Invoke(this, currentMeasurePoint);
                    Thread.Sleep(500);
                    cycleNumber++;
                } while (WillMeasureContinue());
                SACDevice.CentralSysPult.Commutator.SetOnGroundState();
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
