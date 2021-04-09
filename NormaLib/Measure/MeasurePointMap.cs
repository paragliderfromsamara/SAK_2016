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
        private MeasurePoint currentPoint; //private int currentPoint;
        private int elementsAmount;
        private int measurePointsPerElement;
        private int measurePointsAmount;

        public int CurrentElementMeasurePointIndex => currentPoint.MeasureIndex;
        public int CurrentElementIndex => currentPoint.ElementIndex;

        public int CurrentElementNumber => currentPoint.ElementNumber;
        public int CurrentMeasurePointNumber => currentPoint.MeasureNumber;

        public MeasurePoint CurrentPoint => currentPoint;

        public int MeasurePointsPerElement => measurePointsPerElement;

        public EventHandler OnMeasurePointChanged;

        public bool NextPointEnabled => currentPoint.PointIndex + 1 < measurePointsAmount;
        public bool NextElementEnabled => currentPoint.ElementIndex + 1 < elementsAmount;
        public bool PrevElementEnabled => currentPoint.ElementIndex > 0;
        public bool PrevPointEnabled => currentPoint.PointIndex > 0;

        private string elementTitleMask = string.Empty;
        private string elementMeasureTitleMask = string.Empty;

        //public string CurrentElementTitle => elementTitleByElNumber(CurrentElementNumber);
        //public string CurrentMeasureTitle => measureTitleByMeasureNumber(CurrentMeasurePointNumber);

        private string measureTitleByMeasureNumber(int currentMeasurePointNumber)
        {
            if (measurePointsPerElement == 1)
                return "";
            else
                return $"{elementMeasureTitleMask} {currentMeasurePointNumber}";
        }

        private string elementTitleByElNumber(int el_number) => $"{elementTitleMask} {el_number}";

        private CableStructure current_structure;
        public CableStructure CurrentStructure => current_structure;

        private uint parameterTypeId;
        public uint ParameterTypeId => parameterTypeId;

        public MeasurePointMap(CableStructure structure, uint parameter_type_id, int start_point_index = 0)
        {
            parameterTypeId = parameter_type_id;
            current_structure = structure;
            
            measurePointsPerElement = MeasuredParameterType.MeasurePointNumberPerStructureElement(parameter_type_id, structure.StructureType.StructureLeadsAmount);
            elementsAmount = (int)structure.RealAmount;
            measurePointsAmount = measurePointsPerElement * elementsAmount;

            elementTitleMask = $"{structure.StructureType.StructureTypeName}";
            elementMeasureTitleMask = (structure.StructureTypeId == CableStructureType.Quattro && measurePointsPerElement == 2) ? "Пара" : "Жила";
            currentPoint = BuildPointByIndex(start_point_index);
        }

        public void SetNextElement()
        {
            if (NextElementEnabled) SetMeasurePoint(measurePointsPerElement * (CurrentElementIndex + 1));
            else throw new MeasurePointException("Следующий элемент находится вне диапазона доступных элементов");
        }

        public void SetNextPoint()
        {
            if (NextPointEnabled) SetMeasurePoint(currentPoint.PointIndex+1);
            else throw new MeasurePointException("Следующая точка находится вне диапазона доступных");
        }

        public void SetPrevPoint()
        {
            if (PrevPointEnabled) SetMeasurePoint(currentPoint.PointIndex - 1);
            else throw new MeasurePointException("Предыдущая точка находится вне диапазона доступных");
        }

        public void SetPrevElement()
        {
            if (PrevElementEnabled) SetMeasurePoint(measurePointsPerElement * (CurrentElementIndex - 1));
            else throw new MeasurePointException("Предыдущий элемент находится вне диапазона доступных элементов");
        }

        public bool TryGetNextPoint()
        {
            try
            {
                SetNextPoint();
                return true;
            }catch(MeasurePointException)
            {
                return false;
            }
        }

        private void SetMeasurePoint(int next_point)
        {
            MeasurePoint ? prev = null, next = null;
            prev = currentPoint;
            if (NextElementEnabled) next = BuildPointByIndex(next_point + 1);
            currentPoint = BuildPointByIndex(next_point);
            OnMeasurePointChanged?.Invoke(this, new MeasurePointEventArgs(currentPoint, prev, next));
        }

        private MeasurePoint BuildPointByIndex(int next_point)
        {
            MeasurePoint point = new MeasurePoint();
            point.PointIndex = next_point;
            point.ElementIndex = next_point / measurePointsPerElement;
            point.MeasureIndex = next_point % measurePointsPerElement;
            point.ElementTitle = elementTitleByElNumber(point.ElementNumber);
            point.MeasureTitle = measureTitleByMeasureNumber(point.MeasureNumber);
            point.StructureId = current_structure.CableStructureId;
            point.ParameterTypeId = parameterTypeId;
            return point;
        }

        public void SetMeasurePoint(MeasurePoint point)
        {
            SetMeasurePoint(point.ElementIndex, point.MeasureIndex);
        }

        public void SetMeasurePoint(int element_index, int element_point_index)
        {
            int next_point = GetPointIndex(element_index, element_point_index);
            if (next_point < measurePointsAmount)
                SetMeasurePoint(next_point);
            else
                throw new MeasurePointException("Новая точка находится за пределами диапазона");
        }
        public int GetPointIndex(uint element_number, uint element_point_number)
        {
            element_number--;
            element_point_number--;
            return GetPointIndex((int)element_number, (int)element_point_number);
        }
        public int GetPointIndex(int element_index, int element_point_index)
        {
            return element_index * measurePointsPerElement + element_point_index;
        }

        public MeasurePoint GetPointByElementIndex(int element_index, int element_point_index) => BuildPointByIndex(GetPointIndex(element_index, element_point_index));
    }

    public struct MeasurePoint
    {
        public int ElementIndex;
        public int MeasureIndex;
        public int ElementNumber => ElementIndex + 1;
        public int MeasureNumber => MeasureIndex + 1;
        public string ElementTitle;
        public string MeasureTitle;
        public int PointIndex;
        public uint StructureId;
        public uint ParameterDataId;
        public uint ParameterTypeId;
    }

    public class MeasurePointException : Exception
    {
        public MeasurePointException(string message) : base(message)
        {

        }
    }

    public class MeasurePointEventArgs : EventArgs
    {
        public MeasurePoint ? PrevPoint;
        public MeasurePoint CurrentPoint;
        public MeasurePoint ? NextPoint;
        public MeasurePointEventArgs(MeasurePoint current, MeasurePoint ? prev, MeasurePoint ? next) : base()
        {
            CurrentPoint = current;
            NextPoint = next;
            PrevPoint = prev;
        }
    }
}
