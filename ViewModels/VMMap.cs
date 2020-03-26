using FlightSimulator.Models;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class VMMap : Notifier
    {
		Models.Model model;
		public VMMap(Models.Model model1)
		{
			model = model1;
			VMLocation = model.MLocation;
		}

		public Location VMLocation
		{
			get 
			{
				Console.WriteLine("get Location in VM: " + model.MLocation);
				return model.MLocation;
			}
			set
			{
				Console.WriteLine("set Location in VM: " + value);
				model.MLocation = value;
				this.NotifyPropertyChanged("VMLOcation");
			}
		}


	}
}
