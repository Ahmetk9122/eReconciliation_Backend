using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Concrete
{
    public class BaBsReconciliationDetail : IEntity
    {
        public int Id { get; set; }

        [ForeignKey("BaBsReconciliation")]
        [Required]
        public int BaBsReconciliationId { get; set; }
        public BaBsReconciliation BaBsReconciliation { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}