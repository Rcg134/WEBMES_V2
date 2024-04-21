using OfficeOpenXml;
using System.Xml.Linq;
using WEBMES_V2.Models.ISQLRepository;

namespace WEBMES_V2.Models.SQLRepositoryImplementation
{
    public class XMLConverter : IXMLConverter
    {
        public XDocument ConvertToXml(List<List<string>> data)
        {
            XElement root = new XElement("NewDataSet");

            foreach (List<string> rowData in data)
            {
                XElement rowElement = new XElement("XMLLogs");
                for (int i = 0; i < rowData.Count; i++)
                {
                    rowElement.Add(new XElement("Column" + (i + 1), rowData[i]));
                }
                root.Add(rowElement);
            }

            return new XDocument(root);
        }

        public List<List<string>> ExcelFileToList(IFormFile file)
        {
            List<List<string>> data = new List<List<string>>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (MemoryStream stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        List<string> rowData = new List<string>();
                        for (int col = 1; col <= colCount; col++)
                        {
                            var cellValue = worksheet.Cells[row, col].Value?.ToString() ?? "";
                            rowData.Add(cellValue);
                        }
                        data.Add(rowData);
                    }
                }
            }

            return data;
        }
    }
}
