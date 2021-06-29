using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pie.Domain.Models
{
    public class FriendRequest : DomainObject
    {
        public FriendRequest()
        {
            TimeCreated = DateTime.Now;
        }

        [Required]
        public User Sender { get; set; }
        [Required]
        public User Receiver { get; set; }
        [Required]
        public DateTime TimeCreated { get; set; }
    }
}
