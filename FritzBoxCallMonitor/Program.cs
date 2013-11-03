using System;
using System.Diagnostics;
using System.Linq;
using System.Net;

//Enable FritzBox Callmonitor: #96*5*
//Disable FritzBox Callmonitor: #96*4*

namespace FritzBoxCallMonitor
{
    internal class Program
    {
        internal static Arguments Arguments;

        private static void Main()
        {
            Arguments = Arguments.ParseCommandline();

            if (Arguments.Minimize)
                ConsoleWindowUnmanaged.Minimize();

            Console.Title = String.Format("FritzBoxCallMonitor on {0}", Arguments.FritzBox);

            IPAddress fritzBoxIp = null;
            IPAddress.TryParse(Arguments.FritzBox, out fritzBoxIp);
            if (fritzBoxIp == null)
                fritzBoxIp = Dns.GetHostEntry(Arguments.FritzBox).AddressList.First();

            Log(String.Format("Program OnOutgoingCall = {0}", Arguments.OnOutgoingCall));
            Log(String.Format("Program OnIncomingCall = {0}", Arguments.OnIncomingCall));
            Log(String.Format("Program OnConnected = {0}", Arguments.OnConnected));
            Log(String.Format("Program OnConnectionEnd = {0}", Arguments.OnConnectionEnd));

            EventDrivenTcpClient tcpClient = new EventDrivenTcpClient(fritzBoxIp, 1012, true);
            tcpClient.DataReceived += tcpClient_DataReceived;
            tcpClient.ConnectionStatusChanged += tcpClient_ConnectionStatusChanged;
            tcpClient.ReconnectInterval = 30000;
            tcpClient.Connect();

            while (true)
                Console.ReadKey(true);
        }

        private static void tcpClient_ConnectionStatusChanged(EventDrivenTcpClient sender, EventDrivenTcpClient.ConnectionStatus status)
        {
            Log(String.Format("Connection status changed: {0}", status));
        }

        private static void tcpClient_DataReceived(EventDrivenTcpClient sender, object data)
        {
            Log(String.Format("Message received: {0}", data));

            //Outgoing call:             Date;CALL;ConnectionID;LocalExtension;LocalNumber;RemoteNumber;
            //Incoming call:             Date;RING;ConnectionID;RemoteNumber;LocalNumber;
            //On connection established: Date;CONNECT;ConnectionID;LocalExtension;RemoteNumber;
            //On connection end:         Date;DISCONNECT;ConnectionID;DurtionInSeconds;

            string[] messageParts = data.ToString().Split(';');
            DateTime timestamp = DateTime.Parse(messageParts[0]);
            string eventType = messageParts[1];
            string connectionId = messageParts[2];

            switch (eventType)
            {
                case "CALL":
                    if (!string.IsNullOrEmpty(Arguments.OnOutgoingCall))
                    {
                        string localExtension = messageParts[3];
                        string localNumber = messageParts[4];
                        string remoteNumber = messageParts[5];

                        Process.Start(Arguments.OnOutgoingCall, String.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\"", timestamp, "CALL", connectionId, localExtension, localNumber, remoteNumber));
                    }
                    break;
                case "RING":
                    if (!string.IsNullOrEmpty(Arguments.OnIncomingCall))
                    {
                        string remoteNumber = messageParts[3];
                        string localNumber = messageParts[4];

                        Process.Start(Arguments.OnIncomingCall, String.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\"", timestamp, "RING", connectionId, remoteNumber, localNumber));
                    }
                    break;
                case "CONNECT":
                    if (!string.IsNullOrEmpty(Arguments.OnConnected))
                    {
                        string localExtension = messageParts[3];
                        string remoteNumber = messageParts[4];

                        Process.Start(Arguments.OnConnected, String.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\"", timestamp, "CONNECT", connectionId, localExtension, remoteNumber));
                    }
                    break;
                case "DISCONNECT":
                    if (!string.IsNullOrEmpty(Arguments.OnConnectionEnd))
                    {
                        int durtionInSeconds = int.Parse(messageParts[3]);

                        Process.Start(Arguments.OnConnectionEnd, String.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\"", timestamp, "DISCONNECT", connectionId, durtionInSeconds));
                    }
                    break;
            }
        }

        private static void Log(string message)
        {
            if (Console.BufferWidth < message.Length)
                Console.BufferWidth = message.Length;

            Console.WriteLine(String.Format("{0:yyyy-MM-dd HH:mm:ss}\t{1}", DateTime.Now, message));
        }
    }
}
