using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NormaMeasure.SAC_APP
{
    class measureCapacityZero : measureMain
    {
        public measureCapacityZero()
        {
            this.fullMeasureModeName = "C0";
            this.shortMeasureModeName = "C0";
            this.quantitativeMeasure = "нФ";

            this.showLeadSelectList = true;
            this.isNotOnlySplitTable = true;

            this.leadNumber = 0;
            this.startPair = 1;
            this.endPair = 1;

            this.coeffA = 1.0f;
            this.coeffB = 0.0f;
        }
    }
}
