using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Control.DAL.NFe.Objects
{
    public class NotaFiscal
    {
        public void GerarDANFE(string arquivoXml)
        {
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://www.webdanfe.com.br/danfe/GeraDanfe.php");

            string postData = "arquivoXml=" + arquivoXml;

            byte[] postBytes = Encoding.UTF8.GetBytes(postData);

            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.ContentLength = postBytes.Length;

            Stream requestStream = Request.GetRequestStream();

            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)Request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Resposta do Servidor: " + response.StatusCode.ToString());

                var stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));

                FileStream writeStream = new FileStream(@"C:\teste.pdf", FileMode.Create, FileAccess.Write);

                ReadWriteStream(stream.BaseStream, writeStream);
            }
        }

        private static void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }
}