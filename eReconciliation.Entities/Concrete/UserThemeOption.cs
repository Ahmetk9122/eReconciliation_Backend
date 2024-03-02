using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;
using eReconciliation.Core.Entities.Concrete;

namespace eReconciliation.Entities.Concrete
{
    public class UserThemeOption : IEntity
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public string SidenavColor { get; set; }
        public string SidenavType { get; set; }
        public string Mode { get; set; }
    }
}