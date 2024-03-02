using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Concrete
{
    public class UserReletionship: IEntity
    {
        public int Id { get; set; }
        public int AdminUserId { get; set; }
        public int UserUserId { get; set; }
    }
}