using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? username { get; set; }
        public string?  password { get; set; }
        public string? token { get; set; }
        public string? role { get; set; }
        public string? email { get; set; }
    }
}
