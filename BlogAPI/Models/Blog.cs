using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class Blog
    {
        [Key]
        public int id { get; set; }
        public string? title { get; set; }
        public string? content { get; set; }
        public string? summary { get; set; }
        public string? author { get; set; }
        public string? imageUrl { get; set; }
        public DateTime? publishDate { get; set; }
        public DateTime? updatedDate { get; set; }
    }
}
