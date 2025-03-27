using stdsys.Utility;

namespace stdsys.Page;

public partial class CurrentSemesterPage : ContentPage
{
	public CurrentSemesterPage()
	{
		InitializeComponent();
		BindingContext = AppData.Student;
		
		OnPropertyChanged(nameof(AppData.Student.CurrentSemester.Courses));


	}
}