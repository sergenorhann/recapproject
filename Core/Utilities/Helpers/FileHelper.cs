using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helper
{
    public class FileHelper
    {
        public static string Add(IFormFile formfile)
        {
            var (newPath, Path2) = NewPath(formfile);
            var sourcepath = Path.GetTempFileName();
            using (var stream = new FileStream(sourcepath, FileMode.Create))
            {
                formfile.CopyTo(stream);
            }

            File.Move(sourcepath, newPath);

            return Path2;
        }

        public static string Update(string sourcePath, IFormFile formfile)
        {
            var (newPath, Path2) = NewPath(formfile);
            using (var stream = new FileStream(newPath, FileMode.Create))
            {
                formfile.CopyTo(stream);
            }

            File.Delete(sourcePath);
            return Path2;
        }

        public static IResult Delete(string path)
        {
            File.Delete(path);
            return new SuccessResult();
        }

        public static (string newPath, string Path2) NewPath(IFormFile formfile)
        {
            FileInfo ff = new (formfile.FileName);
            string fileExtension = ff.Extension;
            var newFileName = Guid.NewGuid().ToString("N") + fileExtension;
            string path12 = @"\wwwroot\Images\";
            string result = Environment.CurrentDirectory + path12 + newFileName;
            return (result, $"\\Images\\{newFileName}");
        }
    }
}
