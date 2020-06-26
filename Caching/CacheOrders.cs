using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataCaching.Caching
{
    public class CacheOrders
    {
        readonly SQLiteAsyncConnection _database;

        public CacheOrders(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Order>().Wait();
        }

        public Task<List<Order>> GetNotesAsync()
        {
            return _database.Table<Order>().ToListAsync();
        }

        public Task<Order> GetNoteAsync(int id)
        {
            return _database.Table<Order>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Order order)
        {
            if (order.Id != 0)
            {
                return _database.UpdateAsync(order);
            }
            else
            {
                return _database.InsertAsync(order);
            }
        }

        public Task<int> DeleteNoteAsync(Order order)
        {
            return _database.DeleteAsync(order);
        }
    }
}
