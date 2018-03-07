using System;
using System.Windows;
using System.Windows.Controls;

namespace SelinaTimeLine.Controls
{
    /// <summary>
    /// Interaction logic for TimeDisplay.xaml
    /// </summary>
    public partial class TimeDisplay : UserControl
    {
        public static readonly DependencyProperty IsLiveStreamingProperty = DependencyProperty.Register("IsLiveStreaming", typeof(bool), typeof(TimeDisplay), new PropertyMetadata(default(bool)));
        public static readonly DependencyProperty IsLiveRightNowProperty = DependencyProperty.Register("IsLiveRightNow", typeof(bool), typeof(TimeDisplay), new PropertyMetadata(default(bool)));
        public static readonly DependencyProperty CurrentTimeSpanProperty = DependencyProperty.Register("CurrentTimeSpan", typeof(TimeSpan), typeof(TimeDisplay), new PropertyMetadata(default(TimeSpan)));
        public static readonly DependencyProperty MaxTimeSpanProperty = DependencyProperty.Register("MaxTimeSpan", typeof(TimeSpan), typeof(TimeDisplay), new PropertyMetadata(default(TimeSpan)));

        public TimeDisplay()
        {
            InitializeComponent();
        }

        public bool IsLiveStreaming
        {
            get => (bool) GetValue(IsLiveStreamingProperty);
            set => SetValue(IsLiveStreamingProperty, value);
        }

        public bool IsLiveRightNow
        {
            get => (bool) GetValue(IsLiveRightNowProperty);
            set => SetValue(IsLiveRightNowProperty, value);
        }

        public TimeSpan CurrentTimeSpan
        {
            get => (TimeSpan) GetValue(CurrentTimeSpanProperty);
            set => SetValue(CurrentTimeSpanProperty, value);
        }

        public TimeSpan MaxTimeSpan
        {
            get => (TimeSpan) GetValue(MaxTimeSpanProperty);
            set => SetValue(MaxTimeSpanProperty, value);
        }
    }
}
