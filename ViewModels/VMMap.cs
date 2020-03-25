using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class VMMap
    {
		Models.Model model;
		public VMMap(Models.Model model1)
		{
			model = model1;

			//vmLocation = new Location(32.0, 34.888852);
		}

		//private Location vmLocation;

		public Location VMLocation
		{
			get 
			{
				return model.MLocation;
			}
			set
			{
				Console.WriteLine("Location in VM: " + model.MLocation);
				model.MLocation = value;
			}
		}


	}
}
