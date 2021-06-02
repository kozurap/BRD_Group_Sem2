using System.ComponentModel.DataAnnotations;

namespace BRD_Sem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string HashedPassword { get; set; }
        
        public byte[] Image;
        public string Role { get; set; }
        public bool IsConfirmed { get; set; }
    }
}