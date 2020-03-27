using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Sockets;
    using System.IO;
    using System.Text.RegularExpressions;
    class MyTcpClient : ItelnetClient
    {
        private static Mutex mutex1 = new Mutex();
        private static Mutex mutex2 = new Mutex();
        private TcpClient tcpClient;
        private Stream stream;
        public void Connect(string ip, int port)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(ip, port);
            this.stream = tcpClient.GetStream();
        }
        /*        void write(string command)
                {
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
                    NetworkStream stream = myClient.GetStream();
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", command);
                }*/
        public void disconnect()
        {
            tcpClient.Close();
        }
        public void write(string command)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
            try
            {
                stream.Write(data, 0, data.Length);
            }
            catch (Exception)
            {
                Console.WriteLine("server disconnected");
                //this.disconnect();
            }
        }
        public string read()
        {
            byte[] data = new byte[100];
            try
            {
                int k = stream.Read(data, 0, 100);
            }
            catch (Exception)
            {
                Console.WriteLine("-99999");
                return "-99999";
            }
            StringBuilder builder = new StringBuilder();
            foreach (char value in data)
            {
                builder.Append(value);
            }
            string returnedValue = builder.ToString();
            //Console.WriteLine(returnedValue);

            //for integer
            string temp = Regex.Match(returnedValue, @"[+-]\d+").Value;
            //for floating point
            returnedValue = Regex.Match(returnedValue, @"[+-]?\d+.\d+").Value;
            //Console.WriteLine(returnedValue);

            if (returnedValue == "" && temp == "")
                return "-999";
            if (returnedValue == "")
            {
                //Console.WriteLine(temp + "check2");
                return temp;
            }
            else return returnedValue;
        }

    }
}