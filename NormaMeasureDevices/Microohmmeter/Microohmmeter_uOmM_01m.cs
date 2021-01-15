using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.Devices.Microohmmeter
{
    public class Microohmmeter_uOmM_01m : DeviceBase
    {
        public Microohmmeter_uOmM_01m(DeviceInfo info) : base(info)
        {
            type_id = DeviceType.Microohmmeter;
            type_name_short = "µОмМ-01м";
            type_name_full = "Микроомметр µОмМ-01м";
        }
    }
}
