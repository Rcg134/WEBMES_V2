
using WEBMES_V2.Models.ISQLRepository;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;

namespace WEBMES_V2.Models.SQLRepositoryImplementation
{
    public class DownloadFiles : IDownloadFile
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DownloadFiles(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        public Task<byte[]> Get_Template_For_Magazine(string path)
        {

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "DownloadableFiles", path);

            if (System.IO.File.Exists(filePath))
            {
                byte[] fileContent = File.ReadAllBytes(filePath);
                return Task.FromResult(fileContent);
            }
            else
            {
                return Task.FromResult<byte[]>(null);
            }
        }
    }
}
