using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NormaMeasure.Devices.XmlObjects;

namespace NormaMeasure.Devices.Microohmmeter
{
    public class Microohmmeter_uOmM_01m : DeviceBase
    {
        private uint cableLength = 1000;
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
            base.SendMeasureParamsToDevice();
        }

        public override DeviceXMLState GetXMLState()
        {
            return new DeviceXMLState(this);
        }

        protected override void PCModeMeasureThreadFunction()
        {
            uint cyclesCounterWas = MeasureCyclesCounter;
            DeviceWorkStatus work_status_was;
             p = CommandProtocol as TOhmM_01_v1_CommandProtocol;
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
            else if (WorkStatus == DeviceWorkStatus.IDLE || work_status_was == DeviceWorkStatus.DEPOLARIZATION)
            {
                measure_cycle_flag = false;
                p.MeasureStartFlag = false;
                //IsOnMeasureCycle = false;
                //threadIsActive = false;
                //break;
            }
        }
    }

    enum MICRO_MEASURE_MODE : ushort
    {
        R = 0,
        RL = 2,
        p = 3
    }
}
