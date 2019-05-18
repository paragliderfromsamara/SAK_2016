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

        public byte PairCommutatorElement_First = 1;
        public byte PairCommutatorElement_Last = 1;
        public byte PairCommutatorElementsInUseCount => (byte)(PairCommutatorElement_Last - PairCommutatorElement_First);
        public MeasuredParameterType ParameterType;
        public List<SACMeasurePoint> MeasurePoints;
        public int TimesForOnePoint = 1;
        public LeadCommutationType LeadCommType;
        public SACCommutationType CommutationType;
        public FrequencyRange[] FreqRanges;

        public SACMeasurePointCollection(MeasuredParameterType pType, LeadCommutationType leadType, SACCommutationType commType)
        {
            MeasurePoints = new List<SACMeasurePoint>();
            MakePointsForParameterType();
        }

        private void MakePointsForParameterType()
        {
            switch(ParameterType.ParameterTypeId)
            {
                case MeasuredParameterType.Co:
                case MeasuredParameterType.Rleads:
                    Make_LeadPoints();
                    break;
                case MeasuredParameterType.Ao:
                case MeasuredParameterType.Az:
                    Make_AoAz_Points();
                    break;
                case MeasuredParameterType.Cp:
                case MeasuredParameterType.Ea:
                case MeasuredParameterType.al:
                    MakePairPoints();
                    break;
            }
        }

        private void Make_LeadPoints()
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

        private void MakePairPoints()
        {
            for (int i = 0; i < PairCommutatorElementsInUseCount; i++)
            {
                SACMeasurePoint point = BuildPoint();
                point.PairCommutatorPosition_1 = (byte)(PairCommutatorElement_First + i); ;
                point.LeadCommType = LeadCommutationType.AB;
                MeasurePoints.Add(point);
            }
        }

        private void Make_AoAz_Points()
        {
            for (int recPair = 0; recPair < PairCommutatorElementsInUseCount; recPair++)
            {
                int firstGenPair = (MeasuredParameterType.Ao == ParameterType.ParameterTypeId) ? recPair + 1 : 0;
                for (int genPair= firstGenPair; genPair < PairCommutatorElementsInUseCount; genPair++  )
                {
                    if (recPair == genPair) continue;
                    foreach(FrequencyRange r in FreqRanges)
                    {
                        SACMeasurePoint point = Make_AoAzPoint((byte)(PairCommutatorElement_First + recPair), (byte)(PairCommutatorElement_First + genPair), r);
                        MeasurePoints.Add(point);
                    }
                }
            }
        }

        private SACMeasurePoint Make_AoAzPoint(byte rec_pair, byte gen_pair, FrequencyRange freq_range)
        {
            SACMeasurePoint point = BuildPoint();
            point.PairCommutatorPosition_1 = rec_pair;
            point.PairCommutatorPosition_2 = gen_pair;
            point.FreqRange = freq_range;
            return point;
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
