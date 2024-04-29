using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Dtos
{
    public class MovieRequestDto : MovieDTO
    {
        public int GenreId { get; set; }
    }
}