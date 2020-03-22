using FlightSimulator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulator.ViewModel
{
    class ViewModel : Notifier
    {

        private Point joystickPosition;
        double[] values = new double[10]; //first 8 values for the values from the simulator and two others from the joystick
        SimulatorModel model;
        //Occurs when a property value changes.

        private double _rudder;
        private double _elevator;
        // insert both sliders and their property
        // create constructors
        // bind
        // where to send messages via model to simulator?

        public double Rudder
        {
            get { return _rudder; }
            set
            {
                if (value != _rudder)
                {
                    Console.WriteLine("changed rudder from" + _rudder.ToString() + "to" + value.ToString());
                    _rudder = value;
                    NotifyPropertyChanged("rudder");
                }
            }
        }

        public double Elevator
        {
            get { return _elevator; }
            set
            {
                if (value != _elevator)
                {
                    Console.WriteLine("changed elevator from" + _elevator.ToString() + "to" + value.ToString());
                    _elevator = value;
                    NotifyPropertyChanged("elevator");
                }
            }
        }

    }
}
