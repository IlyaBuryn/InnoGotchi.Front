using FluentValidation;
using InnoGotchi.WEB.Utility;

namespace InnoGotchi.WEB.Validation
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
