using System.ComponentModel.DataAnnotations;

namespace BRD_Sem.Models
{
    public class Expired
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfessorName { get; set; }
        public string Description { get; set; }
    }
}