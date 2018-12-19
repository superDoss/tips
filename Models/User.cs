using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tips.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, Display(Name = "First Name")]        
        [StringLength(30, ErrorMessage = "First name cannot be longer than 30 characters.")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "Last name cannot be longer than 30 characters.")]
        public string LastName { get; set; }
        [Display(Name = "Full Name")]
        public string fullName { get { return FirstName + " " + LastName; } }       
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        // There is no boolean type in SQLite
        [Range(0, 1)]
        public int Admin { get; set; }

        public ICollection<Tip> Tips { get; set; }
        public ICollection<TipRating> TipRatings { get; set; }
        public ICollection<UserRating> UserRatings { get; set; }
    }
}
