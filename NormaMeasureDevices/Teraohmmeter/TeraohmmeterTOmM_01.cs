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
        public TeraohmmeterTOmM_01(DeviceInfo info) : base(info)
        {
            type_id = DeviceType.Teraohmmeter;
            type_name_short = "ТОмМ-01";
            type_name_full = "Тераомметр ТОмМ-01";
        }


        protected override void CheckDeviceConnectionThreadFunc()
        {
            int tryTimes = 50;
            DeviceCommandProtocol p = null;
            retry:
            try
            {
                p = new DeviceCommandProtocol(PortName);
                while (work_status == DeviceStatus.WAITING_FOR_COMMAND)
                {
                    DeviceInfo info = p.GetDeviceInfo();
                    if (info.type != this.TypeId || info.SerialNumber != this.SerialNumber || info.SerialYear != this.SerialYear || info.ModelVersion != this.ModelVersion)
                    {
                        work_status = DeviceStatus.DISCONNECTED;
                    }
                    Debug.WriteLine($"Попыток на отправку гы гы: {tryTimes};");
                    tryTimes = 50;
                }
                p.Dispose();
            }
            catch
            {
                if (p != null) p.Dispose();
                if (tryTimes-- > 0) goto retry;
                work_status = DeviceStatus.DISCONNECTED;
            }
        }

        public override DeviceXMLState GetXMLState()
        {
            return new DeviceXMLState(this);
        }
    }
}
