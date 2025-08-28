using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieCounter.Db
{
    public class FoodInfoDbService
    {
        private SQLiteAsyncConnection db;
        public FoodInfoDbService()
        {
            var databasepath = Path.Combine(FileSystem.AppDataDirectory,"FoodInfo.db");
            db = new SQLite.SQLiteAsyncConnection(databasepath);
            db.CreateTableAsync<FoodInfoDbModel>();
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
    }
}
