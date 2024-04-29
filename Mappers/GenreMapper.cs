using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Dtos;
using Movies.Models;

namespace Movies.Mappers
{
    public static class GenreMapper
    {
        public static GenreResponseDTO ToMovieGenreDTO(this Genre genreModel)
        {
            return new GenreResponseDTO
            {
                Title = genreModel.Title,
            };
        }

        public static GenreResponseDTO ToGenreDTO(this Genre genreModel)
        {
            return new GenreResponseDTO
            {
                Title = genreModel.Title,
                Movies = genreModel.Movies.Select(m => m?.ToMovieDTO()).ToList()
            };
        }
    }
}