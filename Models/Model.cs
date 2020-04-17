using Microsoft.Maps.MapControl.WPF;
using System;
using System.ComponentModel;
using System.Threading;

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

        //Location property.
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

        //Implementation of NotifyPropertyChanged function.
        public void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        //Vairables properties:

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

        //Declaration of tcp client.
        public MyTcpClient myClient = new MyTcpClient();
        public void Connect(string ip, int port)
        {
            myClient.Connect(ip, port);
            this.Start();
        }
        //Start the thread and play the Start-Read-And-Write function.
        public void Start()
        {
            Thread thread = new Thread(StartReadAndWrite);
            thread.Start();
        }

        //Write and read data to and from the server with second thread.
        private void StartReadAndWrite()
        {
            string temp;
            double tempX = 0;
            double tempY = 0;
            bool xReceived;
            bool yReceived;
            double tempVal;
            while (!stop)
            {
                xReceived = false;
                yReceived = false;
                //Values from the server.
                myClient.Write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                }
                else
                {
                    tempVal = Double.Parse(temp);
                    if (tempVal != -99999)
                    {
                        HeadingDeg = tempVal;
                        NotifyPropertyChanged("Server");
                    }
                    else
                        NotifyPropertyChanged("Error");
                }

                myClient.Write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                    NotifyPropertyChanged("Server");
                }
                else
                {
                    tempVal = Double.Parse(temp);
                    if (tempVal != -99999)
                    {
                        VerticalSpeed = tempVal;
                        NotifyPropertyChanged("Server");
                    }
                    else
                        NotifyPropertyChanged("Error");
                }

                myClient.Write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                    NotifyPropertyChanged("Server");
                }
                else
                {
                    tempVal = Double.Parse(temp);
                    if (tempVal != -99999)
                    {
                        GroundSpeedKt = tempVal;
                        NotifyPropertyChanged("Server");
                    }
                    else
                        NotifyPropertyChanged("Error");
                }

                myClient.Write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                }
                else
                {
                    tempVal = Double.Parse(temp);
                    if (tempVal != -99999)
                    {
                        IndicatedSpeedKt = tempVal;
                        NotifyPropertyChanged("Server");
                    }
                    else
                        NotifyPropertyChanged("Error");
                }

                myClient.Write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                }
                else
                {
                    tempVal = Double.Parse(temp);
                    if (tempVal != -99999)
                    {
                        NotifyPropertyChanged("Server");
                        AltitudeFt = tempVal;
                    }
                }

                myClient.Write("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                }
                else
                {
                    tempVal = Double.Parse(temp);
                    if (tempVal != -99999)
                    {
                        RollDeg = tempVal;
                        NotifyPropertyChanged("Server");
                    }
                    else
                        NotifyPropertyChanged("Error");
                }

                myClient.Write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                }
                else
                {
                    tempVal = Double.Parse(temp);
                    if (tempVal != -99999)
                    {
                        IndicatedAlitudeFt = tempVal;
                        NotifyPropertyChanged("Server");
                    }
                    else
                        NotifyPropertyChanged("Error");
                }

                myClient.Write("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                }
                else
                {
                    tempVal = Double.Parse(temp);
                    if (tempVal != -99999)
                    {
                        PitchDeg = tempVal;
                        NotifyPropertyChanged("Server");
                    }
                    else
                        NotifyPropertyChanged("Error");
                }


                myClient.Write("get /position/latitude-deg\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                }
                else
                {
                    tempVal = Double.Parse(temp);
                    if (tempVal != -99999)
                    {
                        tempX = tempVal;
                        NotifyPropertyChanged("Server");
                        xReceived = true;
                    }
                    else
                        NotifyPropertyChanged("Error");
                }

                myClient.Write("get /position/longitude-deg\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                }
                else
                {
                    tempVal = Double.Parse(temp);
                    if (tempVal != -99999)
                    {
                        tempY = tempVal;
                        NotifyPropertyChanged("Server");
                        yReceived = true;
                    }
                    else
                        NotifyPropertyChanged("Error");
                }

                if (xReceived && yReceived)
                {
                    if (tempX >= 90 || tempX <= -90)
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
                    }
                    else if (tempY >= 180 || tempY <= -180)
                    {
                        if (tempY >= 180)
                        {
                            Location = new Location(Location.Latitude, 180);
                            Msg = "Plane is out of bounds";
                        }
                        else
                        {
                            Location = new Location(Location.Latitude, -180);
                            Msg = "Plane is out of bounds";
                        }
                    }
                    else
                    {
                        Location = new Location(tempX, tempY);
                        Msg = "Plane is in bounds";
                    }
                }

                myClient.Write("set /controls/flight/rudder " + valuesFromView[0].ToString() + "\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");                   
                }
                myClient.Write("set /controls/flight/elevator " + valuesFromView[1].ToString() + "\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                }
                myClient.Write("set /controls/engines/current-engine/throttle " + valuesFromView[2].ToString() + "\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                }
                myClient.Write("set /controls/flight/aileron " + valuesFromView[3].ToString() + "\r\n");
                temp = myClient.Read();
                if (temp.Equals("Timeout"))
                {
                    this.NotifyPropertyChanged("Timeout");
                }
                Thread.Sleep(200);
            }
            this.myClient.Disconnect();
        }
        //Updating values from the view.
        public float UpdateValue(String info, double newVal)
        {
            if (info == "rudder")
            {
                valuesFromView[0] = UpdateRudder(newVal);
                return (float)valuesFromView[0];
            }
            if (info == "elevator")
            {
                valuesFromView[1] = UpdateElevator(newVal);
                return (float)valuesFromView[1];

            }
            if (info == "throttle")
            {
                valuesFromView[2] = UpdateThrottle(newVal);
                return (float)valuesFromView[2];

            }
            if (info == "aileron")
            {
                valuesFromView[3] = UpdateAileron(newVal);
                return (float)valuesFromView[3];

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