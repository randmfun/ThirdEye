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
        public static DataGrid CurrentDataGrid { get; set; }

        public BatchList()
        {
            InitializeComponent();
            this.DataContext = SensorDataModel;

            CurrentDataGrid = this.grdSensor;
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
    }
}
