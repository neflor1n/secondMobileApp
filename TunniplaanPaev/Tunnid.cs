using System;
using System.Collections.Generic;

namespace secondMobileApp.TunniplaanPaev
{
    public class Tunnid
    {
        public string Subject { get; set; }
        public string Teacher { get; set; }
        public string Class { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // Конструктор для создания урока
        public Tunnid(string subject, string teacher, string className, TimeSpan startTime, TimeSpan endTime)
        {
            Subject = subject;
            Teacher = teacher;
            Class = className;
            StartTime = startTime;
            EndTime = endTime;
        }
    }

    // Класс для хранения списка уроков
    public class Schedule
    {
        public List<Tunnid> Lessons { get; set; } = new List<Tunnid>();

        public void AddLesson(string subject, string teacher, string className, TimeSpan startTime, TimeSpan endTime)
        {
            var lesson = new Tunnid(subject, teacher, className, startTime, endTime);
            Lessons.Add(lesson);  
        }

        
    }
}
