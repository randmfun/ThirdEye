﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Randmfun.DataModel;
using Randmfun.ScanLib;
using TimberPlantController.Common;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using PrintDialog = System.Windows.Forms.PrintDialog;

namespace TimberPlantController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static CommunicationState currentCommunicationState = CommunicationState.None;

        public MainWindow()
        {
            InitializeComponent();

            var result = new Authenticate().ShowDialog();
            if (!(result.HasValue && result.Value))
            {
                this.Close();
            }
        }

        private void MenuItem_Setup_Communication_Click(object sender, RoutedEventArgs e)
        {
            new CommunicationSetup().ShowDialog();
        }

        private void GenerateBarCode(object sender, RoutedEventArgs e)
        {
            this.dockpanel.Children.Clear();
            this.dockpanel.Children.Add(new BarCodeDemoCtrl());
        }

        private void ClickGraphView(object sender, RoutedEventArgs e)
        {
            this.dockpanel.Children.Clear();
            this.dockpanel.Children.Add(new GraphControl());
        }

        private void ClickListView(object sender, RoutedEventArgs e)
        {
            this.dockpanel.Children.Clear();
            this.dockpanel.Children.Add(new BatchList());
        }

        private void OpenArchiveClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
                                                {
                                                    Title = "Select Archive File",
                                                    DefaultExt = ".bin",
                                                    Filter = "Binary-file (.bin)|*.bin",
                                                    CheckFileExists = true
                                                };
            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;

                var sensorDataModel = Randmfun.Archiver.Serializer.DeSerializeData(fileName);
                ThirdEyeApplicationContext.SetCurrentSensorModel(sensorDataModel);

                this.lblArchive.Visibility = System.Windows.Visibility.Visible;
                this.lblDetail.Visibility = System.Windows.Visibility.Visible;
                this.lblDetailName.Visibility = System.Windows.Visibility.Visible;
                this.lblFilePath.Visibility = System.Windows.Visibility.Visible;

                this.lblFilePath.Content = fileName;
                this.lblDetailName.Content = sensorDataModel.DetailsName;
            }
        }

        private void ClickSaveArchive(object sender, RoutedEventArgs e)
        {
            new SaveArchive().ShowDialog();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private ISerialIo currentGlobalSerialIO;
        private SerialPort currentGlobalSerialPort;
 
        private void StartClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var sensorDataModel = new SensorDataModel();

                ThirdEyeApplicationContext.SetCurrentSensorModel(sensorDataModel);

                var comunicationModel = ThirdEyeApplicationContext.GetCommunicationConfigViewModel();

                this.currentGlobalSerialPort = new SerialPort();

                currentGlobalSerialPort.PortName = comunicationModel.SelectedComPort;
                currentGlobalSerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), comunicationModel.SelectedParity);
                currentGlobalSerialPort.BaudRate = int.Parse(comunicationModel.SelectedBaudRate);
                currentGlobalSerialPort.DataBits = int.Parse(comunicationModel.SelectedDataBit);
                currentGlobalSerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comunicationModel.SelectedStopBit);

                this.currentGlobalSerialIO = new SerialIoLayer(currentGlobalSerialPort);
                currentGlobalSerialIO.LogData += (serialIo_LogData);

                currentGlobalSerialIO.Start();

                this.lblArchive.Visibility = System.Windows.Visibility.Collapsed;
                this.lblDetail.Visibility = System.Windows.Visibility.Collapsed;
                this.lblDetailName.Visibility = System.Windows.Visibility.Collapsed;
                this.lblFilePath.Visibility = System.Windows.Visibility.Visible;
                this.lblFilePath.Content = string.Format("Reading from {0}, at {1} baudrate", currentGlobalSerialPort.PortName, currentGlobalSerialPort.BaudRate.ToString());

                currentCommunicationState = CommunicationState.Reading;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + exception.InnerException);
            }
        }

        private void StopReadingClick(object sender, RoutedEventArgs e)
        {
            if(currentGlobalSerialIO != null)
                currentGlobalSerialIO.Stop();

            if(currentGlobalSerialPort != null)
                currentGlobalSerialPort.Close();

            this.lblArchive.Visibility = System.Windows.Visibility.Collapsed;
            this.lblDetail.Visibility = System.Windows.Visibility.Collapsed;
            this.lblDetailName.Visibility = System.Windows.Visibility.Collapsed;
            this.lblFilePath.Visibility = System.Windows.Visibility.Collapsed;

            if (currentGlobalSerialPort != null)
            {
                this.lblFilePath.Content = string.Format("Stopped Reading from {0}, at {1} baudrate",
                                                         currentGlobalSerialPort.PortName,
                                                         currentGlobalSerialPort.BaudRate.ToString());
                this.lblFilePath.Visibility = System.Windows.Visibility.Visible;
            }

            currentCommunicationState = CommunicationState.Closed;
        }

        private void LoadDummyDataClick(object sender, RoutedEventArgs e)
        {
            var _sensorDataModel = new SensorDataModel();
            _sensorDataModel.SensorCollection = new ObservableCollection<SensorModel>();
            for (int i = 20, j=21, k=22; i < 90; i++, j++, k++)
            {
                int year = 1870 + i;
                int month = i%10 + 1;
                int day = i%28 + 1;
                _sensorDataModel.SensorCollection.Add(new SensorModel()
                                                          {
                                                              DateTime = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day)),
                                                              Sensor1 = i.ToString(), 
                                                              Sensor2 = j.ToString(),
                                                              Sensor3 = k.ToString()
                                                          });
            }

            ThirdEyeApplicationContext.SetCurrentSensorModel(_sensorDataModel);
        }
        
        private void CloseViewsClick(object sender, RoutedEventArgs e)
        {
            this.dockpanel.Children.Clear();
        }

        void serialIo_LogData(CommunicationState communicationState, SerialDataArgs evenArgs)
        {
            ThirdEyeApplicationContext.UpdateCurrentSensorModel(evenArgs.Sender);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopReadingClick(null, null);
        }

        public void PrintClick(object sender, RoutedEventArgs e)
        {
            new PrintSenorDetails().Print(); 

        }

        public void PrintPreviewClick(object sender, RoutedEventArgs e)
        {
            new PrintSenorDetails().PrintPreview();
        }
    }
}
