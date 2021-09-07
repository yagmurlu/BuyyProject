using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori Adı Boş Geçilemez!");
            RuleFor(x => x.CategoryName).MinimumLength(3).WithMessage("Kategori Adı 3 Karaketerden Küçük Olamaz!");
            RuleFor(x => x.CategoryName).MaximumLength(50).WithMessage("Kategori Adı 50 Karakterden Büyük Olamaz!");

           // RuleFor(x => x.CategoryStatus).NotEmpty().WithMessage("Kategori Durumu Boş Geçilemez!");
        }

    }
}
