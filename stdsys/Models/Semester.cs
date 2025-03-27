using System;
using System.Collections.ObjectModel;

namespace stdsys.Models;

public class Semester
{
    public required string SemesterName { get; set; }
    // public required List<Course> Courses { get; set; }
    public ObservableCollection<Course> Courses { get; set; } = new ObservableCollection<Course>();
    
}
