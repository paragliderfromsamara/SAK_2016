using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NormaMeasure.SAC_APP
{
    public class measureMain  
    {
        public double[] measureMemory;
        //private int maxMeasMemIndex = 100;


        public string[] measuredParameters = {"Rж", "C0", "Cр", "Rиз", "al", "A0", "Aз", "ВсВИ" };
        public double[] freqRange = {0.8f, 10, 40, 128, 256, 512, 1024, 2048};
        public int minFreqStep = 4;
        //параметры для измерения
        public string fullMeasureModeName { set; get; }  //полное название режима
        public string shortMeasureModeName { set; get; } //короткое название режима
        public string quantitativeMeasure { set; get; }  //Мера измерения
        public bool showLeadSelectList { set; get; }     // скрыть/показать выпадающий список номера жилы
        public bool showPair2SelectList { set; get; }  // скрыть/показать выпадающий список конечных пар
        public string pair1Label {set; get;}             // обозначение первой выбраной пары
        public string pair2Label { set; get; }           // обозначение второй выбраной пары



        public bool isNotOnlySplitTable { set; get;} //false - может быть только с ДК, false - может быть и с ДК и без ДК
        public bool mergeTableFlag { set; get; }     //true - без ДК, false - c ДК

        
        public bool isNeedFreqMenu { set; get; } //true - показывать меню частотных параметров, false - скрывать меню частотных параметров 
        public double startFreq { get; set; }    //начальная частота
        public double endFreq { get; set; }      //конечная частота
        public double stepFreq { get; set; }     //шаг частоты

        public int leadNumber { set; get; }              // номер жилы
        public int commutatorSetCommand { set; get; }    // команда установки коммутатора

        public int startPair { set; get; } //начальная пара
        public int endPair { set; get; }   //конечная пара
        public int cableLength { set; get; } //длина кабеля
        public float coeffA { set; get; }//коэффициент А приведения результата
        public float coeffB { set; get; } //коэффициент B приведения результата


        //Диапазоны
        public bool isHasRangeList { set; get; } //имеет ли диапазоны данный вид измерения
        public int[] rangesCommanList { set; get; } //список комманд установки диапазона
        public int[] rangesEdgeValues { set; get; } //значения переходов на следующий диапазон
        //для тестового испытания
        public int[] fakeValueRange { set; get; } //задает диапазон возможных значений
        


        public measureMain()
        {
            this.fullMeasureModeName = "N/A";
            this.shortMeasureModeName = "N/A";
            this.quantitativeMeasure = "N/A";



            this.isNotOnlySplitTable = false;
 
            this.showLeadSelectList = true;
            this.showPair2SelectList = true;
            this.pair1Label = "с";
            this.pair2Label = "по";

            this.cableLength = 1000;
            //freqMenu
            this.startFreq = 10;
            this.endFreq = 2000;
            this.stepFreq = 40;
            //freqMenu end
            this.leadNumber = 0;
            this.startPair = 1;
            this.endPair = 1;

            this.coeffA = 1.0f;
            this.coeffB = 0.0f;

            this.fakeValueRange = new int[] {5000, 6000};
        }




        public double convertResult(double r)
        {
            return (r*this.coeffA - this.coeffB);
        }

        public int getResult()
        {
            return 1000;
        }

        public void stopMeasure() //Остановка испытаний
        { 
            //to_do
        }

        public int getFakeResult() //Берем случайное значение
        {
            int rslt;
            Random result = new Random();
            rslt = result.Next(this.fakeValueRange[0], this.fakeValueRange[1]);
            return rslt;
        }

        private void initMeasureMemory()
        {
            
        }
    }

}
