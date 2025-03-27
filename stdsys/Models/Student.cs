using System.Text.Json.Serialization;

namespace stdsys.Models;

public class Student
{
    public required string ProfilePictureUrl { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string StudentId { get; set; }
    [JsonPropertyName("email")] // Maps the "email" JSON field to the Email property
    public required string Email { get; set; }
    public required int Year { get; set; }
    public required Curriculum Curriculum { get; set; }
    public double Gpa { get; set; }
    [JsonPropertyName("currentSemester")]
    public required Semester CurrentSemester { get; set; }
    [JsonPropertyName("semesters")]
    public required List<Semester> PastSemesters { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}