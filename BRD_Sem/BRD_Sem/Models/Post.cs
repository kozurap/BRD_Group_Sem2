using System.ComponentModel.DataAnnotations;

namespace BRD_Sem.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public byte[] Image { get; set; }
    }
}