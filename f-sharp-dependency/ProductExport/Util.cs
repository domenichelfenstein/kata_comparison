using System.Globalization;
using System.Xml;

namespace FSharp.Dependency.ProductExport
{
    public static class XmlFormatter
    {
        public static string PrettyPrint(string xml)
        {
            var xmlDoc = new XmlDocument();
            var sw = new StringWriter();
            xmlDoc.LoadXml(xml);
            xmlDoc.Save(sw);
            return sw.ToString();
        }
    }

    public class Util
    {
        public static DateTime FromIsoDate(string datetime)
        {
            return DateTime.Parse(datetime, null, DateTimeStyles.RoundtripKind);
        }

        public static string ToIsoDate(DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}