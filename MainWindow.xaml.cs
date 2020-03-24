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
    public partial class MainWindow : Window
    {
        private string firstVal;
        private string secondVal;
        public MainWindow()
        {
            InitializeComponent();
            Models.Model model = new Models.Model();
           // model.Connect("127.0.0.1", 5402);
            VMJoystick jvm = new VMJoystick(model);
            Console.WriteLine(jvm.VMAileron);
            Throttle.DataContext = jvm;
            Aileron.DataContext = jvm;
            joystick1.DataContext = jvm;
            VMDashboard dvm = new VMDashboard(model);
            Board.DataContext = dvm;
            VMMap mapvm = new VMMap();
            Map.DataContext = mapvm;
            //model.Start();         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            firstVal = MyTextBox.Text;
            secondVal = MyTextBox2.Text;
            var window = new FlightSimulator.SubMainWindow(this.firstVal,this.secondVal);
            this.Hide();
            window.ShowDialog();
        }

        private void MyTextBox2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyTextBox2.Text = "";
        }

        private void MyTextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyTextBox.Text = "";

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            firstVal = "127.0.0.1";
            secondVal = "5402";
            var window = new FlightSimulator.SubMainWindow(this.firstVal, this.secondVal);
            this.Hide();
            window.ShowDialog();

        }
    }
}
