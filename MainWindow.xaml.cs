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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            firstVal = MyTextBox.Text;
            secondVal = MyTextBox2.Text;
            SubMainWindow sub;
            Models.Model model = new Models.Model();
            try
            {
                model.Connect(firstVal, int.Parse(secondVal));
                sub = new SubMainWindow(model);
                this.Close();
                sub.ShowDialog();
            }
            catch (Exception)
            {
                string message = String.Format("The port or ip are not good, please try again.\n");
                MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Error);

            }
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
            Models.Model model = new Models.Model();
            SubMainWindow sub;
            try
            {
                model.Connect("127.0.0.1", 5402);
                sub = new SubMainWindow(model);
                this.Close();
                sub.ShowDialog();
            }
            catch (Exception)
            {
                Console.WriteLine("there is no server, good bye");
                string message = String.Format("The port or ip are not good, please try again.\n");
                MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Error);


            }
        }
    }
}
