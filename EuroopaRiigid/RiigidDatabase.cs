using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace secondMobileApp.EuroopaRiigid
{
    public class RiigidDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public RiigidDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Riik>().Wait();
        }

        // Получение списка всех стран
        public Task<List<Riik>> GetRiigidAsync()
        {
            return _database.Table<Riik>().ToListAsync();
        }

        // Получение страны по названию
        public Task<Riik> GetRiikByNameAsync(string name)
        {
            return _database.Table<Riik>().FirstOrDefaultAsync(r => r.Nimetus == name);
        }

        // Добавление страны (если её нет в базе)
        public async Task<int> AddRiikAsync(Riik riik)
        {
            var existingRiik = await GetRiikByNameAsync(riik.Nimetus);
            if (existingRiik == null)
            {
                return await _database.InsertAsync(riik);
            }
            return 0; 
        }

        // Обновление данных о стране
        public Task<int> UpdateRiikAsync(Riik riik)
        {
            return _database.UpdateAsync(riik);
        }



        // Удаление страны
        public Task<int> DeleteRiikAsync(Riik riik)
        {
            return _database.DeleteAsync(riik);
        }

        // Поиск страны по айди
        public Task<Riik> GetRiikByIdAsync(int id)
        {
            return _database.Table<Riik>().FirstOrDefaultAsync(r => r.Id == id);
        }

    }
}
