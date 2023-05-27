using CrmAssistant.Models;
using FluentValidation;

namespace CrmAssistant.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor( x  => x.Name ).NotNull().NotEmpty().WithMessage("The field name is required");
            RuleFor( x  => x.Lastname ).NotNull().NotEmpty().WithMessage("The field lastname is required");
            RuleFor( x  => x.Email ).NotNull().NotEmpty().EmailAddress().WithMessage("The field lastname is required");
            RuleFor( x  => x.Password ).NotNull().NotEmpty().WithMessage("The field lastname is required");
        }
    }
}
