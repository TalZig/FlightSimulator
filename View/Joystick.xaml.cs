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
        Boolean stop = false;
        public Joystick()
        {
            InitializeComponent();

        }
        private Point center;
        //private double x1 = 0;
        //private double y1 = 0;
        private void CenterKnob_Completed(Object sender, EventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            stop = true;
            knobPosition.X = 0;
            knobPosition.Y = 0;
            UIElement element = (UIElement)Knob;
            element.ReleaseMouseCapture();
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                center = e.GetPosition(this);
                (Knob).CaptureMouse();
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = (e.GetPosition(this).X - center.X);
                double y = (e.GetPosition(this).Y - center.Y);
                if (Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) + (KnobBase.Width / 2) < BlackCircle.Width / 2)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                    //update on view model (normalized)
                    RudderValue = (x + 2) / ((BlackCircle.Width - KnobBase.Width) / 2);
                    ElevatorValue = (y / (KnobBase.Width / 2));
                }
                else
                {
                    if (x + (KnobBase.Width / 2) > BlackCircle.Width / 2)
                    {
                        knobPosition.X = (BlackCircle.Width / 2) - (KnobBase.Width / 2);
                    }
                    if (x - (KnobBase.Width / 2) < -(BlackCircle.Width / 2))
                    {
                        knobPosition.X = -((BlackCircle.Width / 2) - (KnobBase.Width / 2));
                    }
                    if (y + (KnobBase.Width / 2) > BlackCircle.Width / 2)
                    {
                        knobPosition.Y = (BlackCircle.Width / 2) - (KnobBase.Width / 2);
                    }
                    if (y - (KnobBase.Width / 2) < -(BlackCircle.Width / 2))
                    {
                        knobPosition.Y = -((BlackCircle.Width / 2) - (KnobBase.Width / 2));
                    }
                }
            }
        }

        /*        private void KnobBase_MouseLeave(object sender, MouseEventArgs e)
                {
                    knobPosition.X = 0;
                    knobPosition.Y = 0;
                }*/

        private void Knob_MouseLeave(object sender, MouseEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }

        public double ElevatorValue
        {
            get { return (double)GetValue(ElevatorValueProperty); }
            set
            {
                SetValue(ElevatorValueProperty, value);
            }
        }

        public static readonly DependencyProperty ElevatorValueProperty =
            DependencyProperty.Register("ElevatorValue", typeof(double), typeof(Joystick));
        public double RudderValue
        {
            get { return (double)GetValue(RudderValueProperty); }
            set
            {
                SetValue(RudderValueProperty, value);
            }
        }

        public static readonly DependencyProperty RudderValueProperty =
            DependencyProperty.Register("RudderValue", typeof(double), typeof(Joystick));

        private void Knob_GotMouseCapture(object sender, MouseEventArgs e)
        {

        }
    }
}