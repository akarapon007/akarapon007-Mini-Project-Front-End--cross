using stdsys.Utility;

namespace stdsys.Page;

public partial class WithdrawCoursesPage : ContentPage
{
    public WithdrawCoursesPage()
    {
        InitializeComponent();
        BindingContext = AppData.Student;
        		OnPropertyChanged(nameof(AppData.Student.CurrentSemester.Courses));

    }

  private async void OnWithdrawClicked(object sender, EventArgs e)
{
    var button = sender as Button;
    var course = button?.BindingContext as Models.Course;

    if (course != null)
    {
        bool confirm = await DisplayAlert(
            "Confirm Withdrawal", 
            $"Are you sure you want to withdraw from {course.CourseName}?", 
            "Yes", 
            "No"
        );

        if (confirm)
        {
            try
            {
                // ใช้ ObservableCollection โดยตรง
                AppData.Student.CurrentSemester.Courses.Remove(course);

                // แจ้งเตือนสำเร็จ
                await DisplayAlert("Success", $"Withdrew from {course.CourseName}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Could not withdraw: {ex.Message}", "OK");
            }
        }
    }
}


}