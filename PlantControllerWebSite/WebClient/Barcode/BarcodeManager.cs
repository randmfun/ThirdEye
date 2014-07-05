using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Randmfun.DataModel;

namespace WebClient.Barcode
{
    public class BarcodeManager
    {
        public static string GenerateBarCodeString(SensorDataModel sensorDataModel)
        {
            if (!IsValid(sensorDataModel))
                return string.Empty;

            const string startThreshold = "25";
            const string endThreshold = "80";
            
            DateTime startTime = default(DateTime);
            DateTime endTime = default(DateTime);
            
            bool computeStartTime=true;
            bool computeEndTime = false;
            
            var enumerator = sensorDataModel.SensorCollection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (computeStartTime)
                {
                    if (int.Parse(enumerator.Current.Sensor1) > int.Parse(startThreshold))
                    {
                        startTime = enumerator.Current.DateTime;
                        computeStartTime = false;
                        computeEndTime = true;
                    }
                }
                if (computeEndTime)
                {
                    if (int.Parse(enumerator.Current.Sensor1) > int.Parse(endThreshold))
                    {
                        endTime = enumerator.Current.DateTime;
                    }
                }
            }

            return string.Format("{0}{1}{2}{3}", 
                startTime.Hour.ToString(), startTime.Minute, 
                endTime.Hour, endTime.Minute);
        }

        public static bool IsValid(SensorDataModel sensorDataModel)
        {
            var enumerator = sensorDataModel.SensorCollection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (int.Parse(enumerator.Current.Sensor2) < 20
                    || int.Parse(enumerator.Current.Sensor2) > 80)
                {
                    return false;
                }
            }

            return true;

        }
    }
}