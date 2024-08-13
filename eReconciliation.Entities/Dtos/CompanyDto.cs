using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;
using eReconciliation.Entities.Concrete;

namespace eReconciliation
{
    public class CompanyDto : IDto
    {
        public Company Company { get; set; }
        public int UserId { get; set; }
    }
}