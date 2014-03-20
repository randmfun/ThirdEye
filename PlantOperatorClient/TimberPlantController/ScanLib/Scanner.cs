namespace Randmfun.ScanLib
{
    public class Scanner
    {
        public ISerialIo SerialIo { get; set; }

        public Scanner(ISerialIo serialIo)
        {
            this.SerialIo = serialIo;
        }

        public string Start()
        {
            var response = SerialIo.Start();
            this.SerialIo.LogData += new SerialDataEventHandler(SerialIo_LogData);
            return "";
        }

        void SerialIo_LogData(CommunicationState communicationState, SerialDataArgs evenArgs)
        {
            
        }

    }
}
