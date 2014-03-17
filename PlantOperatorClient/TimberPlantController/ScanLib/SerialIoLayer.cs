using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace ScanLib
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

        public string Start()
        {
            this._serialPort.Open();
            this._serialPort.DtrEnable = true;

            this._serialPort.DataReceived += (SerialPortDataReceived);
            this._scanDataParser.LogData += (_scanDataParser_ReadComplete);

            this._serialPort.Write("y");

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