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
    class MyTcpClient : ITelnetClient
    {
        private TcpClient tcpClient;
        private Stream stream;
        public void Connect(string ip, int port)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(ip, port);
            this.stream = tcpClient.GetStream();
        }
        
        //Function that disconnect from the server.
        public void Disconnect()
        {
            tcpClient.Close();
        }
        //Function to write to the server.
        public void Write(string command)
        {
            //Console.WriteLine(command);
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
        //Function that read from the server.
        public string Read()
        {
            byte[] data = new byte[100];
            this.stream.ReadTimeout = 10000;
            try
            {
                int k = stream.Read(data, 0, 100);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("time"))
                    return "Timeout";
                return "-99999";
            }
            StringBuilder builder = new StringBuilder();
            foreach (char value in data)
            {
                builder.Append(value);
            }
            string returnedValue = builder.ToString();

            //Taking the number from the string with regex:

            //For integer
            string temp = Regex.Match(returnedValue, @"[+-]\d+").Value;
            //For floating point
            returnedValue = Regex.Match(returnedValue, @"[+-]?\d+.\d+").Value;

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