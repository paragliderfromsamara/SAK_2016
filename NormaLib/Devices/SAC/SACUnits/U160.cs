using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NormaLib.DBControl.Tables;
using System.Diagnostics;

namespace NormaLib.Devices.SAC.SACUnits
{


    public class U160 : CPSMeasureUnit
    {
        public U160(SACCPS _cps) : base(_cps)
        {
        }

        public override void SetMeasureMode()
        {
            byte header = (byte)(unitCMD_Address | SACCPS.ReadResult | SACCPS.TwoBytes_cmd);
            byte mode = (byte)(MeasureMode_CMD | currentRanges[selectedRangeIdx].RangeCommand);
            cps.WriteBytes(new byte[] { header, FreqCode, mode });
            Debug.WriteLine($"U160.SetMeasureMode(): Узел {unitName} {UnitId}; Команда {header.ToString("X")} Частота {FreqCode.ToString("X")} Режим {mode.ToString("X")}");
            Thread.Sleep(AFTER_SET_MODE_DELAY);
        }

        protected override void SetUnitAddress()
        {
            unitCMD_Address = 0x03;
        }

        protected override void SetMeasureModeCMDByParameterType()
        {
            base.SetMeasureModeCMDByParameterType();
            switch(CurrentParameterType.ParameterTypeId)
            {
                case MeasuredParameterType.al:
                    MeasureMode_CMD = 0x20;
                    break;
                case MeasuredParameterType.Ao:
                case MeasuredParameterType.Az:
                    MeasureMode_CMD = 0x00;
                    break;
            }
        }

        protected override UnitMeasureRange[] GetDefaultRanges()
        {
            if (CurrentParameterType.ParameterTypeId == MeasuredParameterType.al)
            {
                return new UnitMeasureRange[] { al_LF, al_10_40, al_40_2000 };
            }else
            {
                return new UnitMeasureRange[] { Ao_Az_LF, Ao_Az_10_40, Ao_Az_40_2000 };
            }
        }

        public override void SetUnitStateByMeasurePoint(SACMeasurePoint point)
        {
            base.SetUnitStateByMeasurePoint(point);
            AFTER_SET_MODE_DELAY = 20;
            BETWEEN_ADC_TIME = 20;
            ChangeRangeCounter = ChangeRangeCounterMax = 3;
            WaveResistance = point.WaveResistance;
            CurrentFrequency = point.CurrentFrequency;
            SelectDefaultRange();

        }

        protected override void SetAllowedMeasuredParameters()
        {
            base.SetAllowedMeasuredParameters();
            allowedMeasuredParameters = new uint[]
            {
                MeasuredParameterType.al,
                MeasuredParameterType.Ao,
                MeasuredParameterType.Az
            };
        }

        protected override void SetUnitInfo()
        {
            unitName = "U160";
            unitTitle = "Узел измерения параметров влияния";
            base.SetUnitInfo();
        }

        /// <summary>
        /// Чтобы не пытался искать диапазон по результату - здесь он по частоте
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override bool CheckRange(double result)
        {
            return false;
        }

        protected override void ConvertResult(ref double result, UnitMeasureRange range)
        {
            base.ConvertResult(ref result, range);
            if (CurrentParameterType.ParameterTypeId != MeasuredParameterType.al)
            {
                if (IsEtalonMeasure)
                {
                    result += WaveResistanceCorrCoeff(150);
                }
                else
                {
                    result += WaveResistanceCorrCoeff();
                }
            }
            result += 32;
            result -= cps.sac.table.SelfCorrFile.GetInfluenceCoeff(CurFrequencyAsInteger, WaveResistance, 45);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wave_res"></param>
        /// <returns></returns>
        protected double WaveResistanceCorrCoeff(int wave_res = 0)
        {
            double corr = 45;
            if (wave_res == 0) wave_res = WaveResistance;
            switch (WaveResistance)
            {
                case 150:
                    corr = 45.0;
                    break;
                case 400:
                    corr = 41.75;
                    break;
                case 500:
                    corr = 41.25;
                    break;
                case 600:
                    corr = 40.95;
                    break;
            }
            return corr;
        }


        protected UnitMeasureRange al_LF
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "LF";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 11.77;
                range.BV = 0;
                range.MinValue = 0.8;
                range.MaxValue = 0.8;
                range.RangeTitle = "al 0.8 кГц";
                range.RangeCommand = 0x08;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }

        protected UnitMeasureRange al_10_40
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "HF_10_40";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 11.8;
                range.BV = 0;
                range.MinValue = 10.0;
                range.MaxValue = 40.0;
                range.RangeTitle = "al 10-40 кГц";
                range.RangeCommand = 0x10;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }

        protected UnitMeasureRange al_40_2000
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "HF_40_2000";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 11.8;
                range.BV = 0;
                range.MinValue = 40.0;
                range.MaxValue = 2000.0;
                range.RangeTitle = "al 40-2000 кГц";
                range.RangeCommand = 0x00;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }

        protected UnitMeasureRange Ao_Az_LF
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "LF";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 11.77;
                range.BV = 0;
                range.MinValue = 0.8;
                range.MaxValue = 0.8;
                range.RangeTitle = "Ao, Az 0.8 кГц";
                range.RangeCommand = 0x08;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }

        protected UnitMeasureRange Ao_Az_10_40
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "HF_10_40";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 11.8;
                range.BV = 0;
                range.MinValue = 10.0;
                range.MaxValue = 40.0;
                range.RangeTitle = "Ao, Az 10-40 кГц";
                range.RangeCommand = 0x10;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }

        protected UnitMeasureRange Ao_Az_40_2000
        {
            get
            {
                UnitMeasureRange range = new UnitMeasureRange();
                range.RangeId = "HF_40_2000";
                range.parameterTypeId = CurrentParameterType.ParameterTypeId;
                range.KK = 11.8;
                range.BV = 0;
                range.MinValue = 40.0;
                range.MaxValue = 2000.0;
                range.RangeTitle = "Ao, Az 40-2000 кГц";
                range.RangeCommand = 0x00;
                range.UnitId = UnitSerialNumber.ToString();
                range.NeedConvertResult = true;
                return range;
            }
        }

        /// <summary>
        /// Текущая частота измерителя. 
        /// Когда задается значение частоты, выбирается диапазон и вычисляется код частоты
        /// </summary>
        public double CurrentFrequency
        {
            get
            {
                return CurFrequency;
            }set
            {
                CurFrequency = value;
                if (CurFrequency >= 40 && CurFrequency <= 2000)
                {
                    CurFrequencyAsInteger = ((int)CurFrequency / 8) * 8;
                    FreqCode = (byte)(CurFrequencyAsInteger / 8); // 40-2000
                    selectedRangeIdx = 2;
                    Debug.WriteLine($"CurrentFrequency = 40... 2000");
                }
                else if (CurFrequency >= 10 && CurFrequency < 40)
                {
                    CurFrequencyAsInteger = ((int)CurFrequency / 2) * 2;
                    FreqCode = (byte)(CurFrequencyAsInteger / 2); // 10-40
                    selectedRangeIdx = 1;
                    Debug.WriteLine($"CurrentFrequency = 10... 40");
                }
                else
                {
                    FreqCode = 5;
                    CurFrequencyAsInteger = 1;
                    selectedRangeIdx = 0;
                    Debug.WriteLine($"CurrentFrequency = 0.8");
                }
            }
        }


        protected override void SetEtalon()
        {
            switch (CurrentParameterType.ParameterTypeId)
            {
                case MeasuredParameterType.al:
                    Etalon = Etalon_al;
                    break;
                case MeasuredParameterType.Ao:
                    Etalon = Etalon_Ao;
                    break;
                case MeasuredParameterType.Az:
                    Etalon = Etalon_Az;
                    break;
            }
        }

        private MeasureUnitEtalon Etalon_al
        {
            get
            {
                MeasureUnitEtalon e = new MeasureUnitEtalon() { TrueValue = 0, MaxErrorPercent = 0.0, MaxErrorAddictive = 0.5, EtalonTitle = "al" };
                return e;
            }
        }

        private MeasureUnitEtalon Etalon_Ao
        {
            get
            {
                MeasureUnitEtalon e = new MeasureUnitEtalon() { TrueValue = 45, MaxErrorPercent = 0.0, MaxErrorAddictive = 1.0, EtalonTitle = "Ao" };
                return e;
            }
        }

        private MeasureUnitEtalon Etalon_Az
        {
            get
            {
                MeasureUnitEtalon e = new MeasureUnitEtalon() { TrueValue = 45, MaxErrorPercent = 0.0, MaxErrorAddictive = 1.0, EtalonTitle = "Az" };
                return e;
            }
        }

        private int WaveResistance = 150;
        private byte FreqCode = 0x00;
        private double CurFrequency;
        public int CurFrequencyAsInteger;

    }
}
