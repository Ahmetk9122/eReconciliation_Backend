using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Concrete
{
    public class ForgotPassword: IEntity
    {
        public int Id { get; set; }
        // public int UserId { get; set; }
        public string Value { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsActive { get; set; }
    }
}