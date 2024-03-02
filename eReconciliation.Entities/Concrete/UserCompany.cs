using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;
using eReconciliation.Core.Entities.Concrete;

namespace eReconciliation.Entities.Concrete
{
    public class UserCompany : IEntity
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Company")]
        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsActive { get; set; }

    }
}