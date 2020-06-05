using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TEST.Models;

namespace TEST.ViewModels
{
    public class MovieFormViewModel
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public byte? GenresId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        public DateTime? AddedDate { get; set; }

        [Display(Name = "Number in stock")]
        [Range(1, 20)]
        [Required]
        public int? StockId { get; set; }

        public IEnumerable<Genres> Genres { get; set; }

        public string Title => Id != 0 ? "Edit Movie" : "New Move";

        public MovieFormViewModel()
        {
            Id = 0;
        }
    }
}