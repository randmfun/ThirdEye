using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Randmfun.DataModel;

namespace TimberPlantController
{
    public class ThirdEyeApplicationContext
    {
        private static SensorDataModel currentSensorDataModel;

        private static CommunicationConfigViewModel communicationConfigViewModel;

        public static SensorDataModel GetCurrentSensorDataModel()
        {
            return currentSensorDataModel ?? new SensorDataModel();
        }

        public static void SetCurrentSensorModel(SensorDataModel sensorDataModel)
        {
            currentSensorDataModel = sensorDataModel;
        }

        public static void UpdateCurrentSensorModel(SensorModel sensorModel)
        {
            Application.Current.Dispatcher.Invoke(
                new Action(() => currentSensorDataModel.SensorCollection.Add(sensorModel)));
        }

        public static CommunicationConfigViewModel GetCommunicationConfigViewModel()
        {
            return communicationConfigViewModel ?? new CommunicationConfigViewModel();
        }

        public static void SetCommunicationConfigViewModel(CommunicationConfigViewModel configViewModel)
        {
            communicationConfigViewModel = configViewModel;
        }
    }
}
