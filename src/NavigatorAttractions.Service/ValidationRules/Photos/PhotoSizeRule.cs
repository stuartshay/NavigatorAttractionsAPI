using FluentValidation;
using NavigatorAttractions.Data.Entities.Photos;

namespace NavigatorAttractions.Service.ValidationRules.Photos
{
    public class PhotoSizeRule : AbstractValidator<PhotoSize>
    {
        public PhotoSizeRule()
        {
            RuleFor(s => s.Suffix).NotEmpty();
            RuleFor(s => s.Label).NotEmpty();
            RuleFor(s => s.Url).NotEmpty();
            RuleFor(s => s.Height).NotEmpty().NotEqual(0);
            RuleFor(s => s.Width).NotEmpty().NotEqual(0);
        }
    }
}
