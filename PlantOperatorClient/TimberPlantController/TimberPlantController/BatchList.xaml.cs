using System;
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

            this.DataContext = _sensorDataModel;
        }

        private SensorDataModel _sensorDataModel; 

        public SensorDataModel SensorDataModel
        {
            get
            {
                return _sensorDataModel ?? ThirdEyeApplicationContext.GetCurrentSensorDataModel();
            }
            set
            {
                _sensorDataModel = value;
                OnNotifyPropertyChanged("SensorDataModel");
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
            _sensorDataModel = new SensorDataModel();
            _sensorDataModel.SensorCollection = new ObservableCollection<SensorModel>();
            for (int i = 0, j=10; i < 50; i++, j++)
            {
                _sensorDataModel.SensorCollection.Add(new SensorModel(){Sensor1 = i.ToString(), Sensor2 = j.ToString()});
            }

            ThirdEyeApplicationContext.SetCurrentSensorModel(_sensorDataModel);
        }
    }
}
