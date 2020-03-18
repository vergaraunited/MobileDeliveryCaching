using DataCaching.Data;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataCaching.Caching
{
    public class CacheStops
    {
        readonly SQLiteAsyncConnection _database;

        public CacheStops(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Stop>().Wait();
        }

        public Task<List<Stop>> GetNotesAsync()
        {
            return _database.Table<Stop>().ToListAsync();
        }

        public Task<Stop> GetNoteAsync(int id)
        {
            return _database.Table<Stop>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Stop stop)
        {
            if (stop.Id != 0)
            {
                return _database.UpdateAsync(stop);
            }
            else
            {
                return _database.InsertAsync(stop);
            }
        }

        public Task<int> DeleteNoteAsync(Stop stop)
        {
            return _database.DeleteAsync(stop);
        }
    }
}