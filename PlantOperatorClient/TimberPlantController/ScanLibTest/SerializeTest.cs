using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Randmfun.DataModel;

namespace ScanLibTest
{
    [TestClass]
    public class SerializeTest
    {
        [TestMethod]
        public void SerializeAndDeSerialize_SensorDataModel_Test()
        {
            var outputFile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            var sensorDataModelExpected = new SensorDataModel();
            sensorDataModelExpected.DetailsName = "Details Name";
            sensorDataModelExpected.DetailsDesc = "DetailsDesc";

            for (int i = 0; i < 100; i++ )
                sensorDataModelExpected.SensorCollection.Add(new SensorModel(){Sensor1 = i.ToString()});

            Randmfun.Archiver.Serializer.SerializeData(sensorDataModelExpected, outputFile);

            var sensorDataModelActual = Randmfun.Archiver.Serializer.DeSerializeData(outputFile);

            Assert.AreEqual(sensorDataModelActual.DetailsDesc, sensorDataModelActual.DetailsDesc);
            Assert.AreEqual(sensorDataModelActual.DetailsName, sensorDataModelActual.DetailsName);
            Assert.AreEqual(sensorDataModelExpected.SensorCollection.Count, sensorDataModelActual.SensorCollection.Count);

            for (int i = 0; i < 100; i++)
                Assert.AreEqual(sensorDataModelActual.SensorCollection[i].Sensor1, sensorDataModelExpected.SensorCollection[i].Sensor1);
        }
    }
}
