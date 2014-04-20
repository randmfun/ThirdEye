using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Randmfun.BarCodeGenerator;

namespace WebClient
{
    public partial class BarCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgBarCode.ImageUrl = GetImage("");
        }

        public string GetImage(object img)
        {
            Code128Content content = new Code128Content("hello");
            var text = string.Join(string.Empty, content.Codes);

            System.Drawing.Image myimg = Code128Rendering.MakeBarcodeImage("hello", 2, true);

            MemoryStream ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
            myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            
            return "data:image/bmp;base64," + Convert.ToBase64String((byte[])ms.GetBuffer());
        }
    }
}