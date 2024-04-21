using System.Xml.Linq;

namespace WEBMES_V2.Models.ISQLRepository
{
    public interface IXMLConverter
    {
        public XDocument ConvertToXml(List<List<string>> data);

        public List<List<string>> ExcelFileToList(IFormFile file);
    }
}
