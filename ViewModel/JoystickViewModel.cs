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
    class JoystickViewModel : Notifier
    {

        private Point joystickPosition;
        double[] values = new double[10]; //first 8 values for the values from the simulator and two others from the joystick
        SimulatorModel model;
        //Occurs when a property value changes.

        private double _x;
        private double _y;
        // insert both sliders and their property
        // create constructors
        // bind
        // where to send messages via model to simulator?

        public double X
        {
            get { return _x; }
            set
            {
                if (value != _x)
                {
                    _x = value;
                }
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                if (value != _y)
                {
                    _y = value;
                }
            }
        }

    }
}
