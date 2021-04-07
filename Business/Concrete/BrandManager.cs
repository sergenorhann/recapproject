using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        [ValidationAspect(typeof(BrandValidator))]
        
        public IResult Add(Brand brand)
        {
            var result = BusinessRules.Run(
                CheckIfBrandNameExists(brand.Name));
            if (result != null)
            {
                return result;
            }
            _brandDal.Add(brand);
            return new SuccessDataResult<Brand>(Messages.BrandAdded);
        }
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessDataResult<Brand>(Messages.BrandDeleted);
        }
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.BrandsListed);
        }
        public IResult Update(Brand brand)
        {
            var result = BusinessRules.Run(
                CheckIfBrandNameExists(brand.Name));
            if (result != null)
            {
                return result;
            }
            _brandDal.Update(brand);
            return new SuccessDataResult<Brand>(Messages.BrandUpdated);
        }
        public IDataResult<Brand> GetById(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == id), Messages.BrandListed);
        }

        private IResult CheckIfBrandNameExists(string brandName)
        {
            var result = _brandDal.GetAll(b => b.Name == brandName).Any();
            if (result)
            {
                return new ErrorResult(Messages.BrandNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
