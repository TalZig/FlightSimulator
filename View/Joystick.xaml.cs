using FlightSimulator.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.View
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        public Joystick()
        {
            InitializeComponent();
            // make the view model the object being binded
            ViewModel.ViewModel vm = new ViewModel.ViewModel();
            DataContext = vm;
        }
        private Point center;
        private void centerKnob_Completed(Object sender, EventArgs e) { }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            center.X = 0;
            center.Y = 0;
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                center = e.GetPosition(this);
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X - center.X;
                double y = e.GetPosition(this).Y - center.Y;
                if (Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) < Base.Width / 2)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                }
            }
        }
    }
}
