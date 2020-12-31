using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NormaMeasure.Devices
{

    public class DeviceBase : IDisposable
    {
        public DeviceBase()
        {

        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public struct DeviceInfo
    {
        DeviceType type;
        int SerialYear;
        byte SerialNumber;
        ushort[] data;

    }

    public enum DeviceType : byte
    {
        Teraohmmeter = 1,
        Microohmmeter = 2,
        PICA_X = 3,
        SAC_TVCH = 4
       
    }
}
