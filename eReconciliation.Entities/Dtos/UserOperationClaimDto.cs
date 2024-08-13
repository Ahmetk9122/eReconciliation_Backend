using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Dtos
{
    public class UserOperationClaimDto : IDto
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int OperationClaimId { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsActive { get; set; }
    }
}