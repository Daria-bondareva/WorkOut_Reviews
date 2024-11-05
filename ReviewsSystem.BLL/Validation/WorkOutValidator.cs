using FluentValidation;
using ReviewsSystem.BLL.DTO.Requests;

public class WorkOutValidator : AbstractValidator<WorkOutRequest>
{
    public WorkOutValidator()
    {
        RuleFor(w => w.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");

        RuleFor(w => w.Description)
            .NotEmpty().WithMessage("Description cannot be empty.")
            .Length(1, 1000).WithMessage("Description must be between 1 and 1000 characters.");

        RuleFor(w => w.TypeId)
            .GreaterThan(0).WithMessage("TypeId must be greater than 0.");

        RuleFor(w => w.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be greater than 0.");

        RuleFor(w => w.TrainingDuration)
            .NotEmpty().WithMessage("TrainingDuration cannot be empty.");

        RuleFor(w => w.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}