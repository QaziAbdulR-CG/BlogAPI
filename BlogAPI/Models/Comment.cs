using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class Comment
    {
        [Key]
        public int id { get; set; }
        public int blogId { get; set; }
        public string? comment { get; set; }
        public string? userName { get; set; }
        public DateTime? publishDate { get; set; }
    }
}
