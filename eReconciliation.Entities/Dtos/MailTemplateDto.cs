using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Dtos
{
    public class MailTemplateDto : IDto
    {
        public int CompanyId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}