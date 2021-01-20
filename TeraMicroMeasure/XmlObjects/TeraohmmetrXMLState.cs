using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;
using NormaMeasure.Devices;
using NormaMeasure.Devices.Teraohmmeter;

namespace TeraMicroMeasure.XmlObjects
{
    public class TeraohmmeterXMLState : DeviceXMLState
    {
        public TeraohmmeterXMLState(TeraohmmeterTOmM_01 device) : base(device)
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
