using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.Utils;
using NormaLib.Devices;
using NormaLib.Devices.Teraohmmeter;

namespace NormaLib.Devices.XmlObjects
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
