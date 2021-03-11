using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;
using System.Diagnostics;


namespace NormaLib.Devices.SAC.SACUnits
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
            SelectCommutatorMode();
            SelectPVCommutatorSettings();
            return SetUSICommutatorState();
        }

        /// <summary>
        /// Выбор CommutatorMode в зависимости от current_point 
        /// </summary>
        private void SelectCommutatorMode()
        {
            switch(current_point.ParameterType.ParameterTypeId)
            {
                case MeasuredParameterType.Calling:
                    SetCommutatorMode_ForCalling();
                    break;
                case MeasuredParameterType.Rleads:
                case MeasuredParameterType.dR:
                    SetCommtator_For_Rleads_And_dR();
                    break;
                case MeasuredParameterType.Co:
                case MeasuredParameterType.Cp:
                case MeasuredParameterType.Ea:
                case MeasuredParameterType.K1:
                case MeasuredParameterType.K2:
                case MeasuredParameterType.K3:
                case MeasuredParameterType.K23:
                case MeasuredParameterType.K9:
                case MeasuredParameterType.K10:
                case MeasuredParameterType.K11:
                case MeasuredParameterType.K12:
                case MeasuredParameterType.K9_12:
                    SetCommtator_For_Cp_And_CoEK();
                    break;
                case MeasuredParameterType.Risol1:
                case MeasuredParameterType.Risol2:
                case MeasuredParameterType.Risol3:
                case MeasuredParameterType.Risol4:
                    SetCommtator_For_Rizol();
                    break;
                case MeasuredParameterType.al:
                case MeasuredParameterType.Ao:
                case MeasuredParameterType.Az:
                    SetCommutator_ForPV();
                    break;
                default:
                    CommutatorMode = (byte)USICommutatorModes.CLEAR;
                    break;
            }
        }

        private void SetCommutator_ForPV()
        {
            if (current_point.CommutationType == SACCommutationType.Etalon)
            {
                CommutatorMode = (byte)USICommutatorModes.PV_ETAL;
            }else
            {
                if (current_point.ParameterType.ParameterTypeId == MeasuredParameterType.al) CommutatorMode = (byte)USICommutatorModes.al;
                else if (current_point.ParameterType.ParameterTypeId == MeasuredParameterType.Ao) CommutatorMode = (byte)USICommutatorModes.Ao;
                else if (current_point.ParameterType.ParameterTypeId == MeasuredParameterType.Az) CommutatorMode = (byte)USICommutatorModes.Az;
            }
        }

        /// <summary>
        /// Установка режима коммутатора для Rизол
        /// </summary>
        private void SetCommtator_For_Rizol()
        {
            if (current_point.CommutationType == SACCommutationType.WithFarEnd)
            {
                CommutatorMode = (byte)USICommutatorModes.RIZ;
            }
            else if (current_point.CommutationType == SACCommutationType.NoFarEnd)
            {
                CommutatorMode = (byte)USICommutatorModes.RIZ_DT;
            }else
            {
                CommutatorMode = (byte)USICommutatorModes.CLEAR;
                Debug.WriteLine("Установка коммутатора БУСИ: Rizol (Эталон)");
            }
        }

        /// <summary>
        /// Установка коммутатора для емкостных параметров
        /// </summary>
        private void SetCommtator_For_Cp_And_CoEK()
        {
            if (current_point.CommutationType == SACCommutationType.WithFarEnd)
            {
                CommutatorMode = (byte)USICommutatorModes.CEK;
            }else
            {
                CommutatorMode = (byte)USICommutatorModes.CEK_DT;
            }
        }

        /// <summary>
        /// Режим коммутатора для Rжил и dR
        /// </summary>
        private void SetCommtator_For_Rleads_And_dR()
        {
            if (current_point.CommutationType == SACCommutationType.WithFarEnd)
            {
                CommutatorMode = (current_point.LeadCommType == LeadCommutationType.A) ? (byte)USICommutatorModes.RGA : (byte)USICommutatorModes.RGB;
                Debug.WriteLine("Установка коммутатора БУСИ: Rжил (С ДК)");
            }
            else if (current_point.CommutationType == SACCommutationType.NoFarEnd)
            {
                CommutatorMode = (current_point.LeadCommType == LeadCommutationType.A) ? (byte)USICommutatorModes.RGA_DT : (byte)USICommutatorModes.RGB_DT;
                Debug.WriteLine("Установка коммутатора БУСИ: Rжил (без ДК)");
            }else
            {
                CommutatorMode = (byte)USICommutatorModes.CLEAR;
                Debug.WriteLine("Установка коммутатора БУСИ: Rжил (Эталон)");
            }
        }

        /// <summary>
        /// Режим коммутатора для прозвонки
        /// </summary>
        private void SetCommutatorMode_ForCalling()
        {
            if (current_point.CallingSubMode == CallingSubModes.Short)
            {
                if (current_point.CommutationType == SACCommutationType.WithFarEnd)
                {
                    CommutatorMode = (byte)USICommutatorModes.SHORT;
                }
                else
                {
                    CommutatorMode = (byte)USICommutatorModes.SHORT_DT;
                }
            }
            else
            {
                if (current_point.CommutationType == SACCommutationType.WithFarEnd)
                {
                    CommutatorMode = (byte)USICommutatorModes.OPEN;
                }
                else
                {
                    CommutatorMode = (byte)USICommutatorModes.OPEN_DT;
                }
            }
        }


        /// <summary>
        /// Установка TransformatorMode и WaveResistance в зависимости от current_point 
        /// </summary>
        private void SelectPVCommutatorSettings()
        {
            if (current_point.ParameterType.IsFreqParameter)
            {
                WaveResistance = (byte)GetWaveResistanceModeForCurrentPoint();
                TransformatorMode = current_point.CurrentFrequency < SAC_Device.MIN_FREQ ? (byte)USITransformatorModes.LowFrequency : (byte)USITransformatorModes.HightFrequency;
            }
            else
            {
                WaveResistance = (byte)WaveResistanceModes.NONE;
                TransformatorMode = (byte)USITransformatorModes.HightFrequency;
            }
        }

        private WaveResistanceModes GetWaveResistanceModeForCurrentPoint()
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
            Debug.WriteLine($"Установка коммутатора БУСИ: основной режим {CommutatorMode.ToString("X")} Нагрузка {WaveResistance.ToString("X")} Трансформатор {TransformatorMode.ToString("X")}");
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
                return waveRes;
            }
            set
            {
                WasChanged |= (value != waveRes);
                waveRes = value;
            }
        }

        /// <summary>
        /// Основной режим коммутатора
        /// </summary>
        public byte CommutatorMode
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
