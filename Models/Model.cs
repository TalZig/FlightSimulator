using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    class Model : Notifier
    {
        public double XairPlaneLocation;
        public double YairPlaneLocation;
        public double [] valuesFromView = new double[4];
        public volatile bool stop = false;
        private double verticalSpeed;
        public double VerticalSpeed
        {
            get 
            {
                return verticalSpeed;
            }
            set
            {
                verticalSpeed = value;
                this.NotifyPropertyChanged("VerticalSpeed");
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
                headingDeg = value;
                this.NotifyPropertyChanged("HeadingDeg");
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
                groundSpeedKt = value;
                this.NotifyPropertyChanged("GroundSpeedKt");
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
                indicatedSpeedKt = value;
                this.NotifyPropertyChanged("IndicatedSpeedKt");
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
                altitudeFt = value;
                this.NotifyPropertyChanged("AltitudeFt");
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
            rollDeg = value;
            this.NotifyPropertyChanged("RollDeg");
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
                pitchDeg = value;
                this.NotifyPropertyChanged("PitchDeg");
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
                indicatedAltitudeFt = value;
                this.NotifyPropertyChanged("IndicatedAltitudeFt");
            }
        }

        MyTcpClient myClient = new MyTcpClient();
        public void Connect(string ip, int port)
        {
            myClient.Connect(ip, port);
        }
        public void Start()
        {
            Thread thread = new Thread(StartReadAndWrite);
            thread.Start();
        }
        private void StartReadAndWrite()
        {
            
            while (!stop)
            {
                //values from the server
                myClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                HeadingDeg = Double.Parse(myClient.read());
                Console.WriteLine(HeadingDeg);
                myClient.write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                VerticalSpeed = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                GroundSpeedKt = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                IndicatedSpeedKt = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                AltitudeFt = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/attitude-indicator/indicated-roll-deg\r\n");
                RollDeg = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/attitude-indicator/indicated-pitch-deg\r\n");
                PitchDeg = Double.Parse(myClient.read());
                myClient.write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                IndicatedAlitudeFt = Double.Parse(myClient.read());
                myClient.write("get /position/latitude-deg\r\n");
                XPos = Double.Parse(myClient.read());
                myClient.write("get /position/longitude-deg\r\n");
                YPos = Double.Parse(myClient.read());

                //values from the view that we need to update
                myClient.write("set /controls/flight/rudder\r\n" + valuesFromView[0].ToString());
                myClient.write("set /controls/flight/elevator\r\n" + valuesFromView[1].ToString());
                myClient.write("set /controls/engines/current-engine/throttle\r\n" + valuesFromView[2].ToString());
                myClient.write("set /controls/flight/aileron\r\n" + valuesFromView[3].ToString());
                //location of the airplane

                
            }
        }
        public void UpdateValue(String info, double newVal)
        {
            if(info == "rudder")
            {
                valuesFromView[0] = UpdateRudder(newVal);
            }
            if(info == "elevator")
            {
                valuesFromView[1] = UpdateElevator(newVal);
            }
            if(info == "throttle")
            {
                valuesFromView[2] = UpdateThrottle(newVal);
            }
            if(info == "aileron")
            {
                valuesFromView[3] = UpdateAileron(newVal);
            }
        }

        private double UpdateRudder(double newVal)
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
