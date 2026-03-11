using LinearCalculator.Unit_Three;

namespace LinearCalculator;

public partial class Unit3Page : ContentPage
{
	public Unit3Page()
	{
		InitializeComponent();
	}
    private async void OnCheckOrthogonalClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OrthogonalCheckPage());
    }
    private async void OnCheckOrthonormalClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OrthonormalCheckerPage());
    }
    private async void OnLinearTransformationClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LinearTransformation());
    }
    private async void OnRotationClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RotationXPage());
    }
    private async void OnRotationYClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RotationYPage());
    }
    private async void OnScalingClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ScalingTransformationPage());
    }
    private async void OnShearClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VerticalShearPage());
    }

}