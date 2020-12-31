using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;

namespace NormaMeasure.Devices
{
    public class COMDeviceFinder
    {
        private EventHandler OnDeviceFound;
        private DeviceCommandProtocol commandProtocol;
        public COMDeviceFinder(DeviceCommandProtocol command_protocol)
        {
            commandProtocol = command_protocol;
        }

        private bool AttemptToConnection()
        {
            string[] port_list;
            bool flag = false;
            string portNameWas = GetLastConnectedPortName();
            isOnFinding = true;
            repeat:
            port_list = SerialPort.GetPortNames();
            if (port_list.Length == 0)
            {
                System.Windows.Forms.DialogResult r = System.Windows.Forms.MessageBox.Show("На компьютере отсутствуют доступные COM порты!", "Ошибка связи", System.Windows.Forms.MessageBoxButtons.RetryCancel, System.Windows.Forms.MessageBoxIcon.Information);
                if (r == System.Windows.Forms.DialogResult.Retry) goto repeat;
            }
            else
            {
                if (port_list.Contains(portNameWas)) flag = TryConnectToPort(portNameWas);
                if (!flag)
                {
                    foreach (string s in port_list)
                    {
                        if (s == portNameWas) continue;
                        if (flag = TryConnectToPort(s)) break;
                    }
                }

            }
            isOnFinding = false;

            Debug.WriteLine($"{DeviceType} status {flag}");
            return flag;
        }
        private bool TryConnectToPort(string port_name)
        {
            bool flag = false;
            try
            {
                ClosePort();
                device_port.PortName = port_name;
                if (OpenPort())
                {
                    if (CheckConnection())
                    {
                        flag = true;
                    }
                    else
                    {
                        ClosePort();
                    }
                }
            }
            catch (System.UnauthorizedAccessException)
            {
                ClosePort();
            }
            catch (System.IO.IOException)
            {
                ClosePort();
            }
            catch (TimeoutException)
            {
                ClosePort();
            }
            return flag;
        }
    }
}
