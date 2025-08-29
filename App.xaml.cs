using CalorieCounter.Db;
using SQLite;

namespace CalorieCounter
{
    public partial class App : Application
    {
        private readonly FoodInfoDbService _foodinfoDb;
        public App(FoodInfoDbService foodinfoDb)
        {
            InitializeComponent();
            _foodinfoDb = foodinfoDb;
        }

        protected async override void OnStart()
        {
           await _foodinfoDb.InitializeAsync();
            base.OnStart();
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}