using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NormaMeasure.SAC_APP
{
    class measureTransitiveAttenuationFarSide : measureMain
    {
        public measureTransitiveAttenuationFarSide()
        {
            this.fullMeasureModeName = "Защищенность на дальнем конце";
            this.shortMeasureModeName = "Aз";
            this.quantitativeMeasure = "дБ";

            this.pair1Label = "генератор";
            this.pair2Label = "приёмник"; 

            this.isNeedFreqMenu = true;
            this.startPair = 1;
            this.endPair = 1;

            this.coeffA = 1.0f;
            this.coeffB = 0.0f;
        }
    }
}
