using FluentValidation;
using InnoGotchi.WEB.Utility;

namespace InnoGotchi.WEB.Validation
{
    public class SessionFarmValidator : AbstractValidator<SessionFarm>
    {
        public SessionFarmValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
