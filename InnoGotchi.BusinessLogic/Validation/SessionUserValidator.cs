using FluentValidation;
using InnoGotchi.BusinessLogic.SessionEntities;

namespace InnoGotchi.BusinessLogic.Validation
{
    public class SessionUserValidator : AbstractValidator<SessionUser>
    {
        public SessionUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Role).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
