using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;
using NormaMeasure.Devices.SAC;



namespace NormaMeasure.Devices.SAC
{
    public delegate void SACMeasurePoint_Handler(SACMeasurePoint point);
    public class SACMeasurePoint
    {
        public SACMeasurePoint()
        {

        }

        /// <summary>
        /// Тип измеряемого параметра
        /// </summary>
        public MeasuredParameterType ParameterType
        {
            get
            {
                return parameterType;
            }
            set
            {
                if (value == null) return;
                if (parameterType != value)
                {
                    parameterType = value;
                    ParameterType_Changed?.Invoke(this);
                    changed = true;
                }
            }
        }

        private MeasuredParameterType parameterType;
        
        /// <summary>
        /// Результат с АЦП
        /// </summary>
        public double RawResult;
        
        /// <summary>
        /// Результат после применения коэффициентов коррекции
        /// </summary>
        public double ConvertedResult
        {
            get
            {
                return convertedResult;
            }
            set
            {
                if (convertedResult != value)
                {
                    convertedResult = value;
                    Result_Changed?.Invoke(this);
                    changed = true;
                }
            }
        }
        private double convertedResult;


        /// <summary>
        /// Тип коммутации
        /// </summary> 
        public SACCommutationType CommutationType
        {
            get
            {
                return commType;
            }
            set
            {
                if (commType != value)
                {
                    commType = value;
                    CommutationType_Changed?.Invoke(this);
                    changed = true;
                }
            }
        }

        private SACCommutationType commType = SACCommutationType.WithFarEnd;

        public byte PairCommutatorPosition_1
        {
            get
            {
                return pairCommPos_1;
            }
            set
            {
                if (value != pairCommPos_1)
                {
                    pairCommPos_1 = value;
                    PairCommutatorPosition_Changed?.Invoke(this);
                    changed = true;
                }
            }
        }
             
        private byte pairCommPos_1=1;

        public byte PairCommutatorPosition_2
        {
            get
            {
                return pairCommPos_2;
            }
            set
            {
                if (value != pairCommPos_2)
                {
                    pairCommPos_2 = value;
                    PairCommutatorPosition_Changed?.Invoke(this);
                    changed = true;
                }
            }
        }


        private byte pairCommPos_2=0;


        public LeadCommutationType LeadCommType
        {
            get
            {
                return leadCommType;
            }
            set
            {
                if (value != leadCommType)
                {
                    leadCommType = value;
                    LeadCommutationType_Changed?.Invoke(this);
                    changed = true;
                }
            }
        }

        public LeadCommutationType leadCommType = LeadCommutationType.A;



        public CableStructureType structureType;
        public int CableElementNumber = 0;

        public float CurrentFrequency;
        public float FrequencyMin;
        public float FrequencyMax;
        public float FrequencyStep;
        private int Wave_Resistance = 0;

        public int WaveResistance
        {
            get
            {
                return Wave_Resistance;
            }set
            {
                if (value != Wave_Resistance)
                {
                    Wave_Resistance = value;
                    changed = true;
                }
            }
        }



        public CallingSubModes CallingSubMode = CallingSubModes.Short;

        public string LeadCommTypeText
        {
            get
            {
                if (leadCommType == LeadCommutationType.A) return "Жила А";
                else if (leadCommType == LeadCommutationType.B) return "Жила B";
                else return "";
            }
        }

        public string CommutationTypeText
        {
            get
            {
                switch (CommutationType)
                {
                    case SACCommutationType.Etalon:
                        return "Эталон";
                    case SACCommutationType.NoFarEnd:
                        return "Без ДК";
                    case SACCommutationType.WithFarEnd:
                        return "C ДК";
                    default:
                        return "Неизвестно";
                }
            }
        }

        private bool changed
        {
            set
            {
                if (value) Changed?.Invoke(this);
            }
        }

        public event SACMeasurePoint_Handler Changed;
        public event SACMeasurePoint_Handler ParameterType_Changed;
        public event SACMeasurePoint_Handler Result_Changed;
        public event SACMeasurePoint_Handler CommutationType_Changed;
        public event SACMeasurePoint_Handler PairCommutatorPosition_Changed;
        public event SACMeasurePoint_Handler LeadCommutationType_Changed;
    }
}
