using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Concrete
{
    public class MailTemplate : IEntity
    {
        public int Id { get; set; }

        [ForeignKey("Company")]
        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}