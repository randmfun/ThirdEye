using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Randmfun.Archiver;

namespace ScanLibTest
{
    [TestClass]
    public class CryptographerTest
    {
        [TestMethod]
        public void EncryptDataTest()
        {
            var data = Encoding.ASCII.GetBytes("Salt text");
            var ms = new MemoryStream(data);

            var outputFile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            Cryptographer.WriteEnCryptedMemoryStream(ms, outputFile);

            var fileInfo = new FileInfo(outputFile);
            Assert.IsTrue(fileInfo.Exists);
            Assert.IsTrue(fileInfo.Length==16);

            using (var streamReader = new StreamReader(outputFile, true))
            {
                const string expectedContent = @"���Ʌ�����-�h";

                var readContent = streamReader.ReadToEnd();
                Assert.AreEqual(readContent, expectedContent);
            }

            DeleteFile(outputFile);
        }

        [TestMethod]
        public void DecryptTest()
        {
            var data = Encoding.ASCII.GetBytes("Salt text");
            var ms = new MemoryStream(data);
            var outputFile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            Cryptographer.WriteEnCryptedMemoryStream(ms, outputFile);

            var memoryStream = Cryptographer.GetDeCryptedMemoryStream(outputFile);
            var actualData = new Byte[memoryStream.Length];

            memoryStream.Read(actualData, 0, (int)memoryStream.Length);

            CollectionAssert.IsSubsetOf(data,actualData);

            DeleteFile(outputFile);
        }

        private static void DeleteFile(string filePath)
        {
            try
            {
                if(File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch{}
        }
    }
}
