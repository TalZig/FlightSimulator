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
        public SubMainWindow(Models.Model model)
        {
            InitializeComponent();
            Board.DataContext = (Application.Current as App).Vmd;
            myJoystick.DataContext = (Application.Current as App).Vmj;
            map.DataContext = (Application.Current as App).Vmm;
            this.DataContext = (Application.Current as App).Vmm;
        }

        //If the user pressed on "stop-fly" button.
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            closeApp = true;
            var mai = new FlightSimulator.MainWindow();
            (Application.Current as App).model.myClient.Disconnect();
            (Application.Current as App).model = new Models.Model();
            (Application.Current as App).Application_Startup(this, null);
            this.Close();
            mai.ShowDialog();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
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