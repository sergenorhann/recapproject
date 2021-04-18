using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using System.Collections.Generic;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserImageService _userImageService;
        
        public UserManager(IUserDal userDal, IUserImageService userImageService)
        {
            _userDal = userDal;
            _userImageService = userImageService;
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            _userImageService.Add(new UserImage {UserId = user.Id});
            return new SuccessResult(Messages.UserAdded);
        }
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            _userImageService.Delete(_userImageService.GetByUserId(user.Id).Data);
            return new SuccessResult(Messages.UserDeleted);
        }
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
        }
        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=>u.Id==id), Messages.UserListed);
        }
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<User> GetByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }
    }
}
