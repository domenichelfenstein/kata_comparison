using System.Text;
using System.Xml;
using CSharp.ProductExport.Implementation;

namespace CSharp.ProductExport.Tests;

[UsesVerify]
public class XmlExporterTests
{
    [Fact]
    public Task ExportTaxDetails()
    {
        var orders = new List<Order> {SampleModelObjects.RecentOrder, SampleModelObjects.OldOrder};
        var xml = XmlExporter.ExportTaxDetails(orders);

        return VerifyXml(xml);
    }

    private static VerifySettings Settings
    {
        get
        {
            var verifySettings = new VerifySettings();
            verifySettings.UseDirectory("./Data");
            verifySettings.UseExtension("xml");
            return verifySettings;
        }
    }

    private static Task VerifyXml(string xml)
    {
        try
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            return Verify(ToIndentedString(xmlDoc), Settings);
        }
        catch (Exception e)
        {
            return Verify(xml);
        }
    }

    static string ToIndentedString(XmlDocument doc)
    {
        var stringWriter = new StringWriter(new StringBuilder());
        var xmlTextWriter = new XmlTextWriter(stringWriter) {Formatting = Formatting.Indented};
        doc.Save(xmlTextWriter);
        return stringWriter.ToString();
    }
}