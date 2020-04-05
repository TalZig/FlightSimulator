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
		private string msg;
		bool stop = false;
		public event PropertyChangedEventHandler PropertyChanged;
		public VMMap(Models.Model model1)
		{
			model = model1;
			msg = "Plane is in bounds";
			model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
			{
				NotifyPropertyChanged("VM" + e.PropertyName);
			};
			VMLocation = model.Location;
		}

		public void NotifyPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(name));
			if (name.Equals("VMLocation"))
				VMLocation = model.Location;
			if (name.Equals("VMStop"))
				VMStop = model.stop;
			if (name.Equals("VMMsg"))
				VMMsg = model.Msg;
		}

		public string VMMsg
		{
			get { return msg; }
			set
			{
				if (value != msg)
				{
					msg = value;
					NotifyPropertyChanged("VMMsg");
				}
			}
		}


		public Boolean VMStop
		{
			get
			{
				return model.stop;
			}
			set
			{
				stop = value;
				Console.WriteLine("check!!");
				VMStatusOfServer = "Server disconnected";
			}
		}
		private string vmStatusOfServer;
		public string VMStatusOfServer
		{
			get
			{
				if (VMStop == true)
					return "Server disconnected";
				else
					return "Server is connect";
			}
			set
			{
				vmStatusOfServer = value;
				NotifyPropertyChanged("VMStatusOfServer");
			}
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
				//this.NotifyPropertyChanged("VMLocation");
			}
		}


	}
}