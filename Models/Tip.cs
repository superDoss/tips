using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tips.Models
{
    public class Tip
    {
        public int Id { get; set; }
        [Required, StringLength(200, MinimumLength =2)]
        public string Title { get; set; }
        [Required]
        public string Content{ get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImagePath { get; set; }
        public string VideoPath { get; set; }
        public string Location { get; set; }
        public User User { get; set; }
        public ICollection<TipCategory> TipCategories { get; set; }
        public ICollection<TipRating> TipRatings { get; set; }

    }
}