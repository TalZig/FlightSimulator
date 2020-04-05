using FlightSimulator.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private bool firstTime = true;
        double x, y;
        bool stop = false;
        VMJoystick jvm;
        public SubMainWindow(Models.Model model)
        {
            InitializeComponent();

            jvm = new VMJoystick(model);
            //Throttle.DataContext = jvm;
            //Aileron.DataContext = jvm;
            //joystick1.DataContext = jvm;
            VMDashboard dvm = new VMDashboard(model);
            Board.DataContext = dvm;
            myJoystick.DataContext = jvm;
            VMMap mapvm = new VMMap(model);
            map.DataContext = mapvm;
            this.DataContext = mapvm;

            /*mapvm.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };*/

            //vm.model.Start() ;            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            jvm.stop = true;
            var mai = new FlightSimulator.MainWindow();
            this.Close();
            mai.ShowDialog();
        }


    }
}