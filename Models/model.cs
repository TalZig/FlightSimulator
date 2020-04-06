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
    public class Model : INotifyPropertyChanged
    {
        public Model()
        {
            location = new Location(32.0, 34.888852);
            msg = "Plane is in bounds";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public double[] valuesFromView = new double[4];
        public volatile bool stop = false;
        private Location location;
        public Location Location
        {
            get
            {
                return location;
            }
            set
            {
                if (!Location.Equals(value))
                {
                    location = value;
                    this.NotifyPropertyChanged("Location");
                }
            }
        }

        public void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
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
                    this.NotifyPropertyChanged("IndicatedAlitudeFt");
                }
            }
        }

        private string msg;
        public string Msg 
        {
            get
            {
                return msg;
            }
            set
            {
                if (value != msg)
                {
                    msg = value;
                    this.NotifyPropertyChanged("Msg");
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
            string temp;
            double tempX = 0;
            double tempY = 0;
            while (!stop)
            {
                //values from the server
                myClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                temp = myClient.read();
                if (temp != "-999")
                    HeadingDeg = Double.Parse(temp);

                myClient.write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                VerticalSpeed = Double.Parse(myClient.read());
                if (VerticalSpeed == -99999)
                {
                    stop = true;
                    this.NotifyPropertyChanged("Stop");
                    break;
                }
                myClient.write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                GroundSpeedKt = Double.Parse(myClient.read());

                myClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                IndicatedSpeedKt = Double.Parse(myClient.read());

                myClient.write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                AltitudeFt = Double.Parse(myClient.read());
                if (AltitudeFt == -99999)
                {
                    stop = true;
                    this.NotifyPropertyChanged("Stop");
                    break;
                }
                myClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                RollDeg = Double.Parse(myClient.read());

                myClient.write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                IndicatedAlitudeFt = Double.Parse(myClient.read());
                Console.WriteLine(IndicatedAlitudeFt);
                if (IndicatedAlitudeFt == -99999)
                {
                    stop = true;
                    this.NotifyPropertyChanged("Stop");
                    break;
                }

                myClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                PitchDeg = Double.Parse(myClient.read());


                myClient.write("get /position/latitude-deg\r\n");
                tempX = Double.Parse(myClient.read());

                myClient.write("get /position/longitude-deg\r\n");
                tempY = Double.Parse(myClient.read());
                
                if (tempX>=90 || tempX <= -90)
                {
                    if (tempX >= 90)
                    {
                        Location = new Location(90, Location.Longitude);
                        Msg = "Plane is out of bounds";
                    }
                    else
                    {
                        Location = new Location(-90, Location.Longitude);
                        Msg = "Plane is out of bounds";
                    }
                } else if(tempY >= 90 || tempY <= -90)
                {
                    if (tempY >= 90)
                    {
                        Location = new Location(Location.Latitude, 90);
                        Msg = "Plane is out of bounds";
                    }
                    else
                    {
                        Location = new Location(Location.Latitude, -90);
                        Msg = "Plane is out of bounds";
                    }
                } else
                {
                    Location = new Location(tempX, tempY);
                    Msg = "Plane is in bounds";
                }
                
                if (tempX == -99999)
                {
                    stop = true;
                    this.NotifyPropertyChanged("Stop");
                    break;
                }
                myClient.write("set /controls/flight/rudder " + valuesFromView[0].ToString() + "\r\n");
                myClient.read();
                myClient.write("set /controls/flight/elevator " + valuesFromView[1].ToString() + "\r\n");
                myClient.read();
                myClient.write("set /controls/engines/current-engine/throttle " + valuesFromView[2].ToString() + "\r\n");
                myClient.read();
                myClient.write("set /controls/flight/aileron " + valuesFromView[3].ToString() + "\r\n");
                myClient.read();
                //values from the view that we need to update
                //location of the airplane
                Thread.Sleep(800);

            }
            this.myClient.disconnect();
        }
        public float UpdateValue(String info, double newVal)
        {
            if (info == "rudder")
            {
                valuesFromView[0] = UpdateRudder(newVal);
                return (float)valuesFromView[0];
                //myClient.write("set /controls/flight/rudder\r\n" + valuesFromView[0].ToString());
            }
            if (info == "elevator")
            {
                valuesFromView[1] = UpdateElevator(newVal);
                return (float)valuesFromView[1];
                //myClient.write("set /controls/flight/elevator\r\n" + valuesFromView[1].ToString());

            }
            if (info == "throttle")
            {
                valuesFromView[2] = UpdateThrottle(newVal);
                return (float)valuesFromView[2];
                //myClient.write("set /controls/engines/current-engine/throttle\r\n" + valuesFromView[2].ToString());

            }
            if (info == "aileron")
            {
                valuesFromView[3] = UpdateAileron(newVal);
                return (float)valuesFromView[3];
                //myClient.write("set /controls/flight/aileron\r\n" + valuesFromView[3].ToString());

            }
            else
                return 0;
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