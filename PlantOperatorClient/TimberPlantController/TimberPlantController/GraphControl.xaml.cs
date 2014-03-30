using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
using Randmfun.DataModel;

namespace TimberPlantController
{
    /// <summary>
    /// Interaction logic for GraphControl.xaml
    /// </summary>
    public partial class GraphControl : UserControl
    {
        public GraphControl()
        {
            InitializeComponent();

            showChart();
        }

        private SensorDataModel _sensorDataModel = ThirdEyeApplicationContext.GetCurrentSensorDataModel();

        //Reference :
        // http://msdn.microsoft.com/en-us/magazine/ff714591.aspx
        //Dynamic Data Display : http://dynamicdatadisplay.codeplex.com/documentation
        private void showChart()
        {
            var sensorCollection = _sensorDataModel.SensorCollection;

            DateTime[] dates = new DateTime[sensorCollection.Count];
            float[] sensor1 = new float[sensorCollection.Count];
            float[] sensor2 = new float[sensorCollection.Count];
            float[] sensor3 = new float[sensorCollection.Count];
            float[] sensor4 = new float[sensorCollection.Count];
            float[] sensor5 = new float[sensorCollection.Count];
            float[] sensor6 = new float[sensorCollection.Count];
            float[] sensor7 = new float[sensorCollection.Count];
            float[] sensor8 = new float[sensorCollection.Count];

            for (int i = 0; i < sensorCollection.Count; ++i)
            {
                dates[i] = sensorCollection[i].DateTime;
                sensor1[i] = GetFloatVal(sensorCollection[i].Sensor1);
                sensor2[i] = GetFloatVal(sensorCollection[i].Sensor2);
                sensor3[i] = GetFloatVal(sensorCollection[i].Sensor3);
                sensor4[i] = GetFloatVal(sensorCollection[i].Sensor4);
                sensor5[i] = GetFloatVal(sensorCollection[i].Sensor5);
                sensor6[i] = GetFloatVal(sensorCollection[i].Sensor6);
                sensor7[i] = GetFloatVal(sensorCollection[i].Sensor7);
                sensor8[i] = GetFloatVal(sensorCollection[i].Sensor8);
            }

            var datesDataSource = new EnumerableDataSource<DateTime>(dates);
            datesDataSource.SetXMapping(x => dateAxis.ConvertToDouble(x));

            create(sensor1, "sensor 1", Brushes.Red, datesDataSource);
            create(sensor2, "sensor 2", Brushes.Green, datesDataSource);
            create(sensor3, "sensor 3", Brushes.Blue, datesDataSource);
            create(sensor4, "sensor 4", Brushes.Brown, datesDataSource);
            create(sensor5, "sensor 5", Brushes.Gold, datesDataSource);
            create(sensor6, "sensor 6", Brushes.GreenYellow, datesDataSource);
            create(sensor7, "sensor 7", Brushes.Lavender, datesDataSource);
            create(sensor8, "sensor 8", Brushes.Lavender, datesDataSource);

            plotter.Viewport.FitToView();
        }

        private float GetFloatVal(string str)
        {
            float retVal = 0.0f;

            float.TryParse(str, out retVal);

            return retVal;
        }

        private void create(float[] sensor, string sensorName, Brush brush, EnumerableDataSource<DateTime> datesDataSource)
        {
            var numbersensordatasource = new EnumerableDataSource<float>(sensor);
            numbersensordatasource.SetYMapping(y => y);

            var compositeSensor1DataSource = new
            CompositeDataSource(datesDataSource, numbersensordatasource);

            plotter.AddLineGraph(compositeSensor1DataSource,
              new Pen(brush, 2),
              new CirclePointMarker { Size = 10.0, Fill = brush },
              new PenDescription(sensorName));

        }
    }
}
