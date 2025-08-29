using CalorieCounter.Db;
using zoft.MauiExtensions.Controls;

namespace CalorieCounter.Pages;

public partial class FoodInfo : ContentPage
{
    private readonly FoodInfoDbService _foodInfoDb;
    private int editFoodId;
    public FoodInfo(FoodInfoDbService foodInfoDb)
	{
		InitializeComponent();
        _foodInfoDb = foodInfoDb;
        Task.Run(async() => FoodInfoView.ItemsSource = await _foodInfoDb.GetAllFoodInfo());
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        //Check if Food Name is null
        if  (string.IsNullOrEmpty(foodEntryField.Text)) 
         {
                await DisplayAlert("Invalid Input", "Food cannot be empty.", "OK");
            return;
        }
        //Check if CaloriePerGram is null
        if (string.IsNullOrEmpty(CalorieEntryField.Text))
        {
            await DisplayAlert("Invalid Input", "Calorie cannot be empty.", "OK");
            return;
        }
        //Check if Food Name is string
        if (float.TryParse(foodEntryField.Text, out float foodName))
        {
            await DisplayAlert("Invalid Input", "Please enter a valid name for Food.", "OK");
            return;
        }
        //Check if CaloriePerGram is number
        if (!float.TryParse(CalorieEntryField.Text, out float caloriesPerGram))
        {
            await DisplayAlert("Invalid Input", "Please enter a valid number for calories per gram.", "OK");
            return;
        }

        if (editFoodId ==0)
        {
            //Add new food info
            await _foodInfoDb.AddFoodInfo(new FoodInfoDbModel
            {
                FoodName = foodEntryField.Text,
                CaloriesPerGram = float.Parse(CalorieEntryField.Text)
            });
        }
        else
        {
           //Update existing food info
            await _foodInfoDb.UpdateFoodInfo(new FoodInfoDbModel
            {
                Id = editFoodId,
                FoodName = foodEntryField.Text,
                CaloriesPerGram = float.Parse(CalorieEntryField.Text)
            });
            editFoodId = 0;
        }
        //Clear Entry Field
        foodEntryField.Text = string.Empty;
        CalorieEntryField.Text = string.Empty;
        //Refresh the view
        FoodInfoView.ItemsSource = await _foodInfoDb.GetAllFoodInfo();
    }

    private async void FoodInfoView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is FoodInfoDbModel foodInfo)
        {
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
            switch (action)
            {
                case "Edit":
                    editFoodId = foodInfo.Id;
                    foodEntryField.Text = foodInfo.FoodName;
                    CalorieEntryField.Text = foodInfo.CaloriesPerGram.ToString();
                    break;
                case "Delete":
                    await _foodInfoDb.DeleteFoodInfo(foodInfo);
                    FoodInfoView.ItemsSource = await _foodInfoDb.GetAllFoodInfo();
                    break;
            }
            // Deselect item after action
            FoodInfoView.SelectedItem = null;
        }
    }
 
}