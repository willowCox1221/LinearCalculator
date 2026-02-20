using LinearCalculator.Unit_One;

namespace LinearCalculator;

public partial class Unit1Page : ContentPage
{
    public Unit1Page()
    {
        InitializeComponent();
    }

    private async void OnAdditionClicked(object sender, EventArgs e) { 
        await Navigation.PushAsync(new MatrixAdditionPage());
        }

    private async void OnSubtractionClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync (new MatrixSubtractionPage());
    }

    private async void OnMultiplicationClicked(object sender, EventArgs e){
        await Navigation.PushAsync (new MatrixMultiplicationPage());
    }

    private async void OnTransposeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TransposePage());
    }

    private async void OnDeterminantClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DeterminantPage());
    }

    private async void OnInverseClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InversePage());
    }

    private async void OnRrefClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RrefPage());
    }
    private async void OnEliminationClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EliminationPage());
    }

    private async void OnSubstitutionClicked(object sender, EventArgs e)
        => await DisplayAlert("Substitution", "Coming soon", "OK");
}