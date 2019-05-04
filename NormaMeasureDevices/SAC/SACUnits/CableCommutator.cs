using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.Devices.SAC.SACUnits
{
    public class PairCommutator : SACUnit
    {
        public PairCommutator(SACTable _table) : base(_table)
        {
            
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

        /// <summary>
        /// Устанавливает ПУ стола с номером pairNumber в состояние comPairState
        /// </summary>
        /// <param name="pairNumber">Номер пары (начиная с 1)</param>
        /// <param name="comPairState"></param>
        /// <returns></returns>
        public bool SetPairTo(byte pairNumber, ComTablePairConncectionState comPairState)
        {
            byte state = (byte)comPairState;
            return table.SendCommand(unitCMD_Address, new byte[] { pairNumber, state });
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
            byte state = (byte)comPairState;
            return table.SendCommand(0x08, new byte[] { firstPair, lastPair, state });
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
