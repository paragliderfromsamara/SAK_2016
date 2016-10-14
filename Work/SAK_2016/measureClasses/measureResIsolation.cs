using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAK_2016
{
    class measureResIsolation : measureMain
    {
        public measureResIsolation()
        {
            this.fullMeasureModeName = "Сопротивление изоляции";
            this.shortMeasureModeName = "Rиз";
            this.quantitativeMeasure = "ГОм";

            this.isNotOnlySplitTable = true;



            this.startPair = 1;
            this.endPair = 1;

            this.coeffA = 1.0f;
            this.coeffB = 0.0f;
        }
    }
}
