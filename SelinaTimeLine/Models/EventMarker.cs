using System;
using System.Windows.Media;

namespace SelinaTimeLine.Models
{
    public class EventMarker
    {
        public string Name { get; set; }

        public ImageDrawing Icon { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}