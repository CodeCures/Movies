using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int MovieId { get; set; }
        [JsonIgnore]
        public Movie? Movie { get; set; }
    }
}