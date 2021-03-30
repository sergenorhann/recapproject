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

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _CustomerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _CustomerDal = customerDal;
        }
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            _CustomerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }
        public IResult Delete(Customer customer)
        {
            _CustomerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);
        }
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_CustomerDal.GetAll(), Messages.CustomersListed);
        }
        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_CustomerDal.Get(c => c.Id == id), Messages.CustomerListed);
        }
        public IResult Update(Customer customer)
        {
            _CustomerDal.Update(customer);
            return new SuccessResult(Messages.CustomerUpdated);
        }
    }
}
