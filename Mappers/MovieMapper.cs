using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Dtos;
using Movies.Models;

namespace Movies.Mappers
{
    public static class MovieMapper
    {
        public static Movie ToMovie(this MovieRequestDto movieRequestDto)
        {
            return new Movie
            {
                Title = movieRequestDto.Title,
                Description = movieRequestDto.Description,
                GenreId = movieRequestDto.GenreId,
                ReleasedOn = movieRequestDto.ReleasedOn
            };
        }

        public static MovieResponseDto ToMovieDTO(this Movie movieModel)
        {
            return new MovieResponseDto
            {
                Id = movieModel.Id,
                Title = movieModel.Title,
                Description = movieModel.Description,
                Genre = movieModel.Genre?.ToMovieGenreDTO(),
                Reviews = movieModel.Reviews.Select(r => r.ToReviewDTO()).ToList(),
                ReleasedOn = movieModel.ReleasedOn.ToString("MMM dd, yyyy"),
            };
        }
    }
}