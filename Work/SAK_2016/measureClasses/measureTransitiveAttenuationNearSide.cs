using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAK_2016 
{
    class measureTransitiveAttenuationNearSide : measureMain //Измерение A0
    {
        public measureTransitiveAttenuationNearSide()
        {
            this.fullMeasureModeName = "A0";
            this.shortMeasureModeName = "Переходное затухание на ближнем конце";
            this.quantitativeMeasure = "дБ";

            this.pair1Label = "генератор";
            this.pair2Label = "приёмник"; 

            this.showLeadSelectList = false;
            this.isNotOnlySplitTable = true;
            this.isNeedFreqMenu = true;
            this.startPair = 1;
            this.endPair = 1;

            this.coeffA = 1.0f;
            this.coeffB = 0.0f;
        }
    }
}
