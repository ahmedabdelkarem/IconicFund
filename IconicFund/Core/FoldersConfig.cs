using IconicFund.Helpers;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace IconicFund.Web.Core
{
    public static class FoldersConfig
    {
        public static void RegisterAll(IWebHostEnvironment _env)
        {
            //Admins Uploaded Files
            var folderPath = $"{_env.WebRootPath}{Constants.AdminsUploadDirectory.Replace('/', '\\')}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            //Attachments Uploaded Files
            folderPath = $"{_env.WebRootPath}{Constants.AttachmentsUploadDirectory.Replace('/', '\\')}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

        }

    }
}
