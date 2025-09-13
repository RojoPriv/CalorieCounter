using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieCounter.Db
{
    public class DailyTrackerDbService
    {
        private readonly SQLiteAsyncConnection db;
        public DailyTrackerDbService()
        {
            var databasepath = Path.Combine(FileSystem.AppDataDirectory, "DailyTracker.db");
            db = new SQLiteAsyncConnection((databasepath), SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

        }

        public async Task InitializeAsyncDaily()
        {
            await db.CreateTableAsync<DailyTrackerDbModel>();
        }
        public async Task AddDaily(DailyTrackerDbModel dailyTracker)
        {
            await db.InsertAsync(dailyTracker);
        }
        public async Task UpdateDaily(DailyTrackerDbModel dailyTracker)
        {
            await db.UpdateAsync(dailyTracker);
        }
        public async Task DeleteDaily(DailyTrackerDbModel dailyTracker)
        {
            await db.DeleteAsync(dailyTracker);
        }
        public async Task<List<DailyTrackerDbModel>> GetAllDaily()
        {
            var fdailyTrackerList = await db.Table<DailyTrackerDbModel>().ToListAsync();
            return fdailyTrackerList;
        }
        public async Task<DailyTrackerDbModel> GetByIdDaily(int id)
        {
            return await db.Table<DailyTrackerDbModel>().Where(x => x.Id == id).FirstOrDefaultAsync();

        }
        public async Task<float> GetTotalCaloriesForDate(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            // Query for entries within the date range
            var result = await db.Table<DailyTrackerDbModel>()
                .Where(x => x.Date >= startDate && x.Date < endDate)
                .ToListAsync();

            return result.Sum(x => x.Calories);
        }
    }
}
