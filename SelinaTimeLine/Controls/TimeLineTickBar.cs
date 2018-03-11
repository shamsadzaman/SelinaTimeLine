using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using SelinaTimeLine.Models;
using SelinaTimeLine.ViewModel;

namespace SelinaTimeLine.Controls
{
    public class TimeLineTickBar : TickBar
    {
        public static readonly DependencyProperty EventMarkersProperty = DependencyProperty.Register("EventMarkers", typeof(List<EventMarker>), typeof(TimeLineTickBar), new PropertyMetadata(default(List<EventMarker>)));
        public static readonly DependencyProperty GoalProperty = DependencyProperty.Register("Goal", typeof(Canvas), typeof(TimeLineTickBar), new PropertyMetadata(default(Canvas), GPropertyChangedCallback));

        private static void GPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyObject is TimeLineTickBar d)
            {
                GoalEventMarker = GetDrawingImageFrom(d.Goal);
            }
        }

        public static readonly DependencyProperty FoulProperty = DependencyProperty.Register("Foul", typeof(Canvas), typeof(TimeLineTickBar), new PropertyMetadata(default(Canvas), FPropertyChangedCallback));

        private static void FPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyObject is TimeLineTickBar d)
            {
                FoulEventMarker = GetDrawingImageFrom(d.Foul);
            }
        }

        public List<EventMarker> EventMarkers
        {
            get => (List<EventMarker>)GetValue(EventMarkersProperty);
            set => SetValue(EventMarkersProperty, value);
        }

        private static DrawingImage FoulEventMarker { get; set; }

        private static DrawingImage GoalEventMarker { get; set; }

        public Canvas Goal
        {
            get { return (Canvas) GetValue(GoalProperty); }
            set { SetValue(GoalProperty, value); }
        }

        public Canvas Foul
        {
            get { return (Canvas) GetValue(FoulProperty); }
            set { SetValue(FoulProperty, value); }
        }


        private static DrawingImage GetDrawingImageFrom(Canvas path)
        {
            //var path = Foul; //(FrameworkElement)Application.Current.FindResource(resourceName);
            var vb = new VisualBrush(path);
            var gd = new GeometryDrawing(
                vb,
                new Pen(Brushes.Transparent, 0),
                new RectangleGeometry(new Rect(0, 0, path.Width, path.Height)));
            var di = new DrawingImage(gd);

            // di.Freeze();
            return di;
        }

        //private DrawingImage GetDrawingImageFrom(string resourceName)
        //{
        //    var path = (FrameworkElement)Application.Current.FindResource(resourceName);
        //    var vb = new VisualBrush(path);
        //    var gd = new GeometryDrawing(
        //        vb,
        //        new Pen(Brushes.Transparent, 0),
        //        new RectangleGeometry(new Rect(0, 0, path.Width, path.Height)));
        //    var di = new DrawingImage(gd);

        //    // di.Freeze();
        //    return di;
        //}

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
            //var geo = new PathGeometry
            //{
            //    FillRule = FillRule.Nonzero,
            //    Figures = new PathFigureCollection
            //    {
            //        new PathFigure
            //        {
            //            // create triangle with (0,0), (6,15), (-6,15) as base
            //            // after that xPosition just moves those triangle horizontally
            //            StartPoint = new Point(0 + xMargin + xPosition, 0 + yMargin),
            //            Segments = new PathSegmentCollection
            //            {
            //                new LineSegment
            //                {
            //                    Point = new Point(6 + xMargin + xPosition, 15 + yMargin)
            //                },
            //                new LineSegment
            //                {
            //                    Point = new Point(-6 + xMargin + xPosition, 15 + yMargin)
            //                },
            //                new LineSegment
            //                {
            //                    Point = new Point(0 + xMargin + xPosition, 0 + yMargin)
            //                }
            //            }
            //        }
            //    }
            //};

            //var fillColor = eventType == EventType.Foul ? Colors.Red : Colors.Blue;
            //var borderColor = eventType == EventType.Foul ? Colors.DarkRed : Colors.DarkBlue;


            //dc.DrawGeometry(new SolidColorBrush(fillColor), new Pen(new SolidColorBrush(borderColor), 1), geo);

            var markerDrawing = eventType == EventType.Foul ? FoulEventMarker : GoalEventMarker;

            if (markerDrawing == null)
            {
                return;
            }

            var rect = new Rect(xPosition, yMargin, markerDrawing.Width, markerDrawing.Height);
            dc.DrawImage(markerDrawing, rect);
        }
    }
}