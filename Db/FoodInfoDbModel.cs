
using SQLite;

namespace CalorieCounter.Db
{
    public class FoodInfoDbModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public string FoodName { get; set; }
        public int CaloriesPerGram { get; set; }
    }
}
