using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool closeApp = false;
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
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
            //Models.Model model = (Application.Current as App).model;
            try
            {
                (Application.Current as App).model.Connect(firstVal, int.Parse(secondVal));
                sub = new SubMainWindow((Application.Current as App).model);
                closeApp = true;
                this.Close();
                sub.ShowDialog();
            }
            catch (Exception)
            {
                string message = String.Format("The port or ip are not good, please try again.\n");
                MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
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
            //Models.Model model = new Models.Model();
            SubMainWindow sub;
            try
            {
                (Application.Current as App).model.Connect(ConfigurationManager.AppSettings["IP"].ToString(), Int32.Parse(ConfigurationManager.AppSettings["port"].ToString()));
                sub = new SubMainWindow((Application.Current as App).model);
                closeApp = true;
                this.Close();
                sub.ShowDialog();
            }
            catch (Exception)
            {
                //Console.WriteLine("there is no server, good bye");
                string message = String.Format("The port or ip are not good, please try again.\n");
                MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Error);


            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            closeApp = true;
            System.Environment.Exit(1);
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!closeApp)
            {
                e.Cancel = true;
                base.OnClosing(e);
            }
        }
    }
}
