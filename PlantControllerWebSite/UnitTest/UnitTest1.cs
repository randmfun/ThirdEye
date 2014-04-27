using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Randmfun.DataModel;
using WebClient.Barcode;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGenerateBarCodeString()
        {
            var genBarCodeString = BarcodeManager.GenerateBarCodeString(CreateSensorDataModel());
            Assert.AreEqual("14141515", genBarCodeString);
        }

        public SensorDataModel CreateSensorDataModel()
        {
            SensorDataModel sensorDataModel = new SensorDataModel();
            sensorDataModel.DetailsName = "Test Details Name";
            sensorDataModel.StartDateTime = DateTime.Now;

            for (int i = 0; i < 10; i++)
                sensorDataModel.SensorCollection.Add(CreateSensorModel(i.ToString()));

            var startSensorModel = CreateSensorModel("26");
            startSensorModel.DateTime = new DateTime(2014, 04, 26, 14, 14, 14);
            sensorDataModel.SensorCollection.Add(startSensorModel);

            for (int i = 27; i < 36; i++)
                sensorDataModel.SensorCollection.Add(CreateSensorModel(i.ToString()));

            var endSensorModel = CreateSensorModel("81");
            endSensorModel.DateTime = new DateTime(2014, 04, 26, 15, 15, 15);
            sensorDataModel.SensorCollection.Add(endSensorModel);

            return sensorDataModel;
        }

        private SensorModel CreateSensorModel(string someVal)
        {
            return new SensorModel
                                  {
                                      DateTime = DateTime.Now,
                                      Sensor1 = someVal,
                                      Sensor2 = someVal,
                                      Sensor3 = someVal,
                                      Sensor4 = someVal,
                                      Sensor5 = someVal,
                                      Sensor6 = someVal,
                                      Sensor7 = someVal,
                                      Sensor8 = someVal
                                  };

        }
    }
}
