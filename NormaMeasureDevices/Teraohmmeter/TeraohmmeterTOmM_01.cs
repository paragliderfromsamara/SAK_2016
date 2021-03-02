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

        protected bool integratorIsStart = false;
        protected uint MeasureCyclesCounter = 0;
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

        protected override void InitMeasureCycleOnDevice()
        {
            SendMeasureParamsToDevice();
            CommandProtocol.MeasureStartFlag = true;
            Thread.Sleep(200);
            IsOnMeasureCycle = CommandProtocol.MeasureStartFlag;
            integratorIsStart = false;
            MeasureCyclesCounter = 0;
        }

        protected virtual void StartIntegrator()
        {
            TOhmM_01_v1_CommandProtocol p = CommandProtocol as TOhmM_01_v1_CommandProtocol;
            p.StartIntegratorFlag = true;
        }

        protected override void PCModeMeasureThreadFunction()
        {
            uint cyclesCounterWas = MeasureCyclesCounter;
            DeviceWorkStatus work_status_was;
            TOhmM_01_v1_CommandProtocol p = CommandProtocol as TOhmM_01_v1_CommandProtocol;
            TeraMeasureResultStruct result;

            work_status_was = WorkStatus;
            WorkStatus = (DeviceWorkStatus)p.WorkStatus;

            if (WorkStatus == DeviceWorkStatus.MEASURE)
            {
                if (!integratorIsStart)
                {
                    StartIntegrator();
                    integratorIsStart = true;
                }
                else
                {
                    //Thread.Sleep(60);
                    if (!p.StartIntegratorFlag)
                    {

                        result = p.MeasureResult;
                        RawResult = (result.ConvertedValue > 0.00001) ? result.ConvertedValue * 1000.0 : 0; //Перевод в МОм
                        ConvertedResult = (result.ConvertedByModeValue > 0.00001) ? result.ConvertedByModeValue * 1000.0 : 0; //Перевод в МОм
                        MeasureStatusId = result.MeasureStatus;
                        OnGetMeasureResult?.Invoke(this, new MeasureResultEventArgs(result));
                        if (MeasureStatusId == (uint)DeviceMeasureResultStatus.SUCCESS)
                        {
                            integratorIsStart = false;
                            Debug.WriteLine($"COUNTER {cyclesCounterWas} -------------------");
                        }
                    }
                    else
                    {
                        MeasureCyclesCounter = p.MeasureCyclesCounter;
                        if (!p.PCModeFlag || p.MeasureLineNumber != ClientId)
                        {
                            measure_cycle_flag = false;
                            p.MeasureStartFlag = false;
                        }
                    }
                }
            }
            else if (WorkStatus == DeviceWorkStatus.IDLE || work_status_was == DeviceWorkStatus.DEPOLARIZATION)
            {
                measure_cycle_flag = false;
                p.MeasureStartFlag = false;
                //IsOnMeasureCycle = false;
                //threadIsActive = false;
                //break;
            }
        }
        /*
        int tryTimes = 50;
        int idx = 0;
        int readCounter = 3;
        uint measureCyclesCounter = 0;
        uint cyclesCounterWas = 0;
        bool integratorIsStart = false;
        DeviceWorkStatus work_status_was;
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
                work_status_was = WorkStatus;
                WorkStatus = (DeviceWorkStatus)p.WorkStatus;

                if (WorkStatus == DeviceWorkStatus.MEASURE)
                {
                    if (!integratorIsStart)
                    {
                        p.StartIntegratorFlag = true;
                        integratorIsStart = true;
                    }else
                    {
                        //Thread.Sleep(60);
                        if (!p.StartIntegratorFlag)
                        {

                            result = p.MeasureResult;
                            RawResult = (result.ConvertedValue > 0.00001) ? result.ConvertedValue*1000.0 : 0; //Перевод в МОм
                            ConvertedResult = (result.ConvertedByModeValue > 0.00001) ? result.ConvertedByModeValue * 1000.0 : 0; //Перевод в МОм
                            MeasureStatusId = result.MeasureStatus;
                            OnGetMeasureResult?.Invoke(this, new MeasureResultEventArgs(result));
                            cyclesCounterWas = measureCyclesCounter;
                            if (MeasureStatusId == (uint)DeviceMeasureResultStatus.SUCCESS)
                            {
                                integratorIsStart = false;
                                Debug.WriteLine($"COUNTER {cyclesCounterWas} -------------------");
                            }
                        }
                        else
                        {
                            measureCyclesCounter = p.MeasureCyclesCounter;
                            if (!p.PCModeFlag || p.MeasureLineNumber != ClientId)
                            {
                                threadIsActive = false;
                                IsOnMeasureCycle = false;
                                p.MeasureStartFlag = false;
                                break;
                            }
                        }
                    }
                }else if (WorkStatus == DeviceWorkStatus.IDLE && work_status_was == DeviceWorkStatus.DEPOLARIZATION)
                {
                    //IsOnMeasureCycle = false;
                    //threadIsActive = false;
                    //break;
                }

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
    */

        public override DeviceXMLState GetXMLState()
        {
            return new DeviceXMLState(this);
        }

        protected override void MeasureStopThreadFunc()
        {
            DeviceWorkStatus statusTmp;
            TOhmM_01_v1_CommandProtocol p = CommandProtocol as TOhmM_01_v1_CommandProtocol;
            if (p.MeasureStartFlag)
            {
                p.MeasureStartFlag = false;
            }else
            {
                statusTmp = (DeviceWorkStatus)p.WorkStatus;
                if (statusTmp != DeviceWorkStatus.POLARIZATION && statusTmp != DeviceWorkStatus.MEASURE && statusTmp != DeviceWorkStatus.DEPOLARIZATION)
                {
                    IsOnMeasureCycle = false;
                }
                else
                {
                    WorkStatus = statusTmp;
                }
            }


            
            /*
            int tryTimes = 150;
            int idx = 3;
            DeviceWorkStatus statusTmp;
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
                        statusTmp = (DeviceWorkStatus)p.WorkStatus;
                        if (statusTmp == (int)DeviceWorkStatus.IDLE)
                        {
                            IsOnMeasureCycle = false;
                            threadIsActive = false;
                        }else
                        {
                            WorkStatus = statusTmp; 
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
            */
        }

        protected override DeviceCommandProtocol GetDeviceCommandProtocol()
        {
            return new TOhmM_01_v1_CommandProtocol(PortName);
        }

        protected override void SendMeasureParamsToDevice()
        {
            TOhmM_01_v1_CommandProtocol p = CommandProtocol as TOhmM_01_v1_CommandProtocol;
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
