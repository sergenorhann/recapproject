using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IUserImageService
    {
        IResult Add(UserImage userImage, IFormFile formFile);
        IResult Update(UserImage userImage, IFormFile formFile);
        IResult Delete(UserImage userImage);
        IDataResult<List<UserImage>> GetAll();
        IDataResult<UserImage> GetById(int id);
        IDataResult<List<UserImage>> GetByUserId(int id);
    }
}
