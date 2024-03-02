using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities.Concrete;

namespace eReconciliation.Entities.Dtos
{
    public class UserCompanyDto : User
    {
        public int CompanyId { get; set; }
    }
}