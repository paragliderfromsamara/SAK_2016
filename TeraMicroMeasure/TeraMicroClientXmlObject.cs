using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;

namespace TeraMicroMeasure
{
    class TeraMicroClientXmlObject : NormaXmlObject
    {
        private string operatorName;
        public string OperatorName
        {
            get
            {
                return operatorName;
            }
            set
            {
                operatorName = value;
                setXmlProp("operatorName", value);
            }
        }
    }
}
