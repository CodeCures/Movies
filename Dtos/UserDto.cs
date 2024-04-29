using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Dtos
{
    public class UserDto
    {
        public required string UserName { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public required string Token { get; set; }
    }
}