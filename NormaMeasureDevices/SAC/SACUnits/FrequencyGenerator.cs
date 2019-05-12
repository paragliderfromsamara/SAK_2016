using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormaMeasure.Devices.SAC.SACUnits
{
    public class FrequencyGenerator : SACUnit
    { 
        public FrequencyGenerator(SACTable _table) : base(_table)
        {

        }

        public void SetFrequency(float frequency)
        {
            int intFreq = ConvertFreqToInt(frequency);
            byte lowByte = (byte)(intFreq % 256);
            byte hightByte = (byte)(intFreq / 256);
            byte[] freqCmd = new byte[] { lowByte, hightByte };
            table.SendCommand(unitCMD_Address, freqCmd);
        }
        
        public int ConvertFreqToInt(float freq)
        {
            int intFreq = (int)freq;
            if (freq < 10) return 1;
            else if (freq < 40) return ((intFreq / 2) * 2);
            else return ((intFreq / 8) * 8);
        }

        protected override void SetUnitAddress()
        {
            base.SetUnitAddress();
            unitCMD_Address = 0x21;
        }

        private byte freqLowByte;
        private byte freqHightByte;
        private float curFrequency;
    }
}
