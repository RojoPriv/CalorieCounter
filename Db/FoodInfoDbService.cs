using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieCounter.Db
{
    public static class FoodInfoDbService
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;
            var databasepath = Path.Combine(FileSystem.AppDataDirectory,"FoodInfo.db");
            db = new SQLite.SQLiteAsyncConnection(databasepath);
            await db.CreateTableAsync<FoodInfoDbModel>();
        }

        public static async Task AddFoodInfo(String FoodName, int CaloriePerGram)
        {   
            await Init();
            var foodInfo = new FoodInfoDbModel
            {
                FoodName = FoodName,
                CaloriesPerGram = CaloriePerGram
            };
            await db.InsertAsync(foodInfo);
        }
        public static async Task DeleteFoodInfo(int id)
        {
            await Init();
            await db.DeleteAsync<FoodInfoDbModel>(id);
        }

        public static async Task<List<FoodInfoDbModel>> GetAllFoodInfo()
        {
            await Init();
            var foodInfoList = await db.Table<FoodInfoDbModel>().ToListAsync();
            return foodInfoList;
        }
    }
}
