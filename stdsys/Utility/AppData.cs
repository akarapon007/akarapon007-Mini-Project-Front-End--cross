using System;
using System.Collections.ObjectModel;
using stdsys.Models;

namespace stdsys.Utility;

public static class AppData
{
    public static Student Student { get; set; }
    
  public static ObservableCollection<Course> AvailableCourses { get; set; } = new ObservableCollection<Course>();

}
