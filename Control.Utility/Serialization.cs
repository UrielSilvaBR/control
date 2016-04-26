using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Control.Utility
{
    public static class Serialization
    {
        public static string Serialize(Object Object)
        {
            MemoryStream stream = null;

            try
            {
                stream = new MemoryStream();

                //Configurações do XML
                XmlWriterSettings settings = new XmlWriterSettings();
                //A linha abaixo omite a declaração do XML: <?xml version="1.0" encoding="utf-8"?>
                //settings.OmitXmlDeclaration = true;
                //Definir a codificação do XML
                settings.Encoding = Encoding.UTF8;

                //Identar o XML automaticamente
                settings.Indent = true;

                //Usar o Create do XmlTextWriter para aplicar as configurações no XML
                XmlWriter writer = XmlTextWriter.Create(stream, settings);

                XmlSerializer serializer = new XmlSerializer(Object.GetType());

                serializer.Serialize(writer, Object);

                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding();
                string xml = utf.GetString(arr).Trim();
                xml = CleanEmptyTags(xml);
                return xml;
            }
            catch (Exception)
            {
                return string.Empty;
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }

        public static string CleanEmptyTags(String xml)
        {
            Regex regex = new Regex(@"(\s)*<(\w)*(\s)*/>");
            return regex.Replace(xml, string.Empty);
        }

        public static T Deserialize<T>(string Data)
        {
            T result;
            StringReader rdr = null;
            try
            {
                rdr = new StringReader(Data);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                result = (T)xmlSerializer.Deserialize(rdr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                rdr.Close();
            }

            return result;
        }

        public static bool CleanUpEmptyLists(object o, string objectNamespace)
        {
            //  Skip if the object is already null
            if (o == null)
            {
                return false;
            }

            //  Get the types of the object
            Type type = o.GetType();

            //  If this is an empty list, set it to null
            if (o is IList)
            {
                IList list = (IList)o;

                if (list.Count == 0)
                {
                    return true;
                }
                else
                {
                    foreach (object obj in list)
                    {
                        CleanUpEmptyLists(obj, objectNamespace);
                    }
                }

                return false;
            }
            //  Ignore any objects that aren't in our namespace for perf reasons
            //  and to avoid getting errors on trying to get into every little detail
            else if (type.Namespace != objectNamespace)
            {
                return false;
            }

            //  Loop over all properties and handle them
            foreach (PropertyInfo property in type.GetProperties())
            {
                //  Get the property value and clean up any empty lists it contains
                object propertyValue = property.GetValue(o, null);
                if (CleanUpEmptyLists(propertyValue, objectNamespace))
                {
                    property.SetValue(o, null, null);
                }
            }

            return false;
        }
    }
}
