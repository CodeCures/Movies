using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Dtos;
using Movies.Helpers;
using Movies.Models;

namespace Movies.interfaces
{
    public interface IMovieRepository
    {
        public Task<PaginationResult<MovieResponseDto>> GetAllAsync(MovieQueryParam movieQuery);
        public Task<Movie?> GetByIdAsync(int id);
        public Task<Movie?> UpdateAsync(int id, Movie movie);
        public Task CreateAsync(MovieRequestDto requestDto);
        public Task DeleteAsync(Movie movie);
    }
}