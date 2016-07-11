using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace Control.Utility
{
    public static class Utilities
    {
        public static string RemoveBookMark(string text)
        {
            string BOMMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (text.StartsWith(BOMMarkUtf8))
                text = text.Remove(0, BOMMarkUtf8.Length);

            return text.Replace("\0", "");
        }

        public static void RemoveNamespaceAttributes(XmlNode node)
        {
            if (node.Attributes != null)
            {
                for (int i = node.Attributes.Count - 1; i >= 0; i--)
                {
                    if (node.Attributes[i].Name.Contains(':') || node.Attributes[i].Name == "xmlns")
                        node.Attributes.Remove(node.Attributes[i]);
                }
            }

            foreach (XmlNode n in node.ChildNodes)
                RemoveNamespaceAttributes(n);
        }

        public static XDocument GetXmlFileSerialized(Object objeto, Encoding encode)
        {
            var arquivoXml = Utilities.RemoveBookMark(Serialization.Serialize(objeto));

            var objXml = new XmlDocument();
            objXml.LoadXml(arquivoXml);

            RemoveNamespaceAttributes((XmlNode)objXml);

            return XDocument.Parse(objXml.OuterXml);
        }

        public static string RemoveWhiteSpaces(string text)
        {
            string ret = string.Empty;

            foreach (var item in text)
                ret += item.ToString().Trim();

            return ret;
        }

        public static IList<T> EnumToList<T>()
        {
            if (!typeof(T).IsEnum)
                throw new Exception("Conversão não pode ser reaizada!\nObjeto não é do tipo Enum!");

            IList<T> list = new List<T>();
            Type type = typeof(T);

            if (type != null)
            {
                Array enumValues = Enum.GetValues(type);

                foreach (T value in enumValues)
                    list.Add(value);
            }

            return list;
        }

        public static String GetDescription(this Enum item)
        {
            Type tipo = item.GetType();
            FieldInfo fi = tipo.GetField(item.ToString());
            DescriptionAttribute[] atributos =
            fi.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    as DescriptionAttribute[];
            if (atributos.Length > 0)
                return atributos[0].Description;
            else
                return String.Empty;
        }

        public static int GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return Convert.ToInt32((T)field.GetValue(null));
                }
                else
                {
                    if (field.Name == description)
                        return Convert.ToInt32((T)field.GetValue(null));
                }
            }

            throw new ArgumentException("Não encontrado", "Descricao");
        }

        public static TResult SafeInvoke<T, TResult>(this T isi, Func<T, TResult> call) where T : ISynchronizeInvoke
        {
            if (isi.InvokeRequired)
            {
                IAsyncResult result = isi.BeginInvoke(call, new object[] { isi });
                object endResult = isi.EndInvoke(result); return (TResult)endResult;
            }
            else
                return call(isi);
        }

        public static void SafeInvoke<T>(this T isi, Action<T> call) where T : ISynchronizeInvoke
        {
            if (isi.InvokeRequired) isi.BeginInvoke(call, new object[] { isi });
            else
                call(isi);
        }

        public static string GetXmlAddressByCEP(string cep)
        {
            try
            {
                if (!IsInternetConnected())
                    throw new Exception("Computador não conectado a Internet, por favor, prossiga com o cadastro de endereço manualmente!");

                var objXml = XDocument.Load(String.Format("http://cep.republicavirtual.com.br/web_cep.php?cep={0}&formato=xml", cep));

                return objXml.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);

        public static Boolean IsInternetConnected()
        {
            bool result;

            try
            {
                int Description;
                result = InternetGetConnectedState(out Description, 0);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public static void ReadWriteStream(Stream readStream, Stream writeStream)
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
