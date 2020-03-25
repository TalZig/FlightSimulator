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
        public SubMainWindow(string ip, string port)
        {
            InitializeComponent();
            Models.Model model = new Models.Model();
            if (ip == "" || port == "")
            {
                try
                {
                    model.Connect("127.0.0.1", 5402);
                }
                catch (Exception e)
                {
                    Console.WriteLine("there is no server, good bye");
                    return;
                }
            }
            else
                try
                {
                    model.Connect(ip, int.Parse(port));
                }
                catch (Exception e)
                {
                    Console.WriteLine("ip or port aren't good, try the default ip and port");
                    try
                    {
                        model.Connect("127.0.0.1", 5402);
                    }
                    catch (Exception e2)
                    {
                        Console.WriteLine("there is no server, good bye");
                        return;
                    }
                }

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
    }
}
