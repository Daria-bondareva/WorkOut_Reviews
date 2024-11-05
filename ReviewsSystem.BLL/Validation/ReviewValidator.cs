using FluentValidation;
using ReviewsSystem.BLL.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsSystem.BLL.Validation
{
    public class ReviewValidator : AbstractValidator<ReviewRequest>
    {
        public ReviewValidator()
        {
            RuleFor(r => r.Comment)
                .NotEmpty().WithMessage("Comment cannot be empty.")
                .Length(1, 500).WithMessage("Comment must be between 1 and 500 characters.");

            RuleFor(r => r.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(r => r.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");

            RuleFor(r => r.WorkOutId)
                .GreaterThan(0).WithMessage("WorkOutId must be greater than 0.");
        }
    }
}
