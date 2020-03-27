using FlightSimulator.Models;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FlightSimulator.ViewModels
{
    public class VMMap : INotifyPropertyChanged
    {
		Models.Model model;
		public event PropertyChangedEventHandler PropertyChanged;
		public VMMap(Models.Model model1)
		{
			model = model1;
			model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				NotifyPropertyChanged("VM"+e.PropertyName);
			};
			VMLocation = model.Location;
		}

		public void NotifyPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(name));
			if (name.Equals("VMLocation"))
				VMLocation = model.Location;
		}

		public Location VMLocation
		{
			get 
			{
				return model.Location;
			}
			set
			{
				model.Location = value;
				//this.NotifyPropertyChanged("VML0cation");
			}
		}


	}
}
