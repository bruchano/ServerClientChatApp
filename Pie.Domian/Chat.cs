using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pie.Domain.Models
{
    public enum ChatType
    {
        Private,
        Group,
        Public
    }
    public class Chat : DomainObject
    {
        public Chat()
        {
            TimeCreated = DateTime.Now;
        }

        [Required]
        public ChatType Type { get; set; }
        [Required]
        public DateTime TimeCreated { get; set; }
        public string? ChatName { get; set; }
        [Required]
        public virtual ICollection<UserChat> Members { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
