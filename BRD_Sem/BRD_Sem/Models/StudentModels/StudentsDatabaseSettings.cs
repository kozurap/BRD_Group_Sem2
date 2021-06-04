namespace BRD_Sem.Models.StudentModels
{
    public class StudentsDatabaseSettings:IStudentsDatabaseSettings
    {
        public string StudentsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}