using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NormaLib.Devices.XmlObjects;
using System.Diagnostics;

namespace NormaLib.Devices.Microohmmeter
{
    public class Microohmmeter_uOmM_01m : DeviceBase
    {
        private float cableLength = 1000.0f;
        private uint beforeMeasureDelay = 200; // в миллисекундах
        private uint betweenMeasureDelay = 1; // в секундах
        private uint statMeasureTimes = 0;
        private uint measureRange;

        protected uint MeasureCyclesCounter = 0;
        protected bool resultWaitFlag;


        private MICRO_MEASURE_MODE measureMode = MICRO_MEASURE_MODE.RL;
        public Microohmmeter_uOmM_01m(DeviceInfo info) : base(info)
        {
            type_id = DeviceType.Microohmmeter;
            type_name_short = "µОмМ-01м";
            type_name_full = "Микроомметр µОмМ-01м";
            AllowRemoteMeasure = true;
            HasCableStore = false;
            HasTestResultStore = false;
        }

        protected override DeviceCommandProtocol GetDeviceCommandProtocol()
        {
            return new Micro_01_m_CommandProtocol(PortName);
        }

        protected override void SetMeasureParametersFromMeasureXMLState(MeasureXMLState measure_state)
        {
            cableLength = measure_state.MeasuredCableLength;
            beforeMeasureDelay = measure_state.BeforeMeasureDelay;
            betweenMeasureDelay = measure_state.AfterMeasureDelay;
            statMeasureTimes = measure_state.AveragingTimes;
        }

        protected override void InitMeasureCycleOnDevice()
        {
            SendMeasureParamsToDevice();
            CommandProtocol.MeasureStartFlag = true;
            Thread.Sleep(200);
            IsOnMeasureCycle = CommandProtocol.MeasureStartFlag;
            resultWaitFlag = false;
            MeasureCyclesCounter = 0;
        }

        protected override void SendMeasureParamsToDevice()
        {
            Micro_01_m_CommandProtocol p = CommandProtocol as Micro_01_m_CommandProtocol;
            p.BeforeMeasureDelay = beforeMeasureDelay < 200 ? 200 : beforeMeasureDelay;
            p.BetweenMeasureDelay = betweenMeasureDelay < 1 ? 1 : betweenMeasureDelay;
            p.CableLength = cableLength;
            p.MeasureModeId = (ushort)measureMode;
            p.StatMeasureAmount = statMeasureTimes;
            p.MeasureRangeId = (ushort)MICRO_MEASURE_RANGE.AUTO;
        }

        public override DeviceXMLState GetXMLState()
        {
            return new DeviceXMLState(this);
        }

        protected void StartMeasure()
        {
            Micro_01_m_CommandProtocol p = CommandProtocol as Micro_01_m_CommandProtocol;
            p.WaitResultFlag = true;
        }

        protected override void PCModeMeasureThreadFunction()
        {
            uint cyclesCounterWas = MeasureCyclesCounter;
            DeviceWorkStatus work_status_was;
            Micro_01_m_CommandProtocol p = CommandProtocol as Micro_01_m_CommandProtocol;
            MicroMeasureResultStruct result;

            work_status_was = WorkStatus;
            WorkStatus = (DeviceWorkStatus)p.WorkStatus;

            if (WorkStatus == DeviceWorkStatus.MEASURE)
            {
                if (!resultWaitFlag)
                {

                    StartMeasure();
                    resultWaitFlag = true;
                }
                else
                {
                    //Thread.Sleep(60);
                    if (!p.WaitResultFlag)
                    {

                        result = p.MeasureResult;
                        RawResult = result.ConvertedValue; 
                        ConvertedResult = result.ConvertedByModeValue; 
                        MeasureStatusId = result.MeasureStatus;
                        OnGetMeasureResult?.Invoke(this, new MeasureResultEventArgs(result));
                        if (MeasureStatusId == (uint)DeviceMeasureStatus.SUCCESS)
                        {
                            resultWaitFlag = false;
                            Debug.WriteLine($"COUNTER {cyclesCounterWas} -------------------");
                        }
                        else
                        {
                            measure_cycle_flag = false;
                            p.MeasureStartFlag = false;
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
            else if (WorkStatus == DeviceWorkStatus.IDLE)
            {
                measure_cycle_flag = false;
                p.MeasureStartFlag = false;
                //IsOnMeasureCycle = false;
                //threadIsActive = false;
                //break;
            }
        }

        protected override void MeasureStopThreadFunc()
        {
            DeviceWorkStatus statusTmp;
            Micro_01_m_CommandProtocol p = CommandProtocol as Micro_01_m_CommandProtocol;
            if (p.MeasureStartFlag)
            {
                p.MeasureStartFlag = false;
            }
            else
            {
                statusTmp = (DeviceWorkStatus)p.WorkStatus;
                if (statusTmp != DeviceWorkStatus.MEASURE)
                {
                    IsOnMeasureCycle = false;
                }
                else
                {
                    WorkStatus = statusTmp;
                }
            }
        }

       

    }



    enum MICRO_MEASURE_MODE : ushort
    {
        R = 0,
        RL = 2,
        p = 3
    }

    public enum MICRO_MEASURE_RANGE : ushort
    {
        AUTO = 0,
        ONE = 1,
        TWO = 2,
        THREE = 3,
        FOUR = 4,
        FIVE = 5, 
        SIX = 6
    }
}
