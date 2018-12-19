using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tips.Models
{
    public class TipCategory
    {
        public int Id { get; set; }
        [Required]
        public int TipId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public Tip Tip { get; set; }
        public Category Category { get; set; }
    }
}
