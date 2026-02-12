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
        => await DisplayAlert("Matrix Subtraction", "Coming soon", "OK");

    private async void OnMultiplicationClicked(object sender, EventArgs e)
        => await DisplayAlert("Matrix Multiplication", "Coming soon", "OK");

    private async void OnTransposeClicked(object sender, EventArgs e)
        => await DisplayAlert("Transpose", "Coming soon", "OK");

    private async void OnDeterminantClicked(object sender, EventArgs e)
        => await DisplayAlert("Determinant", "Coming soon", "OK");

    private async void OnInverseClicked(object sender, EventArgs e)
        => await DisplayAlert("Inverse", "Coming soon", "OK");

    private async void OnRrefClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RrefPage());
    }
    private async void OnEliminationClicked(object sender, EventArgs e)
        => await DisplayAlert("Elimination", "Coming soon", "OK");

    private async void OnSubstitutionClicked(object sender, EventArgs e)
        => await DisplayAlert("Substitution", "Coming soon", "OK");
}