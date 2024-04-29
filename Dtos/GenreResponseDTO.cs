using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movies.Dtos
{
    public class GenreResponseDTO
    {
        public string Title { get; set; } = string.Empty;
        [JsonIgnore]
        public List<MovieResponseDto?> Movies { get; set; } = [];
    }
}