using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;
using NormaMeasure.Utils;

namespace NormaMeasure.Devices.SAC
{
    public delegate void SAC_EventHandler(SAC sac);
    public delegate void SAC_CPS_EventHandler(SAC sac, SACCPS cps);
    public delegate void SAC_Table_EventHandler(SAC sac, SACTable table);

    public class SAC
    {
        private bool isOnMeasure = false;
        public bool IsOnMeasure => isOnMeasure;
        public SACIniFile SettingsFile
        {
            get
            {
                if (settingsFile == null) InitSettingsFile();
                return settingsFile;
            }
        }


        private SACIniFile settingsFile;
        /// <summary>
        /// Система автоматизированного контроля кабелей связи
        /// </summary>
        public SAC()
        {
            InitCps();
            //InitTables();
        }



        private void InitSettingsFile()
        {
            settingsFile = new SACIniFile(this);

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
            centralSysPult = new SACCPS();
            centralSysPult.Device_Connected += Cps_Device_Connected;
            centralSysPult.Device_LostConnection += Cps_Device_LostConnection;
            //centralSysPult.Find();
        }

        /// <summary>
        /// Ищем пульт системы
        /// </summary>
        public void FindCPS()
        {
            centralSysPult.Find();
        }

        private void Cps_Device_LostConnection(DeviceBase device)
        {
            OnCPSFound?.Invoke(this, device as SACCPS);
        }

        private void Cps_Device_Connected(DeviceBase device)
        { 
            OnCPSLost?.Invoke(this, device as SACCPS);
            SettingsFile.RefreshLastCPSNumber();
        }

        public double MakeEtalonMeasure(MeasuredParameterType parameter_type)
        {
            double value = centralSysPult.MakeMeasureParameter(parameter_type, true);
            return value;
        }

        /// <summary>
        /// Поиск пультов системы
        /// </summary>
        protected virtual void FindCps()
        {
            centralSysPult.Find();
        }

        /// <summary>
        /// Поиск коммутационных столов системы
        /// </summary>
        protected virtual void FindTables()
        {

        }



        private Dictionary<string, SACTable> comTablesList;
        private SACCPS centralSysPult;
        public SACCPS CentralSysPult => centralSysPult;

        public event SAC_CPS_EventHandler OnCPSFound;
        public event SAC_CPS_EventHandler OnCPSLost;

        private event SAC_Table_EventHandler OnTableFound;
        private event SAC_Table_EventHandler OnTableLost;
    }

    public class SACIniFile
    {
        private string fileName = "SACSettings.ini";
        private IniFile file;

        private SAC sac;

        public SACIniFile(SAC _sac)
        {
            sac = _sac;
            InitFile();
        }

        public void RefreshLastCPSNumber()
        {
            file.Write("device_id", sac.CentralSysPult.DeviceId, "LAST CPS");
            file.Write("port_name", sac.CentralSysPult.PortName, "LAST CPS");
        }

        private void InitFile()
        {
            file = new IniFile(fileName);
        }
    }
}
