using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ProtoBuf;

namespace Randmfun.DataModel
{
    [ProtoContract()]
    public class SensorDataModel : INotifyPropertyChanged
    {
        private DateTime _startDateTime = System.DateTime.Now;
        [ProtoMember(1)]
        public DateTime StartDateTime
        {
            get { return _startDateTime; }
            set
            {
                _startDateTime = value;
                this.OnNotifyPropertyChanged("StartDateTime");
            }
        }

        private string _detailsName = string.Empty;
        [ProtoMember(2)]
        public string DetailsName
        {
            get { return _detailsName; }
            set
            {
                _detailsName = value;
                this.OnNotifyPropertyChanged("DetailsName");
            }
        }

        private string _detailsDesc = string.Empty;
        [ProtoMember(3)]
        public string DetailsDesc
        {
            get { return _detailsDesc; }
            set
            {
                _detailsDesc = value;
                this.OnNotifyPropertyChanged("DetailsDesc");
            }
        }

        private ObservableCollection<SensorModel> _sensorCollection = new ObservableCollection<SensorModel>();
        [ProtoMember(4)]
        public ObservableCollection<SensorModel> SensorCollection
        {
            get { return _sensorCollection; }
            set
            {
                _sensorCollection = value;
                this.OnNotifyPropertyChanged("SensorCollection");
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
