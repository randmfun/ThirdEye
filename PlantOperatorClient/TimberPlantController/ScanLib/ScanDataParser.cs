using System.Collections.Generic;
using Randmfun.DataModel;

namespace Randmfun.ScanLib
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
                    NotifyLogData(communicationState, null);
                }
            }
            else if (communicationState == CommunicationState.Reading)
            {
                var charAry = inputBuffer.Split(' ');

                List<string > lst = new List<string>();
                foreach (var s in charAry)
                {
                    if(!string.IsNullOrWhiteSpace(s))
                        lst.Add(s);
                }

                AddToReading(lst);
            }
        }

        private void AddToReading(List<string> value)
        {
            if(value.Count < 8 )
                return;

            var currentModel = new SensorModel();
            currentModel.Sensor1 = value[0];
            currentModel.Sensor2 = value[1];
            currentModel.Sensor3 = value[2];
            currentModel.Sensor4 = value[3];
            currentModel.Sensor5 = value[4];
            currentModel.Sensor6 = value[5];
            currentModel.Sensor7 = value[6];
            currentModel.Sensor8 = value[7];

            NotifyLogData(CommunicationState.Reading, new SerialDataArgs(currentModel));
        }

        private void NotifyLogData(CommunicationState communicationState, SerialDataArgs serialDataArgs)
        {
            if(LogData != null)
                LogData(communicationState, serialDataArgs);
        }
    }
}
