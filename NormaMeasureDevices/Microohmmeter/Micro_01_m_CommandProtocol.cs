using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.Devices.Microohmmeter
{
    class Micro_01_m_CommandProtocol : DeviceCommandProtocol
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

        protected override void InitAddressMap()
        {
            base.InitAddressMap();
            PCModeFlagAddr = 0x0080;
            MeasureLineNumberAddr = 0x0081;
            MeasureStartFlagAddr = 0x0082;
            MeasureStatusAddr = 0x0083;

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

        }



    }
}
