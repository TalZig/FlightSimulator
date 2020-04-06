using FlightSimulator.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
        bool closeApp = false;
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        private LocationRect bounds;
        private bool firstTime = true;
        double x, y;
        bool stop = false;
        VMJoystick jvm;
        public SubMainWindow(Models.Model model)
        {
            InitializeComponent();

            //jvm = new VMJoystick(model);
            //Throttle.DataContext = jvm;
            //Aileron.DataContext = jvm;
            //joystick1.DataContext = jvm;
           // VMDashboard dvm = new VMDashboard(model);
            Board.DataContext = (Application.Current as App).vmd;
            myJoystick.DataContext = (Application.Current as App).vmj;
            //VMMap mapvm = new VMMap(model);
            map.DataContext = (Application.Current as App).vmm;
            this.DataContext = (Application.Current as App).vmm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            closeApp = true;
            jvm.stop = true;
            var mai = new FlightSimulator.MainWindow();
            this.Close();
            mai.ShowDialog();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {

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