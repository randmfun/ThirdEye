using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Randmfun.Archiver
{
    public class Cryptographer
    {
        public static void WriteEnCryptedMemoryStream(MemoryStream memoryStream, string outputFilePath, string password = "password")
        {
            var fs = new FileStream(outputFilePath, FileMode.Create);

            var algorithm = GetAlgorithm(password);

            var length = memoryStream.Length > 1024 ? 1024 : memoryStream.Length;
            var fileData = new byte[length];

            var encryptedStream = new CryptoStream(fs, algorithm.CreateEncryptor(), CryptoStreamMode.Write);

            while (memoryStream.Read(fileData, 0, fileData.Length) != 0)
            {
                encryptedStream.Write(fileData, 0, fileData.Length);
            }

            encryptedStream.Flush();
            encryptedStream.Close();

            memoryStream.Flush();
            memoryStream.Close();
        }

        public static MemoryStream GetDeCryptedMemoryStream(string outputFilePath, string password = "password")
        {
            var ms = new MemoryStream();

            var inFile = new FileStream(outputFilePath, FileMode.Open, FileAccess.Read);
            
            var algorithm = GetAlgorithm(password);

            var length = inFile.Length > 1024 ? 1024 : inFile.Length;
            var fileData = new byte[length];

            var encryptedStream = new CryptoStream(inFile, algorithm.CreateDecryptor(), CryptoStreamMode.Read);

            while (encryptedStream.Read(fileData, 0, fileData.Length) != 0)
            {
                ms.Write(fileData, 0, fileData.Length);
            }

            encryptedStream.Flush();
            encryptedStream.Close();
            inFile.Close();

            ms.Position = 0;

            return ms;
        }

        private static RijndaelManaged GetAlgorithm(string password)
        {
            var algorithm = new RijndaelManaged();
            var key = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes("Salt text"));
            algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
            algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);
            return algorithm;
        }
    }
}
