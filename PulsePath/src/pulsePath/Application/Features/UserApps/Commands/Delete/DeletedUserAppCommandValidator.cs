using FluentValidation;

namespace Application.Features.UserApps.Commands.Delete;

public class DeleteUserAppCommandValidator : AbstractValidator<DeleteUserAppCommand>
{
    public DeleteUserAppCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}