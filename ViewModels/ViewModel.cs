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
    class ViewModel : Notifier
    {

        Models.model model;
        private double _rudder;
        private double _elevator;
        private double _aileron;
        private double _throttle;

        // insert both sliders and their property
        // create constructors
        // bind
        // where to send messages via model to simulator?

        public double VMRudder
        {
            get { return _rudder; }
            set
            {
                if (value != _rudder)
                {
                    Console.WriteLine("changed rudder from" + _rudder.ToString() + "to" + value.ToString());
                    _rudder = value;
                    this.NotifyPropertyChanged("rudder");
                    //send command to the model
                    //model.updateValue("rudder", value);
                }
            }
        }

        public double VMElevator
        {
            get { return _elevator; }
            set
            {
                if (value != _elevator)
                {
                    Console.WriteLine("changed elevator from" + _elevator.ToString() + "to" + value.ToString());
                    _elevator = value;
                    NotifyPropertyChanged("elevator");
                    //send command to simulator

                }
            }
        }

        public double VMAileron
        {
            get { return _aileron; }
            set
            {
                if (value != _aileron)
                {
                    Console.WriteLine("changed elevator from" + _aileron.ToString() + "to" + value.ToString());
                    _aileron = value;
                    NotifyPropertyChanged("aileron");
                    //send command to simulator
                }
            }
        }

        public double VMThrottle
        {
            get { return _throttle; }
            set
            {
                if (value != _throttle)
                {
                    Console.WriteLine("changed elevator from" + _throttle.ToString() + "to" + value.ToString());
                    _throttle = value;
                    NotifyPropertyChanged("throttle");

                }
            }
        }

    }
}
