using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    interface ItelnetClient
    {
        void Connect(string ip, int port);
        void disconnect();
        void write(string command);
        String read();

    }
}
