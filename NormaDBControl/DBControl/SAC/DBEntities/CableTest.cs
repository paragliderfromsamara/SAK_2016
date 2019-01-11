using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NormaMeasure.DBControl.SAC.DBEntities
{
    public class CableTest : DBEntityBase
    {
        private uint _cableId = 0;
        public uint CableId
        {
            get
            {
                return _cableId;
            }
        }

        protected void setDefaultParameters()
        {
            throw new NotImplementedException();
        }
    }
}
