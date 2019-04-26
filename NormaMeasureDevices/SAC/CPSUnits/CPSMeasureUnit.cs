using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.Devices.SAC.CPSUnits
{
    public class CPSMeasureUnit : CPSUnit
    {
        protected uint[] allowedMeasuredParameters;
        private MeasuredParameterType currentParameter;
        protected int currentRangeId;

        public CPSMeasureUnit(SACCPS _cps) : base(_cps)
        { 
            SetAllowedMeasuredParameters(); //Устанавливаем все доступные для узла параметры
        }

        protected override void InitUnit()
        {
            base.InitUnit();
            SetAllowedMeasuredParameters();
        }


        /// <summary>
        /// Алгоритм для измерения
        /// </summary>
        /// <param name="result"></param>
        /// <param name="pType"></param>
        /// <param name="isEtalonMeasure">true - измеряется эталон, false - измеряется стол</param>
        /// <returns></returns>
        public virtual bool MakeMeasure(ref double result, MeasuredParameterType pType, bool isEtalonMeasure = false)
        {
            //if (IsAllowedParameter(pType.ParameterTypeId)) return false;
            currentParameter = pType;
            
            //PrepareCommutator(isEtalonMeasure);
           // result = 
            return true;
        }

        protected virtual double GetUnitResult()
        {
            return 0; 
        }



        /// <summary>
        /// Алгоритм измерения эталона
        /// </summary>
        private double MakeEtalonMeasure()
        {
            return 0;
        }


        private double MakeTableMeasure()
        {
            return 0;
        }

        /// <summary>
        /// Поддерживает ли модуль параметр с указанным id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsAllowedParameter(uint id)
        {
            return allowedMeasuredParameters.Contains(id);
        }

        /// <summary>
        /// Установка доступных ихмеряемых параметров
        /// </summary>
        protected virtual void SetAllowedMeasuredParameters() { allowedMeasuredParameters = new uint[] { }; }

        

    }

    public enum CPSMeasureUnit_Status : Int32
    {
        UnloadReleRizolNotWork = 0xFFFC,
        NotAnswer = 0xFFFD,
        InProcess = 0xFFFE,
        GTVLineInNull = 0xFFFF
    }
}
