using System;

namespace MunicipalReporter.Models
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }

        public Event() { }

        public Event(string title, string category, string description, DateTime date, string location)
        {
            Title = title;
            Category = category;
            Description = description;
            Date = date;
            Location = location;
        }

        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd} - {Title} ({Category}) @ {Location}";
        }
    }
}