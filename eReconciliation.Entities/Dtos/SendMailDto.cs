using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Entities.Dtos
{
    public class SendMailDto : IDto
    {
        public MailParameter MailParameter { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}