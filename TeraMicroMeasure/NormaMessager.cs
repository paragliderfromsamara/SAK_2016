using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NormaMeasure.SocketControl;

namespace TeraMicroMeasure
{ 
    public partial class NormaMessager : Form
    {
        public EventHandler SendMessage;
        private NormaServerClient _client;
        private NormaServerClient client
        {
            set
            {
                _client = value;
                _client.OnMessageReceived += OnMeassageReceived;
                _client.MessageSent += OnMeassageSent; 
            }get { return _client; }
        }
        public NormaMessager(NormaServerClient cl)
        {
            InitializeComponent();
            client = cl;
        }

        private string OnMeassageReceived(string message, NormaServerClient cl)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new NormaServerClientMessageDelegate(OnMeassageReceived), new object[] { message, cl });
                return "Ok";
            }
            else
            {
                AddToChat(message, cl.IpAddress);
                return "Ok";
            }
        }

        private string OnMeassageSent(string message, NormaServerClient cl)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new NormaServerClientMessageDelegate(OnMeassageReceived), new object[] { message, cl });
                return "Ok";
            }
            else
            {
                SetMessageSent();
                return "Ok";
            }
        }

        public void AddToChat(string s, string sender)
        {
            richTextBox1.Text = richTextBox1.Text + $"{sender}:\n{s}\n";
        }

        

        public void SetMessageSent()
        {
            AddToChat(richTextBox2.Text, "Я");
            richTextBox2.ResetText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = richTextBox2.Text;
            client.MessageTo = msg;
        }
    }
}
