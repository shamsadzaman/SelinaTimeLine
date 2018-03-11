using System;

namespace SelinaTimeLine.Models
{
    public interface IMarker
    {
        string Name { get; set; }

        TimeSpan MarkerPosition { get; set; }
    }
}