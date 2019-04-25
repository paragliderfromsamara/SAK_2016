using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace NormaMeasure.Devices.SAC
{
    public class SACCPS : DeviceBase
    {
        #region Команды ЦПС
        /// <summary>
        /// Команда запроса номера ЦПС
        /// </summary>
        const byte GetCPSNumber_cmd = 0xA8;

        #endregion

        public SACCPS() : base()
        {
            deviceTypeName = "CPS";
        }


        #region overrides
        protected override bool CheckConnection()
        {
            bool flag = false;
            byte[] cmd = new byte[] { GetCPSNumber_cmd };
            byte serial = 0;
            if (IsOpen) DevicePort.Close();
            DevicePort.Open();
            DevicePort.Write(cmd, 0, cmd.Length);
            serial = (byte)DevicePort.ReadByte();
            DevicePort.Close();
            flag = CheckCPSNumber(serial);
            this.deviceId = flag ? serial.ToString() : "N/A";
            return flag;
            //DevicePort.Read();
            /*
            bool flag = false;
            byte serial = 0;
            byte[] buffer = new byte[1];
            byte[] cmd = new byte[] { GetCPSNumber_cmd };

            int i = WriteCmdAndReadBytesArr(cmd, buffer, 0, buffer.Length);
            if (i>0)
            {
                serial = buffer[0];
                flag = CheckCPSNumber(serial);
            }
            this.deviceId = flag ? serial.ToString() : "N/A";
            return flag;
            */
            //return base.CheckConnection(port_name);

        }

        protected override void ConfigureDevicePort()
        {
            base.ConfigureDevicePort();
            DevicePort.BaudRate = 9600;
            DevicePort.ReadTimeout = 1000;
        }

        #endregion

        /// <summary>
        /// Проверка номера ЦПС
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        protected virtual bool CheckCPSNumber(byte number)
        {
            return (240 > number & number > 130);
        }

        //
    }
}
