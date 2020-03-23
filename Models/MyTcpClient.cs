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
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", command);
            }
            public string read()
            {
                byte[] data = new byte[100];
                int k = stream.Read(data, 0, 100);
                StringBuilder builder = new StringBuilder();
                foreach (char value in data)
                {
                    builder.Append(value);
                }
                string returnedValue = builder.ToString();
                Console.WriteLine(returnedValue);
                //for floating point
                returnedValue = Regex.Match(returnedValue, @"\d+.\d+").Value;
                Console.WriteLine(returnedValue);

                //for integer
                string temp = Regex.Match(returnedValue, @"\d+").Value;

            if (returnedValue == "")
            {
                Console.WriteLine(temp + "check");
                return temp;
            }
            else return returnedValue;
            }

        }
    }

