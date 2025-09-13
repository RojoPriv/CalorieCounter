using CalorieCounter.Db;
using SQLite;

namespace CalorieCounter
{
    public partial class App : Application
    {
        private readonly FoodInfoDbService _foodinfoDb;
        private readonly DailyTrackerDbService _dailyTrackerDb;
        public App(FoodInfoDbService foodinfoDb, DailyTrackerDbService dailyTrackerDb)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JFaF5cXGRCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXZfc3VQR2BZU0Z1VkVWYEg=");

            InitializeComponent();
            _foodinfoDb = foodinfoDb;
            _dailyTrackerDb = dailyTrackerDb;
        }

        protected async override void OnStart()
        {
           await _foodinfoDb.InitializeAsync();
            await _dailyTrackerDb.InitializeAsyncDaily();
            base.OnStart();
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}