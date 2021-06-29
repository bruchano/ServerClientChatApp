using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pie.Domain.Models
{
    public class Friendship : DomainObject
    {
        public Friendship()
        {
            TimeCreated = DateTime.Now;
        }

        [Required]
        public virtual User Inviter { get; set; }
        [Required]
        public virtual User Accepter { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
