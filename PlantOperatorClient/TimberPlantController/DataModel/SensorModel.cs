using System;
using System.ComponentModel;
using ProtoBuf;

namespace Randmfun.DataModel
{
    [ProtoContract()]
    public class SensorModel : INotifyPropertyChanged
    {
        private DateTime _dateTime = System.DateTime.Now;
        [ProtoMember(1)]
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
        [ProtoMember(2)]
        public string Sensor1
        {
            get { return _sensor1; }
            set
            {
                _sensor1 = value;
                this.OnNotifyPropertyChanged("Sensor1");
            }
        }

        private string _sensor2 = "0";
        [ProtoMember(3)]
        public string Sensor2
        {
            get { return _sensor2; }
            set
            {
                _sensor2 = value;
                this.OnNotifyPropertyChanged("Sensor2");
            }
        }

        private string _sensor3 = "0.0";
        [ProtoMember(4)]
        public string Sensor3
        {
            get { return _sensor3; }
            set
            {
                _sensor3 = value;
                this.OnNotifyPropertyChanged("Sensor3");
            }
        }

        private string _sensor4 = "0.0";
        [ProtoMember(5)]
        public string Sensor4
        {
            get { return _sensor4; }
            set
            {
                _sensor4 = value;
                this.OnNotifyPropertyChanged("Sensor4");
            }
        }
        private string _sensor5 = "0.0";
        [ProtoMember(6)]
        public string Sensor5
        {
            get { return _sensor5; }
            set
            {
                _sensor5 = value;
                this.OnNotifyPropertyChanged("Sensor5");
            }
        }
        private string _sensor6 = "0.0";
        [ProtoMember(7)]
        public string Sensor6
        {
            get { return _sensor6; }
            set
            {
                _sensor6 = value;
                this.OnNotifyPropertyChanged("Sensor6");
            }
        }
        private string _sensor7 = "0.0";
        [ProtoMember(8)]
        public string Sensor7
        {
            get { return _sensor7; }
            set
            {
                _sensor7 = value;
                this.OnNotifyPropertyChanged("Sensor7");
            }
        }

        private string _sensor8 = "0.0";
        [ProtoMember(9)]
        public string Sensor8
        {
            get { return _sensor8; }
            set
            {
                _sensor8 = value;
                this.OnNotifyPropertyChanged("Sensor8");
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

