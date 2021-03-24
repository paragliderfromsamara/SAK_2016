using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;

namespace NormaLib.Measure
{


    public class MeasurePointMap
    {
        private int currentPoint;
        private int elementsAmount;
        private int measurePointsPerElement;
        private int measurePointsAmount;

        public int CurrentElementMeasurePointIndex => currentPoint % measurePointsPerElement;
        public int CurrentElementIndex => currentPoint / measurePointsPerElement;

        public int CurrentElementNumber => CurrentElementIndex + 1;
        public int CurrentMeasurePointNumber => CurrentElementMeasurePointIndex + 1;

        public int CurrentPoint => currentPoint;

        public int MeasurePointsPerElement => measurePointsPerElement;

        public EventHandler OnMeasurePointChanged;

        public bool NextPointEnabled => currentPoint+1 < measurePointsAmount;
        public bool NextElementEnabled => CurrentElementIndex + 1 < elementsAmount;
        public bool PrevElementEnabled => CurrentElementIndex > 0;
        public bool PrevPointEnabled => currentPoint > 0;

        private string elementTitleMask = string.Empty;
        private string elementMeasureTitleMask = string.Empty;

        public string CurrentElementTitle => elementTitleByElNumber(CurrentElementNumber);
        public string CurrentMeasureTitle => measureTitleByMeasureNumber(CurrentMeasurePointNumber);

        private string measureTitleByMeasureNumber(int currentMeasurePointNumber)
        {
            if (measurePointsPerElement == 1)
                return "";
            else
                return $"{elementMeasureTitleMask} {currentMeasurePointNumber}";
        }

        private string elementTitleByElNumber(int el_number) => $"{elementTitleMask} {el_number}";

        public MeasurePointMap(CableStructure structure, uint parameter_type_id, int start_point = 0)
        {
            currentPoint = start_point;
            measurePointsPerElement = MeasuredParameterType.MeasurePointNumberPerStructureElement(parameter_type_id, structure.StructureType.StructureLeadsAmount);
            elementsAmount = (int)structure.RealAmount;
            measurePointsAmount = measurePointsPerElement * elementsAmount;

            elementTitleMask = $"{structure.StructureType.StructureTypeName}";
            elementMeasureTitleMask = (structure.StructureTypeId == CableStructureType.Quattro && measurePointsPerElement == 2) ? "Пара" : "Жила";
            
        }

        public void SetNextElement()
        {
            if (NextElementEnabled) SetMeasurePoint(measurePointsPerElement * (CurrentElementIndex + 1));
            else throw new MeasurePointException("Следующий элемент находится вне диапазона доступных элементов");
        }

        public void SetNextPoint()
        {
            if (NextPointEnabled) SetMeasurePoint(currentPoint+1);
            else throw new MeasurePointException("Следующая точка находится вне диапазона доступных");
        }

        public void SetPrevPoint()
        {
            if (PrevPointEnabled) SetMeasurePoint(currentPoint - 1);
            else throw new MeasurePointException("Предыдущая точка находится вне диапазона доступных");
        }

        public void SetPrevElement()
        {
            if (PrevElementEnabled) SetMeasurePoint(measurePointsPerElement * (CurrentElementIndex - 1));
            else throw new MeasurePointException("Предыдущий элемент находится вне диапазона доступных элементов");
        }

        private void SetMeasurePoint(int next_point)
        {
            MeasurePointEventArgs a = new MeasurePointEventArgs();
            a.PrevPoint = currentPoint;
            a.PrevElementIndex = CurrentElementIndex;
            a.PrevElementMeasurePointIndex = CurrentElementMeasurePointIndex;
            currentPoint = next_point;
            OnMeasurePointChanged?.Invoke(this, a);
        }

        public void SetMeasurePoint(int element_index, int element_point_index)
        {
            int next_point = element_index * measurePointsPerElement + element_point_index;
            if (next_point < measurePointsAmount)
                SetMeasurePoint(next_point);
            else
                throw new MeasurePointException("Новая точка находится за пределами диапазона");
        }
    }

    public class MeasurePointException : Exception
    {
        public MeasurePointException(string message) : base(message)
        {

        }
    }

    public class MeasurePointEventArgs : EventArgs
    {
        public int PrevPoint;
        public int PrevElementIndex;
        public int PrevElementMeasurePointIndex;

        public MeasurePointEventArgs() : base()
        {

        }
    }
}
