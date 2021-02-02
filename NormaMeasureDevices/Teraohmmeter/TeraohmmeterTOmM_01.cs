using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using NormaMeasure.Devices.XmlObjects;

namespace NormaMeasure.Devices.Teraohmmeter
{
    public class TeraohmmeterTOmM_01 : DeviceBase
    {

        private uint polarDelay = 0;
        private uint depolarDelay = 0;
        private uint cableLength = 1000;
        private uint temperature = 20;
        private int materialId = 0;
        private uint statMeasureTimes = 0;
        private uint innerCameraDiameter = 55;
        private uint outerCameraDiameter = 56;
        private uint voltageValue = 10;
        private uint normaValue = 10000;
        private uint normaValueMeasureId = 0;
        private TERA_MEASURE_MODE measureMode = TERA_MEASURE_MODE.RL;
         
        public TeraohmmeterTOmM_01(DeviceInfo info) : base(info)
        {
            type_id = DeviceType.Teraohmmeter;
            type_name_short = "ТОмМ-01";
            type_name_full = "Тераомметр ТОмМ-01";
        }

        protected override void SetMeasureParametersFromMeasureXMLState(MeasureXMLState measure_state)
        {
            polarDelay = measure_state.BeforeMeasureDelay;
            depolarDelay = measure_state.AfterMeasureDelay;
            cableLength = measure_state.MeasuredCableLength;
            voltageValue = measure_state.MeasureVoltage;
            materialId = measure_state.MeasuredMaterialID;
            temperature = measure_state.Temperature;       
        }

        protected override void PCModeMeasureThread()
        {
            int tryTimes = 50;
            int idx = 0;
            int readCounter = 0;
            TOhmM_01_v1_CommandProtocol p = null;
            TeraMeasureResultStruct result;
            DeviceInfo info;
            retry:
            try
            {
                p = new TOhmM_01_v1_CommandProtocol(PortName);
                while (threadIsActive)
                {
                    Debug.WriteLine($"-----------------{idx}--------------------------------PCModeMeasureThread-------------------------------");
                    if (idx < 7)
                    {
                        repeas_switch:
                        switch(idx)
                        {
                            case 0:
                                p.Temperature = temperature;
                                idx++;
                                goto repeas_switch;
                            case 1:
                                p.PolarDelay = polarDelay;
                                idx++;
                                goto repeas_switch;
                            case 2:
                                p.DepolarDelay = depolarDelay;
                                idx++;
                                goto repeas_switch;
                            case 3:
                                p.NormaValue = normaValue;
                                idx++;
                                goto repeas_switch;
                            case 4:
                                p.MaterialId = materialId;
                                idx++;
                                goto repeas_switch;
                            case 5:
                                p.CableLength = cableLength;
                                idx++;
                                goto repeas_switch;
                            case 6:
                                p.MeasureStartFlag = true;
                                idx++;
                                goto repeas_switch;

                        }
                    }
                    Debug.WriteLine("-------------------------------------------------PCModeMeasureThread------ParamsSet-----------------");
                    if (readCounter % 2 == 0)
                    {
                        info = p.GetDeviceInfo();
                        if (info.type != this.TypeId || info.SerialNumber != this.SerialNumber || info.SerialYear != this.SerialYear || info.ModelVersion != this.ModelVersion)
                        {
                            IsConnected = false;
                        }
                        else
                        {
                            WorkStatus = info.WorkStatus;
                        }
                    }else
                    {
                        result = p.MeasureResult;
                        ConvertedResult = result.ConvertedValue;
                        RawResult = result.ConvertedByModeValue;
                        MeasureStatusId = result.MeasureStatus;
                        OnGetMeasureResult?.Invoke(this, new MeasureResultEventArgs(result));
                    }
                    //if (p.MeasureStatus == )
                    Thread.Sleep(200);
                    tryTimes = 50;
                    readCounter++;
                }
                p.Dispose();
                OnThreadWillFinish?.Invoke();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("----------------------------M E S S A G E--------------------");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("----------------------------M E S S A G E--E N D--------------");
                if (p != null) p.Dispose();
                if (tryTimes-- > 0) goto retry;
                IsConnected = false;
            }
        }

        public override DeviceXMLState GetXMLState()
        {
            return new DeviceXMLState(this);
        }

        protected override void MeasureStopThreadFunc()
        {
            int tryTimes = 50;
            TOhmM_01_v1_CommandProtocol p = null;
            retry:
            try
            {
                p = new TOhmM_01_v1_CommandProtocol(PortName);
                p.MeasureStartFlag = false;
                p.Dispose();
                OnThreadWillFinish?.Invoke();
            }
            catch
            {
                if (p != null) p.Dispose();
                if (tryTimes-- > 0) goto retry;
                IsOnPCMode = false;
                IsConnected = false;
            }
        }
    }

    

    enum TERA_MEASURE_MODE : ushort
    {
        R = 12,
        RL = 13,
        pV = 14,
        pS = 15
    }
}
