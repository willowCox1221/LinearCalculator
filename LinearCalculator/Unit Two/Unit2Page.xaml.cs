using LinearCalculator.Unit_Two;


namespace LinearCalculator;

public partial class Unit2Page : ContentPage
{
	public Unit2Page()
	{
		InitializeComponent();
	}
	private async void OnVectorClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new VectorPage());
    }
	private async void OnUnitVectorClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new UnitVectorPage());
	}
	private async void OnCrossProductClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new CrossProductPage());
	}
	

}