using FluentValidation;
using ReviewsSystem.BLL.DTO.Requests;


namespace ReviewsSystem.BLL.Validation
{

    public class UserValidator : AbstractValidator<UserRequest>
    {
        public UserValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("Username cannot be empty.")
                .Length(3, 20).WithMessage("Username must be between 3 and 20 characters.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
