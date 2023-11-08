using CrateTrack.Database;
using Npgsql;

namespace CrateTrack;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		RedirectToSignIn();
	}

	private User user = new User();

    private async void RedirectToSignIn()
	{
		if (user.Authorised == true)
		{
			return;
		}
		else
		{
            await Navigation.PushAsync(new SignIn());
        }
    }
}

