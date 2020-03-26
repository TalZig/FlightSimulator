using FlightSimulator.ViewModels;
using Microsoft.Maps.MapControl.WPF;
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

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SubMainWindow : Window
    {
        private LocationRect bounds;
        private bool firstTime;
        double x, y;
        bool stop = false;
        public SubMainWindow(Models.Model model)
        {
            InitializeComponent();
            

            VMJoystick jvm = new VMJoystick(model);
            Throttle.DataContext = jvm;
            Aileron.DataContext = jvm;
            joystick1.DataContext = jvm;
            VMDashboard dvm = new VMDashboard(model);
            Board.DataContext = dvm;
            VMMap mapvm = new VMMap(model);
            Map.DataContext = mapvm;

            //vm.model.Start() ;            
        }

        
        private void pin_LayoutUpdated(object sender, EventArgs e)
        {
            if (airplane.Location != null)
            {
                this.bounds = Map.BoundingRectangle;
                double centerLat = bounds.Center.Latitude;
                double centerLon = bounds.Center.Longitude;
                // Console.WriteLine("x: " + PlainPosition.X);
                //Console.WriteLine("y: " + PlainPosition.Y);
                //Update the current latitude and longitude
                double latitude = airplane.Location.Latitude;
                double longtitude = airplane.Location.Longitude;
                if (firstTime)
                {
                    Map.SetView(new Location(latitude, longtitude), 4);
                    PlainPosition.X = 0;
                    PlainPosition.Y = 0;
                    firstTime = false;
                    x = latitude;
                    y = longtitude;
                    return;
                }

                //
                if ((longtitude - y) == 0)
                {

                }
                else
                {
                    double m = (latitude - x) / (longtitude - y);

                    if (m > 0 && latitude + 0.5 >= bounds.North)
                    {
                        if (longtitude >= centerLon)
                        {
                            Map.SetView(new Location(2 * latitude - centerLat - 2.5, 2 * longtitude - centerLon), 4);
                        }
                        else
                        {
                            Map.SetView(new Location(2 * latitude - centerLat - 2.5, centerLon), 4);
                        }
                    }

                    else if (m < 0 && latitude - 2.5 <= bounds.South)
                    {
                        if (longtitude >= centerLon)
                        {
                            Map.SetView(new Location(2 * latitude - centerLat + 2.5, 2 * longtitude - centerLon), 4);
                        }
                        else
                        {
                            Map.SetView(new Location(2 * latitude - centerLat + 2.5, centerLon), 4);
                        }
                    }

                    else if (m > 0 && longtitude + 0.5 >= bounds.East)
                    {
                        if (latitude >= centerLat)
                        {
                            Map.SetView(new Location(2 * latitude - centerLat + (bounds.North - centerLat), 2 * longtitude - centerLon - 2.5), 4);
                        }
                        else
                        {
                            Map.SetView(new Location(centerLat, 2 * longtitude - centerLon - 2.5), 4);
                        }
                    }

                    else if (m < 0 && longtitude + 0.5 >= bounds.East)
                    {
                        if (latitude <= centerLat)
                        {
                            Map.SetView(new Location(2 * latitude - centerLat + (bounds.North - centerLat), 2 * longtitude - centerLon - 2.5), 4);
                        }
                        else
                        {
                            Map.SetView(new Location(centerLat, 2 * longtitude - centerLon - 2.5), 4);
                        }
                    }

                    else if (m < 0 && longtitude - 2.5 <= bounds.West)
                    {
                        if (latitude >= centerLat)
                        {
                            Map.SetView(new Location(2 * latitude - centerLat + (bounds.North - centerLat), 2 * longtitude - centerLon + 2.5), 4);
                        }
                        else
                        {
                            Map.SetView(new Location(centerLat, 2 * longtitude - centerLon + 2.5), 4);
                        }
                    }

                    else if (m > 0 && longtitude - 2.5 <= bounds.West)
                    {
                        if (latitude <= centerLat)
                        {
                            Map.SetView(new Location(2 * latitude - centerLat + (bounds.North - centerLat), 2 * longtitude - centerLon + 2.5), 4);
                        }
                        else
                        {
                            Map.SetView(new Location(centerLat, 2 * longtitude - centerLon + 2.5), 4);
                        }
                    }
                }
                x = latitude;
                y = longtitude;
            }

        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var mai = new FlightSimulator.MainWindow();
            this.Close();
            mai.ShowDialog();

        }
    }
}