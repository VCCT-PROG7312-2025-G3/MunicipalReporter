using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalReporter.Models
{
    public class EventRepository
    {
        private SortedDictionary<DateTime, List<Event>> eventsByDate = new SortedDictionary<DateTime, List<Event>>();
        private Dictionary<string, List<Event>> eventsByCategory = new Dictionary<string, List<Event>>(StringComparer.OrdinalIgnoreCase);
        private HashSet<string> categories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private Stack<Event> recentAddedStack = new Stack<Event>();
        private Queue<Event> processingQueue = new Queue<Event>();

        public EventRepository()
        {
            SeedSampleEvents();
        }

        private void SeedSampleEvents()
        {
            AddEvent(new Event("Community Market", "Market", "Local vendors & food", DateTime.Today.AddDays(3), "Town Square"));
            AddEvent(new Event("Health Outreach", "Health", "Free checkups", DateTime.Today.AddDays(7), "Community Centre"));
            AddEvent(new Event("Roadworks Info Session", "Public Works", "Planned roadworks discussion", DateTime.Today.AddDays(10), "Library Hall"));
            AddEvent(new Event("Music Night", "Music", "Open mic and bands", DateTime.Today.AddDays(5), "Park Stage"));
            AddEvent(new Event("Safety Workshop", "Safety", "Fire and personal safety tips", DateTime.Today.AddDays(1), "Fire Station"));
        }

        public void AddEvent(Event ev)
        {
            var dateKey = ev.Date.Date;
            if (!eventsByDate.TryGetValue(dateKey, out var list))
            {
                list = new List<Event>();
                eventsByDate[dateKey] = list;
            }
            list.Add(ev);

            if (!eventsByCategory.TryGetValue(ev.Category, out var catList))
            {
                catList = new List<Event>();
                eventsByCategory[ev.Category] = catList;
                categories.Add(ev.Category);
            }
            catList.Add(ev);

            recentAddedStack.Push(ev);
            processingQueue.Enqueue(ev);
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return eventsByDate.Values.SelectMany(x => x).OrderBy(e => e.Date);
        }

        public IEnumerable<Event> GetEventsByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category)) return GetAllEvents();
            return eventsByCategory.ContainsKey(category) ? eventsByCategory[category].OrderBy(e => e.Date) : Enumerable.Empty<Event>();
        }

        public IEnumerable<Event> GetEventsByDateRange(DateTime? start, DateTime? end)
        {
            var startDate = start?.Date ?? DateTime.MinValue.Date;
            var endDate = end?.Date ?? DateTime.MaxValue.Date;
            foreach (var kv in eventsByDate)
            {
                if (kv.Key < startDate) continue;
                if (kv.Key > endDate) break;
                foreach (var ev in kv.Value)
                    yield return ev;
            }
        }

        public IEnumerable<Event> SearchEvents(string query, string category = null, DateTime? start = null, DateTime? end = null)
        {
            var lower = query?.Trim().ToLowerInvariant();
            var baseSet = GetAllEvents();
            if (!string.IsNullOrWhiteSpace(category))
                baseSet = baseSet.Where(e => string.Equals(e.Category, category, StringComparison.OrdinalIgnoreCase));
            if (start.HasValue || end.HasValue)
                baseSet = baseSet.Where(e => e.Date.Date >= (start?.Date ?? DateTime.MinValue.Date) && e.Date.Date <= (end?.Date ?? DateTime.MaxValue.Date));
            if (string.IsNullOrWhiteSpace(lower)) return baseSet;
            return baseSet.Where(e => (e.Title != null && e.Title.ToLowerInvariant().Contains(lower))
                                       || (e.Description != null && e.Description.ToLowerInvariant().Contains(lower))
                                       || (e.Location != null && e.Location.ToLowerInvariant().Contains(lower)))
                          .OrderBy(e => e.Date);
        }

        public IEnumerable<string> GetCategories()
        {
            return categories.OrderBy(c => c);
        }

        public Event PopMostRecentAdded()
        {
            return recentAddedStack.Count > 0 ? recentAddedStack.Pop() : null;
        }

        public Event DequeueNextForProcessing()
        {
            return processingQueue.Count > 0 ? processingQueue.Dequeue() : null;
        }

        public IEnumerable<Event> GetEventsByCategoryOrNearestDate(string category, int maxResults = 5)
        {
            if (!string.IsNullOrWhiteSpace(category) && eventsByCategory.ContainsKey(category))
                return eventsByCategory[category].OrderBy(e => e.Date).Take(maxResults);
            return GetAllEvents().Where(e => e.Date >= DateTime.Today).OrderBy(e => e.Date).Take(maxResults);
        }
    }
}