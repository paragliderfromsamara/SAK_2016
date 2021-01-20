using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;
using NormaMeasure.Devices;
using NormaMeasure.Devices.Microohmmeter;

namespace NormaMeasure.Devices.XmlObjects
{
    public class MicroohmmeterXMLState : DeviceXMLState
    {
        public MicroohmmeterXMLState(Microohmmeter_uOmM_01m device) : base(device)
        {

        }
        protected override void fillXMLDocument()
        {
            base.fillXMLDocument();
        }

        protected override void buildFromXML()
        {
            base.buildFromXML();
        }
    }
}
