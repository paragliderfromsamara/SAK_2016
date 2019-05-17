using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.Devices.SAC
{
    public class SACMeasurePointCollection
    {
        public double MinLimit = double.MinValue;
        public double MaxLimit = double.MaxValue;
        public byte PairCommutatorElement_First = 1;
        public byte PairCommutatorElement_Last = 1;
        public byte PairCommutatorElementsInUseCount => (byte)(PairCommutatorElement_Last - PairCommutatorElement_First);
        public MeasuredParameterType ParameterType;
        public List<SACMeasurePoint> MeasurePoints;
        public int TimesForOnePoint = 1;
        public LeadCommutationType LeadCommType;
        public SACCommutationType CommutationType;

        public SACMeasurePointCollection(MeasuredParameterType pType, LeadCommutationType leadType, SACCommutationType commType)
        {
            MeasurePoints = new List<SACMeasurePoint>();
            MakePointsForParameterType();
        }

        private void MakePointsForParameterType()
        {
            switch(ParameterType.ParameterTypeId)
            {
                case MeasuredParameterType.Rleads:
                    Make_RleadPoints();
                    break;
            }
        }

        private void Make_RleadPoints()
        {
            for(int i = 0; i< PairCommutatorElementsInUseCount; i++ )
            {
                byte CurPosition = (byte)(PairCommutatorElement_First + i);
                if (LeadCommType == LeadCommutationType.A || LeadCommType == LeadCommutationType.AB)
                {
                    SACMeasurePoint point = BuildPoint();
                    point.PairCommutatorPosition_1 = CurPosition;
                    point.LeadCommType = LeadCommutationType.A;
                    MeasurePoints.Add(point);
                }
                if (LeadCommType == LeadCommutationType.B || LeadCommType == LeadCommutationType.AB)
                {
                    SACMeasurePoint point = BuildPoint();
                    point.PairCommutatorPosition_1 = CurPosition;
                    point.LeadCommType = LeadCommutationType.B;
                    MeasurePoints.Add(point);
                }
            }
        }

        private SACMeasurePoint BuildPoint()
        {
            SACMeasurePoint point = new SACMeasurePoint();
            point.ParameterType = ParameterType;
            point.CommutationType = CommutationType;
            return point;
        }


    }
}
