using System;
using System.Collections.Generic;
using System.IO;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class UserImageManager : IUserImageService
    {
        private readonly IUserImageDal _userImageDal;
        public UserImageManager(IUserImageDal userImageDal)
        {
            _userImageDal = userImageDal;
        }
        
        public IResult Add(UserImage userImage)
        {
            userImage.ImagePath = @"\Images\userDefault.jpg";
            _userImageDal.Add(userImage);
            return new SuccessResult(Messages.UserImageAdded);
        }
        public IResult Update(UserImage userImage, IFormFile formFile)
        {
            var result = _userImageDal.Get(ui => ui.UserId == userImage.UserId);
            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + result.ImagePath;
            userImage.Id = result.Id;
            userImage.ImagePath = result.ImagePath == @"\Images\userDefault.jpg" ? FileHelper.Add(formFile) : FileHelper.Update(oldPath, formFile);
            _userImageDal.Update(userImage);
            return new SuccessResult(Messages.UserImageUpdated);
        }
        public IResult Delete(UserImage userImage)
        {
            var result = BusinessRules.Run
            (
                FileHelper.Delete
                (
                    Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot"))
                    + _userImageDal.Get(ui => ui.Id == userImage.Id).ImagePath
                )
            );

            if (result != null)
            {
                return result;
            }
            _userImageDal.Delete(userImage);
            return new SuccessResult(Messages.UserImageDeleted);
        }
        public IDataResult<List<UserImage>> GetAll()
        {
            return new SuccessDataResult<List<UserImage>>(_userImageDal.GetAll(), Messages.UserImagesListed);
        }
        public IDataResult<UserImage> GetById(int id)
        {
            return new SuccessDataResult<UserImage>(_userImageDal.Get(ui => ui.Id == id), Messages.UserImageListed);
        }
        public IDataResult<UserImage> GetByUserId(int id)
        {
            return new SuccessDataResult<UserImage>(_userImageDal.Get(ui => ui.UserId == id),
                Messages.UserImagesListed);
        }
    }
}
