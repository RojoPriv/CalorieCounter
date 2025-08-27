using CalorieCounter.Db;

namespace CalorieCounter.Pages;

public partial class FoodInfo : ContentPage
{
	public FoodInfo()
	{
		InitializeComponent();
       Task.Run(async() => FoodInfoView.ItemsSource = await FoodInfoDbService.GetAllFoodInfo());
        
    }


    private async void addButton_Clicked(object sender, EventArgs e)
    {
	
       await FoodInfoDbService.AddFoodInfo(foodEntryField.Text, int.Parse(CalorieEntryField.Text));

        //Clear Entry Field
        foodEntryField.Text = string.Empty;
        CalorieEntryField.Text = string.Empty;
        //Refresh the view
        FoodInfoView.ItemsSource = await FoodInfoDbService.GetAllFoodInfo();
    }

    private void FoodInfoView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
      
    }
}