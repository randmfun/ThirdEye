using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Randmfun.Archiver;
using Randmfun.BarCodeGenerator;
using WebClient.Barcode;

namespace WebClient
{
    public partial class BarCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void Generate()
        {
            
        }

        public string GetImage(string binFilePath)
        {
            var sensorModel = Serializer.DeSerializeData(binFilePath);

            var barCodeString = BarcodeManager.GenerateBarCodeString(sensorModel);

            Code128Content content = new Code128Content(barCodeString);
            var text = string.Join(string.Empty, content.Codes);

            //Update UI - clean this up
            this.lblGeneratedBarCodeContent.Text = text;

            System.Drawing.Image myimg = Code128Rendering.MakeBarcodeImage(text, 2, true);

            MemoryStream ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
            myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            
            return "data:image/bmp;base64," + Convert.ToBase64String((byte[])ms.GetBuffer());
        }

        protected void btnGenBarCode_Click(object sender, EventArgs e)
        {
            var savePath = @"C:\Users\jacob.a.a.CORP\Documents\randmfun\uploads\";
            try
            {
                if (this.FileUploadControl.HasFile)
                {
                    // Get the name of the file to upload.
                    string fileName = Server.HtmlEncode(FileUploadControl.FileName);

                    // Get the extension of the uploaded file.
                    string extension = System.IO.Path.GetExtension(fileName);
                    if (FileUploadControl.PostedFile.ContentType == "application/octet-stream"
                        && extension == ".bin")
                    {
                        // Append the name of the file to upload to the path.
                        savePath += fileName;

                        // Get byte stream here 
                        // this.FileUploadControl.FileBytes;

                        //Save to disk
                        this.FileUploadControl.SaveAs(savePath);

                        this.lblUploadedFileNameContent.Text = fileName;
                        this.imgBarCode.ImageUrl = GetImage(savePath);
                        
                        //Delete the file
                        if(File.Exists(savePath))
                            File.Delete(savePath);

                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
            }
        }

        protected void btnBrowseBinFile_Click(object sender, EventArgs e)
        {
            
        }
        
    }
}