using FluentValidation;

namespace Application.Features.UserApps.Commands.Update;

public class UpdateUserAppCommandValidator : AbstractValidator<UpdateUserAppCommand>
{
    public UpdateUserAppCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.PasswordHash).NotEmpty();
        RuleFor(c => c.CreatedAt).NotEmpty();
        RuleFor(c => c.UpdatedAt).NotEmpty();
        RuleFor(c => c.Profile).NotEmpty();
    }
}