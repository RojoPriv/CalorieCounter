using SQLite;
using System.Collections.ObjectModel;

namespace CalorieCounter.Db
{
    public class FoodInfoDbService
    {
        private readonly SQLiteAsyncConnection db;
        
        public FoodInfoDbService()
        {
            var databasepath = Path.Combine(FileSystem.AppDataDirectory,"FoodInfo.db");
           db = new SQLiteAsyncConnection((databasepath),SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create |SQLiteOpenFlags.SharedCache);
           
        }
        public async Task InitializeAsync()
        {
            await db.CreateTableAsync<FoodInfoDbModel>();
        }

        public async Task AddFoodInfo(FoodInfoDbModel foodInfo)
        {   
            await db.InsertAsync(foodInfo);
        }

        public async Task UpdateFoodInfo(FoodInfoDbModel foodInfo)
        {
            await db.UpdateAsync(foodInfo);
        }
        public  async Task DeleteFoodInfo(FoodInfoDbModel foodInfo)
        {
            await db.DeleteAsync(foodInfo);
        }

        public async Task<List<FoodInfoDbModel>> GetAllFoodInfo()
        {
            var foodInfoList = await db.Table<FoodInfoDbModel>().ToListAsync();
            return foodInfoList;
        }

        public async Task<FoodInfoDbModel> GetById(int id)
        {
            return await db.Table<FoodInfoDbModel>().Where(x => x.Id == id).FirstOrDefaultAsync();
           
        }

        public async Task<float?> GetCaloriesPerGramById(int id)
        {
            var result = await db.Table<FoodInfoDbModel>().Where(x => x.Id == id).FirstOrDefaultAsync();
            return result?.CaloriesPerGram;
        }

        public async Task<int?> GetIdByFoodName(string foodname)
        {
            var result = await db.Table<FoodInfoDbModel>().Where(x => x.FoodName == foodname).FirstOrDefaultAsync();
            return result?.Id;
        }

        public async Task<List<FoodInfoDbModel>>SearchFoodName(string searchterm)
        {
            var result = await db.Table<FoodInfoDbModel>().Where(x => x.FoodName.StartsWith(searchterm)).ToListAsync();
            return result;
        }
        public async Task<FoodInfoDbModel> GetFoodInfoByFoodName(string foodname)
        {
            return await db.Table<FoodInfoDbModel>().Where(x => x.FoodName == foodname).FirstOrDefaultAsync();
        }

    }
}
