using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Dtos
{
    public class MailParameterDto : IDto
    {
        public int CompanyId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
    }
}