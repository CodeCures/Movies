using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Mappers;
using Movies.Models;

namespace Movies.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController(MovieApplicationContext context) : ControllerBase
    {
        private readonly MovieApplicationContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _context.Genres
                .Include(g => g.Movies)
                .ToListAsync();

            return Ok(genres.Select(g => g.ToGenreDTO()));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            var genres = await _context.Genres.FindAsync(Id);
            if (genres == null) return NotFound();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] Genre updateGenre)
        {
            var genre = await _context.Genres.FindAsync(Id);
            if (genre == null) return NotFound();

            genre.Title = updateGenre.Title;

            try
            {
                _context.Entry(genre).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(genre);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(Id)) return NotFound();
            }
            return Ok(genre);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var genre = await _context.Genres.FindAsync(Id);
            if (genre == null) return NotFound();

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}