using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.DataAccess;

namespace eReconciliation.Business
{
    public class BaBsReconciliationService : IBaBsReconciliationService
    {
        private readonly IBaBsReconciliationDal _baBsReconciliationDal;

        public BaBsReconciliationService(IBaBsReconciliationDal baBsReconciliationDal)
        {
            _baBsReconciliationDal = baBsReconciliationDal;
        }
    }
}