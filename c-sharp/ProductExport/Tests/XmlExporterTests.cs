using System.Text;
using System.Text.RegularExpressions;
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

        return VerifyXml(xml, DefaultSettings);
    }

    [Fact]
    public Task ExportStore()
    {
        var store = SampleModelObjects.FlagshipStore;
        var xml = XmlExporter.ExportStore(store);
        return VerifyXml(xml, DefaultSettings);
    }

    [Fact]
    public Task ExportHistory()
    {
        var orders = new List<Order> {SampleModelObjects.RecentOrder, SampleModelObjects.OldOrder};
        var xml = XmlExporter.ExportHistory(orders);
        var settings = DefaultSettings;
        settings.AddScrubber(
            input =>
            {
                var regex = "createdAt=\"[^\"]+\"";
                var replacement = "createdAt=\"2018-09-20T00:00Z\"";
                var scrubbed = Regex.Replace(input.ToString(), regex, replacement);
                input.Clear().Append(scrubbed);
            });
        return VerifyXml(xml, settings);
    }

    private static VerifySettings DefaultSettings
    {
        get
        {
            var verifySettings = new VerifySettings();
            verifySettings.UseDirectory("./Data");
            verifySettings.UseExtension("xml");
            return verifySettings;
        }
    }

    private static Task VerifyXml(string xml, VerifySettings settings)
    {
        try
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            return Verify(ToIndentedString(xmlDoc), settings);
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