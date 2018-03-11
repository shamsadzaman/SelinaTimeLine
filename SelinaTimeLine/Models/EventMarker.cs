using System;
using System.Windows.Media;
using SelinaTimeLine.ViewModel;

namespace SelinaTimeLine.Models
{
    public class EventMarker
    {
        public string Name { get; set; }

        public ImageDrawing Icon { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public EventType EventType { get; set; }
    }
}