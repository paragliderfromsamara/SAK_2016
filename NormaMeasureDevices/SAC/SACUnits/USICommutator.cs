using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.Devices.SAC.SACUnits
{
    public class USICommutator : SACUnit
    {
        SACMeasurePoint current_point;

        public USICommutator(SACTable _table) : base(_table)
        {

        }

        protected override void SetUnitAddress()
        {
            unitCMD_Address = 0x01;
        }

        public bool SetUSICommutatorState(SACMeasurePoint curPoint)
        {
            current_point = curPoint;
            SetCommutatorMode();
            SetPVCommutatorSettings();
            return SetUSICommutatorState();
        }

        /// <summary>
        /// Установка CommutatorMode в зависимости от current_point 
        /// </summary>
        private void SetCommutatorMode()
        {

        }

        /// <summary>
        /// Установка TransformatorMode и WaveResistance в зависимости от current_point 
        /// </summary>
        private void SetPVCommutatorSettings()
        {
            if (current_point.parameterType.IsFreqParameter)
            {
                WaveResistance = (byte)GetWaveResistanceModeFromCurrentPoint();
                TransformatorMode = current_point.FrequencyMin < SAC_Device.MIN_FREQ ? (byte)USITransformatorModes.LowFrequency : (byte)USITransformatorModes.HightFrequency;
            }
            else
            {
                WaveResistance = (byte)WaveResistanceModes.NONE;
                TransformatorMode = (byte)USITransformatorModes.HightFrequency;
            }
        }

        private WaveResistanceModes GetWaveResistanceModeFromCurrentPoint()
        {
            switch (current_point.WaveResistance)
            {
                case 150:
                    return WaveResistanceModes.WR_150;
                case 400:
                    return WaveResistanceModes.WR_400;
                case 500:
                    return WaveResistanceModes.WR_500;
                case 600:
                    return WaveResistanceModes.WR_600;
                default:
                    return WaveResistanceModes.WR_150;
            }
        }

        public bool SetUSICommutatorState()
        {
            if (WasChanged)
            {
                WasChanged = !table.SendCommand(unitCMD_Address, new byte[] { CommutatorMode, WaveResistance, TransformatorMode });
            }
            return !WasChanged;
                
        }

        byte transformatorMode = 0x00;
        byte waveRes = 0x00;
        byte commutatorMode = 0x00;

        /// <summary>
        /// Коммутация трансформатора ВЧ или НЧ
        /// </summary>
        public byte TransformatorMode
        {
            get
            {
                return transformatorMode;
            }set
            {
                WasChanged |= (value != transformatorMode);
                transformatorMode = value;
            }
        }

        /// <summary>
        /// Реле включения волнового сопротивления
        /// </summary>
        public byte WaveResistance
        {
            get
            {
                return commutatorMode;
            }
            set
            {
                WasChanged |= (value != commutatorMode);
                commutatorMode = value;
            }
        }

        /// <summary>
        /// Основной режим коммутатора
        /// </summary>
        public byte CommutatorMode
        {
            get
            {
                return waveRes;
            }
            set
            {
                WasChanged |= (value != commutatorMode);
                commutatorMode = value;
            }
        }

        bool WasChanged = true;
    }

    enum WaveResistanceModes : byte
    {
        WR_150 = 0,
        WR_400 = 1,
        WR_500 = 2,
        WR_600 = 3,
        NONE = 4
    }

    enum USITransformatorModes : byte
    {
        LowFrequency = 0,
        HightFrequency = 1
    }

    enum USICommutatorModes : byte
    {
         CLEAR = 0,
         OPEN = 1,
         OPEN_DT = 2,
         SHORT = 3,
         SHORT_DT = 4,
         RGA = 5,
         RGB = 6,
         RGA_DT = 7,
         RGB_DT = 8,
         CEK = 9,
         CEK_DT = 10,
         RIZ = 11,
         RIZ_DT = 12,
         PV_ETAL = 13,
         Ao = 14,
         al = 15,
         Az = 16
    }
}
