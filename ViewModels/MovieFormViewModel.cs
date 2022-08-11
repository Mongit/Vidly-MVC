using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly2.Models;

namespace Vidly2.ViewModels
{
	public class MovieFormViewModel
	{
		public IEnumerable<Genre> Genres { get; set; }

		public int? Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Release Date")]
		public DateTime? ReleaseDate { get; set; }

		[Required]
		[Display(Name = "Number in Stock")]
		[Range(1, 20)]
		public byte? NumberInStock { get; set; }

		[Required]
		[Display(Name = "Genre")]
		public byte? GenreId { get; set; }

		public string Title 
		{ 
			get
			{
				return Id != 0 ? "Edit Movie" : "New Movie";
			}
		}

        public MovieFormViewModel()
        {
			Id = 0; //Make sure that inf MovieForm.cshtml, hidden field movie.ID is not empty and get required error validation
        }

        public MovieFormViewModel(Movie movie)
        {
			Id = movie.Id;
			Name = movie.Name;
			ReleaseDate = movie.ReleaseDate;
			NumberInStock = movie.NumberInStock;
			GenreId = movie.GenreId;
        }
	}
}