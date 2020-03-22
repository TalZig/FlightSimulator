using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    class model : Notifier
    {
        public double[] valuesFromServer = new double[8];
        public double[] valuesFromView = new double[4];
        public volatile bool stop = false;
        MyTcpClien myClient = new MyTcpClien();
        public void connect(string ip, int port)
        {
            myClient.connect(ip, port);
        }
        public void start()
        {
            Thread thread = new Thread(startReadAndWrite);
            thread.Start();
        }
        private void startReadAndWrite()
        {
            while (!stop)
            {
                //values from the server
                myClient.write("get /instrumentation/heading-indicator/indicated-heading-deg");
                valuesFromServer[0] = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/gps/indicated-vertical-speed");
                valuesFromServer[1] = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/gps/indicated-ground-speed-kt");
                valuesFromServer[2] = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt");
                valuesFromServer[3] = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/gps/indicated-altitude-ft");
                valuesFromServer[4] = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/attitude-indicator/indicated-roll-deg");
                valuesFromServer[5] = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/attitude-indicator/indicated-pitch-deg");
                valuesFromServer[6] = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/altimeter/indicated-altitude-ft");
                valuesFromServer[7] = Double.Parse(myClient.read());

                //values from the view that we need to update
                myClient.write("set /controls/flight/rudder" + valuesFromView[0].ToString());
                myClient.write("set /controls/flight/elevator" + valuesFromView[1].ToString());
                myClient.write("set /controls/engines/current-engine/throttle" + valuesFromView[2].ToString());
                myClient.write("set /controls/flight/aileron" + valuesFromView[3].ToString());
                //location of the airplane
            }
        }
        public void updateValue(String info, double newVal)
        {
            if(info == "rudder")
            {
                valuesFromView[0] = updateRudder(newVal);
            }
            if(info == "elevator")
            {
                valuesFromView[1] = updateElevator(newVal);
            }
            if(info == "throttle")
            {
                valuesFromView[2] = updateThrottle(newVal);
            }
            if(info == "aileron")
            {
                valuesFromView[3] = updateAileron(newVal);
            }
        }

        private double updateRudder(double newVal)
        {
            if(newVal > 1)
            {
                return 1;
            }
            if(newVal < -1)
            {
                return -1;
            }
            return newVal;
        }

        private double updateElevator(double newVal)
        {
            {
                if (newVal > 1)
                {
                    return 1;
                }
                if (newVal < -1)
                {
                    return -1;
                }
                return newVal;
            }
        }

        private double updateThrottle(double newVal)
        {
            {
                if (newVal > 1)
                {
                    return 1;
                }
                if (newVal < -1)
                {
                    return -1;
                }
                return newVal;
            }
        }

        private double updateAileron(double newVal)
        {
            {
                if (newVal > 1)
                {
                    return 1;
                }
                if (newVal < 0)
                {
                    return 0;
                }
                return newVal;
            }
        }
    }
}
