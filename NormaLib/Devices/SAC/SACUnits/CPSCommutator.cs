using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NormaLib.DBControl.Tables;

namespace NormaLib.Devices.SAC.SACUnits
{
    public class CPSCommutator : SACUnit
    {
        public delegate void CPSCommutator_Handler(CPSCommutator commutator);
        public CPSCommutator(SACCPS _cps) : base(_cps)
        {
            SetOnGroundState();
        }

        /// <summary>
        /// Формирует карту реле коммутатора
        /// </summary>
        protected virtual void MakeReleMap()
        {
            releMap = new byte[][] {
                K35,
                K36,
                K37,
                K38,
                K39,
                K40,
                K41_42,
                K43_44,
                K45,
                K46,
                K47,
                K48,
                K49,
                K50,
                K51_52
            };
        }

        protected override void SetUnitAddress()
        {
            unitCMD_Address = 0x07;
        }



        /// <summary>
        /// Устанавливает коммутатор в необходимое состояние
        /// </summary>
        /// <param name="newState"></param>
        public void SetCommutatorState(byte[][] newState)
        {
            byte[] state = BuildState(newState); 
            CurrentState = state;
        }

        /// <summary>
        /// Объединяет несколько реле в одно состояние
        /// </summary>
        /// <param name="newState"></param>
        /// <returns></returns>
        private byte[] BuildState(byte[][] newState)
        {
            byte[] state = (byte[])ZeroState.Clone();
            for (int i = 0; i < newState[0].Length; i++)
            {
                for (int j = 0; j < newState.Length; j++) state[i] |= newState[j][i];
            }
            return state;
        }

        public void ClearState()
        {
            CurrentState = ZeroState;
        }

        /// <summary>
        /// Замыкание дополнительной шины на землю
        /// </summary>
        public void SetOnGroundState()
        {
            CurrentState = OnGround_ReleState;
        }



        /// <summary>
        /// Список всех известных реле
        /// </summary>
        public byte[][] ReleMap
        {
            get
            {
                if (releMap == null) MakeReleMap();
                return releMap;
            }
        }

        /// <summary>
        /// Поочередно включает реле
        /// </summary>
        public void Test()
        {
            for(int i = 0; i<5; i++)
            {
                foreach (byte[] rele in ReleMap)
                {
                    byte[][] state = new byte[][] { rele };
                    SetCommutatorState(state);
                    Thread.Sleep(300);
                }
                ClearState();
            }
        }


        /// <summary>
        /// Формирует команду коммутатора в ЦПС
        /// </summary>
        /// <param name="releState"></param>
        /// <returns></returns>
        private byte[] BuildCommandByState(byte[] releState)
        {
            byte j = 1;
            List<byte> cmd = new List<byte>();
            for(int i=0; i<releState.Length; i++)
            {
                cmd.Add((byte)(unitCMD_Address | SACCPS.TwoBytes_cmd));
                cmd.Add(j);
                cmd.Add((byte)(releState[i]));
                j <<= 1;
            }
            return cmd.ToArray();
        }

        protected byte[] zeroState;
        protected byte[][] releMap;

        protected byte[] curState;
        public byte[] State => CurrentState;
        protected byte[] CurrentState
        {
            get
            {
                return curState;
            }
            set
            {
                if (curState != value)
                {
                    byte[] cmd = BuildCommandByState(value);
                    cps.WriteBytes(cmd);
                    curState = value;
                    OnCommutator_StateChanged?.Invoke(this);
                    Thread.Sleep(300);
                }
            }
        }


        /// <summary>
        /// Задаёт состояние коммутатора по типу измеряемого параметра
        /// </summary>
        /// <param name="pTypeId"></param>
        /// <param name="isEtalon"></param>
        /// <param name="RisolType"></param>
        public virtual void SetCommutatorByParameterType(SACMeasurePoint point)
        {
            bool is_etalon = point.CommutationType == SACCommutationType.Etalon;
            switch (point.ParameterType.ParameterTypeId)
            {
                case MeasuredParameterType.Calling:
                    CurrentState = Calling_ReleState;
                    break;
                case MeasuredParameterType.Rleads:
                    CurrentState = is_etalon ? RleadsEtalon_ReleState : Rleads_ReleState;
                    break;
                case MeasuredParameterType.Cp:
                    CurrentState = is_etalon ? Cp_Etalon_ReleState : CapacityMeasure_ReleState;
                    break;
                case MeasuredParameterType.Co:
                case MeasuredParameterType.Ea:
                    CurrentState = is_etalon ? Co_Ea_Etalon_ReleState : CapacityMeasure_ReleState;
                    break;
                case MeasuredParameterType.K1:
                case MeasuredParameterType.K2:
                case MeasuredParameterType.K3:
                case MeasuredParameterType.K23:
                case MeasuredParameterType.K9:
                case MeasuredParameterType.K10:
                case MeasuredParameterType.K11:
                case MeasuredParameterType.K12:
                case MeasuredParameterType.K9_12:
                    CurrentState = is_etalon ? K1_12_Etalon_ReleState : CapacityMeasure_ReleState;
                    break;
                case MeasuredParameterType.Risol1:
                case MeasuredParameterType.Risol2:
                case MeasuredParameterType.Risol3:
                case MeasuredParameterType.Risol4:
                    if (!is_etalon)
                    {
                        switch(point.LeadCommType)
                        {
                            case LeadCommutationType.A:
                                CurrentState = Risol_A_ReleState;
                                break;
                            case LeadCommutationType.B:
                                CurrentState = Risol_B_ReleState;
                                break;
                            case LeadCommutationType.AB:
                                CurrentState = Risol_AB_ReleState;
                                break;
                        }
                    }
                    else CurrentState = Risol_Etalon_ReleState;
                    break;
                case MeasuredParameterType.al:
                case MeasuredParameterType.Ao:
                case MeasuredParameterType.Az:
                    CurrentState = PVUnit_ReleState;
                    break;
                default:
                    CurrentState = ZeroState;
                    break;
            }
        }

        private byte[] OnGround_ReleState => BuildState(ReleToGround);

        private readonly byte[][] ReleToGround = new byte[][] { K39 };

        #region Коммутация узла 110


        private readonly byte[][] Calling_ReleList = new byte[][] { K39, K43_44, K45 };
        private readonly byte[][] Rleads_ReleList = new byte[][] { K43_44, K46 };
        private readonly byte[][] Rleads_Etalon_ReleList = new byte[][] { K51_52 };

        /// <summary>
        /// Коммутация реле для Rжил
        /// </summary>
        public byte[] Rleads_ReleState => BuildState(Rleads_ReleList);
        /// <summary>
        /// Коммутация реле для измерения эталона Rжил
        /// </summary>
        public byte[] RleadsEtalon_ReleState => BuildState(Rleads_Etalon_ReleList);
        /// <summary>
        /// Коммутация реле для прозвонки
        /// </summary>
        public byte[] Calling_ReleState => BuildState(Calling_ReleList);

        #endregion

        #region Коммутация узла 120
        private readonly byte[][] CapacityMeasure_ReleList = new byte[][] { K41_42 };
        private readonly byte[][] Cp_Etalon_ReleList = new byte[][] { K47 };
        private readonly byte[][] Co_Ea_Etalon_ReleList = new byte[][] { K49 };
        private readonly byte[][] K1_12_Etalon_ReleList = new byte[][] { K48 };

        /// <summary>
        /// Коммутация реле для измерения емкостных параметров CEK
        /// </summary>
        public byte[] CapacityMeasure_ReleState => BuildState(CapacityMeasure_ReleList);

        /// <summary>
        /// Коммутация реле для измерения эталона Cp
        /// </summary>
        public byte[] Cp_Etalon_ReleState => BuildState(Cp_Etalon_ReleList);
        /// <summary>
        /// Коммутация реле для измерения эталона Co и Ea
        /// </summary>
        public byte[] Co_Ea_Etalon_ReleState => BuildState(Co_Ea_Etalon_ReleList);
        /// <summary>
        /// Коммутация реле для измерения эталона К параметров
        /// </summary>
        public byte[] K1_12_Etalon_ReleState => BuildState(K1_12_Etalon_ReleList);

        #endregion

        #region Коммутация узла 130

        private readonly byte[][] Risol_A_ReleList = new byte[][] { K35, K36, K39 };
        private readonly byte[][] Risol_B_ReleList = new byte[][] { K35, K37, K39 };
        private readonly byte[][] Risol_AB_ReleList = new byte[][] { K35, K50, K39 };
        private readonly byte[][] Risol_Etalon_ReleList = new byte[][] { K38 };

        /// <summary>
        /// Коммутация реле для измерения Rиз жилы А
        /// </summary>
        public byte[] Risol_A_ReleState => BuildState(Risol_A_ReleList);

        /// <summary>
        ///  Коммутация реле для измерения Rиз жилы B
        /// </summary>
        public byte[] Risol_B_ReleState => BuildState(Risol_B_ReleList);

        /// <summary>
        /// Коммутация реле для измерения Rиз между АB
        /// </summary>
        public byte[] Risol_AB_ReleState => BuildState(Risol_AB_ReleList);

        /// <summary>
        /// Коммутация реле для измерения эталона Rиз 
        /// </summary>
        public byte[] Risol_Etalon_ReleState => BuildState(Risol_Etalon_ReleList);
        #endregion

        #region Коммутация узла 160
        private readonly byte[][] PVMeasure_ReleList = new byte[][] { K40};
        /// <summary>
        /// Коммутация реле для αl, Ао, Аз и эталона
        /// </summary>
        public byte[] PVUnit_ReleState => BuildState(PVMeasure_ReleList);
        #endregion

        #region Карта реле
        public static readonly byte[] ZeroState = new byte[] { 0x00, 0x00 };
        public static readonly byte[] K35 = new byte[] { 0x01, 0x00 };
        public static readonly byte[] K36 = new byte[] { 0x02, 0x00 };
        public static readonly byte[] K37 = new byte[] { 0x04, 0x00 };
        public static readonly byte[] K38 = new byte[] { 0x08, 0x00 };
        public static readonly byte[] K39 = new byte[] { 0x10, 0x00 };
        public static readonly byte[] K40 = new byte[] { 0x20, 0x00 };
        public static readonly byte[] K41_42 = new byte[] { 0x40, 0x00 };
        public static readonly byte[] K43_44 = new byte[] { 0x80, 0x00 };
        public static readonly byte[] K45 = new byte[] { 0x00, 0x01 };
        public static readonly byte[] K46 = new byte[] { 0x00, 0x02 };
        public static readonly byte[] K47 = new byte[] { 0x00, 0x04 };
        public static readonly byte[] K48 = new byte[] { 0x00, 0x08 };
        public static readonly byte[] K49 = new byte[] { 0x00, 0x10 };
        public static readonly byte[] K50 = new byte[] { 0x00, 0x20 };
        public static readonly byte[] K51_52 = new byte[] { 0x00, 0x40 };
        #endregion

        public event CPSCommutator_Handler OnCommutator_StateChanged;
    }
}
