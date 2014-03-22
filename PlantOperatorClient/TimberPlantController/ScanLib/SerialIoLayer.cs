using System;
using System.IO.Ports;
using System.Threading;
using Randmfun.DataModel;

namespace Randmfun.ScanLib
{
    public class SerialIoLayer : ISerialIo
    {
        public CommunicationState CommunicationState { get; set; }
        public event SerialDataEventHandler LogData;

        private readonly ScanDataParser _scanDataParser;
        private readonly SerialPort _serialPort;

        public SerialIoLayer(SerialPort serialPort)
        {
            this._serialPort = serialPort;
            this.CommunicationState = CommunicationState.None;
            
            this._scanDataParser = new ScanDataParser();
        }

        static bool _continue;
        int index = 0;
        private void LoadDummyDataEvery2Secs()
        {
            while (_continue)
            {
                try
                {
                    index += 11;
                    int sens2Val = index + 10;
                    
                    Thread.Sleep(2000);

                    if (this.LogData != null)
                    {
                        this.LogData(ScanLib.CommunicationState.Reading,
                                     new SerialDataArgs(new SensorModel()
                                                            {
                                                                DateTime = DateTime.Now,
                                                                Sensor1 = index.ToString(),
                                                                Sensor2 = sens2Val.ToString(),
                                                                Sensor3 = sens2Val.ToString()
                                                            }));
                    }
                }
                catch (TimeoutException) { }
            }
        }

        public string Start()
        {
            this._serialPort.Open();
            this._serialPort.DtrEnable = true;

            this._serialPort.DataReceived += (SerialPortDataReceived);
            this._scanDataParser.LogData += (_scanDataParser_ReadComplete);

            //TODO: Testing Code remove please
            Thread readThread = new Thread(LoadDummyDataEvery2Secs);
            readThread.Start();
            _continue = true;
            
            //this._serialPort.Write("y"););
            return "";
        }

        public void Stop()
        {
            if (_serialPort.IsOpen)
            {
                this._serialPort.Close();
                this._serialPort.DataReceived -= (SerialPortDataReceived);
                this._scanDataParser.LogData -= (_scanDataParser_ReadComplete);
            }
            _continue = false;
            this.CommunicationState = CommunicationState.Closed;
        }

        void _scanDataParser_ReadComplete(CommunicationState communicationState, SerialDataArgs e)
        {
            if(this.LogData != null)
                this.LogData(communicationState, e);
        }

        void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                var line = _serialPort.ReadExisting();
                this._scanDataParser.Parse(_serialPort.ReadExisting(), this.CommunicationState);
            }
        }

    }

    public interface ISerialIo
    {
        string Start();
        void Stop();

        CommunicationState CommunicationState { get; set; }
        event SerialDataEventHandler LogData;
    }

    public enum CommunicationState
    {
        None,
        Open,
        Reading,
        Closed
    }

    public delegate void SerialDataEventHandler(CommunicationState communicationState, SerialDataArgs evenArgs);
 
    public class SerialDataArgs : EventArgs
    {
        public SensorModel Sender { get; private set; } 

        public SerialDataArgs(SensorModel sender)
        {
            this.Sender = sender;
        }
    }
}