using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Randmfun.BarCodeGenerator;

namespace TimberPlantController
{
    /// <summary>
    /// Interaction logic for BarCodeDemoCtrl.xaml
    /// </summary>
    public partial class BarCodeDemoCtrl : UserControl
    {
        public BarCodeDemoCtrl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Code128Content content = new Code128Content(txtInput.Text);
                this.txtGenBarCode.Text  = string.Join(string.Empty, content.Codes);

                System.Drawing.Image myimg = Code128Rendering.MakeBarcodeImage(txtInput.Text, int.Parse(txtWeight.Text), true);

                MemoryStream ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
                myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                BitmapImage ix = new BitmapImage();
                ix.BeginInit();
                ix.CacheOption = BitmapCacheOption.OnLoad;
                ix.StreamSource = ms;
                ix.EndInit();
                
                this.imgBarCode.Source = ix;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, this.txtInput.Text);
            }

        }
    }
}
