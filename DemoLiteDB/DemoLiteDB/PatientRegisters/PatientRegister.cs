using DemoLiteDB.Models;
using LiteDB;
using Microsoft.Extensions.Options;
using LiteDB.Async;

namespace DemoLiteDB.PatientRegisters
{
    public class PatientRegister
    {
        private readonly LiteDatabase _database;


        public PatientRegister(IOptions<DatabaseSettings> dataBaseSettings)
        {
            var Database = new LiteDatabase(dataBaseSettings.Value.ConnectionString);
            var LiteDbName = Database.GetCollection(dataBaseSettings.Value.PatientDB);
            _database = (LiteDatabase?)LiteDbName;
        }

        public async Task<List<Patient>> GetAsync()
        {
            return _database.GetCollection<Patient>()
                .FindAll().ToList();
        }
        public async Task<Patient> GetAsync(int id)
        {
            return _database.GetCollection<Patient>()
                .Find(x => x.Id == id).FirstOrDefault();

        }
        public async Task CreateAsync(Patient newPatient)
        {
            _database.GetCollection<Patient>().Insert(newPatient);
        }
    }
}




