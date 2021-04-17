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

        public IResult Add(UserImage userImage, IFormFile formFile)
        {
            var result = BusinessRules.Run(
                CheckIfUserImageLimitExceeded(userImage.UserId)
            );
            if (result != null)
            {
                return result;
            }
            userImage.ImagePath = formFile != null ? FileHelper.Add(formFile) : @"\Images\userDefault.jpg";
            _userImageDal.Add(userImage);
            return new SuccessResult(Messages.UserImageAdded);
        }
        public IResult Update(UserImage userImage, IFormFile formFile)
        {
            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) +
                          _userImageDal.Get(ui => ui.Id == userImage.UserId).ImagePath;

            userImage.ImagePath = FileHelper.Update(oldPath, formFile);
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
        public IDataResult<List<UserImage>> GetByUserId(int id)
        {
            return new SuccessDataResult<List<UserImage>>(_userImageDal.GetAll(ui => ui.UserId == id),
                Messages.UserImagesListed);
        }
        private IResult CheckIfUserImageLimitExceeded(int id)
        {
            var result = _userImageDal.GetAll(ui => ui.UserId == id).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.UserImageLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
