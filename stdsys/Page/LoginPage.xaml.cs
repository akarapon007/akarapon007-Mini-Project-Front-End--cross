using System.Diagnostics;
using stdsys.Utility;

namespace stdsys.Page;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
		
    }

   private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(EmailEntry.Text) || !EmailEntry.Text.Contains("@"))
        {
            await DisplayAlert("Error", "Please enter a valid email", "OK");
            return;
        }
        if (string.IsNullOrEmpty(PasswordEntry.Text))
        {
            await DisplayAlert("Error", "Password cannot be empty", "OK");
            return;
        }
        // Mock login check
        if (EmailEntry.Text == AppData.Student.Email && PasswordEntry.Text == "123")
        {
            await Navigation.PushAsync(new ProfilePage());
        }
        else
        {
            await DisplayAlert("Error", "Invalid email or password", "OK");
        }
    }
}

