using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.Measure
{


    public class MeasurePointMap
    {
        private int currentPoint;
        private int elementsAmount;
        private int measurePointsPerElement;
        private int measurePointsAmount;

        public int CurrentElementMeasurePointIndex => currentPoint % measurePointsPerElement;
        public int CurrentElementIndex => (CurrentElementMeasurePointIndex > 0) ? ((currentPoint / measurePointsPerElement) + 1) : (currentPoint / measurePointsPerElement);

        public int CurrentPoint => currentPoint;

        public EventHandler OnMeasurePointChanged;

        public bool NextPointEnabled => currentPoint+1 < measurePointsAmount;
        public bool NextElementEnabled => CurrentElementIndex + 1 < elementsAmount;
        public bool PrevElementEnabled => CurrentElementIndex - 1 >= 0;
        public bool PrevPointEnabled => currentPoint - 1 >= 0; 

        public MeasurePointMap(int elements_amount, int meas_points_per_elements, int start_point = 0)
        {
            currentPoint = start_point;
            elementsAmount = elements_amount;
            measurePointsPerElement = meas_points_per_elements;
            measurePointsAmount = meas_points_per_elements * elements_amount;
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
