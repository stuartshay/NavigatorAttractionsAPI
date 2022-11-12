using FluentValidation;
using NavigatorAttractions.Service.Models.Photos;

namespace NavigatorAttractions.Service.ValidationRules.Photos
{
    public class AuthorRule : AbstractValidator<AuthorModel>
    {
        public AuthorRule()
        {
            RuleFor(m => m.Id).NotEmpty().WithMessage("Author.Id must be defined");
            RuleFor(m => m.Name).NotEmpty().WithMessage("Author.Name must be defined");
        }
    }
}
