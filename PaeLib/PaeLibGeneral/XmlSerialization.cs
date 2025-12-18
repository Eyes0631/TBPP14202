using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PaeLibGeneral
{
    /// <summary>
    /// XML序列化物件
    /// </summary>
    public static class XmlObjectSerializer
    {
        /// <summary>
        /// 將物件序列化成XML格式字串
        /// </summary>
        /// <typeparam name="T">物件型別</typeparam>
        /// <param name="obj">物件</param>
        /// <returns>XML格式字串</returns>
        public static string Serialize<T>(T obj) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(writer, obj);
                return PrintXML(stringWriter.ToString());
            }
        }

        /// <summary>
        /// 將XML格式字串反序列化成物件
        /// </summary>
        /// <typeparam name="T">物件型別</typeparam>
        /// <param name="xmlString">XML格式字串</param>
        /// <returns>反序列化後的物件</returns>
        public static T Deserialize<T>(string xmlString) where T : class
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(xmlString))
            {
                object deserializationObj = deserializer.Deserialize(reader);
                return deserializationObj as T;
            };
        }

        /// <summary>
        /// 格式化 XML 字串 (方便人員閱讀)
        /// 【 XML Formatter Link 】 - 將 XML 內容格式化排列整齊，方便閱讀的網站。
        /// Formats a XML string/file with your desired indentation level.
        /// The formatting rules are not configurable but it uses a per-element indentation pattern giving the best readability.
        /// https://www.freeformatter.com/xml-formatter.html
        /// </summary>
        /// <param name="XML">格式化前的 XML 字串</param>
        /// <returns>格式化後的 XML 字串</returns>
        public static String PrintXML(String XML)
        {
            String Result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
            XmlDocument document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                document.LoadXml(XML);

                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                String FormattedXML = sReader.ReadToEnd();

                Result = FormattedXML;
            }
            catch (XmlException)
            {
            }

            mStream.Close();
            writer.Close();

            return Result;
        }
    }
}