using CalorieCounter.Db;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using zoft.MauiExtensions.Controls;

namespace CalorieCounter.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly FoodInfoDbService _foodInfoDb;
        public MainPage(FoodInfoDbService foodInfoDb)
        {
            InitializeComponent();
            _foodInfoDb = foodInfoDb;
        }

        private void CalorieInfoView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void addButton_Clicked(object sender, EventArgs e)
        {
            //Add new food info
            await _foodInfoDb.AddFoodInfo(new FoodInfoDbModel
            {
                FoodName = foodEntryField.Text,
                CaloriesPerGram = float.Parse(gramEntryField.Text)
            });
        }

        //private void foodEntryField_TextChanged(object sender, AutoCompleteEntryTextChangedEventArgs e)
        //{
        //    var searchText = foodEntryField.Text ?? string.Empty;

        //    Console.WriteLine(searchText);
        //    //      var results = await _foodInfoDb.SearchFoodName(searchText); // This line returns a List<FoodInfoDbModel> 

        //    // Create a new list of food names from the search results

        //}

        private void foodEntryField_TextChanged(object sender, AutoCompleteEntryTextChangedEventArgs e)
        {
            if (e.Reason == AutoCompleteEntryTextChangeReason.UserInput)
            {
                var searchText = foodEntryField.Text ?? string.Empty;
               // Console.WriteLine(searchText);
               // var results = await _foodInfoDb.SearchFoodName(searchText);
              //  Console.WriteLine(results);
               // foodEntryField.ItemsSource = results.Select(item => item.FoodName).Distinct().ToList();

                //var FoodNames = new ObservableCollection<string>();
                //foreach (var item in results)
                //{
                //    if (!string.IsNullOrWhiteSpace(item.FoodName) && !FoodNames.Contains(item.FoodName))
                //        FoodNames.Add(item.FoodName);
                //}
                //foodEntryField.ItemsSource = FoodNames;
            }
        }
    }

}



