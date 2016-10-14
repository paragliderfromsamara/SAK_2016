using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAK_2016
{
    class measureWorkAttenuation : measureMain //al - рабочее затухание
    {
        public measureWorkAttenuation()
        {
            this.fullMeasureModeName = "Рабочее затухание";
            this.shortMeasureModeName = "al";
            this.quantitativeMeasure = "дБ";

            this.pair1Label = "измеряемая пара";
            this.showPair2SelectList = false;

            this.isNeedFreqMenu = true;
            this.startPair = 1;
            this.endPair = 1;

            this.coeffA = 1.0f;
            this.coeffB = 0.0f;
        }
    }
}
