using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    class SimulatorModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        double[] values = new double[8];
        //Server server;
        TcpClient client;
    }
}
