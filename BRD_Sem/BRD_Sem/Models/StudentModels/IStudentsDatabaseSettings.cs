namespace BRD_Sem.Models.StudentModels
{
    public interface IStudentsDatabaseSettings
    {
        string StudentsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}