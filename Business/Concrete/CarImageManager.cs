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
        /*
            Update bölümünde carid değiştirince hata veriyor. 
         */
        private readonly ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(CarImage carImage, IFormFile formFile)
        {
            var result = BusinessRules.Run(CheckIfCarImageLimitExceeded(carImage.CarId));
            
            if (result != null)
            {
                return result;
            }

            carImage.ImagePath = FileHelper.Add(formFile);

            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        public IResult Delete(CarImage carImage)
        {

            var oldPath =_carImageDal.Get(p => p.Id == carImage.Id).ImagePath.ToString();
            var result = BusinessRules.Run(FileHelper.Delete(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot"))+oldPath));
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
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(cı => cı.CarId == id),
                Messages.CarImageListed);
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(cı => cı.Id == id), Messages.CarImageListed);
        }

        public IResult Update(CarImage carImage, IFormFile formFile)
        {
            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) +
                          _carImageDal.Get(p => p.Id == carImage.CarId).ImagePath;

            carImage.ImagePath = FileHelper.Update(oldPath, formFile);
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        private IResult CheckIfCarImageLimitExceeded(int id)
        {
            var result = _carImageDal.GetAll(cı => cı.CarId == id).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCarImageNull(string imagePath)
        {
            if (imagePath == "")
            {
                const string path = @"\uploads\default.jpg";
            }

            return new SuccessResult();

            //List<CarImage> carImage = new List<CarImage>();
            //    carImage.Add(new CarImage {CarId = id, ImagePath = path, Date = DateTime.Now});
            //    return new SuccessDataResult<List<CarImage>>(carImage);
        }
    }
}
