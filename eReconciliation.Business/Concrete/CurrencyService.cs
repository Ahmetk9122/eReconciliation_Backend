using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.DataAccess;

namespace eReconciliation.Business.Concrete
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyDal _currencyDal;

        public CurrencyService(ICurrencyDal currencyDal)
        {
            _currencyDal = currencyDal;
        }

    }
}