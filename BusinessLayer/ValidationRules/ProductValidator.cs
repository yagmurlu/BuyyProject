using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün Adı Boş Geçilemez!");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Ürün Adı 3 Karaketerden Küçük Olamaz!");
            RuleFor(x => x.ProductPrice).NotEmpty().WithMessage("Lütfen bir fiyat belirleyiniz!");

        }
    }
}
