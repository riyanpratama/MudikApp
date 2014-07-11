using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MudikApp2014.Classes
{
    public class XMLHelper
    {
        public static T Deserialize<T>(string xmlString)
        {
            TextReader textReader = new StringReader(xmlString);
            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore };

            using (XmlReader stream = XmlReader.Create(textReader, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T result;
                try
                {
                    result = (T)serializer.Deserialize(stream);
                }
                catch (Exception ex)
                {
                    result = default(T);
                }
                return result;
            }
        }

        public static string Serialize<T>(T t)
        {
            string xmlResult = string.Empty;
            MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            try
            {
                serializer.Serialize(stream, t);
                stream.Position = 0;
                StreamReader sr = new StreamReader(stream);
                xmlResult = sr.ReadToEnd();
                sr.Close();
            }
            catch
            {

            }
            return xmlResult;
        }
    }
}
