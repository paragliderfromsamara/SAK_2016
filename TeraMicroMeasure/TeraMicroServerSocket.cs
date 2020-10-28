using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace TeraMicroMeasure
{
    public class ClientObject
    {
        public TcpClient client;
        public ClientObject(TcpClient tcpClient)
        {
            client = tcpClient;
        }

        public void Process()
        {
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[64]; // буфер для получаемых данных
                while (true)
                {
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();

                    // Console.WriteLine(message);
                    
                    // отправляем обратно сообщение в верхнем регистре
                    message = message.Substring(message.IndexOf(':') + 1).Trim().ToUpper();
                    data = Encoding.Unicode.GetBytes(message);
                    Debug.WriteLine(message);
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка соединения");
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }
        }
    }

    public class TeraMicroServerThread
    {
        const int port = 13389;
        TcpListener listener;
        Thread serverThread;
        public TeraMicroServerThread()
        {
            serverThread = new Thread(InitServer);
            serverThread.Start();
            Debug.WriteLine(GetLocalIPAddress());
        }
        private void InitServer()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            Debug.WriteLine(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[3];
            Debug.WriteLine(ipAddress.GetAddressBytes()[0].ToString() + ":" + ipAddress.GetAddressBytes()[1].ToString() + ":" + ipAddress.GetAddressBytes()[2].ToString() + ":" + ipAddress.GetAddressBytes()[3].ToString());
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            //TcpClient client;
            try
            {
                listener = new TcpListener(ipAddress, port);
                listener.Start();
                //Console.WriteLine("Ожидание подключений...");
                Debug.WriteLine("Ожидание подключений...");
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client);

                    // создаем новый поток для обслуживания нового клиента
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                    Debug.WriteLine("Найден клиент...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка соединения");
            }
            finally
            {
                if (listener != null) listener.Stop();
            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

    }
}
