using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.MeasureControl;
using NormaMeasure.DBControl.Tables;
using System.Threading;

namespace NormaMeasure.MeasureControl.SAC
{
    public delegate void CableTestMeasure_Handler(CableTestMeasure measure);
    public class CableTestMeasure : MeasureBase
    {
         
        public CableTestMeasure(CableTest test) : base()
        {
            cableTest = test;
            MeasureBody = measureFunction;

        }

        private void measureFunction()
        {
           currentTableConnector = cableTest.CableConnectedFrom;
         
            foreach (MeasuredParameterType pType in cableTest.MeasuredParameterTypes)
            {
                currentParameter = pType;

                foreach (TestedCableStructure structure in cableTest.TestedCable.CableStructures.Rows)
                {
                    currentStructure = structure;
                    measureCurrentStructure();
                    // if (structure.MeasuredParameters_ids.Contains(pType.ParameterTypeId))
                    // {
                    //     currentStructure = structure;
                    //     measureCurrentStructure();
                    // }else
                    // {
                    //currentTableConnector += structure.StructureType;
                    // }
                }

            }
        }

        private void measureCurrentStructure()
        {
            Random rnd = new Random();
            System.Windows.Forms.MessageBox.Show("measureCurrentStructure");
            for (uint element=0; element < currentStructure.RealAmount; element++)
            {
                for (uint subElement = 0; subElement < currentStructure.StructureType.StructureLeadsAmount; subElement++)
                {
                    CableTestResult r = cableTest.BuildTestResult(currentParameter, currentStructure, element, subElement);
                    r.Result = rnd.Next(10, 50);
                    cableTest.AddResult(r);
                    lastResult = r;
                    Thread.Sleep(800);

                    //currentStructure
                }
                /*
                switch(currentStructure.StructureTypeId)
                {
                    case CableStructureType.Lead:
                        break;
                    case CableStructureType.Pair:
                        break;
                    case CableStructureType.Triplet:
                        break;
                    case CableStructureType.Quattro:
                    case CableStructureType.HightFreqQuattro:
                        break;
                } */
            }
        }


       


        private void measureLead(uint elNumber)
        {

        }

        private void measurePair(uint elNumber)
        {

        }

        private void measureTriplet(uint elNumber)
        {

        }

        private void measureQuattro(uint elNumber)
        {

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
        /// Испытание кабеля
        /// </summary>
        public CableTest cableTest;

        private TestedCableStructure currentStructure;
        private MeasuredParameterType currentParameter;
        private uint structureElementNumber;
        private uint currentTableConnector;
        private uint cableMeasureCycle;

        public CableTestResult LastResult => lastResult;
        private CableTestResult lastResult
        {
            set
            {
                lastResult = value;
            }
            get
            {
                return lastResult;
            }
        }


        private event CableTestMeasure_Handler Result_Gotten;
    }
}
