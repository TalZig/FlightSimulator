using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    public partial class App : Application
    {        
        public ViewModels.VMDashboard vmd { get; internal set; }
        public ViewModels.VMJoystick vmj { get; internal set; }
        public ViewModels.VMMap vmm { get; internal set; }
        public Models.Model model = new Models.Model();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            vmd = new ViewModels.VMDashboard(model);
            vmj = new ViewModels.VMJoystick(model);
            vmm = new ViewModels.VMMap(model);
        }
    }
}
