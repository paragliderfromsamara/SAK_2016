using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.ProtocolBuilder.MSWord;
namespace AppTest
{
    class WordProtocolTest
    {
        public static void Start()
        {
            MSWordProtocol protocol = new MSWordProtocol();
            protocol.Init();
            protocol.Finalise();
        }
    }
}
