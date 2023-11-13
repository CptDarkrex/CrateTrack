namespace CrateTrack;
using CrateTrack.Database;
using Npgsql;
using System.Globalization;

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

    public async Task ShowFlashMessage(string message, Color bg_color, int duration = 3000)
    {
        flashMessageLayout.BackgroundColor = bg_color;
        flashWrapper.BackgroundColor = bg_color;

        flashMessageLabel.Text = message;

        flashMessageLayout.IsVisible = true;
        flashWrapper.IsVisible = true;

        await Task.Delay(duration);

        flashWrapper.IsVisible = false;
    }


    // Example event handler for a Login button click event
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        if (emailEntry.Text == "klaros")
        {
            await ShowFlashMessage("Success", Colors.LightGreen, 3000);
        }
        else
        {
            await ShowFlashMessage("Failed", Colors.Red, 3000);
        }
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
