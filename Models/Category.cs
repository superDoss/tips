using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tips.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(30, MinimumLength =2)]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<TipCategory> TipCategories { get; set; }
    }
}