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
using DataModel;

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

            this.DataContext = this;
        }

        private ObservableCollection<SensorDataModel> _sensorCollection = new ObservableCollection<SensorDataModel>(); 

        public ObservableCollection<SensorDataModel> SensorCollection
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
            this._sensorCollection.Add(new SensorDataModel());
            this._sensorCollection.Add(new SensorDataModel());
            this._sensorCollection.Add(new SensorDataModel());
            this._sensorCollection.Add(new SensorDataModel());
        }
    }
}
