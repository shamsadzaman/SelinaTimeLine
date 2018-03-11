using System;

namespace SelinaTimeLine.Models
{
    public interface IMarker
    {
        TimeSpan MarkerPosition { get; set; }
    }
}