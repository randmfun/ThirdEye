using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanLib
{
    public class ScanDataParser
    {
        public event SerialDataEventHandler LogData;
 
        public void Parse(string inputBuffer, CommunicationState communicationState)
        {
            if (communicationState == CommunicationState.Open)
            {
                if (inputBuffer.Contains("Y"))
                {
                    readerIndex = 0;
                    currentModel = new SensorModel();
                    NotifyLogData(communicationState, null);
                }
            }
            else if (communicationState == CommunicationState.Reading)
            {
                var charAry = inputBuffer.Split('\r');

                foreach (var value in charAry)
                {
                    AddToReading(value);
                }
            }
        }

        private int readerIndex = 0;
        private SensorModel currentModel;

        private void AddToReading(string value)
        {
            readerIndex = readerIndex%8;

            if (readerIndex == 0)
            {
                currentModel = new SensorModel();
                currentModel.Sensor_1 = value;
            }
            else if(readerIndex == 1)
            {
                currentModel.Sensor_2 = value;
            }
            else if (readerIndex == 2)
            {
                currentModel.Sensor_3 = value;
            }
            else if (readerIndex == 3)
            {
                currentModel.Sensor_4 = value;
            }
            else if (readerIndex == 4)
            {
                currentModel.Sensor_5 = value;
            }
            else if (readerIndex == 5)
            {
                currentModel.Sensor_6 = value;
            }
            else if (readerIndex == 6)
            {
                currentModel.Sensor_7 = value;
            }
            else if (readerIndex == 7)
            {
                currentModel.Sensor_8 = value;
                NotifyLogData(CommunicationState.Reading, new SerialDataArgs(currentModel));
            }

            readerIndex++;
        }

        private void NotifyLogData(CommunicationState communicationState, SerialDataArgs serialDataArgs)
        {
            if(LogData != null)
                LogData(communicationState, serialDataArgs);
        }
    }
}
