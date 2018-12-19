using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tips.Models
{
    public class UserRating
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RatedUserId { get; set; }
        [Required, Range(1,10)]
        public int Reputation { get; set; }

        public User User { get; set; }
        public User RatedUser { get; set; }
    }
}
