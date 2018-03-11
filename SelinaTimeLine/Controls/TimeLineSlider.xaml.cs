using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SelinaTimeLine.Models;

namespace SelinaTimeLine.Controls
{
    /// <summary>
    /// Interaction logic for TimeLineSlider.xaml
    /// </summary>
    public partial class TimeLineSlider : UserControl
    {
        public static readonly DependencyProperty CurrentTimeSpanProperty = DependencyProperty.Register(
            "CurrentTimeSpan", typeof(TimeSpan), typeof(TimeLineSlider), new PropertyMetadata(default(TimeSpan)));

        //public static readonly DependencyProperty TimeMarkerVisibilityProperty = DependencyProperty.Register("TimeMarkerVisibility", typeof(Visibility), typeof(TimeLineSlider), new PropertyMetadata(default(Visibility)));
        public static readonly DependencyProperty MaxTimeSpanProperty = DependencyProperty.Register("MaxTimeSpan", typeof(TimeSpan), typeof(TimeLineSlider), new PropertyMetadata(new TimeSpan(2, 15, 0)));
        //public static readonly DependencyProperty LiveControlVisibilityProperty = DependencyProperty.Register("LiveControlVisibility", typeof(Visibility), typeof(TimeLineSlider), new PropertyMetadata(default(Visibility)));
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(double), typeof(TimeLineSlider), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty CurrentValueProperty = DependencyProperty.Register("CurrentValue", typeof(double), typeof(TimeLineSlider), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty IsLiveStreamingProperty = DependencyProperty.Register(
            "IsLiveStreaming", typeof(bool), typeof(TimeLineSlider), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsLiveRightNowProperty = DependencyProperty.Register("IsLiveRightNow", typeof(bool), typeof(TimeLineSlider), new PropertyMetadata(default(bool)));
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(TimeLineSlider), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty SelectionStartProperty = DependencyProperty.Register("SelectionStart", typeof(double), typeof(TimeLineSlider), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty SelectionEndProperty = DependencyProperty.Register("SelectionEnd", typeof(double), typeof(TimeLineSlider), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty EventMarkersProperty = DependencyProperty.Register("EventMarkers", typeof(List<PenaltyMarker>), typeof(TimeLineSlider), new PropertyMetadata(default(List<PenaltyMarker>)));

        public bool IsLiveStreaming
        {
            get => (bool) GetValue(IsLiveStreamingProperty);
            set => SetValue(IsLiveStreamingProperty, value);
        }

        public TimeSpan CurrentTimeSpan
        {
            get => (TimeSpan) GetValue(CurrentTimeSpanProperty);
            set => SetValue(CurrentTimeSpanProperty, value);
        }

        //public Visibility LiveControlVisibility => IsLiveStreaming ? Visibility.Visible : Visibility.Collapsed;

        //public Visibility TimeMarkerVisibility => IsLiveStreaming ? Visibility.Collapsed : Visibility.Visible;

        public TimeSpan MaxTimeSpan
        {
            get => (TimeSpan) GetValue(MaxTimeSpanProperty);
            set => SetValue(MaxTimeSpanProperty, value);
        }


        public double MaxValue
        {
            get => (double) GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public double CurrentValue
        {
            get => (double) GetValue(CurrentValueProperty);
            set => SetValue(CurrentValueProperty, value);
        }

        public bool IsLiveRightNow
        {
            get => (bool) GetValue(IsLiveRightNowProperty);
            set => SetValue(IsLiveRightNowProperty, value);
        }

        public double Minimum
        {
            get => (double) GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public double SelectionStart
        {
            get => (double) GetValue(SelectionStartProperty);
            set => SetValue(SelectionStartProperty, value);
        }

        public double SelectionEnd
        {
            get => (double) GetValue(SelectionEndProperty);
            set => SetValue(SelectionEndProperty, value);
        }

        public List<PenaltyMarker> EventMarkers
        {
            get => (List<PenaltyMarker>) GetValue(EventMarkersProperty);
            set => SetValue(EventMarkersProperty, value);
        }

        public TimeLineSlider()
        {
            InitializeComponent();
        }

        //public double ConvertTimeSpanToSliderValue(TimeSpan timeSpan)
        //{
        //    var sliderValuePerTick = MaxValue / MaxTimeSpan.Ticks;

        //    var sliderValue = timeSpan.Ticks * sliderValuePerTick;

        //    return sliderValue;
        //}

        //public TimeSpan ConvertSliderValueToTimeSpan(double sliderValue)
        //{
        //    var tickPerSliderValue = MaxTimeSpan.Ticks / MaxValue;

        //    var timeSpanTicks = tickPerSliderValue * sliderValue;

        //    var timeSpan = new TimeSpan((long)timeSpanTicks);

        //    return timeSpan;
        //}

        //private static void CurrentValueChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        //{
        //    //var timeSpan = ConvertSliderValueToTimeSpan((double)dependencyObject.GetValue(CurrentValueProperty));
        //    //SetValue(CurrentTimeSpanProperty, timeSpan);
        //    var slider = dependencyObject as TimeLineSlider;

        //    slider?.SetCurrntTimespan(slider.CurrentValue);
        //}

        //private void SetCurrntTimespan(double sliderCurrentValue)
        //{
        //    CurrentTimeSpan = ConvertSliderValueToTimeSpan(sliderCurrentValue);
        //}
    }
}
