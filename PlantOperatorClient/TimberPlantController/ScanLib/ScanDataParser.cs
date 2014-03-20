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
                currentModel.Sensor1 = value;
            }
            else if (readerIndex == 7)
            {
                currentModel.Sensor1 = value;
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
