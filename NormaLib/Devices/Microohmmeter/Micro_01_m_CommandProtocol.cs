using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.Devices.Microohmmeter
{
    public class Micro_01_m_CommandProtocol : DeviceCommandProtocol
    {
        ushort CableLengthAddr;
        ushort BetweenMeasureDelayAddr;
        ushort BeforeMeasureDelayAddr;
        ushort MaterialIdAddr;
        ushort TemperatureAddr;
        ushort CurrentMeasureRangeAddr;
        ushort ConvertedResistanceValueAddr;
        ushort ConvertedByMeasureModeResistanceValueAddr;
        ushort MeasureModeAddr;
        ushort StatMeasureAmountAddr;
        ushort LengthBringingTypeIdAddr;
        ushort CableLengthMeasureIdAddr;
        ushort MeasureCyclesCounterAddr;
        ushort WaitResultFlagAddr;
        ushort LeadDiameterAddr;


        public double LeadDiameter
        {
            get
            {
                return ReadFloatValue(LeadDiameterAddr);
            }
            set
            {
                WriteFloatValue(LeadDiameterAddr, (float)value);
            }
        }

        public double CableLength
        {
            get
            {
                return ReadFloatValue(CableLengthAddr);
            }
            set
            {
                WriteFloatValue(CableLengthAddr, (float)value);
            }
        }

        public uint BeforeMeasureDelay
        {
            get
            {
                return ReadSingleHolding(BeforeMeasureDelayAddr);
            }
            set
            {
                WriteSingleHolding(BeforeMeasureDelayAddr, (ushort)value);
            }
        }

        public uint BetweenMeasureDelay
        {
            get
            {
                return ReadSingleHolding(BetweenMeasureDelayAddr);
            }
            set
            {
                WriteSingleHolding(BetweenMeasureDelayAddr, (ushort)value);
            }
        }

        public uint Temperature
        {
            get
            {
                return ReadSingleHolding(TemperatureAddr);
            }
            set
            {
                WriteSingleHolding(TemperatureAddr, (ushort)value);
            }
        }

        public int MaterialId
        {
            get
            {
                return (int)ReadSingleHolding(MaterialIdAddr);
            }
            set
            {
                WriteSingleHolding(MaterialIdAddr, (ushort)value);
            }
        }

        public bool WaitResultFlag
        {
            get
            {
                return ReadBoolValue(WaitResultFlagAddr);
            }
            set
            {
                WriteBoolValue(WaitResultFlagAddr, value);
            }
        }

        public uint MeasureCyclesCounter
        {
            get
            {
                return ReadSingleHolding(MeasureCyclesCounterAddr);
            }
        }

        public uint MeasureModeId
        {
            get
            {
                return ReadSingleHolding(MeasureModeAddr);
            }
            set
            {
                WriteSingleHolding(MeasureModeAddr, (ushort)value);
            }
        }

        public uint StatMeasureAmount
        {
            get
            {
                return ReadSingleHolding(StatMeasureAmountAddr);
            }
            set
            {
                WriteSingleHolding(StatMeasureAmountAddr, (ushort)value);
            }
        }
        public Micro_01_m_CommandProtocol(string port_name) : base(port_name)
        {

        }

        public MicroMeasureResultStruct MeasureResult
        {
            get
            {
                ushort[] resultArr = ReadHoldings(MeasureStatusAddr, (ushort)(ConvertedByMeasureModeResistanceValueAddr + 1));
                MicroMeasureResultStruct resultStruct = new MicroMeasureResultStruct();
                resultStruct.MeasureStatus = resultArr[0];
                resultStruct.Range = resultArr[2];
                resultStruct.ConvertedValue = GetFloatFromUSHORT(resultArr[6], resultArr[5]);
                resultStruct.ConvertedByModeValue = GetFloatFromUSHORT(resultArr[8], resultArr[7]);
                if (resultStruct.ConvertedValue == 0 && resultStruct.MeasureStatus == (uint)DeviceMeasureStatus.SUCCESS)
                {
                    resultStruct.ConvertedByModeValue = resultStruct.ConvertedValue = float.MaxValue;
                }
                return resultStruct;
            }
        }


        protected override void InitAddressMap()
        {
            base.InitAddressMap();
            PCModeFlagAddr = 0x0080;
            MeasureLineNumberAddr = 0x0081;
            MeasureStartFlagAddr = 0x0082;
            MeasureStatusAddr = 0x0083;
            WaitResultFlagAddr = 0x0097;

            CurrentMeasureRangeAddr = 0x0047;
            ConvertedResistanceValueAddr = 0x0088;
            ConvertedByMeasureModeResistanceValueAddr = 0x008A;
            MeasureCyclesCounterAddr = 0x0096;
            LeadDiameterAddr = 0x0042;
            MeasureModeAddr = 0x0030;
            CableLengthAddr = 0x0031;
            TemperatureAddr = 0x0032;
            BetweenMeasureDelayAddr = 0x0049;
            BeforeMeasureDelayAddr = 0x0045;
            StatMeasureAmountAddr = 0x0036;
            MaterialIdAddr = 0x003A;
            CableLengthMeasureIdAddr = 0x003C;
            LengthBringingTypeIdAddr = 0x003F;
            MeasureRangeIdAddr = 0x0047;

        }



    }

    public struct MicroMeasureResultStruct
    {
        public double ConvertedValue;
        public double ConvertedByModeValue;
        public uint MeasureStatus;
        public uint Range;
    }

    public enum mkOhmM_01_m_MeasureStatus : ushort
    {
        SUCCESS = 100,
        SHORT_CIRCUIT = 103,
        ISTATUS_IN_WORK = 105,

    }
}
