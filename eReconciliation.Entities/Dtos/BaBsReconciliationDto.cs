using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Dtos
{
    public class BaBsReconciliationDto : IDto
    {
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int CurrencyAccountId { get; set; }
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
    }
}