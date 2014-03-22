using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace TimberPlantController
{
    public class CommunicationConfigViewModel: INotifyPropertyChanged
    {
        public List<string> ComPorts { get; set; }
        public List<string> BaudRates { get; set; }

        public string SelectedComPort { get; set; }
        public string SelectedBaudRate { get; set; }

        public CommunicationConfigViewModel()
        {
            ComPorts= GetComPorts();
            BaudRates = GetBaudRate();

            SelectedComPort = this.ComPorts.First();
            SelectedBaudRate = this.BaudRates.First();

            this.radioStopBits = getRadioStopBits();
            this.radioParity = GetRadioPartiyBits();
            this.radioDataBits = GetRadioDataBits();
        }

        public string SelectedDataBit
        {
            get { return this.RadioDataBits.Find(item => item.CheckedProperty).Header; }
        }

        public string SelectedParity
        {
            get { return this.RadioParity.Find(item => item.CheckedProperty).Header; }
        }

        public string SelectedStopBit
        {
            get { return this.RadioStopBits.Find(item => item.CheckedProperty).Header; }
        }

        public CommunicationConfigViewModel Clone()
        {
            return (CommunicationConfigViewModel)this.MemberwiseClone();
        }

        private List<RadioClass> radioDataBits;
        public List<RadioClass> RadioDataBits
        {
            get { return radioDataBits; }
            set { radioDataBits = value;
            this.OnPropertyChanged("RadioDataBits");
            }
        }

        private List<RadioClass> radioParity;
        public List<RadioClass> RadioParity
        {
            get { return radioParity; }
            set { radioParity = value;
                this.OnPropertyChanged("RadioParity");
            }
        }

        private List<RadioClass> radioStopBits;
        public List<RadioClass> RadioStopBits
        {
            get
            {
                return radioStopBits;
            }
            set
            {
                radioStopBits = value;
                this.OnPropertyChanged("RadioStopBits");
            }
        }

        #region Private Helpers
        
        private List<RadioClass> getRadioStopBits()
        {
            List<RadioClass> list = new List<RadioClass>();
            list.Add(new RadioClass { Header = "One", CheckedProperty = true });
            list.Add(new RadioClass { Header = "OnePointFive", CheckedProperty = false });
            list.Add(new RadioClass { Header = "Two", CheckedProperty = false });
            return list;
        }

        private List<RadioClass> GetRadioPartiyBits()
        {
            List<RadioClass> list = new List<RadioClass>();
            list.Add(new RadioClass { Header = "None", CheckedProperty = true });
            list.Add(new RadioClass { Header = "Odd", CheckedProperty = false });
            list.Add(new RadioClass { Header = "Even", CheckedProperty = false });
            return list;
        }

        private List<RadioClass> GetRadioDataBits()
        {
            List<RadioClass> list = new List<RadioClass>();
            list.Add(new RadioClass { Header = "5", CheckedProperty = false });
            list.Add(new RadioClass { Header = "6", CheckedProperty = false });
            list.Add(new RadioClass { Header = "7", CheckedProperty = false });
            list.Add(new RadioClass { Header = "8", CheckedProperty = true });
            return list;            
        }

        private static List<string> GetComPorts()
        {
            return SerialPort.GetPortNames().ToList();
        }

        private static List<string> GetBaudRate()
        {
            return new List<string> {"1200","2400","4800","9600", "19200","38400","57600","115200","128000","256000"};
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class RadioClass : INotifyPropertyChanged
    {
        private string header;
        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                this.OnPropertyChanged("Header");
            }
        }

        private bool checkedProperty;
        public bool CheckedProperty
        {
            get { return checkedProperty; }
            set
            {
                checkedProperty = value;
                this.OnPropertyChanged("CheckedProperty");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
