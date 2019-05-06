using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.DBControl.Tables;
using NormaMeasure.Utils;
using NormaMeasure.Devices.SAC.SACUnits;
using System.Threading;

namespace NormaMeasure.Devices.SAC
{
    public delegate void SAC_EventHandler(SAC_Device sac);
    public delegate void SAC_CPS_EventHandler(SAC_Device sac, SACCPS cps);
    public delegate void SAC_Table_EventHandler(SAC_Device sac, SACTable table);

    public class SAC_Device : IDisposable
    {
        public const int MIN_FREQ = 10;
        public const int FREQ_STEP_8KGZ_BEGIN = 40;
        public const int MAX_FREQ = 2000;

        private bool isOnMeasure = false;
        public bool IsOnMeasure => isOnMeasure;
        public SACIniFile SettingsFile
        {
            get
            {
                if (settingsFile == null) settingsFile = new SACIniFile(this);
                return settingsFile;
            }
        }


        private SACIniFile settingsFile;
        /// <summary>
        /// Система автоматизированного контроля кабелей связи
        /// </summary>
        public SAC_Device()
        {
            InitCps();
            InitTable();
            //InitTables();
        }

        /// <summary>
        /// Инициализация столов
        /// </summary>
        public virtual void InitTable()
        {
            table = new SACTable(1, this);
            table.Device_Connected += Table_Device_Connected;
            table.Device_LostConnection += Table_Device_LostConnection;
        }

        private void Table_Device_LostConnection(DeviceBase device)
        {
            OnTableLost?.Invoke(this, (SACTable)device);

            //throw new NotImplementedException();
           // System.Windows.Forms.MessageBox.Show($"Table_Device_LostConnection ({device.PortName})");
        }

        private void Table_Device_Connected(DeviceBase device)
        {
            OnTableFound?.Invoke(this, (SACTable)device);
            ((SACTable)device).ClearCableCommutator();
            //System.Windows.Forms.MessageBox.Show($"Table_Device_Connected  ({device.PortName})");
            
        }

        /// <summary>
        /// Инициализация крейта системы
        /// </summary>
        protected virtual void InitCps()
        {
            centralSysPult = new SACCPS(this);
            centralSysPult.Device_Connected += Cps_Device_Connected;
            centralSysPult.Device_LostConnection += Cps_Device_LostConnection;
            //centralSysPult.Find();
        }

        private void findThread()
        {
            centralSysPult.Find();
            table.Find();
        }

        /// <summary>
        /// Ищем пульт системы
        /// </summary>
        public void Find()
        {
            Thread th = new Thread(findThread);
            th.Start();
        }

        private void Cps_Device_LostConnection(DeviceBase device)
        {
            OnCPSLost?.Invoke(this, device as SACCPS);
        }

        private void Cps_Device_Connected(DeviceBase device)
        { 
            SettingsFile.RefreshLastCPSNumber();
            CentralSysPult.InitUnits();
            OnCPSFound?.Invoke(this, device as SACCPS);
        }


        /// <summary>
        /// Установка коммутации и поиск узла для измерения
        /// </summary>
        /// <param name="point"></param>
        public CPSMeasureUnit SetMeasurePoint(SACMeasurePoint point)
        {
            CPSMeasureUnit unit = centralSysPult.GetMeasureUnit(point);
            if (unit != null)
            {
                centralSysPult.Commutator.SetCommutatorByParameterType(point);
                return unit;
            }
            else
            {
                SetDefaultSACState();
                return null;
            }
        }

        /// <summary>
        /// Устанавливает состояние по умолчанию
        /// </summary>
        public void SetDefaultSACState()
        {
            if (centralSysPult.IsConnected)
            {
                centralSysPult.Commutator.SetOnGroundState();
            }else
            {

            }

        }

        /// <summary>
        /// Поиск коммутационных столов системы
        /// </summary>
        protected virtual void FindTables()
        {
            
        }

        public void Dispose()
        {
            centralSysPult?.Dispose();
            table?.Dispose();
        }

        private Dictionary<string, SACTable> comTablesList;
        private SACCPS centralSysPult;
        public SACTable table;
        public SACCPS CentralSysPult => centralSysPult;

        public event SAC_CPS_EventHandler OnCPSFound;
        public event SAC_CPS_EventHandler OnCPSLost;

        public event SAC_Table_EventHandler OnTableFound;
        public event SAC_Table_EventHandler OnTableLost;
    }

    public class SACIniFile
    {
        private string UnitMap_SectionName = "UNITS";
        private string EtalonsMap_SectionName = "ETALONS";

        private string fileName = "SACSettings.ini";
        private IniFile file;

        private SAC_Device sac;



        public SACIniFile(SAC_Device _sac)
        {
            sac = _sac;
            InitFile();
        }

        public string GetLastCPSPortName()
        {
            string port_name = "COM1";
            if (file.KeyExists("port_name", "LAST CPS"))
            {
                port_name = file.Read("port_name", "LAST CPS");
            }
            return port_name;
        }

        public void RefreshLastCPSNumber()
        {
            file.Write("device_id", sac.CentralSysPult.DeviceId, "LAST CPS");
            file.Write("port_name", sac.CentralSysPult.PortName, "LAST CPS");
        }

        #region Управление данными стола 
        public string GetTablePortName(int tableNumber)
        {
            string port_name = "COM1";
            if (file.KeyExists("port_name", $"TABLE:{tableNumber}"))
            {
                port_name = file.Read("port_name", $"TABLE:{tableNumber}");
            }else
            {
                SetTablePortName(tableNumber, port_name);
            }
            return port_name;
        }

        public void SetTablePortName(int tableNumber, string port_name)
        {
            file.Write("port_name", port_name, $"TABLE:{tableNumber}");
        }

        #endregion

        #region Управление данными по узлам

        /// <summary>
        /// Поиск или внесение значения коэффициента диапазона по умолчанию
        /// </summary>
        /// <param name="range"></param>
        public void GetOrSet_UnitMeasureParameterRange(ref UnitMeasureRange range)
        {
            float BV = range.BV;
            float KK = range.KK;
            string pKey = GetParameterKeyTitle(range.parameterTypeId);
            string sectionName = $"{pKey}:{range.UnitId}";
            string KK_Key = $"KK<{range.RangeId}>";
            string BV_Key = $"BV<{range.RangeId}>";
            bool doesKKExist = file.KeyExists(KK_Key, sectionName);
            bool doesBVExist = file.KeyExists(BV_Key, sectionName);

            if (doesKKExist)
            {
                float.TryParse(file.Read(KK_Key, sectionName), out KK);
            }
            else
            {
                file.Write(KK_Key, KK.ToString(), sectionName);
            }
            if (doesBVExist)
            {
                float.TryParse(file.Read(BV_Key, sectionName), out BV);
            } else
            {
                file.Write(BV_Key, BV.ToString(), sectionName);
            }


            range.BV = BV;
            range.KK = KK;
        }

        private string GetParameterKeyTitle(uint pTypeId)
        {
            string s = string.Empty;
            switch (pTypeId)
            {
                case MeasuredParameterType.Rleads:
                    s = "Rg";
                    break;
                case MeasuredParameterType.Cp:
                    s = "Cр";
                    break;
                case MeasuredParameterType.Co:
                    s = "Co";
                    break;
                case MeasuredParameterType.Ea:
                    s = "Ea";
                    break;
                case MeasuredParameterType.K1:
                    s = "K1";
                    break;
                case MeasuredParameterType.K2:
                case MeasuredParameterType.K3:
                case MeasuredParameterType.K23:
                    s = "K2,K3";
                    break;
                case MeasuredParameterType.K9:
                case MeasuredParameterType.K10:
                case MeasuredParameterType.K11:
                case MeasuredParameterType.K12:
                case MeasuredParameterType.K9_12:
                    s = "K9-K12";
                    break;
                case MeasuredParameterType.Risol1:
                case MeasuredParameterType.Risol2:
                case MeasuredParameterType.Risol3:
                case MeasuredParameterType.Risol4:
                    s = "Riz";
                    break;
                case MeasuredParameterType.al:
                    s = "al";
                    break;
                case MeasuredParameterType.Ao:
                    s = "Ao";
                    break;
                case MeasuredParameterType.Az:
                    s = "Az";
                    break;
                default:
                    s = $"parameter_type_{pTypeId}";
                    break;
            }
            return s;
        }

        /// <summary>
        /// Поиск или добавление номера данного типа узла
        /// </summary>
        /// <param name="unitName">Название типа узла</param>
        /// <returns></returns>
        public int GetOrSetUnitNumber(string unitName)
        {
            int n = 1;
            string sectionName = $"{UnitMap_SectionName}:{sac.CentralSysPult.DeviceId}";
            if (file.KeyExists(unitName, sectionName))
            {
                int.TryParse(file.Read(unitName, sectionName), out n);
            }else
            {
                file.Write(unitName, n.ToString(),sectionName);
            }
            return n;
        } 
        //private 

        #endregion


        private void InitFile()
        {
            file = new IniFile(fileName);
        }


    }


    /// <summary>
    /// Тип коммутации системы 
    /// Для измерения эталона
    /// Без дальнего конца (1-104)
    /// С Дальним концом 
    /// </summary>
    public enum SACCommutationType
    {
        NoFarEnd,
        WithFarEnd,
        Etalon
    }
}
