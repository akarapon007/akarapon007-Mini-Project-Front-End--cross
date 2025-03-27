using stdsys.Utility;

namespace stdsys.Page;

public partial class PastSemestersPage : ContentPage
{
	public PastSemestersPage()
	{
		InitializeComponent();
		BindingContext = AppData.Student;
	}
	private void OnSemesterSelected(object sender, EventArgs e)
    {
        var selectedSemester = semesterPicker.SelectedItem as Models.Semester;
        if (selectedSemester != null)
        {
            coursesCollectionView.ItemsSource = selectedSemester.Courses;
        }
    }
}