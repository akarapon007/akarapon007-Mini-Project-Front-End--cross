using stdsys.Models;
using stdsys.Utility;
using System.Text.Json;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace stdsys;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Load student data
            using var studentStream = await FileSystem.OpenAppPackageFileAsync("student_data.json");
            using var studentReader = new StreamReader(studentStream);
            var studentJson = await studentReader.ReadToEndAsync();
            Debug.WriteLine($"Raw Student JSON: {studentJson}");
            
            var studentData = JsonSerializer.Deserialize<Student>(studentJson, options);
            if (studentData != null)
            {
                AppData.Student = studentData;
                Debug.WriteLine("Deserialized Student: Success");
            }
            else
            {
                Debug.WriteLine("Deserialized Student: Failed");
            }

            // Load available courses
            using var coursesStream = await FileSystem.OpenAppPackageFileAsync("available_courses.json");
            using var coursesReader = new StreamReader(coursesStream);
            var coursesJson = await coursesReader.ReadToEndAsync();
            Debug.WriteLine($"Raw Courses JSON: {coursesJson}");
            
            var coursesData = JsonSerializer.Deserialize<List<Course>>(coursesJson, options);
           if (coursesData != null)
{
    AppData.AvailableCourses = new ObservableCollection<Course>(coursesData);
    Debug.WriteLine("Deserialized Courses: Success");
}

            else
            {
                Debug.WriteLine("Deserialized Courses: Failed");
            }

            // Log the student's email after data is loaded
            Debug.WriteLine($"Student Email: {AppData.Student?.Email ?? "Email is null"}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading data: {ex.Message}");
            Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}