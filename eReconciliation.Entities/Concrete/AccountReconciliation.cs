using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Concrete
{
    public class AccountReconciliation : IEntity
    {
        public int Id { get; set; }

        [ForeignKey("Company")]
        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [ForeignKey("CurrencyAccount")]
        [Required]
        public int CurrencyAccountId { get; set; }
        public CurrencyAccount CurrencyAccount { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }

        [ForeignKey("Currency")]
        [Required]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public decimal CurrencyDebit { get; set; }
        public decimal CurrencyCredit { get; set; }
        public bool IsSendEmail { get; set; }
        public DateTime? SendEmailDate { get; set; }
        public bool? IsEmailRead { get; set; }
        public DateTime? EmailReadDate { get; set; }
        public bool? IsResultSucceed { get; set; }
        public DateTime? ResultDate { get; set; }
        public string? ResultNote { get; set; }
        public string? Guid { get; set; }
    }
}