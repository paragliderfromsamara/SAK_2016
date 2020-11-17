using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;

namespace TeraMicroMeasure.XmlObjects
{
    public class ServerXmlState : NormaXmlObject
    {
        const string ClientsListTagName = "ClientsList";
        const string ClientElementTagName = "ClientXmlState";



        public ServerXmlState() : base()
        {

        }

        public ServerXmlState(string inner_xml) : base(inner_xml)
        {

        }

        /// <summary>
        /// IP адрес сервера
        /// </summary>
        public string IPAddress
        {
            get
            {
                return getXmlProp("ip");
            }
            set
            {
                setXmlProp("ip", value);
            }
        }

        /// <summary>
        /// TCP порт на котором работает сервер
        /// </summary>
        public int Port
        {
            get
            {
                int p = 8000;
                tryGetIntXmlProp("port", out p);
                return p;
            }
            set
            {
                setXmlProp("port", value.ToString());
            }
        }

        /// <summary>
        /// Периодичность запросов клиентов к серверу
        /// </summary>
        public int RequestPeriod
        {
            get
            {
                int p = 1000;
                tryGetIntXmlProp("request_period", out p);
                return p;
            }
            set
            {
                setXmlProp("request_period", value.ToString());
            }
        }

        private Dictionary<string, ClientXmlState> _clients = null;
        /// <summary>
        /// Список подключенных клиентов по IP адресам
        /// </summary>
        public Dictionary<string, ClientXmlState> Clients
        {
            get
            {
                if (_clients == null)
                {
                    Dictionary<string, string> raw = GetNodesInnerXmlFromContainer(ClientsListTagName, ClientElementTagName);
                    _clients = new Dictionary<string, ClientXmlState>();
                    foreach (string ip in raw.Keys)
                    {
                        _clients.Add(ip, new ClientXmlState(raw[ip]));
                    }
                }
                return _clients;
                //xm
            }
        }

        private void resetClients() { _clients = null; }

        /// <summary>
        /// Добавление клиента в списко подключенных
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="cl"></param>
        public void AddClient(ClientXmlState cl)
        {
            AddElementToContainer(ClientsListTagName, cl.InnerXml);
            resetClients();
        }

        public void RemoveClient(ClientXmlState cs)
        {
            RemoveElementFromContainer(ClientsListTagName, cs.RootElementTagName, cs.ClientIP);
            resetClients();
        }

        public void ReplaceClient(ClientXmlState cl)
        {
            ReplaceElementOnContainer(ClientsListTagName, cl.RootElementTagName, cl.InnerXml, cl.ClientIP);
            resetClients();
        }

    }
}
