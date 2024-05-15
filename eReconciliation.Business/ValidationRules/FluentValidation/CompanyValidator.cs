using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Entities.Concrete;
using FluentValidation;

namespace eReconciliation.Business.ValidationRules.FluentValidation
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Şirket adı boş olamaz");
            RuleFor(x => x.Name).MinimumLength(4).WithMessage("Şirket adı en az 4 karakter olmalıdır.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Şirket adresi boş olamaz");
        }

    }
}