using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.Devices.SAC.SACUnits
{
    public class SACUnit
    {
        public SACUnit(SACTable _table)
        {
            table = _table;
            InitUnit();
        }
        public SACUnit(SACCPS _cps)
        {
            cps = _cps;
            InitUnit();
        }

        protected virtual void InitUnit()
        {
            SetUnitAddress();
            SetUnitInfo();
        }

        /// <summary>
        /// Установка адреса узла
        /// </summary>
        protected virtual void SetUnitAddress() { }

        protected virtual void SetUnitInfo() { }

        protected byte unitCMD_Address;
        protected SACCPS cps;
        protected SACTable table;

        protected string unitName;
        protected string unitTitle;
        protected int unitId;

        public string UnitName => unitName;
        public string UnitTitle => unitTitle;
        public int UnitId => unitId;
    }
}
