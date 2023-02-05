using FluentValidation;
using InnoGotchi.BusinessLogic.SessionEntities;

namespace InnoGotchi.BusinessLogic.Validation
{
    public class SessionFarmValidator : AbstractValidator<SessionFarm>
    {
        public SessionFarmValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
