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
        static CommunicationConfigViewModel communicationConfigViewModel = new CommunicationConfigViewModel();
        public CommunicationSetup()
        {
            InitializeComponent();
            this.DataContext = communicationConfigViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = communicationConfigViewModel.SelectedDataBit;
            var d = communicationConfigViewModel.SelectedStopBit;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
