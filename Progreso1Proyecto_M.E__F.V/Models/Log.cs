using System.Net;
using System;
using System.IO;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Progreso1Proyecto_M.E__F.V.Models
{
    public sealed class Log
    {
        private static Log instance = null;
        private static readonly object lockObject = new object();
        private StreamWriter logWriter;
        private string ipAddress;

        private Log()
        {
            logWriter = new StreamWriter("log.txt", true);
            ipAddress = GetLocalIPAddress();
        }

        public static Log Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new Log();
                        }
                    }
                }
                return instance;
            }
        }

        public void LogButtonClick(string action)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"{timestamp}|{ipAddress}|{action}";
            logWriter.WriteLine(logEntry);
        }

        public void CloseLogFile()
        {
            logWriter.Close();
            instance = null; // Reset the instance for reinitialization
        }

        private string GetLocalIPAddress()
        {
            string ipAddress = string.Empty;
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                    break;
                }
            }

            return ipAddress;
        }
    }
}

