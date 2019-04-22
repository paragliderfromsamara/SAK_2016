using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.MeasureControl;
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.MeasureControl.SAC
{
    class CableTestMeasure : MeasureBase
    {
         
        public CableTestMeasure() : base()
        {
            
        }




        /// <summary>
        /// Кабель который планируется протестировать
        /// </summary>
        public Cable SelectedCable
        {
            get
            {
                return selectedCable;
            }
            set
            {
                selectedCable = value;
            }
        }

        /// <summary>
        /// Испытание кабеля из БД
        /// </summary>
        public CableTest CableTest
        {
            set
            {
                cableTest = value;
            }
            get
            {
                return cableTest;
            }
        }


        /// <summary>
        /// Кабель который планируется протестировать
        /// </summary>
        protected Cable selectedCable;
        /// <summary>
        /// Тестируемый кабель, создается из selectedCable в процессе испытаний
        /// </summary>
        protected TestedCable testedCable;

        /// <summary>
        /// Испытание кабеля
        /// </summary>
        public CableTest cableTest;

        public int tableCapacity;
        public int cableConnectedFrom_ElementNumber;
        public bool isSplittedTable;

    }
}
