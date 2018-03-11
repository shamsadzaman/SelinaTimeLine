using System;

namespace SelinaTimeLine.Models
{
    public class GoalMarker : IMarker
    {
        public string Name { get; set; }

        public TimeSpan MarkerPosition { get; set; }
    }
}