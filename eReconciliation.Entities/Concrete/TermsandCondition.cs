using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Concrete
{
    public class TermsandCondition: IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }

    }
}