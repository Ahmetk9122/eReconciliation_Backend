using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Dtos
{
    public class CurrencyAccountDto : IDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? TaxDepartment { get; set; }
        public string? TaxIdNumber { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Email { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsActive { get; set; }
    }
}