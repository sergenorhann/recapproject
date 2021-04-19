using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;


namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
       
        public IResult Add(CarImage carImage)
        {
            carImage.ImagePath =  @"/Images/carDefault.jpg";
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }
        public IResult Delete(CarImage carImage)
        {
            var result = BusinessRules.Run
            (
                FileHelper.Delete
                (
                    Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot"))
                    + _carImageDal.Get(ci => ci.Id == carImage.Id).ImagePath
                )
            );

            if (result != null)
            {
                return result;
            }
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.CarImagesListed);
        }
        public IDataResult<List<CarImage>> GetByCarId(int id)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(ci => ci.CarId == id),
                Messages.CarImageListed);
        }
        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(ci => ci.Id == id), Messages.CarImageListed);
        }
        public IResult Update(CarImage carImage, IFormFile formFile)
        {
            var result = _carImageDal.Get(ci => ci.CarId == carImage.CarId);
            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + result.ImagePath;
            carImage.Id = result.Id;
            carImage.ImagePath = result.ImagePath == @"\Images\carDefault.jpg" ? FileHelper.Add(formFile) : FileHelper.Update(oldPath, formFile);
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }
    }
}
