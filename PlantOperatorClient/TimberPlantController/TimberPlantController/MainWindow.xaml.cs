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

namespace TimberPlantController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Setup_Communication_Click(object sender, RoutedEventArgs e)
        {
            var obj = new CommunicationSetup();
            obj.ShowDialog();
        }

        private void ReadData()
        {
            
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

    }
}
