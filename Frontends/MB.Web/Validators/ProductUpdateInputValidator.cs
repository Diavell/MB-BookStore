//using FluentValidation;
//using MB.Web.Models.Catalog;

//namespace MB.Web.Validators
//{
//    public class ProductUpdateInputValidator : AbstractValidator<ProductUpdateInput>
//    {
//        public ProductUpdateInputValidator()
//        {
//            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
//            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required");
//            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
//            RuleFor(x => x.Description).NotEmpty().WithMessage("Stock is required");
//            RuleFor(x => x.PhotoFormFile).NotEmpty().WithMessage("Picture is required");
//        }
//    }
//}
