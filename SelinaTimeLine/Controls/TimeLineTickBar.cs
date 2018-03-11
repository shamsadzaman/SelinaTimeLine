using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using SelinaTimeLine.Models;
using SelinaTimeLine.ViewModel;

namespace SelinaTimeLine.Controls
{
    public class TimeLineTickBar : TickBar
    {
        public static readonly DependencyProperty EventMarkersProperty = DependencyProperty.Register("EventMarkers", typeof(List<EventMarker>), typeof(TimeLineTickBar), new PropertyMetadata(default(List<EventMarker>)));

        protected override void OnRender(DrawingContext dc)
        {
            //base.OnRender(dc);

            if (EventMarkers == null)
            {
                return;
            }

            var xMargin = 6;
            var yMargin = -7;

            // subtract margin from left/right side to get the width of
            // the slider line
            var xPerValue = (ActualWidth - (xMargin * 2)) / Maximum;

            Console.WriteLine("xPerValue: " + xPerValue);


            foreach (var eventMarker in EventMarkers)
            {
                if (eventMarker.FromTime.TotalMilliseconds > Maximum)
                {
                    // ignore marker that's out of max range
                    continue;
                }

                var xPosition = xPerValue * eventMarker.FromTime.TotalMilliseconds;

                Console.WriteLine("(ActualWidth - (xMargin * 2)): " + (ActualWidth - (xMargin * 2)));
                Console.WriteLine("eventMarker.FromTime.Milliseconds / Maximum" + eventMarker.FromTime.TotalMilliseconds );
                Console.WriteLine("xPosition: " + xPosition);

                DrawEventMarker(dc, xMargin, xPosition, yMargin, eventMarker.EventType);
            }
        }

        private static void DrawEventMarker(DrawingContext dc, int xMargin, double xPosition, int yMargin, EventType eventType)
        {
            //todo: Get the drawing/icon from EventMarker
            var geo = new PathGeometry
            {
                FillRule = FillRule.Nonzero,
                Figures = new PathFigureCollection
                {
                    new PathFigure
                    {
                        // create triangle with (0,0), (6,15), (-6,15) as base
                        // after that xPosition just moves those triangle horizontally
                        StartPoint = new Point(0 + xMargin + xPosition, 0 + yMargin),
                        Segments = new PathSegmentCollection
                        {
                            new LineSegment
                            {
                                Point = new Point(6 + xMargin + xPosition, 15 + yMargin)
                            },
                            new LineSegment
                            {
                                Point = new Point(-6 + xMargin + xPosition, 15 + yMargin)
                            },
                            new LineSegment
                            {
                                Point = new Point(0 + xMargin + xPosition, 0 + yMargin)
                            }
                        }
                    }
                }
            };

            var fillColor = eventType == EventType.Foul ? Colors.Red : Colors.Blue;
            var borderColor = eventType == EventType.Foul ? Colors.DarkRed : Colors.DarkBlue;


            dc.DrawGeometry(new SolidColorBrush(fillColor), new Pen(new SolidColorBrush(borderColor), 1), geo);
        }

        public List<EventMarker> EventMarkers
        {
            get => (List<EventMarker>) GetValue(EventMarkersProperty);
            set => SetValue(EventMarkersProperty, value);
        }
    }
}