using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.Devices.Teraohmmeter
{
    public class TOhmM_01_v1_CommandProtocol : DeviceCommandProtocol
    {
        ushort VoltageValueAddr;
        ushort CableLengthAddr;
        ushort PolarDelayAddr;
        ushort DepolarDelayAddr;
        ushort OuterCameraDiameterAddr;
        ushort InnerCameraDiameterAddr;
        ushort NormaValueAddr;
        ushort MaterialIdAddr;
        ushort TemperatureAddr;
        ushort Integrator50HzTimesToReachAddr;
        ushort CurrentMeasureRangeAddr;
        ushort IntegratorDifferenceAddr;
        ushort MeasuredIntegratorDifferenceAddr;
        ushort ConvertedResistanceValueAddr;
        ushort ConvertedByMeasureModeResistanceValueAddr;
        ushort MeasureModeAddr;
        ushort StatMeasureAmountAddr;
        ushort LengthBringingTypeIdAddr;
        ushort MaterialHeightAddr;
        ushort CableLengthMeasureIdAddr;
        ushort MeasureCyclesCounterAddr;
        ushort IntegratorStartValueAddr;
        ushort StartIntegratorFlagAddr;
        public TeraMeasureResultStruct MeasureResult
        {
            get
            {
                ushort[] resultArr = ReadHoldings(MeasureStatusAddr, (ushort)(ConvertedByMeasureModeResistanceValueAddr + 1));
                TeraMeasureResultStruct resultStruct = new TeraMeasureResultStruct();
                resultStruct.MeasureStatus = resultArr[0];
                resultStruct.TimeToReach = resultArr[1];
                resultStruct.Range = resultArr[2];
                resultStruct.IntegratorDifference = resultArr[3];
                resultStruct.MeasuredIntegratorDifference = resultArr[4];
                resultStruct.ConvertedValue = GetFloatFromUSHORT(resultArr[6], resultArr[5]);
                resultStruct.ConvertedByModeValue = GetFloatFromUSHORT(resultArr[8], resultArr[7]);
                if (resultStruct.ConvertedValue == 0 && resultStruct.MeasureStatus == (uint)DeviceMeasureStatus.SUCCESS)
                {
                    resultStruct.ConvertedByModeValue = resultStruct.ConvertedValue = float.MaxValue;
                }
                return resultStruct;
            }
        }

        public uint VoltageValue
        {
            get
            {
                ushort v = ReadSingleHolding(VoltageValueAddr);
                return (uint)v;
            }set
            {
                WriteSingleHolding(VoltageValueAddr, (ushort)value);
            }
        }

        public uint CableLength
        {
            get
            {
                return ReadSingleHolding(CableLengthAddr);
            }
            set
            {
                WriteSingleHolding(CableLengthAddr, (ushort)value);
            }
        }

        public uint PolarDelay
        {
            get
            {
                return ReadSingleHolding(PolarDelayAddr);
            }
            set
            {
                WriteSingleHolding(PolarDelayAddr, (ushort)value);
            }
        }

        public uint DepolarDelay
        {
            get
            {
                return ReadSingleHolding(DepolarDelayAddr);
            }
            set
            {
                WriteSingleHolding(DepolarDelayAddr, (ushort)value);
            }
        }

        public uint OuterCameraDiameter
        {
            get
            {
                return ReadSingleHolding(OuterCameraDiameterAddr);
            }
            set
            {
                WriteSingleHolding(OuterCameraDiameterAddr, (ushort)value);
            }
        }

        public uint InnerCameraDiameter
        {
            get
            {
                return ReadSingleHolding(InnerCameraDiameterAddr);
            }
            set
            {
                WriteSingleHolding(InnerCameraDiameterAddr, (ushort)value);
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

        public uint NormaValue
        {
            get
            {
                return ReadSingleHolding(NormaValueAddr);
            }
            set
            {
                WriteSingleHolding(NormaValueAddr, (ushort)value);
            }
        }

        public uint StartIntegratorValue
        {
            get
            {
                return ReadSingleHolding(IntegratorStartValueAddr);
            }set
            {
                WriteSingleHolding(IntegratorStartValueAddr, (ushort)value);
            }
        }

        public bool StartIntegratorFlag
        {
            get
            {
                return ReadBoolValue(StartIntegratorFlagAddr);
            }set
            {
                WriteBoolValue(StartIntegratorFlagAddr, value);
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

        public TOhmM_01_v1_CommandProtocol(string port_name) : base(port_name)
        {

        }

        

        protected override void InitAddressMap()
        {
            base.InitAddressMap();
            PCModeFlagAddr = 0x0080;
            MeasureLineNumberAddr = 0x0081;
            MeasureStartFlagAddr = 0x0082;
            MeasureStatusAddr = 0x0083;
            Integrator50HzTimesToReachAddr = 0x0084;
            CurrentMeasureRangeAddr = 0x0085;
            IntegratorDifferenceAddr = 0x0086;
            MeasuredIntegratorDifferenceAddr = 0x0087;
            ConvertedResistanceValueAddr = 0x0088;
            ConvertedByMeasureModeResistanceValueAddr = 0x008A;
            StartIntegratorFlagAddr = 0x0097;
            MeasureCyclesCounterAddr = 0x0096;
            IntegratorStartValueAddr = 0x0090;

            MeasureModeAddr = 0x0030;
            CableLengthAddr = 0x0031;
            TemperatureAddr = 0x0032;
            MaterialHeightAddr = 0x0033;
            PolarDelayAddr = 0x0034;
            DepolarDelayAddr = 0x0035;
            StatMeasureAmountAddr = 0x0036;
            InnerCameraDiameterAddr = 0x0037;
            OuterCameraDiameterAddr = 0x0038;
            VoltageValueAddr = 0x0039;
            MaterialIdAddr = 0x003A;
            CableLengthMeasureIdAddr = 0x003B;
            NormaValueAddr = 0x003D;
            
            

            LengthBringingTypeIdAddr = 0x003F;
            
        }
    }



    public struct TeraMeasureResultStruct
    {
        public float ConvertedValue;
        public float ConvertedByModeValue;
        public uint Range;
        public uint TimeToReach;
        public uint MeasureStatus;
        public int IntegratorDifference;
        public int MeasuredIntegratorDifference;
    }

}
