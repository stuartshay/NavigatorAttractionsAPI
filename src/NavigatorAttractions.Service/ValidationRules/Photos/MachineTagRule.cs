using FluentValidation;
using NavigatorAttractions.Data.Entities.Attractions;

namespace NavigatorAttractions.Service.ValidationRules.Photos
{
    public class MachineTagRule : AbstractValidator<MachineTag>
    {
        public MachineTagRule()
        {
            RuleFor(m => m.Tag).NotNull().WithMessage("MachineTag.Tag Must be defied");
        }
    }
}
