using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movies.Dtos
{
    public class MovieResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ReleasedOn { get; set; } = string.Empty;
        public List<ReviewResponseDTO> Reviews { get; set; } = [];
        public GenreResponseDTO? Genre { get; set; }
    }
}