using stdsys.Models;
using stdsys.Utility;
using System.Collections.ObjectModel; // เพิ่มเพื่อใช้ ObservableCollection
using System.Linq;

namespace stdsys.Page;

public partial class SearchCoursesPage : ContentPage
{
    private List<Course> allCourses;
    private List<Course> availableCourses;

    public SearchCoursesPage()
    {
        InitializeComponent();

        // Populate all courses (replace with your data source)
        allCourses = AppData.AvailableCourses.ToList();

        // ตรวจสอบและแปลง CurrentSemester.Courses เป็น ObservableCollection ถ้าจำเป็น
        if (AppData.Student.CurrentSemester.Courses is not ObservableCollection<Course>)
        {
            AppData.Student.CurrentSemester.Courses = new ObservableCollection<Course>(AppData.Student.CurrentSemester.Courses);
        }

        // กรองวิชาที่อยู่ใน CurrentSemester.Courses ออก
        var registeredCourses = AppData.Student.CurrentSemester.Courses.ToList();
        availableCourses = allCourses
            .Where(c => !registeredCourses.Any(rc => rc.CourseCode == c.CourseCode))
            .ToList();

        SearchResults = new List<Course>(availableCourses);
        coursesCollectionView.ItemsSource = SearchResults;

        // Set up binding context
        BindingContext = this;

        // Initialize collections
        AddedCourses = new List<Course>();
    }

    public List<Course> SearchResults { get; set; }
    public List<Course> AddedCourses { get; set; }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchTerm = e.NewTextValue?.ToLower() ?? "";

        SearchResults = availableCourses
            .Where(c => c.CourseName.ToLower().Contains(searchTerm) ||
                        c.CourseCode.ToLower().Contains(searchTerm))
            .ToList();

        OnPropertyChanged(nameof(SearchResults));
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var course = button?.BindingContext as Course;

        if (course != null)
        {
            bool confirm = await DisplayAlert(
                "Add Course", 
                $"Do you want to add {course.CourseName}?", 
                "Yes", 
                "No"
            );

            if (confirm)
            {
                try
                {
                    // เพิ่มคอร์สเข้าไปใน CurrentSemester.Courses (สำหรับ CurrentSemesterPage และ WithdrawCoursesPage)
                    ((ObservableCollection<Course>)AppData.Student.CurrentSemester.Courses).Add(course);

                    // เพิ่มคอร์สเข้าไปใน AddedCourses (สำหรับหน้านี้)
                    AddedCourses.Add(course);
                    OnPropertyChanged(nameof(AddedCourses));

                    // ลบคอร์สออกจาก availableCourses และ SearchResults
                    availableCourses.Remove(course);
                    SearchResults.Remove(course);

                    // อัปเดต UI
                    coursesCollectionView.ItemsSource = null;
                    coursesCollectionView.ItemsSource = SearchResults;

                    // แจ้งเตือนสำเร็จ
                    await DisplayAlert("Success", $"{course.CourseName} has been added.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Could not add course: {ex.Message}", "OK");
                }
            }
        }
    }
}