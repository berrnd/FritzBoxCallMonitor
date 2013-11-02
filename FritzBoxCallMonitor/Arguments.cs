using System;

namespace FritzBoxCallMonitor
{
    public class Arguments
    {
        private Arguments()
        {
            this.FritzBox = "fritz.box";
        }

        public string FritzBox { get; private set; }
        public string OnIncomingCall { get; private set; }
        public string OnOutgoingCall { get; private set; }
        public string OnConnected { get; private set; }
        public string OnConnectionEnd { get; private set; }

        public static Arguments ParseCommandline()
        {
            Arguments parsed = new Arguments();

            foreach (string item in Environment.GetCommandLineArgs())
            {
                if (item.Contains("="))
                {
                    string key = item.Split('=')[0].ToLower();
                    string value = item.Split('=')[1];

                    switch (key)
                    {
                        case "onincomingcall":
                            parsed.OnIncomingCall = value;
                            break;
                        case "onoutgoingcall":
                            parsed.OnOutgoingCall = value;
                            break;
                        case "onconnected":
                            parsed.OnConnected = value;
                            break;
                        case "onconnectionend":
                            parsed.OnConnectionEnd = value;
                            break;
                        case "fritzbox":
                            parsed.FritzBox = value;
                            break;
                    }
                }
            }

            return parsed;
        }
    }
}
