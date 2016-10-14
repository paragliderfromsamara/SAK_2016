using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAK_2016
{
    class measureHightVoltageTest : measureMain
    {
        public measureHightVoltageTest()
        {
            this.fullMeasureModeName = "Высоковольтные испытания";
            this.shortMeasureModeName = "ВсВИ";
            this.quantitativeMeasure = "В";

            this.startPair = 1;
            this.endPair = 1;

            this.coeffA = 1.0f;
            this.coeffB = 0.0f;
        }
    }
}
