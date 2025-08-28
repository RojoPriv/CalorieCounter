using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CalorieCounter.Db
{
    public class DailyTrackerDbModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string? Food { get; set; }
        public float Grams { get; set; }
        public float Calories { get; set; }
    }
}
