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
    public class UserManager : IUserService
    {
        IUserDal _UserDal;
        public UserManager(IUserDal userDal)
        {
            _UserDal = userDal;
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _UserDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }
        public IResult Delete(User user)
        {
            _UserDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_UserDal.GetAll(), Messages.UsersListed);
        }
        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_UserDal.Get(u=>u.Id==id), Messages.UserListed);
        }
        public IResult Update(User user)
        {
            _UserDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}
