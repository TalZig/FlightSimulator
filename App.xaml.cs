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
        //initialize the view models and model.
        public ViewModels.VMDashboard Vmd { get; internal set; }
        public ViewModels.VMJoystick Vmj { get; internal set; }
        public ViewModels.VMMap Vmm { get; internal set; }
        public Models.Model model = new Models.Model();

        public void Application_Startup(object sender, StartupEventArgs e)
        {
            Vmd = new ViewModels.VMDashboard(model);
            Vmj = new ViewModels.VMJoystick(model);
            Vmm = new ViewModels.VMMap(model);
        }
    }
}
