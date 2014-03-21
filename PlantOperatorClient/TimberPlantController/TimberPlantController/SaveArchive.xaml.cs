using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Randmfun.DataModel;

namespace TimberPlantController
{
    /// <summary>
    /// Interaction logic for SaveArchive.xaml
    /// </summary>
    public partial class SaveArchive : Window
    {
        SaveArchiveViewModel saveArchiveViewModel = new SaveArchiveViewModel();

        public SaveArchive()
        {
            this.Owner = Application.Current.MainWindow;
            InitializeComponent();
            this.DataContext = saveArchiveViewModel;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var sensorDataModel = GetCurrentSensorDataModel();

                sensorDataModel.DetailsDesc = this.saveArchiveViewModel.DetailsDesc;
                sensorDataModel.DetailsName = this.saveArchiveViewModel.DetailsName;

                var fileFullPath = Path.Combine(this.saveArchiveViewModel.FilePath, this.saveArchiveViewModel.FileName);

                Randmfun.Archiver.Serializer.SerializeData(sensorDataModel, fileFullPath);

                MessageBox.Show("Archived Sucessfully at" + fileFullPath);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Archive FAILED " + exception.Message + exception.InnerException);
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private SensorDataModel GetCurrentSensorDataModel()
        {
            return ThirdEyeApplicationContext.GetCurrentSensorDataModel();
        }

    }

    public class SaveArchiveViewModel: INotifyPropertyChanged
    {
        public SaveArchiveViewModel()
        {
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RandMFun");
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value;
            this.OnNotifyPropertyChanged("FilePath");
            }
        }

        private string _fileName = string.Empty;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                this.OnNotifyPropertyChanged("FileName");
            }
        }

        private string _detailsName=string.Empty;
        public string DetailsName
        {
            get { return _detailsName; }
            set
            {
                _detailsName = value;
                this.OnNotifyPropertyChanged("DetailsName");
            }
        }

        private string _detailsDesc = string.Empty;
        public string DetailsDesc
        {
            get { return _detailsDesc; }
            set
            {
                _detailsDesc = value;
                this.OnNotifyPropertyChanged("DetailsDesc");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnNotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
