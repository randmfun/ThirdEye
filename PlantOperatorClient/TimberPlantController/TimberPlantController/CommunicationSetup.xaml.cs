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
using System.Windows.Shapes;

namespace TimberPlantController
{
    /// <summary>
    /// Interaction logic for CommunicationSetup.xaml
    /// </summary>
    public partial class CommunicationSetup : Window
    {
        private readonly CommunicationConfigViewModel communicationConfigViewModel;
        public CommunicationSetup()
        {
            InitializeComponent();

            communicationConfigViewModel = ThirdEyeApplicationContext.GetCommunicationConfigViewModel();
            this.DataContext = communicationConfigViewModel;
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            ThirdEyeApplicationContext.SetCommunicationConfigViewModel(communicationConfigViewModel);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
