using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.Devices.SAC
{
    public delegate void SAC_EventHandler(SAC sac);
    public delegate void SAC_CPS_EventHandler(SAC sac, SACCPS cps);
    public delegate void SAC_Table_EventHandler(SAC sac, SACTable table);

    public class SAC
    {

        /// <summary>
        /// Система автоматизированного контроля кабелей связи
        /// </summary>
        public SAC()
        {
            InitCps();
            //InitTables();

        }

        /// <summary>
        /// Инициализация столов
        /// </summary>
        protected virtual void InitTables()
        {

            throw new NotImplementedException();
        }

        /// <summary>
        /// Инициализация крейта системы
        /// </summary>
        protected virtual void InitCps()
        {
            cps = new SACCPS();
            cps.Device_Connected += Cps_Device_Connected;
            cps.Device_LostConnection += Cps_Device_LostConnection;
            cps.Find();
        }

        private void Cps_Device_LostConnection(DeviceBase device)
        {
            OnCPSFound?.Invoke(this, device as SACCPS);
        }

        private void Cps_Device_Connected(DeviceBase device)
        {
            OnCPSLost?.Invoke(this, device as SACCPS);
        }




        /// <summary>
        /// Поиск пультов системы
        /// </summary>
        protected virtual void FindCps()
        {
            
        }

        /// <summary>
        /// Поиск коммутационных столов системы
        /// </summary>
        protected virtual void FindTables()
        {

        }

        private Dictionary<string, SACTable> comTablesList;
        private SACCPS cps;

        private event SAC_CPS_EventHandler OnCPSFound;
        private event SAC_CPS_EventHandler OnCPSLost;

        private event SAC_Table_EventHandler OnTableFound;
        private event SAC_Table_EventHandler OnTableLost;
    }
}
