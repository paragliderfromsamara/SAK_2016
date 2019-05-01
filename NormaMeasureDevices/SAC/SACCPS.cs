using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using NormaMeasure.Devices.SAC.CPSUnits;
using NormaMeasure.DBControl.Tables;

namespace NormaMeasure.Devices.SAC
{
    public class SACCPS : DeviceBase
    {
        #region Команды ЦПС
        /// <summary>
        /// Флаг подразумевающий чтение резульатата
        /// </summary>
        public const byte Read_cmd = 0x80;
        /// <summary>
        /// Байт характеризующий отправляемую команду как двух байтную
        /// </summary>
        public const byte TwoBytes_cmd = 0x40;
        /// <summary>
        /// Байт для считывания результата
        /// </summary>
        public const byte ReadResult = 0x20;

        /// <summary>
        /// Команда запроса номера ЦПС
        /// </summary>
        public const byte GetCPSNumber_cmd = 0xA8;
        public const byte SetRizolLed_cmd = 0x00;

        #endregion

        public SACCPS(SAC_Device _sac) : base()
        {
            sac = _sac;
            deviceTypeName = "CPS";
            //InitUnits();
        }


        public virtual void InitUnits()
        { 
            commutator = new CPSCommutator(this);
            InitMeasureUnits();
        }

        protected virtual void InitMeasureUnits()
        {
            unit110 = new U110(this);
            unit120 = new U120(this);
            unit130 = new U130(this, 1);
            unit160 = new U160(this);
            measureUnits.Add(unit110);
            measureUnits.Add(unit120);
            measureUnits.Add(unit130);
            measureUnits.Add(unit160);
        }


        public CPSMeasureUnit GetMeasureUnit(SACMeasurePoint point)
        {
            foreach(CPSMeasureUnit unit in measureUnits)
            {
                if (unit.IsAllowedParameter(point.parameterType.ParameterTypeId)) return unit;
            }
            return null;
        }


        #region overrides
        protected override bool CheckConnectionResult(byte[] data)
        {
            byte serial = data[0];
            if (CheckCPSNumber(serial))
            {
                this.deviceId = serial.ToString();
                return true;
            }else
            {
                return false;
            }
        }

        protected override void ConfigureDevicePort()
        {
            base.ConfigureDevicePort();
            DevicePort.BaudRate = 9600;
            DevicePort.ReadTimeout = 1000;
        }

        protected override void FillDeviceCommands()
        {
            base.FillDeviceCommands();
            FindDevice_cmd = new byte[] { ReadResult | GetCPSNumber_cmd, 0x00 };
        }

        #endregion

        /// <summary>
        /// Проверка номера ЦПС
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        protected virtual bool CheckCPSNumber(byte number)
        {
            bool f = (240 > number & number > 130);
            f &= ((String.IsNullOrWhiteSpace(deviceId)) || (deviceId == number.ToString()));
            return f;
        }

        /// <summary>
        /// Установка узлов Rизол
        /// </summary>
        /// <param name="led_1">1-Диод</param>
        /// <param name="led_2">2-Диод</param>
        /// <param name="led_3">3-Диод</param>
        protected virtual void SetRizolLedLine(bool led_1, bool led_2, bool led_3)
        {
            byte[] cmd = new byte[] { SetRizolLed_cmd, 0x00 };
            if (led_1) cmd[1] |= 0x01;
            if (led_2) cmd[1] |= 0x02;
            if (led_3) cmd[1] |= 0x03;
            WriteBytes(cmd, true);
        }

        /// <summary>
        /// Тест диодов узлов Rиз
        /// </summary>
        public void LedLineTest()
        {
            int state = 1;
            bool l1, l2, l3;
            for(int i = 0; i<6; i++)
            {
                SetRizolLedLine(state == 1, state == 2, state == 4);
                l1 = state == 1;
                l2 = state == 2;
                l3 = state == 4;
                Thread.Sleep(800);
                state *= 2;
                if (state > 4) state = 1;
            }
        }

        //

        private CPSCommutator commutator;

        public SAC_Device sac;

        private U110 unit110;
        private U120 unit120;
        private U130 unit130;
        private U160 unit160;

        public CPSCommutator Commutator => commutator;

        private List<CPSMeasureUnit> measureUnits = new List<CPSMeasureUnit>();
        public U110 Unit110 => unit110;
        public U120 Unit120 => unit120;
        public U130 Unit130 => unit130;
        public U160 Unit160 => unit160;



    }

    public enum LeadCommutationType
    {
        A, B, AB
    }
}
