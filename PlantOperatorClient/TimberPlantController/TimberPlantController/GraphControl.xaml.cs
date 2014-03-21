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

        private void showChart()
        {
            this.lineChart.DataContext = _sensorDataModel;


            List<KeyValuePair<string, int >> valueList = new List<KeyValuePair<string, int>>();
            foreach (var sensorModel in _sensorDataModel.SensorCollection)
            {
                valueList.Add(new KeyValuePair<string, int>(sensorModel.Sensor1, int.Parse(sensorModel.Sensor2)));

            }
            //valueList.Add(new KeyValuePair<string, int>("Developer", 60));
            //valueList.Add(new KeyValuePair<string, int>("Misc", 20));
            //valueList.Add(new KeyValuePair<string, int>("Tester", 50));
            //valueList.Add(new KeyValuePair<string, int>("QA", 30));
            //valueList.Add(new KeyValuePair<string, int>("Project Manager", 40));

            ////<Charting:LineSeries  DependentValuePath="Value" IndependentValuePath="Key" ItemsSource="{Binding}" IsSelectionEnabled="True"/>
            ////<Charting:LineSeries  DependentValuePath="Sensor1" IndependentValuePath="Sensor2" ItemsSource="{Binding SensorCollection}" IsSelectionEnabled="True"/>
            this.lineChart.DataContext = valueList;
        }
    }
}
