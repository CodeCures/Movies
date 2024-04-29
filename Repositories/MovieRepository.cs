using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Dtos;
using Movies.Helpers;
using Movies.interfaces;
using Movies.Mappers;
using Movies.Models;

namespace Movies.Repositories
{
    public class MovieRepository(MovieApplicationContext context) : IMovieRepository
    {
        private readonly MovieApplicationContext _context = context;

        public async Task CreateAsync(MovieRequestDto requestDto)
        {
            _context.Movies.Add(requestDto.ToMovie());
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginationResult<MovieResponseDto>> GetAllAsync(MovieQueryParam movieQuery)
        {
            var query = _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Reviews)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(movieQuery.Title))
            {
                query = query.Where(m => m.Title.Contains(movieQuery.Title));
            }

            if (movieQuery.ReleasedOn.HasValue)
            {
                query = query.Where(m => m.ReleasedOn.Date == movieQuery.ReleasedOn.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(movieQuery.SortBy))
            {
                if (movieQuery.SortBy == "Title")
                {
                    query = movieQuery.IsDescending
                        ? query.OrderByDescending(m => m.Title)
                        : query.OrderBy(m => m.Title);
                }
            }

            var totalCount = await query.CountAsync();

            query = query.Skip((movieQuery.PageNumber - 1) * movieQuery.PageSize)
                 .Take(movieQuery.PageSize);

            var result = await query
                .Select(m => m.ToMovieDTO())
                .ToListAsync();

            return new PaginationResult<MovieResponseDto>
            {
                Data = result,
                PageNumber = movieQuery.PageNumber,
                PageSize = movieQuery.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / movieQuery.PageSize)
            };
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            return movie ?? null;
        }

        public async Task<Movie?> UpdateAsync(int id, Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return movie;
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

    }

}