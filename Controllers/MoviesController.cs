using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Dtos;
using Movies.Helpers;
using Movies.interfaces;
using Movies.Mappers;
using Movies.Models;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController(MovieApplicationContext context, IMovieRepository movieRepository) : ControllerBase
    {
        private readonly MovieApplicationContext _context = context;
        private readonly IMovieRepository _movieRepo = movieRepository;

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies([FromQuery] MovieQueryParam movieQuery)
        {
            return Ok(await _movieRepo.GetAllAsync(movieQuery));
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _movieRepo.GetByIdAsync(id);

            return movie == null ? NotFound() : movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id) return BadRequest();

            if (await _movieRepo.UpdateAsync(id, movie) == null)
                return NotFound();

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieRequestDto requestDto)
        {
            await _movieRepo.CreateAsync(requestDto);

            return CreatedAtAction("GetMovie", new { id = requestDto }, requestDto.ToMovie());
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _movieRepo.GetByIdAsync(id);
            if (movie == null) return NotFound();

            await _movieRepo.DeleteAsync(movie);

            return NoContent();
        }
    }
}
