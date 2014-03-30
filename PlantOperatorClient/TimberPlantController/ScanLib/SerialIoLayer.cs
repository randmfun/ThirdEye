using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
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
        private void LoadDummyDataEvery2Secs()
        {
            while (_continue)
            {
                try
                {
                    Thread.Sleep(3000);

                    if (_serialPort.IsOpen)
                        this._serialPort.Write("y");
                }
                catch (Exception exception)
                {
                    var msg = exception.Message;
                }
            }
        }

        public string Start()
        {
            this._serialPort.Open();
            this._serialPort.DtrEnable = true;

            this._serialPort.DataReceived += (SerialPortDataReceived);
            this._scanDataParser.LogData += (_scanDataParser_ReadComplete);

            var readThread = new Thread(LoadDummyDataEvery2Secs);
            readThread.Start();

            _continue = true;
            
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
            try
            {
                if (_serialPort.IsOpen)
                {
                    var line = _serialPort.ReadLine();
                    this._scanDataParser.Parse(line, ScanLib.CommunicationState.Reading);
                }
            }
            catch (Exception ex){}
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
        Closed,
        OpenArchive
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