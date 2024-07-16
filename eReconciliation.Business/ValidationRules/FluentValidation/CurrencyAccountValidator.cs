using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Entities.Concrete;
using FluentValidation;

namespace eReconciliation.Business.ValidationRules.FluentValidation
{
    public class CurrencyAccountValidator : AbstractValidator<CurrencyAccount>
    {
        public CurrencyAccountValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty().WithMessage("Şirket boş olamaz");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Firma Adı boş olamaz");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Firma Adresi boş olamaz");
            RuleFor(x => x.Name).MinimumLength(4).WithMessage("Firma Adı adı en az 4 karakter olmalıdır.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Şirket adresi boş olamaz");
        }
    }
}