﻿using System;

namespace Vidly2.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        public byte NumberInStock { get; set; }

        public byte GenreId { get; set; }

        public GenreDTO Genre { get; set; }
    }
}