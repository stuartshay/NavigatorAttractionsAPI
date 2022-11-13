using FluentValidation;
using NavigatorAttractions.Service.Models.Photos;
using NavigatorAttractions.Service.ValidationRules.Photos;

namespace NavigatorAttractions.Service.ValidationRules
{
    public class PhotoRule : AbstractValidator<PhotoModel>
    {
        public PhotoRule()
        {
            RuleFor(p => p.PhotoId).GreaterThan(0).WithMessage("PhotoId must be greater then 0");
            RuleFor(p => p.LastUpdated).NotEmpty();

            RuleFor(p => p.Author).SetValidator(new AuthorRule());
            RuleFor(p => p.MachineTags).NotNull().WithMessage("MachineTags Must be defined");
            RuleFor(p => p.PhotoSizes).NotNull().WithMessage("PhotoSizes Must be defined");

            //RuleFor(p => p.MachineTags).SetCollectionValidator(new MachineTagRule());
            //RuleFor(p => p.PhotoSizes).SetCollectionValidator(new PhotoSizeRule());

            RuleFor(m => m.DateTaken).NotEmpty();
        }
    }
}
