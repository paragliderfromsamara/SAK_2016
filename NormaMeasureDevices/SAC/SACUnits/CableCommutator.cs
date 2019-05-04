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
        /// Задаёт состояние всего стола
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
