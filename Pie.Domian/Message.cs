using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pie.Domain.Models
{
    public class Message : DomainObject
    {
        public Message()
        {
            TimeCreated = DateTime.Now;
        }

        [Required]
        public Chat Chat { get; set; }
        [Required]
        public User Sender { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime TimeCreated { get; set; }
    }
}
