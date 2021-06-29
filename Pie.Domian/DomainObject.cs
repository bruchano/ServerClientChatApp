using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pie.Domain.Models
{
    public class DomainObject
    {
        [Key]
        public int ID { get; set; }
    }
}
