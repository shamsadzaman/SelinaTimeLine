using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace SelinaTimeLine.Controls
{
    public class TimeLineTickBar : TickBar
    {
        protected override void OnRender(DrawingContext dc)
        {
            //base.OnRender(dc);

            //dc.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 20), new Point(6, 0), new Point(6, 15));

            var xMargin = 6;
            var yMargin = 3;

            // subtract margin from left/right side to get the width of
            // the slider line
            var xPerValue = (ActualWidth - (xMargin * 2)) / Maximum;

            var value = 200;

            var xPosition = xPerValue * value;

            Console.WriteLine("xPosition: " + xPosition);

            var geo = new PathGeometry
            {
                Figures = new PathFigureCollection
                {
                    new PathFigure
                    {
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

            dc.DrawGeometry(new SolidColorBrush(Colors.Red), new Pen(new SolidColorBrush(Colors.Red), 1), geo);
        }
    }
}