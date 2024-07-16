using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eReconciliation.Entities.Dtos
{
    public class CurrencyAccountExcelDto
    {
        public IFormFile File { get; set; }
        public int CompanyId { get; set; }
    }
}