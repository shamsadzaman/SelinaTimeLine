using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using SelinaTimeLine.Models;

namespace SelinaTimeLine.Controls
{
    public class TimeLineTickBar : TickBar
    {
        private const int XMargin = 6;
        private const int YMargin = -26;

        public static readonly DependencyProperty EventMarkersProperty = DependencyProperty.Register("EventMarkers", typeof(List<IMarker>), typeof(TimeLineTickBar), new PropertyMetadata(default(List<IMarker>)));
        public static readonly DependencyProperty GoalProperty = DependencyProperty.Register("Goal", typeof(Canvas), typeof(TimeLineTickBar), new PropertyMetadata(default(Canvas), GPropertyChangedCallback));

        private static void GPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyObject is TimeLineTickBar d)
            {
                GoalMarkerIcon = GetDrawingImageFrom(d.Goal);
            }
        }

        public static readonly DependencyProperty PenaltyProperty = DependencyProperty.Register("Penalty", typeof(Canvas), typeof(TimeLineTickBar), new PropertyMetadata(default(Canvas), FPropertyChangedCallback));

        private static void FPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyObject is TimeLineTickBar d)
            {
                PenaltyMarkerIcon = GetDrawingImageFrom(d.Penalty);
            }
        }

        public List<IMarker> EventMarkers
        {
            get => (List<IMarker>)GetValue(EventMarkersProperty);
            set => SetValue(EventMarkersProperty, value);
        }

        //todo: rename foul to penalty
        private static DrawingImage PenaltyMarkerIcon { get; set; }

        private static DrawingImage GoalMarkerIcon { get; set; }

        public Canvas Goal
        {
            get => (Canvas) GetValue(GoalProperty);
            set => SetValue(GoalProperty, value);
        }

        public Canvas Penalty
        {
            get => (Canvas) GetValue(PenaltyProperty);
            set => SetValue(PenaltyProperty, value);
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

            

            // subtract margin from left/right side to get the width of
            // the slider line
            var xPerValue = (ActualWidth - (XMargin * 2)) / Maximum;

            Console.WriteLine("xPerValue: " + xPerValue);

            foreach (var eventMarker in EventMarkers)
            {
                if (eventMarker.MarkerPosition.TotalMilliseconds > Maximum)
                {
                    // ignore marker that's out of max range
                    continue;
                }

                var xPosition = xPerValue * eventMarker.MarkerPosition.TotalMilliseconds;

                Console.WriteLine("(ActualWidth - (xMargin * 2)): " + (ActualWidth - (XMargin * 2)));
                Console.WriteLine("eventMarker.MarkerPosition.Milliseconds / Maximum" + eventMarker.MarkerPosition.TotalMilliseconds );
                Console.WriteLine("xPosition: " + xPosition);

                DrawEventMarker(dc, XMargin, xPosition, YMargin, eventMarker);
            }
        }

        private static void DrawEventMarker(DrawingContext dc, int xMargin, double xPosition, int yMargin, IMarker type)
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

            var markerDrawing = type is PenaltyMarker ? PenaltyMarkerIcon : GoalMarkerIcon;

            if (markerDrawing == null)
            {
                return;
            }
            
            var rect = new Rect(xPosition + xMargin, yMargin, markerDrawing.Width, markerDrawing.Height);
            dc.DrawImage(markerDrawing, rect);
        }
    }
}