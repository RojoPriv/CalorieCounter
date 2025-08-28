
using SQLite;

namespace CalorieCounter.Db
{
    [Table("FoodInfo")]
    public class FoodInfoDbModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? FoodName { get; set; }
        public float CaloriesPerGram { get; set; }
    }
}
