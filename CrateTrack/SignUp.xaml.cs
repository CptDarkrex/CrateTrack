namespace CrateTrack;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
	}

    private async void OnSignInTapped(object sender, EventArgs e)
    {
        // Navigate to the Sign In page
        await Navigation.PushAsync(new MainPage());
    }
}