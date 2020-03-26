using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    public class Model : Notifier
    {
        public Model() {}
        public double[] valuesFromView = new double[4];
        public volatile bool stop = false;
        private Location vmLocation;
        public Location MLocation
        {
            get
            {
                return vmLocation;
            }
            set
            {
                vmLocation = value;
                this.NotifyPropertyChanged("MLocation");
                Console.WriteLine("Location in M: " + MLocation);
            } 
        }


        private double verticalSpeed;
        public double VerticalSpeed
        {
            get
            {
                return verticalSpeed;
            }
            set
            {
                if (verticalSpeed != value)
                {
                    verticalSpeed = value;
                    this.NotifyPropertyChanged("VerticalSpeed");
                }
            }
        }
        private double headingDeg;
        public double HeadingDeg
        {
            get
            {
                return headingDeg;
            }
            set
            {
                if (headingDeg != value)
                {
                    headingDeg = value;
                    this.NotifyPropertyChanged("HeadingDeg");
                }
            }
        }
        private double groundSpeedKt;
        public double GroundSpeedKt
        {
            get
            {
                return groundSpeedKt;
            }
            set
            {
                if (groundSpeedKt != value)
                {
                    groundSpeedKt = value;
                    this.NotifyPropertyChanged("GroundSpeedKt");
                }
            }
        }
        private double indicatedSpeedKt;
        public double IndicatedSpeedKt
        {
            get
            {
                return indicatedSpeedKt;
            }
            set
            {
                if (indicatedSpeedKt != value)
                {
                    indicatedSpeedKt = value;
                    this.NotifyPropertyChanged("IndicatedSpeedKt");
                }
            }
        }
        private double altitudeFt;
        public double AltitudeFt
        {
            get
            {
                return altitudeFt;
            }
            set
            {
                if (altitudeFt != value)
                {
                    altitudeFt = value;
                    this.NotifyPropertyChanged("AltitudeFt");
                }
            }
        }
        private double rollDeg;
        public double RollDeg
        {
            get
            {
                return rollDeg;
            }
            set
            {
                if (rollDeg != value)
                {
                    rollDeg = value;
                    this.NotifyPropertyChanged("RollDeg");
                }
            }
        }
        private double pitchDeg;
        public double PitchDeg
        {
            get
            {
                return pitchDeg;
            }
            set
            {
                if (pitchDeg != value)
                {
                    pitchDeg = value;
                    this.NotifyPropertyChanged("PitchDeg");
                }
            }
        }
        private double indicatedAltitudeFt;
        public double IndicatedAlitudeFt
        {
            get
            {
                return indicatedAltitudeFt;
            }
            set
            {
                if (indicatedAltitudeFt != value)
                {
                    indicatedAltitudeFt = value;
                    this.NotifyPropertyChanged("IndicatedAltitudeFt");
                }
            }
        }

        MyTcpClient myClient = new MyTcpClient();
        public void Connect(string ip, int port)
        {
            myClient.Connect(ip, port);
            this.Start();
        }
        public void Start()
        {
            Thread thread = new Thread(StartReadAndWrite);
            thread.Start();
        }
        private void StartReadAndWrite()
        {
            double tempX = 0;
            double tempY = 0;
            while (!stop)
            {
                //values from the server
                myClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                HeadingDeg = Double.Parse(myClient.read());
                
                myClient.write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                VerticalSpeed = Double.Parse(myClient.read());
                
                myClient.write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                GroundSpeedKt = Double.Parse(myClient.read());
                
                myClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                IndicatedSpeedKt = Double.Parse(myClient.read());
                
                myClient.write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                AltitudeFt = Double.Parse(myClient.read());

                myClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                RollDeg = Double.Parse(myClient.read());

                myClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                PitchDeg = Double.Parse(myClient.read());

                myClient.write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                IndicatedAlitudeFt = Double.Parse(myClient.read());

                myClient.write("get /position/latitude-deg\r\n");
                tempX = Double.Parse(myClient.read());

                myClient.write("get /position/longitude-deg\r\n");
                tempY = Double.Parse(myClient.read());

                myClient.write("set /controls/flight/rudder\r\n" + valuesFromView[0].ToString());
                myClient.write("set /controls/flight/elevator\r\n" + valuesFromView[1].ToString());
                myClient.write("set /controls/engines/current-engine/throttle\r\n" + valuesFromView[2].ToString());
                myClient.write("set /controls/flight/aileron\r\n" + valuesFromView[3].ToString());
                MLocation = new Location(tempX, tempY);

                //values from the view that we need to update
                //location of the airplane
                Thread.Sleep(2000);

            }
            this.myClient.disconnect();
        }
        public void UpdateValue(String info, double newVal)
        {
            if (info == "rudder")
            {
                valuesFromView[0] = UpdateRudder(newVal);
                //myClient.write("set /controls/flight/rudder\r\n" + valuesFromView[0].ToString());
            }
            if (info == "elevator")
            {
                valuesFromView[1] = UpdateElevator(newVal);
                //myClient.write("set /controls/flight/elevator\r\n" + valuesFromView[1].ToString());

            }
            if (info == "throttle")
            {
                valuesFromView[2] = UpdateThrottle(newVal);
                //myClient.write("set /controls/engines/current-engine/throttle\r\n" + valuesFromView[2].ToString());

            }
            if (info == "aileron")
            {
                valuesFromView[3] = UpdateAileron(newVal);
                //myClient.write("set /controls/flight/aileron\r\n" + valuesFromView[3].ToString());

            }
        }

        private double UpdateRudder(double newVal)
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

        private double UpdateElevator(double newVal)
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

        private double UpdateThrottle(double newVal)
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

        private double UpdateAileron(double newVal)
        {
            {
                if (newVal > 1)
                {
                    return 0;
                }
                if (newVal < 0)
                {
                    return 1;
                }
                return 1 - newVal;
            }
        }
    }
}
