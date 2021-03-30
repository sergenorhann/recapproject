using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _RentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _RentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(CheckIfRentalReturnDateNull(rental.CarId));
            if(result != null)
            {
                return result;
            }
            _RentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }
        public IResult Delete(Rental rental)
        {
            _RentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_RentalDal.GetAll(), Messages.RentalsListed);
        }
        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_RentalDal.Get(r => r.Id == id), Messages.RentalListed);
        }
        public IResult Update(Rental rental)
        {
            _RentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }
        public IDataResult<List<RentalDetailDto>> GetRentalDetail()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_RentalDal.GetRentalDetail(), Messages.RentalsListed);
        }

        private IResult CheckIfRentalReturnDateNull(int carId)
        {
            /*
             * Kiralanacak aracın ReturnDate'i Null olursa araç kirada (ödevlerden biri buydu) demektir. Bundan dolayı araç kiralanamaz.
               ReturnDate bilgisini göndermeyince tarihi 01.01.0001 olarak kayıt ediyor, Kontrolü şimdilik Null üzerinden yapamıyorum.
               Bu yüzden kontrolü Null değil, 01.01.0001 üzerinden yaptım.
            */
            var result = _RentalDal.GetAll(r => r.CarId == carId).LastOrDefault().ReturnDate;
            if (result == new DateTime(0001, 01, 01))
            {
                return new ErrorResult(Messages.CarRented);
            }
            return new SuccessResult();
        }
    }
}
