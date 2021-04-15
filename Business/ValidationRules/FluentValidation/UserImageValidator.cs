using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    class UserImageValidator : AbstractValidator<UserImage>
    {
        public UserImageValidator()
        {

        }
    }
}
