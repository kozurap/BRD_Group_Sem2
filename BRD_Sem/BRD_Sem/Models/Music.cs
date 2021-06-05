using System.ComponentModel.DataAnnotations;

namespace BRD_Sem.Models
{
    public class Music
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
    }
}