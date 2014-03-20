﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Randmfun.DataModel;

namespace TimberPlantController
{
    /// <summary>
    /// Interaction logic for BatchList.xaml
    /// </summary>
    public partial class BatchList : UserControl, INotifyPropertyChanged
    {
        public BatchList()
        {
            InitializeComponent();

            PopulateTemp();

            this.DataContext = _sensorCollection;
        }

        private SensorDataModel _sensorCollection = new SensorDataModel(); 

        public SensorDataModel SensorCollection
        {
            get { return _sensorCollection; }
            set
            {
                _sensorCollection = value;
                OnNotifyPropertyChanged("SensorCollection");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnNotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PopulateTemp()
        {
            _sensorCollection.SensorCollection = new ObservableCollection<SensorModel>();
            _sensorCollection.SensorCollection.Add(new SensorModel());
            _sensorCollection.SensorCollection.Add(new SensorModel());
            _sensorCollection.SensorCollection.Add(new SensorModel());
            _sensorCollection.SensorCollection.Add(new SensorModel());
        }
    }
}
