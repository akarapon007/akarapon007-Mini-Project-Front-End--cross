using System;
using System.Collections.Generic;

namespace stdsys.Models;

public class Course
{
    public required string CourseCode { get; set; }
    public required string CourseName { get; set; }
    public int Credits { get; set; }
    public string? Grade { get; set; }
    public bool IsCompleted { get; set; } // เพิ่มสถานะว่าวิชานี้เรียนจบหรือยัง
    public string? Semester { get; set; } // เพิ่มข้อมูลเทอมที่เรียนวิชานี้
    public List<string>? Prerequisites { get; set; } // เพิ่มวิชาที่ต้องเรียนมาก่อน
    public bool IsWithdrawn { get; set; } // เพิ่มสถานะว่าถอนวิชานี้หรือไม่

    // ลบ List<Course> Courses ออก เพราะอาจไม่จำเป็นในคลาสนี้
    // ถ้าต้องการให้ Course มีรายการวิชาย่อย อาจใช้คลาสอื่นเช่น Semester แทน

    // Constructor
    public Course()
    {
        Prerequisites = new List<string>();
    }

    // เมธอดเพิ่มเติม: คำนวณเกรดเป็นตัวเลข (ถ้ามี)
    public double? GetGradePoint()
    {
        if (string.IsNullOrEmpty(Grade)) return null;

        return Grade switch
        {
            "A" => 4.0,
            "A-" => 3.7,
            "B+" => 3.3,
            "B" => 3.0,
            "B-" => 2.7,
            "C+" => 2.3,
            "C" => 2.0,
            "C-" => 1.7,
            "D+" => 1.3,
            "D" => 1.0,
            "F" => 0.0,
            _ => null // กรณีเกรดไม่ถูกต้อง
        };
    }

    // เมธอดเพิ่มเติม: ตรวจสอบว่าสามารถถอนวิชาได้หรือไม่
    public bool CanWithdraw()
    {
        return !IsCompleted && !IsWithdrawn;
    }

    // Override ToString เพื่อแสดงข้อมูลวิชา
    public override string ToString()
    {
        return $"{CourseCode} - {CourseName} ({Credits} credits)" + 
               (Grade != null ? $", Grade: {Grade}" : "");
    }
}