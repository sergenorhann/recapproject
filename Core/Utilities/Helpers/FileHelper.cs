using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;


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
            return path2;
        }

        public static string Update(string sourcePath, IFormFile formFile)
        {
            var (newPath, path2) = NewPath(formFile);
            using (var stream = new FileStream(newPath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            if (sourcePath != @"\Images\userDefault.jpg" && sourcePath != @"\Images\carDefault.jpg")
            {
                File.Delete(sourcePath);
            }
            return path2;
        }

        public static IResult Delete(string path)
        {
            if (path != @"\Images\userDefault.jpg" && path != @"\Images\carDefault.jpg")
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
