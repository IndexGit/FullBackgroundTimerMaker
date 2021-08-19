using System.IO;
using System.Xml.Serialization;

namespace TimerClases.Objects.Helpers
{
    public static class DeserializeHelper
    {
        /// <summary>
        /// Десериализует файл в объект типа T
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="XmlFilename">Путь до файла</param>
        /// <returns></returns>
        public static T DeserializeXMLFileToObject<T>(string XmlFilename)
        {
            if (string.IsNullOrEmpty(XmlFilename)) return default(T);


            StreamReader xmlStream = new StreamReader(XmlFilename);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            var returnObject = (T)serializer.Deserialize(xmlStream);

            return returnObject;
        }
    }
}
