using System;

namespace SelinaTimeLine.Models
{
    public class PenaltyMarker : IMarker
    {
        public string Name { get; set; }

        public TimeSpan MarkerPosition { get; set; }
    }
}