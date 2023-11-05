namespace CrateTrack;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
    // Example event handler for a Login button click event
    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
        // Handle the login logic here
    }

    // Example event handler for a Sign-up label tap event
    private void OnSignupLabelTapped(object sender, EventArgs e)
    {
        // Handle the sign-up navigation or logic here
    }

    // Example event handler for a Forgot Password label tap event
    private void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        // Handle the forgot password logic here
    }
    private async void OnSignUpTapped(object sender, EventArgs e)
    {
        // Navigate to the Sign In page
        await Navigation.PushAsync(new SignUp());
    }
}

