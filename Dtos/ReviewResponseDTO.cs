using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movies.Dtos
{
    public class ReviewResponseDTO
    {
        public decimal Rating { get; set; }
        public string Comment { get; set; } = string.Empty;

        [JsonIgnore]
        public MovieResponseDto? Movie { get; set; }
    }
}