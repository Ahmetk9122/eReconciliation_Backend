using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Concrete
{
    public class AccountReconciliationDetail : IEntity
    {
        public int Id { get; set; }

        [ForeignKey("AccountReconciliation")]
        [Required]
        public int AccountReconciliationId { get; set; }
        public AccountReconciliation AccountReconciliation { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        [ForeignKey("Currency")]
        [Required]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public decimal CurrencyDebit { get; set; }
        public decimal CurrencyCredit { get; set; }
    }
}