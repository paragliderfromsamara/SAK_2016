using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;

namespace TeraMicroMeasure.XmlObjects
{
    class MeasureState : NormaXmlObject
    {
        public int MeasureType
        {
            set
            {
                setXmlProp("measure_type", value.ToString());
            }get
            {
                int v = 0;
                tryGetIntXmlProp("measure_type", out v);
                return v;
            }
        }

        public int CableId
        {
            set
            {
                setXmlProp("cable_id", value.ToString());
            }
            get
            {
                int v = 0;
                tryGetIntXmlProp("cable_id", out v);
                return v;
            }
        }

        public int CableLength
        {
            set
            {
                setXmlProp("cable_length", value.ToString());
            }
            get
            {
                int v = 1000;
                tryGetIntXmlProp("cable_length", out v);
                if (v == 0) v = 1000;
                return v;
            }
        }

        public int MeasureStatus
        {
            set
            {
                setXmlProp("measure_status", value.ToString());
            }
            get
            {
                int v = 0;
                tryGetIntXmlProp("measure_status", out v);
                return v;
            }
        }

        public MeasureState() : base()
        {

        }

        public MeasureState(string inner_xml) : base(inner_xml)
        {

        }

        
    }
}
