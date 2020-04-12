using FlightSimulator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulator.ViewModels
{
    public class VMJoystick : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Models.Model model;
        private float _rudder;
        private float _elevator;
        private double _aileron;
        private double _throttle;
        public bool Stop
        {
            get
            {
                return model.stop;
            }
            set
            {
                this.model.stop = value;
            }
        }

        public VMJoystick(Model model1)
        {
            this.model = model1;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM"+e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        // insert both sliders and their property
        // create constructors
        // bind
        // where to send messages via model to simulator?



        //VMRudder property:
        public float VMRudder
        {
            get { return _rudder; }
            set
            {
                if (value != _rudder)
                {
                    _rudder = model.UpdateValue("rudder", value);
                    this.NotifyPropertyChanged("VMRudder");
                }
            }
        }

        //VMElevator property:
        public float VMElevator
        {
            get { return _elevator; }
            set
            {
                if (value != _elevator)
                {
                    _elevator = model.UpdateValue("elevator", value);
                    NotifyPropertyChanged("VMElevator");
                }
            }
        }

        //VMAileron property:
        public double VMAileron
        {
            get { return _aileron; }
            set
            {
                if (value != _aileron)
                {
                    _aileron = model.UpdateValue("aileron", value);
                }
            }
        }

        //VMThrottle property:
        public double VMThrottle
        {
            get { return _throttle; }
            set
            {
                if (value != _throttle)
                {
                    _throttle = model.UpdateValue("throttle", value);

                }
            }
        }
    }
}
