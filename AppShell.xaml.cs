using CalorieCounter.Pages;

namespace CalorieCounter
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(FoodInfo), typeof(FoodInfo));
        }
    }
}
