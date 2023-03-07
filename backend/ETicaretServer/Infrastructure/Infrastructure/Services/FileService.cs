using Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _environment;

        public FileService(IHostingEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile formFile)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await fileStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch(Exception ex)
            {
                //loglama için yapılacak
                throw ex;
            }
        }

        public Task<string> FileRenameAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadpath = Path.Combine(_environment.WebRootPath, path);
            if(!Directory.Exists(uploadpath))
                Directory.CreateDirectory(uploadpath);
            List<(string fileName,string path)> datas= new();
            List<bool> results = new();
            foreach(IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(file.FileName);
                bool result =  await CopyFileAsync($"{uploadpath}\\{fileNewName}",file);
                datas.Add((fileNewName, $"{uploadpath}\\{fileNewName}"));
                results.Add(result);
            }
            if (results.TrueForAll(r => r.Equals(true)))
                return datas;

            return null;
        }
    }
}
