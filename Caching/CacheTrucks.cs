using DataCaching.Data;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataCaching.Caching
{
    public class CacheTrucks
    {
        readonly SQLiteAsyncConnection _database;

        public CacheTrucks(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Truck>().Wait();
        }

        public Task<List<Truck>> GetNotesAsync()
        {
            return _database.Table<Truck>().ToListAsync();
        }

        public Task<Truck> GetNoteAsync(int id)
        {
            return _database.Table<Truck>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Truck truck)
        {
            if (truck.Id != 0)
            {
                return _database.UpdateAsync(truck);
            }
            else
            {
                return _database.InsertAsync(truck);
            }
        }

        public Task<int> DeleteNoteAsync(Truck truck)
        {
            return _database.DeleteAsync(truck);
        }
    }
}
