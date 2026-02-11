namespace LinearCalculator;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnUnit1Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Unit1Page());
    }

    private async void OnUnit2Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Unit2Page());
    }

    private async void OnUnit3Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Unit3Page());
    }

    private async void OnUnit4Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Unit4Page());
    }
}