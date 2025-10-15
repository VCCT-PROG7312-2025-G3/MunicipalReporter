using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalReporter.Models
{
    public class RecommendationEngine
    {
        private Dictionary<string, int> searchTermCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private EventRepository repo;

        public RecommendationEngine(EventRepository repository)
        {
            repo = repository;
        }

        public void RegisterSearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return;
            var key = searchTerm.Trim().ToLowerInvariant();
            if (!searchTermCounts.ContainsKey(key)) searchTermCounts[key] = 0;
            searchTermCounts[key]++;
        }

        public IEnumerable<string> TopSearchTerms(int n = 5)
        {
            return searchTermCounts.OrderByDescending(kv => kv.Value).Take(n).Select(kv => kv.Key);
        }

        public IEnumerable<Event> RecommendEvents(int maxRecommendations = 5)
        {
#if NET6_0_OR_GREATER
            var pq = new PriorityQueue<Event, int>();
            foreach (var kv in searchTermCounts.OrderByDescending(k => k.Value))
            {
                string term = kv.Key;
                int weight = kv.Value;
                var matches = repo.SearchEvents(term).Take(10);
                foreach (var ev in matches)
                {
                    int score = Math.Max(1, weight * 100 - (int)((ev.Date - DateTime.Today).TotalDays));
                    int priority = int.MaxValue / 2 - score;
                    pq.Enqueue(ev, priority);
                }
            }

            if (pq.Count == 0)
                return repo.GetAllEvents().Where(e => e.Date >= DateTime.Today).OrderBy(e => e.Date).Take(maxRecommendations);

            var results = new List<Event>();
            var seen = new HashSet<Guid>();
            while (pq.Count > 0 && results.Count < maxRecommendations)
            {
                var ev = pq.Dequeue();
                if (ev != null && !seen.Contains(ev.Id))
                {
                    results.Add(ev);
                    seen.Add(ev.Id);
                }
            }

            return results;
#else
            // If not .NET 6+, consumer should replace usage with SimplePriorityQueue (see Utils/SimplePriorityQueue.cs)
            var fallback = new List<Event>();
            foreach (var term in TopSearchTerms(5))
            {
                fallback.AddRange(repo.SearchEvents(term).Take(3));
                if (fallback.Count >= maxRecommendations) break;
            }
            if (fallback.Count == 0)
                return repo.GetAllEvents().Where(e => e.Date >= DateTime.Today).OrderBy(e => e.Date).Take(maxRecommendations);
            return fallback.Distinct().Take(maxRecommendations);
#endif
        }
    }
}