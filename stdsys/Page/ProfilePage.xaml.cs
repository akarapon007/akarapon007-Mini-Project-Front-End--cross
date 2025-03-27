using stdsys.Utility;

namespace stdsys.Page;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
        BindingContext = AppData.Student;
    }

    private async void OnCurrentSemesterClicked(object sender, EventArgs e) => 
        await Navigation.PushAsync(new CurrentSemesterPage());
    private async void OnPastSemestersClicked(object sender, EventArgs e) => 
        await Navigation.PushAsync(new PastSemestersPage());
    private async void OnSearchCoursesClicked(object sender, EventArgs e) => 
        await Navigation.PushAsync(new SearchCoursesPage());
    private async void OnWithdrawCoursesClicked(object sender, EventArgs e) => 
        await Navigation.PushAsync(new WithdrawCoursesPage());
}