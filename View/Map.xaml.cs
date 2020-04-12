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
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulator.View
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        public Map()
        {
            InitializeComponent();
        }

        private void Pin_LayoutUpdated(object sender, EventArgs e)
        {
            /*if (planePushpin.Location != null)
            {
                double latitude = planePushpin.Location.Latitude;
                double longtitude = planePushpin.Location.Longitude;
                if (firstTime)
                {
                    Map.SetView(new Location(latitude, longtitude), 10);
                    PlanePosition.X = 0;
                    PlanePosition.Y = 0;
                    firstTime = false;
                    return;
                }
            }*/
        }
    }
}
