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
using System.Windows.Media.Animation;
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
            center = new Point(Base.Width / 2, Base.Height / 2);
        }
        private Storyboard sb;
        private Point center;
        private void CenterKnob_Completed(Object sender, EventArgs e)
        {
            RudderValue = 0;
            ElevatorValue = 0;
            
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            sb = Knob.FindResource("CenterKnob") as Storyboard;
            sb.Begin();
            UIElement element = (UIElement)Knob;
            element.ReleaseMouseCapture();
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sb = Knob.FindResource("CenterKnob") as Storyboard;
            sb.Stop();
            if (e.ChangedButton == MouseButton.Left)
            {
                (Knob).CaptureMouse();
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double deltaX = (e.GetPosition(this).X - center.X);
                double deltaY = (e.GetPosition(this).Y - center.Y);
                double distFromCenter = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
                //Check if the mouse is in the base circle.
                if (distFromCenter <= Base.Width / 2)
                {
                    knobPosition.X = deltaX;
                    knobPosition.Y = deltaY;
                    //Update on view model (normalized).
                    RudderValue = deltaX / (Base.Width / 2);
                    ElevatorValue = deltaY / (Base.Width / 2);
                }
                //If the mouse is out of the base circle.
                else
                {
                    double m = deltaY / deltaX;
                    double coEfX = (e.GetPosition(this).X > center.X)? 1 : -1;
                    double coEfY = (e.GetPosition(this).Y > center.Y) ? 1 : -1;

                    double tempX = Math.Sqrt(Math.Pow(Base.Width/2, 2) / (m*m + 1));
                    double tempY = (tempX)* m;
                    if (coEfY != coEfX)
                        tempY *= -1;
                    knobPosition.X = tempX*coEfX;
                    knobPosition.Y = tempY*coEfY;
                    
                    //Update the proprties values that need to be transfer to the server.
                    ElevatorValue = knobPosition.Y / (Base.Height / 2);
                    RudderValue = knobPosition.X / (Base.Width / 2);
                }
            }
        }

        private void Knob_MouseLeave(object sender, MouseEventArgs e)
        {
        //    knobPosition.X = 0;
        //    knobPosition.Y = 0;
        }

        //Elevator property:
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

        //Rudder property:
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
    }
}