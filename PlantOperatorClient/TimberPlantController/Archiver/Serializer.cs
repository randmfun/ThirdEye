using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Randmfun.DataModel;

namespace Randmfun.Archiver
{
    public class Serializer
    {
        public static void SerializeData(SensorDataModel sensorDataModel, string outputFilePath)
        {
            var ms = new MemoryStream();
            
            ProtoBuf.Serializer.Serialize<SensorDataModel>(ms, sensorDataModel);

            ms.Position = 0;
            using (ms)
            {
                Cryptographer.WriteEnCryptedMemoryStream(ms, outputFilePath);
            }
        }

        public static SensorDataModel DeSerializeData(string outputFilePath)
        {
            var ms = Cryptographer.GetDeCryptedMemoryStream(outputFilePath);
            SensorDataModel sensorDataModel;
            
            using (ms)
            {
                sensorDataModel = ProtoBuf.Serializer.Deserialize<SensorDataModel>(ms);
            }

            return sensorDataModel;
        }

    }
}
