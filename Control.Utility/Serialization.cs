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
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Xsl;

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
                settings.Indent = false;

                //Usar o Create do XmlTextWriter para aplicar as configurações no XML
                XmlWriter writer = XmlTextWriter.Create(stream, settings);

                XmlSerializer serializer = new XmlSerializer(Object.GetType());

                serializer.Serialize(writer, Object);

                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding(true);
                string xml = utf.GetString(arr).Trim();
                xml = RemoveEmptyTags(xml);
                return xml;
            }
            catch (Exception ex)
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

        public static string RemoveEmptyTags(string sXML)
        {
            System.Text.StringBuilder sb = new StringBuilder();

            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.Append("<xsl:stylesheet ");
            sb.Append("     version=\"1.0\" ");
            sb.Append("     xmlns:msxsl=\"urn:schemas-microsoft-com:xslt\"");
            sb.Append("     xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">");
            sb.Append("     <xsl:output method=\"xml\" version=\"1.0\" encoding=\"UTF-8\"/>");
            sb.Append("   <!-- Whenever you match any node or any attribute -->");
            sb.Append("   <xsl:template match=\"node()|@*\">");
            sb.Append("      <!-- Copy the current node -->");
            sb.Append("     <xsl:if test=\"normalize-space(.) != '' or normalize-space(./@*) != '' \">");
            sb.Append("          <xsl:copy>");
            sb.Append("              <!-- Including any attributes it has and any child nodes -->");
            sb.Append("               <xsl:apply-templates select=\"@*|node()\"/>");
            sb.Append("          </xsl:copy>");
            sb.Append("     </xsl:if>");
            sb.Append("   </xsl:template>");
            sb.Append("</xsl:stylesheet>");
            return transXMLStringThroughXSLTString(Utility.Utilities.RemoverBookMark(sXML), sb.ToString());
        }

        private static string transXMLStringThroughXSLTString(string sXML, string sXSLT)
        {
            //This is the logic of the application.
            XslCompiledTransform objTransform = new XslCompiledTransform();

            StringReader xmlStream = new StringReader(sXML);
            XmlReader xmlReader = new XmlTextReader(xmlStream);

            StringReader stream = new StringReader(sXSLT);
            XmlReader xmlReaderXslt = new XmlTextReader(stream);
            
            objTransform.Load(xmlReaderXslt, null, null);

            Utf8StringWriter objStream = new Utf8StringWriter();
            
            objTransform.Transform(xmlReader, null, objStream);

            return objStream.ToString().Replace(@"encoding=""utf-16""?>", @"encoding=""utf-8""?>");
        }
    }
}
