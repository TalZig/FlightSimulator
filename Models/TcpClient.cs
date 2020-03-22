using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Configuration;


namespace FlightSimulator.Models
{
    class TcpClient : Notifier
    {
        volatile bool stop = false;
        System.Net.Sockets.TcpClient myClient;
         void connect()
        {
            try
            {
                myClient = new System.Net.Sockets.TcpClient((ConfigurationManager.AppSettings["IP"]),
                    Int32.Parse(ConfigurationManager.AppSettings["IP"]));
            }
            catch (Exception)
            {
                Console.WriteLine("problem with connect to the server");
            }
        }
        void write(string command)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
            NetworkStream stream = myClient.GetStream();
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", command);
        }
        public void disconnect()
        {
            stop = true;
            myClient.Close();
        }
        public void start()
        {
            //new Thread()
            //{

            //}
        }



    }
}
