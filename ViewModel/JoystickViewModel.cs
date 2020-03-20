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
    class JoystickViewModel : INotifyPropertyChanged
    {

        private Point joystickPosition;
        double[] values = new double[10]; //first 8 values for the values from the simulator and two others from the joystick
        SimulatorModel model;
        //     Occurs when a property value changes.
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }
    }
}
