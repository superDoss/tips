using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tips.Models
{
    public class TipRating
    {
        public int Id { get; set; }
        [Required]
        public int TipId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required, Range(1, 10)]
        public int RateValue { get; set; }

        public User User { get; set; }
        public Tip Tip { get; set; }
    }
}
