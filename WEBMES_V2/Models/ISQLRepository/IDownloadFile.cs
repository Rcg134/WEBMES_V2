using WEBMES_V2.Models.StaticModels.Generic;

namespace WEBMES_V2.Models.ISQLRepository
{
    public interface IDownloadFile
    {
        public Task<byte[]>  Get_Template_For_Magazine(string path);

    }
}
