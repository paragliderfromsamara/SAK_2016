using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Devices;
using NormaMeasure.Devices.SAC;

namespace AppTest
{
    class DeviceTest
    {
        public static void Start()
        {
            Program.PrintTitle("Тест устройств");
            TestSAC_cps();

        }

        public static void TestSAC_cps()
        {
            Program.PrintTitle("Тест ЦПС");
            SACCPS cps = new SACCPS();
            cps.Device_Connected += Cps_Device_Connected;
            cps.Device_LostConnection += Cps_Device_LostConnection;
            cps.Device_NotFound += Cps_Device_NotFound;
            cps.Device_Finding += Cps_Device_Finding;
            cps.OnFindingException += Cps_OnFindingException;
            cps.Commutator.OnCommutator_StateChanged += Commutator_OnCommutator_StateChanged;
            cps.Find();
            //cps.LedLineTest();
            if (cps.IsConnected)
            {
                //cps.Commutator.Test();
                //cps.LedLineTest();
            }
            

        }

        private static void Commutator_OnCommutator_StateChanged(NormaMeasure.Devices.SAC.CPSUnits.CPSCommutator commutator)
        {
            Console.WriteLine($"{commutator.State[0]} {commutator.State[1]}");
        }

        private static void Cps_OnFindingException(DeviceBase device, Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        private static void Cps_Device_Finding(DeviceBase device)
        {
            Console.WriteLine($"Поиск ЦПС...");
        }

        private static void Cps_Device_NotFound(DeviceBase device)
        {
            Console.WriteLine($"ЦПС не найден");
        }

        private static void Cps_Device_LostConnection(DeviceBase device)
        {
            Console.WriteLine($"ЦПС отключен");
        }

        private static void Cps_Device_Connected(DeviceBase device)
        {
            Console.WriteLine($"Подключен ЦПС {device.DeviceType} №{device.DeviceId}");
        }
    }
}
