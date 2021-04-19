using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;


namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        public static string Add(IFormFile formFile)
        {
            if (formFile==null) return "";
            var (newPath, path2) = NewPath(formFile);
            var sourcePath = Path.GetTempFileName();
            using (var stream = new FileStream(sourcePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            File.Move(sourcePath, newPath);
            return path2.Replace("\\", "/");
        }

        public static string Update(string sourcePath, IFormFile formFile)
        {
            var (newPath, path2) = NewPath(formFile);
            using (var stream = new FileStream(newPath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            if (sourcePath.Split('/').Last() !=  @"userDefault.jpg" && sourcePath.Split('/').Last() != @"carDefault.jpg")
            {
                File.Delete(sourcePath);
            }
            return path2.Replace("\\", "/");
        }

        public static IResult Delete(string path)
        {
            path = path.Replace("/", "\\");
            if (path.Split('\\').Last() != @"userDefault.jpg" && path.Split('\\').Last() != @"carDefault.jpg")
            {
                File.Delete(path);
            }
            return new SuccessResult();
        }

        public static (string newPath, string Path2) NewPath(IFormFile formFile)
        {
            FileInfo ff = new (formFile.FileName);
            var fileExtension = ff.Extension;
            var newFileName = Guid.NewGuid().ToString("N") + fileExtension;
            var result = Environment.CurrentDirectory + @"\wwwroot\Images\" + newFileName;
            return (result, $"\\Images\\{newFileName}");
        }

    }
}
