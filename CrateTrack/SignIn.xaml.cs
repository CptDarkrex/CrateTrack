namespace CrateTrack;
using CrateTrack.Database;
using Npgsql;

public partial class SignIn : ContentPage
{
    private DbHelper _dbHelper;
	public SignIn()
	{
		InitializeComponent();
	}
    private User GetUserByUsername(string username)
    {
        using var conn = _dbHelper.GetConnection();
        conn.Open();

        using var cmd = new NpgsqlCommand("SELECT user_id, username, password_hash FROM users WHERE username = @username", conn);
        cmd.Parameters.AddWithValue("username", username);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new User
            {
                UserId = reader.GetInt32(0),
                Username = reader.GetString(1),
                PasswordHash = reader.GetString(2)
            };
        }
        return null;
    }


    // Example event handler for a Login button click event
    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
        // Handle the login logic here
        var user = GetUserByUsername(emailEntry.Text);
        if (user != null && user.Authenticate(passwordEntry.Text, emailEntry.Text))
        {
            user.CreateSession();

            // Store session token somewhere, perhaps in a static variable or properties.
            // Then, you can use User.GetBySessionToken(token) to validate user sessions.
        }
        else
        {
            return;
        }
    }

    // Example event han
    // dler for a Sign-up label tap event
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
        await Navigation.PushAsync(new MainPage());
    }
}
