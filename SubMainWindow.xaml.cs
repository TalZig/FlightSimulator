using FlightSimulator.ViewModels;
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
        public SubMainWindow()
        {
            Models.Model model = new Models.Model();
            model.Connect("127.0.0.1", 5402);
            VMJoystick jvm = new VMJoystick(model);
            //Throttle.DataContext = jvm;
            // Aileron.DataContext = jvm;
            // joystick1.DataContext = jvm;
            VMDashboard dvm = new VMDashboard(model);
            //  Board.DataContext = dvm;
            //vm.model.Start();
            InitializeComponent();
        }

    }
}
