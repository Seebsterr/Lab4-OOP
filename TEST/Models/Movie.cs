using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Genres Genres { get; set; }

        [Required]
        public byte GenresId { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        
        public DateTime? AddedDate { get; set; }

        [Display(Name="Number in stock")]
        [Range(1,20)]
        public int StockId { get; set; }
    }
}