using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pie.Domain.Models
{
    public class UserChat : DomainObject
    {
        [Required]
        public User User { get; set; }
        [Required]
        public Chat Chat { get; set; }
    }
}
