using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Concrete
{
    public class BaBsReconciliation : IEntity
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
        public string Type { get; set; }
        public int Mounth { get; set; }
        public int Year { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
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