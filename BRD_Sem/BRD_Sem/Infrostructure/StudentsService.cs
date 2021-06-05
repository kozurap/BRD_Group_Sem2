using System.Collections.Generic;
using BRD_Sem.Models.StudentModels;
using MongoDB.Driver;

namespace BRD_Sem.Infrostructure
{
    public class StudentsService
    {
        private readonly IMongoCollection<Students> _students;

        public StudentsService(IStudentsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _students = database.GetCollection<Students>(settings.StudentsCollectionName);
        }
        public List<Students> Get() =>
            _students.Find(s => true).ToList();
    }
}