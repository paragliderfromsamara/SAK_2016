using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NormaMeasure.Devices.SAC.CPSUnits
{
    public class CPSCommutator : CPSUnit
    {
        public delegate void CPSCommutator_Handler(CPSCommutator commutator);
        public CPSCommutator(SACCPS _cps) : base(_cps)
        {
            ClearState();
        }

        /// <summary>
        /// Формирует карту реле коммутатора
        /// </summary>
        protected virtual void MakeReleMap()
        {
            releMap = new byte[][] {
                K35,
                K36,
                K37,
                K38,
                K39,
                K40,
                K41_42,
                K43_44,
                K45,
                K46,
                K47,
                K48,
                K49,
                K50,
                K51_52
            };
        }

        protected override void SetUnitAddress()
        {
            unitCMD_Address = 0x07;
        }



        /// <summary>
        /// Устанавливает коммутатор в необходимое состояние
        /// </summary>
        /// <param name="newState"></param>
        public void SetCommutatorState(byte[][] newState)
        {
            byte[] state = (byte[])ZeroState.Clone(); 
            for (int i=0; i<newState[0].Length; i++)
            {
                for(int j=0; j<newState.Length; j++)
                {
                    state[i] |= newState[j][i];
                }
            }
            CurrentState = state;

        }


        public void ClearState()
        {
            CurrentState = ZeroState;
        }

        public static readonly byte[] ZeroState = new byte[] { 0x00, 0x00 };
        public static readonly byte[] K35 = new byte[] { 0x01, 0x00 };
        public static readonly byte[] K36 = new byte[] { 0x02, 0x00 };
        public static readonly byte[] K37 = new byte[] { 0x04, 0x00 };
        public static readonly byte[] K38 = new byte[] { 0x08, 0x00 };
        public static readonly byte[] K39 = new byte[] { 0x10, 0x00 };
        public static readonly byte[] K40 = new byte[] { 0x20, 0x00 };
        public static readonly byte[] K41_42 = new byte[] { 0x40, 0x00 };
        public static readonly byte[] K43_44 = new byte[] { 0x80, 0x00 };
        public static readonly byte[] K45 = new byte[] { 0x00, 0x01 };
        public static readonly byte[] K46 = new byte[] { 0x00, 0x02 };
        public static readonly byte[] K47 = new byte[] { 0x00, 0x04 };
        public static readonly byte[] K48 = new byte[] { 0x00, 0x08 };
        public static readonly byte[] K49 = new byte[] { 0x00, 0x10 };
        public static readonly byte[] K50 = new byte[] { 0x00, 0x20 };
        public static readonly byte[] K51_52 = new byte[] { 0x00, 0x40 };



        /// <summary>
        /// Список всех известных реле
        /// </summary>
        public byte[][] ReleMap
        {
            get
            {
                if (releMap == null) MakeReleMap();
                return releMap;
            }
        }

        /// <summary>
        /// Поочередно включает реле
        /// </summary>
        public void Test()
        {
            for(int i = 0; i<5; i++)
            {
                foreach (byte[] rele in ReleMap)
                {
                    byte[][] state = new byte[][] { rele };
                    SetCommutatorState(state);
                    Thread.Sleep(300);
                }
                ClearState();
            }

        }

        /// <summary>
        /// Формирует команду коммутатора в ЦПС
        /// </summary>
        /// <param name="releState"></param>
        /// <returns></returns>
        private byte[] BuildCommandByState(byte[] releState)
        {
            byte j = 1;
            List<byte> cmd = new List<byte>();
            for(int i=0; i<releState.Length; i++)
            {
                cmd.Add((byte)(unitCMD_Address | SACCPS.TwoBytes_cmd));
                cmd.Add(j);
                cmd.Add((byte)(releState[i]));
                j <<= 1;
            }
            return cmd.ToArray();
        }

        protected byte[] zeroState;
        protected byte[][] releMap;

        protected byte[] curState;
        public byte[] State => CurrentState;
        protected byte[] CurrentState
        {
            get
            {
                return curState;
            }
            set
            {
                if (curState != value)
                {
                    byte[] cmd = BuildCommandByState(value);
                    if (cps.Write(cmd))
                    {
                        curState = value;
                        OnCommutator_StateChanged?.Invoke(this);
                        Thread.Sleep(300);
                    }
                }
            }
        }

        public event CPSCommutator_Handler OnCommutator_StateChanged;
    }
}
