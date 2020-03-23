﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Models;
using System.ComponentModel;

namespace FlightSimulator.ViewModels
{
    class VMDashboard : Notifier
    {
        Model model;

        public VMDashboard(Model model1)
        {
            this.model = model1;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM" + e.PropertyName);
            };
        }
        public double VMHeadingDeg
        {
            get
            {
                return model.HeadingDeg;
            }
        }
        public double VMVerticalSpeed
        {
            get
            {
                return model.VerticalSpeed;
            }
        }
        public double VMGroundSpeedKt
        {
            get
            {
                return model.GroundSpeedKt;
            }
        }
        public double VMIndicatedSpeedKt
        {
            get
            {
                return model.IndicatedSpeedKt;
            }
        }
        public double VMAltitudeFt
        {
            get
            {
                return model.AltitudeFt;
            }
        }
        public double VMRollDeg
        {
            get
            {
                return model.RollDeg;
            }
        }
        public double VMPitchDeg
        {
            get
            {
                return model.PitchDeg;
            }
        }
        public double VMIndicatedAlitudeFt
        {
            get
            {
                return model.IndicatedAlitudeFt;
            }
        }

    }
}
