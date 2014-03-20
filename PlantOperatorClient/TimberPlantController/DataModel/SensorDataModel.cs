﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataModel
{
    public class SensorDataModel : INotifyPropertyChanged
    {
        private DateTime _dateTime = System.DateTime.Now;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                this.OnNotifyPropertyChanged("DateTime");
            }
        }

        private string _sensor1 = "0.0";
        public string Sensor1
        {
            get { return _sensor1; }
            set { _sensor1 = value;
            this.OnNotifyPropertyChanged("Sensor1");
            }
        }

        private string _sensor2= "0.0";
        public string Sensor2
        {
            get { return _sensor2; }
            set
            {
                _sensor2 = value;
                this.OnNotifyPropertyChanged("Sensor2");
            }
        }

        private string _sensor3 ="0.0";
        public string Sensor3
        {
            get { return _sensor3; }
            set
            {
                _sensor3 = value;
                this.OnNotifyPropertyChanged("Sensor3");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnNotifyPropertyChanged(string propertyName)
        {
            if(this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
