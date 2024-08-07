using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Dtos
{
    public class AccountReconciliationDetailDto : IDto
    {
        public int Id { get; set; }
        public int AccountReconciliationId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int CurrencyId { get; set; }
        public decimal CurrencyDebit { get; set; }
        public decimal CurrencyCredit { get; set; }
    }
}