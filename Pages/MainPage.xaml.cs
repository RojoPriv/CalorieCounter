using CalorieCounter.Db;
using CalorieCounter.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Threading;
namespace CalorieCounter.Pages
{
    public partial class MainPage : ContentPage
    {
        private int editDailyId;
        private readonly FoodInfoDbService _foodInfoDb;
        private readonly DailyTrackerDbService _dailyTrackerDb;
        public ObservableCollection<FoodInfoDbModel> FoodInfoCollection { get; set; }
        public MainPage(FoodInfoDbService foodInfoDb, DailyTrackerDbService dailyTrackerDb )
        {
            InitializeComponent();
            _foodInfoDb = foodInfoDb;
            _dailyTrackerDb = dailyTrackerDb;
            FoodInfoCollection = new ObservableCollection<FoodInfoDbModel>();
            Task.Run(async () => CalorieInfoView.ItemsSource =  await _dailyTrackerDb.GetAllDaily());
            Task.Run(async () => await UpdateTotalCaloriesDisplay());
            Task.Run(async () => await LoadAutoCompleteSource());
            WeakReferenceMessenger.Default.Register<FoodUpdatedMessage>(this, async (r, m) =>
            {
                
               await LoadAutoCompleteSource();
            });
        }

        public async Task LoadAutoCompleteSource()
        {
            foodSearchBar.ItemsSource = await _foodInfoDb.GetAllFoodInfo(); ;
        }

        private async void addButton_Clicked(object sender, EventArgs e)
        {

            var selectedFoodName = "Good";// foodSearchBar.Text;
            if (string.IsNullOrEmpty(selectedFoodName) || !float.TryParse(gramEntryField.Text, out float grams) || grams <= 0)
            {
                // Display an error or do nothing if the input is invalid
                await DisplayAlert("Error", "Please select a food and enter a valid number of grams.", "OK");
                return;
            }

            try
            {
                var foodInfo = await _foodInfoDb.GetFoodInfoByFoodName(selectedFoodName);

                Console.WriteLine(foodInfo.CaloriesPerGram);

                if (foodInfo != null)
                {
                    // Calculate total calories
                    var totalCalories = foodInfo.CaloriesPerGram * grams;
                    //  var Date = System.DateTime.Now;

                    if (editDailyId == 0)
                        { 
                    await _dailyTrackerDb.AddDaily(new DailyTrackerDbModel
                    {
                        Date = System.DateTime.Now,
                        Food = selectedFoodName,
                        Grams = grams,
                        Calories = totalCalories
                    });
                    }
                    else
                    {
                        await _dailyTrackerDb.UpdateDaily(new DailyTrackerDbModel
                        {
                            Id = editDailyId,
                            Date = System.DateTime.Now,
                            Food = selectedFoodName,
                            Grams = grams,
                            Calories = totalCalories
                        });
                        editDailyId = 0;
                    }

                    await UpdateTotalCaloriesDisplay();
                    // Clear the input fields after successful addition
                   // foodSearchBar.Text = string.Empty;
                    gramEntryField.Text = string.Empty;

                    CalorieInfoView.ItemsSource = await _dailyTrackerDb.GetAllDaily();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding daily tracker entry: {ex.Message}");
                await DisplayAlert("Error", "Failed to add the food item to your daily tracker.", "OK");
            }

        }

        private async Task UpdateTotalCaloriesDisplay()
        {
            var totalCalories = await _dailyTrackerDb.GetTotalCaloriesForDate(DateTime.Now);
            totalCaloriesLabel.Text = $"{totalCalories:F0} kcal";
        }

        private async void CalorieInfoView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is DailyTrackerDbModel dailyTracker)
            {
                var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
                switch (action)
                {
                    case "Edit":
                        editDailyId = dailyTracker.Id;
                  //      foodSearchBar.Text = dailyTracker.Food;
                        gramEntryField.Text = dailyTracker.Grams.ToString();
                        break;
                    case "Delete":
                        await _dailyTrackerDb.DeleteDaily(dailyTracker);
                        CalorieInfoView.ItemsSource = await _dailyTrackerDb.GetAllDaily();
                        await UpdateTotalCaloriesDisplay();
                        break;
                }
                // Deselect item after action
                CalorieInfoView.SelectedItem = null;
            }
        }
    }

    }





