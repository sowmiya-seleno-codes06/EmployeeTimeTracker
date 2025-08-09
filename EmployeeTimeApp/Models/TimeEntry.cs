using System;

namespace EmployeeTimeApp.Models
{
    public class TimeEntry
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StarTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public string EntryNotes { get; set; }
        public DateTime? DeletedOn { get; set; }
    }

    public class EmployeeTotal
    {
        public string EmployeeName { get; set; }
        public double TotalHours { get; set; }
    }
}
