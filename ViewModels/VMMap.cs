using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class VMMap
    {
		private double lat;

		public double Lat
		{
			get { return lat; }
			set { lat = value; }
		}

		private double lon;

		public double Lon
		{
			get { return lon; }
			set { lon = value; }
		}


	}
}
