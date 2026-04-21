using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Presentation.Domain
{
    public class ArtistDto
    {
        public int ArtistId { get; set; }
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        public decimal? PricePerShow { get; set; }
        public string? Bio { get; set; }
        public string? VerificationStatus { get; set; }
        public decimal? Rating { get; set; }
        public int? TotalShows { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public string? Status { get; set; }
        public string? FileUrl { get; set; }
        public string? Title { get; set; }

        public int TotalCount { get; set; }
    }

    public class RatingDto
    {
        public int Id { get; set; }

        public int ArtistId { get; set; }

        public int UserId { get; set; }

        public decimal RatingValue { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
    public class UserDto
    {
        public string? Token { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? PasswordHash { get; set; }
        public int? Role { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? City { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

    public class ArtistSampltDto
    {
        public int Id { get; set; }

        public int ArtistId { get; set; }

        public string Title { get; set; } = null!;

        public string Type { get; set; } = null!;

        public IFormFile File { get; set; } = null!;
        public string? FileUrl { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
    public class ArtistMediaDto
    {
        public int ArtistMediaId { get; set; }

        public int ArtistId { get; set; }

        public string? MediaCategory { get; set; }

        public string? FileUrl { get; set; }

        public string? Title { get; set; }

        public int? DisplayOrder { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool? IsDeleted { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public IFormFile? BannerImage { get; set; }
        public List<IFormFile>? ConcertImages { get; set; }
        public string? YouTubelink { get; set; }
    }
}


