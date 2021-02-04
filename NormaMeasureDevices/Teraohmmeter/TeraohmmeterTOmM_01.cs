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
            int readCounter = 3;
            TOhmM_01_v1_CommandProtocol p = null;
            TeraMeasureResultStruct result;
            DeviceInfo info;
           // retry:
            try
            {
                p = new TOhmM_01_v1_CommandProtocol(PortName);
                SendMeasureParamsToDevice(p);

                while (threadIsActive)
                {
                    if (!IsOnMeasureCycle)
                    {
                        p.MeasureStartFlag = true;
                        IsOnMeasureCycle = p.MeasureStartFlag;
                        continue;
                    }
                    //WorkStatus = (DeviceWorkStatus)p.WorkStatus;
                    if (readCounter++ % 3 == 0)
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
                        if (!p.PCModeFlag || p.MeasureLineNumber != ClientId)
                        {
                            threadIsActive = false;
                            IsOnMeasureCycle = false;
                            p.MeasureStartFlag = false;
                            break;
                        }
                    }


                    /*
                    Debug.WriteLine($"-----------------{idx}--------------------------------PCModeMeasureThread-------------------------------");
                    if (idx < 8)
                    {
                        Debug.WriteLine("------------------------MEASURE_START---------------");
                        repeat_switch:
                        switch(idx)
                        {
                            case 0:
                                //p.PCModeFlag = true;
                                //p.MeasureLineNumber = (short)ClientId;
                                idx++;
                                break;
                            case 1:
                                p.Temperature = temperature;
                                idx++;
                                goto repeat_switch;
                            case 2:
                                p.PolarDelay = polarDelay;
                                idx++;
                                goto repeat_switch;
                            case 3:
                                p.DepolarDelay = depolarDelay;
                                idx++;
                                goto repeat_switch;
                            case 4:
                                p.NormaValue = normaValue;
                                idx++;
                                goto repeat_switch;
                            case 5:
                                p.MaterialId = materialId;
                                idx++;
                                goto repeat_switch;
                            case 6:
                                p.CableLength = cableLength;
                                idx++;
                                goto repeat_switch;
                            case 7:
                                p.VoltageValue = voltageValue;
                                idx++;
                                goto repeat_switch;
                            case 8:
                                reinit:
                                p.MeasureStartFlag = true;
                                if (!p.MeasureStartFlag) goto reinit;
                                else IsOnMeasureCycle = true;
                                idx++;
                                goto repeat_switch;
                        }
                    }
                    if (readCounter % 3 == 0)
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
                    }else if (readCounter % 3 == 1)
                    {
                        result = p.MeasureResult;
                        ConvertedResult = result.ConvertedValue;
                        RawResult = result.ConvertedByModeValue;
                        MeasureStatusId = result.MeasureStatus;
                        OnGetMeasureResult?.Invoke(this, new MeasureResultEventArgs(result));
                    }
                    else if (readCounter % 3 == 2)
                    {
                        if (p.MeasureLineNumber != ClientId)
                        {
                            threadIsActive = false;
                            IsOnMeasureCycle = false;
                            p.MeasureStartFlag = false;
                        }
                    }
                    readCounter++;
                    tryTimes = 50;
                    */
                }
                p.Dispose();
                //OnThreadWillFinish?.Invoke();
            }
            catch(DeviceCommandProtocolException ex)
            {
                Debug.WriteLine("----------------------------M E S S A G E--------------------");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.InnerException.Message);
                Debug.WriteLine("----------------------------M E S S A G E--E N D--------------");
                if (p != null) p.Dispose();
                if (threadIsActive)
                {
                    //if (tryTimes-- > 0) goto retry;
                    IsConnected = false;
                }
                IsOnMeasureCycle = false;

            }
        }

        public override DeviceXMLState GetXMLState()
        {
            return new DeviceXMLState(this);
        }

        protected override void MeasureStopThreadFunc()
        {
            int tryTimes = 150;
            int idx = 3;
            TOhmM_01_v1_CommandProtocol p = null;
            bool flag = true;
            try
            {
                p = new TOhmM_01_v1_CommandProtocol(PortName);
                Debug.WriteLine("MEASURE_STOP_THEAD ------ START");
                while (threadIsActive)
                {
                   // while(flag)
                   // {
                   //     p.MeasureStartFlag = false;
                   //     Thread.Sleep(15);
                   //     flag = p.MeasureStartFlag;
                   // }
                    if (idx % 3 == 0)
                    {
                        if (flag) flag = p.MeasureStartFlag = false;
                        else flag = p.MeasureStartFlag;
                        
                        //Thread.Sleep(5);
                    }
                    else if (idx % 3 == 1)
                    {
                        threadIsActive = IsOnPCMode = p.PCModeFlag;
                    }
                    else if (idx % 3 == 2 && !flag)
                    {
                        if (p.WorkStatus == (int)DeviceWorkStatus.IDLE)
                        {
                            IsOnMeasureCycle = false;
                            threadIsActive = false;
                        }
                    }
                    idx++;
                    tryTimes = 150;
                }
                p.Dispose();
                Debug.WriteLine("MEASURE_STOP_THEAD ------ STOP");
                //OnThreadWillFinish?.Invoke();
            }
            catch(DeviceCommandProtocolException ex)
            {
                Debug.WriteLine("----------------------------M E S S A G E--------------------");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.InnerException.Message);
                Debug.WriteLine("----------------------------M E S S A G E--E N D--------------");
                if (p != null) p.Dispose();
                IsOnPCMode = false;
                IsOnMeasureCycle = false;
                IsConnected = false;
            }
        }

        protected override DeviceCommandProtocol GetDeviceCommandProtocol()
        {
            return new TOhmM_01_v1_CommandProtocol(PortName);
        }

        protected override void SendMeasureParamsToDevice(DeviceCommandProtocol protocol)
        {
            base.SendMeasureParamsToDevice(protocol);
            TOhmM_01_v1_CommandProtocol p = protocol as TOhmM_01_v1_CommandProtocol;
            p.Temperature = temperature;
            p.PolarDelay = polarDelay;
            p.DepolarDelay = depolarDelay;
            p.NormaValue = normaValue;
            p.MaterialId = materialId;
            p.CableLength = cableLength;
            p.VoltageValue = voltageValue;
        }

        protected override bool CheckDeviceIsOnMeasureCycle(DeviceCommandProtocol protocol)
        {
            TOhmM_01_v1_CommandProtocol p = protocol as TOhmM_01_v1_CommandProtocol;
            int work_status = p.MeasureStatus;
            if (work_status == (int)DeviceWorkStatus.MEASURE) return true;
            else if (work_status == (int)DeviceWorkStatus.POLARIZATION) return true;
            else if (work_status == (int)DeviceWorkStatus.DEPOLARIZATION) return true;
            else return false;
        }

        protected override void ReadMeasureResult(DeviceCommandProtocol protocol)
        {
            TOhmM_01_v1_CommandProtocol p = protocol as TOhmM_01_v1_CommandProtocol;
            TeraMeasureResultStruct result = p.MeasureResult;
            ConvertedResult = result.ConvertedValue;
            RawResult = result.ConvertedByModeValue;
            MeasureStatusId = result.MeasureStatus;
            OnGetMeasureResult?.Invoke(this, new MeasureResultEventArgs(result));
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
