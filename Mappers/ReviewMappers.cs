using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Dtos;
using Movies.Models;

namespace Movies.Mappers
{
    public static class ReviewMappers
    {
        public static ReviewResponseDTO ToReviewDTO(this Review reviewModel)
        {
            return new ReviewResponseDTO
            {
                Rating = reviewModel.Rating,
                Comment = reviewModel.Comment,
            };
        }
    }
}