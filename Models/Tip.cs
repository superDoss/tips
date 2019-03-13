using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tips.api;
namespace Tips.Models
{
    public class Tip
    {
        public Tip()
        {
            CreateDate = DateTime.Now;
        }
        public Tip(TipCreateReq req)
        {
            
            Title = req.Title;
            Content = req.Content;
            //UserId
            CreateDate = DateTime.Now;
            ImagePath = req.ImagePath;
            VideoPath = req.VideoPath;
            Location = req.Location;
        }
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