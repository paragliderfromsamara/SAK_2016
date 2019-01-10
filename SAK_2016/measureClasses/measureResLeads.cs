using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NormaMeasure.SAC_APP
{
    public partial class measureResLeads : measureMain
    {
        public measureResLeads()
        {
            this.fullMeasureModeName = "Сопротивление жил";
            this.shortMeasureModeName = "Rж";
            this.quantitativeMeasure = "Ом";

            this.showLeadSelectList = true;
            this.showPair2SelectList = false;
            this.pair1Label = "измеряемая пара";
            this.isNotOnlySplitTable = true;


            this.cableLength = 1000;

            this.leadNumber = 0;
            this.startPair = 1;
            this.endPair = 1;

            this.coeffA = 1.0f;
            this.coeffB = 0.0f;
        }
    }
}
