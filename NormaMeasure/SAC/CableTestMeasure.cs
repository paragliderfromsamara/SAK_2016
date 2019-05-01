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
         
            foreach (MeasuredParameterType pType in cableTest.TestProgramMeasuredParameterTypes)
            {
                currentParameter = pType;
                foreach (TestedCableStructure structure in cableTest.TestedCable.CableStructures.Rows)
                {
                    currentStructure = structure;
                    if (!measureCurrentStructure()) goto finish;
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
            finish:
            CableTestFinished?.Invoke(this);
        }

        /// <summary>
        /// Измерение параметров текущей структуры кабеля
        /// </summary>
        private bool measureCurrentStructure()
        {
            Random rnd = new Random();
            for (uint element=1; element <= currentStructure.RealAmount; element++)
            {
                for (uint subElement = 1; subElement <= currentStructure.StructureType.StructureLeadsAmount; subElement++)
                {
                    if (IsWillFinished) return false;
                    CurrentMeasurePoint = cableTest.BuildTestResult(currentParameter, currentStructure, element, subElement);
                    CurrentMeasurePoint.Result = rnd.Next(10, 50);
                    
                    cableTest.AddResult(CurrentMeasurePoint);

                    //Result_Gotten?.Invoke(this);
                    Thread.Sleep(300);
                    
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
            return true;
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

        public CableTestResult LastResult
        {
            set
            {
                lastResult = value;
                if (lastResult != null)Result_Gotten?.Invoke(this);
            }
            get
            {
                return lastResult;
            }
        }

        public CableTestResult CurrentMeasurePoint
        {
            get
            {
                return currentResult;
            }set
            {
                if (currentResult != null) LastResult = currentResult;
                currentResult = value;
                MeasurePointChanged?.Invoke(this);
            }
        }

        private CableTestResult currentResult;
        private CableTestResult lastResult;

        private TestedCableStructure currentStructure
        {
            get
            {
                return _currentStructure;
            }
            set
            {
                if (_currentStructure != value)
                {
                    _currentStructure = value;
                    CurrentStructureChanged?.Invoke(this);
                }
            }
        }

        private MeasuredParameterType currentParameter
        {
            get
            {
                return _currentParameter;
            }
            set
            {
                if (_currentParameter != value)
                {
                    _currentParameter = value;
                    CurrentParameterChanged?.Invoke(this);
                }
            }
        }


        private TestedCableStructure _currentStructure;
        private MeasuredParameterType _currentParameter;

        private uint structureElementNumber;
        private uint currentTableConnector;
        private uint cableMeasureCycle;



        public event CableTestMeasure_Handler Result_Gotten;
        public event CableTestMeasure_Handler MeasurePointChanged;
        public event CableTestMeasure_Handler CurrentParameterChanged;
        public event CableTestMeasure_Handler CurrentStructureChanged;

        public event CableTestMeasure_Handler CableTestFinished;
    }
}
