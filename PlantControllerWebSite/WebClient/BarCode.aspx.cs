using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Randmfun.Archiver;
using Randmfun.BarCodeGenerator;
using Randmfun.DataModel;
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

        public string GetGenerateBarCodeText(SensorDataModel sensorDataModel)
        {
            var barCodeString = BarcodeManager.GenerateBarCodeString(sensorDataModel);

            Code128Content content = new Code128Content(barCodeString);
            var text = string.Join(string.Empty, content.Codes);

            return text;
        }

        public string GetImageText(string generateBarCodeText)
        {
            //Update UI - clean this up
            this.lblGeneratedBarCodeContent.Text = generateBarCodeText;

            System.Drawing.Image myimg = Code128Rendering.MakeBarcodeImage(generateBarCodeText, 2, true);

            MemoryStream ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
            myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            return "data:image/bmp;base64," + Convert.ToBase64String((byte[])ms.GetBuffer());
        }

        public byte[] GetImageBuffer(string generateBarCodeText)
        {
            //Update UI - clean this up
            this.lblGeneratedBarCodeContent.Text = generateBarCodeText;

            System.Drawing.Image myimg = Code128Rendering.MakeBarcodeImage(generateBarCodeText, 2, true);

            MemoryStream ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
            myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            //return "data:image/bmp;base64," + Convert.ToBase64String((byte[])ms.GetBuffer());
            return ms.GetBuffer();
        }

        public SensorDataModel GetSensorDataModel(string binFilePath)
        {
            return Serializer.DeSerializeData(binFilePath);
        }

        protected void btnGenBarCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileUploadControl.HasFile)
                {
                    var serverMapPath = Server.MapPath("/data/");

                    // Get the name of the file to upload.
                    string fileName = Server.HtmlEncode(FileUploadControl.FileName);

                    // Get the extension of the uploaded file.
                    string extension = System.IO.Path.GetExtension(fileName);
                    if (FileUploadControl.PostedFile.ContentType == "application/octet-stream"
                        && extension == ".bin")
                    {
                        // Append the name of the file to upload to the path.
                        string binSaveFilePath = serverMapPath + fileName;
                        var randomFileName = Path.GetRandomFileName() + ".png";
                        var imageSavePathName = Path.Combine(serverMapPath, randomFileName);

                        //Save to disk
                        this.FileUploadControl.SaveAs(binSaveFilePath);

                        this.lblUploadedFileNameContent.Text = fileName;

                        var sensorDataModel = GetSensorDataModel(binSaveFilePath);
                        var generatedBarCodeText = GetGenerateBarCodeText(sensorDataModel);
                        var strImageContent = GetImageBuffer(generatedBarCodeText);

                        using (FileStream fs = File.Create(imageSavePathName))
                        {
                            // Add some text to file
                            Byte[] title = strImageContent;
                            fs.Write(title, 0, title.Length);
                        }

                        this.imgBarCode.ImageUrl = @"Data/" + randomFileName;

                        var binFileBytes = new byte[this.FileUploadControl.PostedFile.InputStream.Length];
                        this.FileUploadControl.PostedFile.InputStream.Read(binFileBytes, 0, binFileBytes.Length);

                        //StoreToDb(binFileBytes, sensorDataModel, generatedBarCodeText);

                        //Delete the file
                        if (File.Exists(binSaveFilePath))
                            File.Delete(binSaveFilePath);

                        //if(File.Exists(imageSavePathName))
                        //    File.Delete(imageSavePathName);
                    }
                }
            }
            catch (Exception exception)
            {
                this.lblUploadedFileNameContent.Text = exception.Message;
            }
        }

        private void StoreToDb(byte[] byteStream, SensorDataModel sensorDataModel, string generateBarCode)
        {
            // Get the UserId of the just-added user
            var newUser = Membership.GetUser();
            var newUserId = newUser.ProviderUserKey.ToString();

            //' Insert a new record into UserProfiles          
            var connectionString =
                ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            string binFileId = Guid.NewGuid().ToString();

            var insertSql =
                "Insert into BinFileStore VALUES(@UserId, @BinFileId, @DetailsName, @DetailsDesc, @UploadedTime, @GeneratedBarCode, @BinFile)";
            using (var myConnection = new SqlConnection(connectionString))
            {
                myConnection.Open();
                var myCommand = new SqlCommand(insertSql, myConnection);
                myCommand.Parameters.AddWithValue("@UserId", newUserId);
                myCommand.Parameters.AddWithValue("@BinFileId", binFileId);
                myCommand.Parameters.AddWithValue("@DetailsName", sensorDataModel.DetailsName);
                myCommand.Parameters.AddWithValue("@DetailsDesc", sensorDataModel.DetailsDesc);
                myCommand.Parameters.AddWithValue("@UploadedTime", DateTime.Now);
                myCommand.Parameters.AddWithValue("@GeneratedBarCode", generateBarCode);
                myCommand.Parameters.AddWithValue("@BinFile", byteStream);

                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

    }
}