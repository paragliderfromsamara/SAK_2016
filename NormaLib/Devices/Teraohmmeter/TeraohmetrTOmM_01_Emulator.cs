using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NormaLib.Devices.Teraohmmeter
{
    public class TeraohmetrTOmM_01_Emulator : TeraohmmeterTOmM_01
    {
        public TeraohmetrTOmM_01_Emulator(DeviceInfo info) : base(info)
        {
            type_id = DeviceType.Teraohmmeter;
            type_name_short = "ТОмМ-01";
            type_name_full = "Тераомметр ТОмМ-01 (Эмуляция)";
            AllowRemoteMeasure = true;
            HasCableStore = false;
            HasTestResultStore = false;
        }

        private DeviceWorkStatus current_status = DeviceWorkStatus.IDLE;
        private bool start_integrator_flag = false;
        private bool measure_start_flag = false;
        protected override void PCModeMeasureThreadFunction()
        {
            uint cyclesCounterWas = MeasureCyclesCounter;
            DeviceWorkStatus work_status_was;
            //TOhmM_01_v1_CommandProtocol p = CommandProtocol as TOhmM_01_v1_CommandProtocol;
            TeraMeasureResultStruct result;

            work_status_was = WorkStatus;
            WorkStatus = (DeviceWorkStatus)current_status;

            if (WorkStatus == DeviceWorkStatus.MEASURE)
            {
                if (!integratorIsStart)
                {
                    Thread.Sleep(200);
                    integratorIsStart = true;
                }
                else
                {
                    //Thread.Sleep(60);
                    if (!start_integrator_flag)
                    {
                        result = GetRandomResult();
                        RawResult = result.ConvertedValue; //Перевод в МОм
                        ConvertedResult = result.ConvertedByModeValue; //Перевод в МОм
                        MeasureStatusId = result.MeasureStatus;
                        OnGetMeasureResult?.Invoke(this, new MeasureResultEventArgs(result));
                        if (MeasureStatusId == (uint)DeviceMeasureStatus.SUCCESS)
                        {
                            integratorIsStart = false;
                        }
                        else
                        {
                            measure_cycle_flag = false;
                        }
                    }
                    /*
                    else
                    {
                        MeasureCyclesCounter = p.MeasureCyclesCounter;
                        if (!p.PCModeFlag || p.MeasureLineNumber != ClientId)
                        {
                            measure_cycle_flag = false;
                            p.MeasureStartFlag = false;
                        }
                    }
                    */
                }
            }
            else if (WorkStatus == DeviceWorkStatus.IDLE || work_status_was == DeviceWorkStatus.DEPOLARIZATION)
            {
                measure_cycle_flag = false;
                //p.MeasureStartFlag = false;
            }
        }

        protected override void MeasureStopThreadFunc()
        {
            DeviceWorkStatus statusTmp;
            TOhmM_01_v1_CommandProtocol p = CommandProtocol as TOhmM_01_v1_CommandProtocol;
            if (p.MeasureStartFlag)
            {
                p.MeasureStartFlag = false;
            }
            else
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
        }

        protected override void CheckDeviceConnectionThreadFunc()
        {
            Debug.WriteLine("------------------------VIRTUAL_DEVICE_SIMPLE_CONNECTION_CHECK---------------");
            /*
            DeviceInfo info = CommandProtocol.GetDeviceInfo();
            if (!DeviceInfoIsValid(info))
            {
                IsConnected = false;
            }
            else
            {
                WorkStatus = info.WorkStatus;
            }*/
            Thread.Sleep(800);
        }

        protected override void InitPCModeOnDevice()
        {
            //CommandProtocol.PCModeFlag = true;
            Thread.Sleep(250);
            client_id_on_device = (short)ClientId;
            Thread.Sleep(250);
            IsOnPCMode = true;
            Thread.Sleep(250);
        }
        short client_id_on_device;
        protected override void PCModeIdleThreadFunction()
        {
            Debug.WriteLine("------------------------PC_MODE_CONNECTION_CHECK_ON_EMULATOR---------------");
            WorkStatus = DeviceWorkStatus.IDLE;
            if (client_id_on_device != next_client_id)
            {
                client_id_on_device = (short)next_client_id;
                //Thread.Sleep(250);
                ClientId = client_id_on_device;
            }
            IsOnPCMode = pc_mode_flag = true;
            Debug.WriteLine($"Escape FROM Set PC MODE FLAG {pc_mode_flag}");
            /*
            DeviceInfo info = CommandProtocol.GetDeviceInfo();
            Debug.WriteLine("------------------------PC_MODE_CONNECTION_CHECK---------------");
            if (!DeviceInfoIsValid(info))
            {
                IsOnPCMode = pc_mode_flag = false;
                Debug.WriteLine("Escape FROM CheckDeviceInfo");
            }
            else
            {
                WorkStatus = info.WorkStatus;
                if (CommandProtocol.MeasureLineNumber != next_client_id)
                {
                    CommandProtocol.MeasureLineNumber = (short)next_client_id;
                    //Thread.Sleep(250);
                    ClientId = CommandProtocol.MeasureLineNumber;
                }
                IsOnPCMode = pc_mode_flag = CommandProtocol.PCModeFlag;
                Debug.WriteLine($"Escape FROM Set PC MODE FLAG {pc_mode_flag}");
            }
            // проверяем актуальность ID клиента, 
            */
        }

        protected override void InitMeasureCycleOnDevice()
        {
            //SendMeasureParamsToDevice();
            //CommandProtocol.MeasureStartFlag = true;
            Thread.Sleep(200);
            IsOnMeasureCycle = true;
            integratorIsStart = false;
            MeasureCyclesCounter = 0;
            WorkStatus = DeviceWorkStatus.MEASURE;
            measure_cycle_flag = true;
        }

        private float GetRandomValue()
        {
            float v;
            Random r = new Random();
            v = (float)r.Next(150, 151);
            v /= 1000f;
            return v;
        }

        private TeraMeasureResultStruct GetRandomResult()
        {
            TeraMeasureResultStruct s = new TeraMeasureResultStruct();
            s.ConvertedValue = GetRandomValue();
            s.ConvertedByModeValue = s.ConvertedValue;
            s.Range = 1;
            s.TimeToReach = 500;
            s.MeasureStatus = (uint)DeviceMeasureStatus.SUCCESS;
            s.IntegratorDifference = 900;
            s.MeasuredIntegratorDifference = 900;
            return s;
        }

    }
}
