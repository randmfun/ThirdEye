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

            var sensorDataModel = new SensorDataModel();
            sensorDataModel.DetailsName = "Details Name";
            sensorDataModel.DetailsDesc = "DetailsDesc";

            sensorDataModel.SensorCollection.Add(new SensorModel());
            sensorDataModel.SensorCollection.Add(new SensorModel());
            sensorDataModel.SensorCollection.Add(new SensorModel());

            Randmfun.Archiver.Serializer.SerializeData(sensorDataModel, outputFile);

            var sensorDataModelActual = Randmfun.Archiver.Serializer.DeSerializeData(outputFile);

            Assert.AreEqual(sensorDataModelActual.DetailsDesc, sensorDataModelActual.DetailsDesc);
            Assert.AreEqual(sensorDataModelActual.DetailsName, sensorDataModelActual.DetailsName);
            Assert.AreEqual(sensorDataModelActual.SensorCollection[0].DateTime, sensorDataModel.SensorCollection[0].DateTime);
        }
    }
}
