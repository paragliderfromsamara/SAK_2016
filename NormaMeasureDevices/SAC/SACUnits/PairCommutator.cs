using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;
using System.Threading;
using System.Diagnostics;

namespace NormaMeasure.Devices.SAC.SACUnits
{
    public class PairCommutator : SACUnit
    {
        private SACMeasurePoint CurrentPoint;
        private Dictionary<byte, ComTablePairConncectionState> CommutationList_ToSend;
        private Dictionary<byte, ComTablePairConncectionState> PrevCommutationList;

        public PairCommutator(SACTable _table) : base(_table)
        {
            CommutationList_ToSend = new Dictionary<byte, ComTablePairConncectionState>();
            PrevCommutationList = new Dictionary<byte, ComTablePairConncectionState>();
        }


        protected override void InitUnit()
        {
            base.InitUnit();
        }

        /// <summary>
        /// Обновляет состояние реле в столах 
        /// </summary>
        /// <returns></returns>
        public bool RefreshAllRele()
        {
            return table.SendCommand(0x07);
        }

        public bool SetCableCommutatorState_ForCurrentPoint(SACMeasurePoint curPoint)
        {
            CurrentPoint = curPoint;
            CommutationList_ToSend.Clear();
            ClearTable();
            if (curPoint.CommutationType == SACCommutationType.Etalon) return true;
            switch (curPoint.ParameterType.ParameterTypeId)
            {
                case MeasuredParameterType.Calling:
                    //SetCommutatorMode_ForCalling();
                    break;
                case MeasuredParameterType.dR:
                case MeasuredParameterType.Rleads:
                    SetPairCommutatorFor_Rleads_And_dR();
                    break;
                case MeasuredParameterType.al:
                    SetPairCommutatorFor_al();
                    break;
                case MeasuredParameterType.Cp:
                case MeasuredParameterType.Co:
                case MeasuredParameterType.Ea:
                    SetPairCommutatorFor_CpСoEa();
                    break;
                case MeasuredParameterType.K1:
                case MeasuredParameterType.K2:
                case MeasuredParameterType.K3:
                //case MeasuredParameterType.K23:
                case MeasuredParameterType.K9:
                case MeasuredParameterType.K10:
                case MeasuredParameterType.K11:
                case MeasuredParameterType.K12:
                //case MeasuredParameterType.K9_12:
                    SetPairCommutatorFor_K_Parameters();
                    break;
                case MeasuredParameterType.Risol1:
                case MeasuredParameterType.Risol2:
                    SetPairCommutatorFor_Rizol_by_pair();
                    break;
                case MeasuredParameterType.Risol3:
                case MeasuredParameterType.Risol4:
                    SetPairCommutatorFor_Rizol_by_bunch();
                    break;
                case MeasuredParameterType.Ao:
                case MeasuredParameterType.Az:
                    SetPairCommutatorFor_AoAz();
                    break;
                default:
                    ClearTable();
                    break;
            }
            return SetCommutationTableByList();
        }

        private void SetPairCommutatorFor_Rizol_by_bunch()
        {
            
        }

        private void SetPairCommutatorFor_AoAz()
        {
            CommutationList_ToSend.Add(CurrentPoint.PairCommutatorPosition_1, ComTablePairConncectionState.spMASTER);
            CommutationList_ToSend.Add((byte)(CurrentPoint.PairCommutatorPosition_1+52), ComTablePairConncectionState.spMASTER);

            CommutationList_ToSend.Add(CurrentPoint.PairCommutatorPosition_2, ComTablePairConncectionState.spSLAVE);
            CommutationList_ToSend.Add((byte)(CurrentPoint.PairCommutatorPosition_2 + 52), ComTablePairConncectionState.spSLAVE);
        }

        private void SetPairCommutatorFor_Rizol_by_pair()
        {
            CommutationList_ToSend.Add(CurrentPoint.PairCommutatorPosition_1, ComTablePairConncectionState.spMASTER);
            //CommutationList_ToSend.Add(CurrentPoint.PairCommutatorPosition_2, ComTablePairConncectionState.spSLAVE);
            Debug.WriteLine($"Установка коммутатора пар для измерений Risol1, Risol2; MASTER {(byte)(CurrentPoint.PairCommutatorPosition_1)}");
        }

        private void SetPairCommutatorFor_K_Parameters()
        {
            CommutationList_ToSend.Add(CurrentPoint.PairCommutatorPosition_1, ComTablePairConncectionState.spMASTER);
            CommutationList_ToSend.Add(CurrentPoint.PairCommutatorPosition_2, ComTablePairConncectionState.spSLAVE);
            Debug.WriteLine($"Установка коммутатора пар для измерений K1, K2, K3, K9, K10, K11, K12; MASTER {(byte)(CurrentPoint.PairCommutatorPosition_1)}; SLAVE {CurrentPoint.PairCommutatorPosition_2}");

        }

        private void SetPairCommutatorFor_CpСoEa()
        {
            CommutationList_ToSend.Add(CurrentPoint.PairCommutatorPosition_1, ComTablePairConncectionState.spMASTER);
            Debug.WriteLine($"Установка коммутатора пар для стола: CpСo; MASTER {(byte)(CurrentPoint.PairCommutatorPosition_1)}");
        }

        private void SetPairCommutatorFor_al()
        {
            CommutationList_ToSend.Add(CurrentPoint.PairCommutatorPosition_1, ComTablePairConncectionState.spMASTER);
            CommutationList_ToSend.Add((byte)(CurrentPoint.PairCommutatorPosition_1+52), ComTablePairConncectionState.spSLAVE);
            Debug.WriteLine($"Установка коммутатора пар для стола: al; SLAVE: {CurrentPoint.PairCommutatorPosition_1+52}; MASTER {(byte)(CurrentPoint.PairCommutatorPosition_1)}");
        }

        private void SetPairCommutatorFor_Rleads_And_dR()
        {
            CommutationList_ToSend.Add(CurrentPoint.PairCommutatorPosition_1, ComTablePairConncectionState.spBOTH);
            if (CurrentPoint.CommutationType == SACCommutationType.WithFarEnd) CommutationList_ToSend.Add((byte)(CurrentPoint.PairCommutatorPosition_1 + 52), ComTablePairConncectionState.spBOTH);
            Debug.WriteLine("Установка коммутатора пар для стола: Rжил");
        }

        private bool SetCommutationTableByList()
        {
            bool flag = true;
            foreach(byte pair in CommutationList_ToSend.Keys)
            {
                flag &= SetPairTo(pair, CommutationList_ToSend[pair]);

            }
            return flag;
        }

        /// <summary>
        /// Устанавливает ПУ стола с номером pairNumber в состояние comPairState
        /// </summary>
        /// <param name="pairNumber">Номер пары (начиная с 1)</param>
        /// <param name="comPairState"></param>
        /// <returns></returns>
        public bool SetPairTo(byte pairNumber, ComTablePairConncectionState comPairState)
        {
            return table.SendCommand(unitCMD_Address, new byte[] { pairNumber, (byte)comPairState });
        }


        /// <summary>
        /// Задаёт состояние диапазона пар стола
        /// </summary>
        /// <param name="firstPair">Начальная пара диапазона (минимум 1)</param>
        /// <param name="lastPair">Конечная пара диапазона (макс 104)</param>
        /// <param name="state">Состояние</param>
        /// <returns></returns>
        public bool SetPairRangeTo(byte firstPair, byte lastPair, ComTablePairConncectionState comPairState)
        {
            return table.SendCommand(0x08, new byte[] { firstPair, lastPair, (byte)comPairState });
        }

        /// <summary>
        /// Очищаем стол
        /// </summary>
        /// <returns></returns>
        protected bool ClearTable()
        {
           return SetAllPairTo(ComTablePairConncectionState.spNONE);
        }



        /// <summary>
        /// Задаёт состояние пар всего стола
        /// </summary>
        /// <param name="comPairState">Состояние в которое надо ввести реле стола</param>
        public bool SetAllPairTo(ComTablePairConncectionState comPairState)
        {
            byte state = (byte)comPairState;
            return table.SendCommand((byte)(unitCMD_Address | 0x01), state);
        }

        protected override void SetUnitAddress()
        {
            base.SetUnitAddress();
            unitCMD_Address = 0x02;
        }

        protected override void SetUnitInfo()
        {
            unitName = $"Коммутационное поле стола №{table.DeviceId}";
        }

    }

    public enum ComTablePairConncectionState : byte
    {
        spNONE = 0,   // Отключить пару от шин.
        spMASTER = 1, // Подключить пару на основную шину.
        spSLAVE = 2,  // Подключить пару на дополнительную шину
        spBOTH = 3    // Подключить пару на обе шины
    }
}
