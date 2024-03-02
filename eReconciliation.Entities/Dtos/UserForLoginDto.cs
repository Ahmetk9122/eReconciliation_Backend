using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Dtos
{
    public class UserForLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}