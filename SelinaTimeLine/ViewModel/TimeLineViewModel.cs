using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using GalaSoft.MvvmLight;
using SelinaTimeLine.Models;

namespace SelinaTimeLine.ViewModel
{
    public class TimeLineViewModel : ViewModelBase
    {
        private double _currentValue;
        private TimeSpan _currentTimeSpan;
        private int _maxSecond;
        private int _maxMinute;
        private int _maxHour;
        private TimeSpan _maxTimeSpan;
        private double _maxValue;
        private bool _isLiveStreaming;
        private double _lowerValue;
        private double _upperValue;
        private bool _isRangeVisisble;

        public double MaxValue
        {
            get => _maxValue;
            set
            {
                if (Math.Abs(_maxValue - value) < .0000001)
                {
                    return;
                }

                _maxValue = value;
                RaisePropertyChanged();
            }
        }

        public double CurrentValue
        {
            get => _currentValue;
            set
            {
                if (Math.Abs(_currentValue - value) < .000001)
                {
                    return;
                }

                _currentValue = value;
                _currentTimeSpan = ConvertSliderValueToTimeSpan(_currentValue);

                RaisePropertyChanged();
                RaisePropertyChanged(() => CurrentTimeSpan);
            }
        }

        public TimeSpan MaxTimeSpan
        {
            get => _maxTimeSpan;
            set
            {
                if (_maxTimeSpan == value)
                {
                    return;
                }

                _maxTimeSpan = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan CurrentTimeSpan
        {
            get => _currentTimeSpan;
            set
            {
                if (_currentTimeSpan == value)
                {
                    return;
                }

                _currentTimeSpan = value;
                //_currentValue = ConvertTimeSpanToSliderValue(_currentTimeSpan);

                RaisePropertyChanged();
                //RaisePropertyChanged(() => CurrentValue);
            }
        }

        public int MaxHour
        {
            get => _maxHour;
            set
            {
                _maxHour = value;
                InitializeMaxTimeSpan();
            }
        }

        public int MaxMinute
        {
            get => _maxMinute;
            set
            {
                _maxMinute = value;
                InitializeMaxTimeSpan();
            }
        }

        public int MaxSecond
        {
            get => _maxSecond;
            set
            {
                _maxSecond = value;
                InitializeMaxTimeSpan();
            }
        }

        public Visibility LiveControlVisibility => IsLiveStreaming ? Visibility.Visible : Visibility.Collapsed;

        public Visibility TimeMarkerVisibility => IsLiveStreaming ? Visibility.Collapsed : Visibility.Visible;

        public bool IsLiveStreaming
        {
            get => _isLiveStreaming;
            set
            {
                if (_isLiveStreaming == value)
                {
                    return;
                }

                _isLiveStreaming = value;
                RaisePropertyChanged();
            }
        }

        public double Minimum => 0;

        public double LowerValue
        {
            get => _lowerValue;
            set
            {
                if (Math.Abs(_lowerValue - value) < .00000001)
                {
                    return;
                }

                _lowerValue = value;
                RaisePropertyChanged();
            }
        }

        public double UpperValue
        {
            get => _upperValue;
            set
            {
                if (Math.Abs(_upperValue - value) < Tolerance)
                {
                    return;
                }

                _upperValue = value;
                RaisePropertyChanged();
            }
        }

        public bool IsRangeVisisble
        {
            get => _isRangeVisisble;
            set
            {
                if (_isRangeVisisble == value)
                {
                    return;
                }

                _isRangeVisisble = value;
                SetRangeValue(value);
                RaisePropertyChanged();
            }
        }

        private void SetRangeValue(bool isRangeVisible)
        {
            if (!isRangeVisible)
            {
                LowerValue = 0;
                UpperValue = 0;
            }
        }

        public const double Tolerance = 0.00000001;

        public TimeLineViewModel()
        {
            MaxValue = 200;
            MaxTimeSpan = new TimeSpan(0, 15, 30);

            CurrentValue = 30;
            IsLiveStreaming = false;

            EventMarkers = new List<EventMarker>
            {
                new EventMarker
                {
                    FromTime = new TimeSpan(0, 10, 30),
                    ToTime = new TimeSpan(0, 15, 30),
                    Name = "Marker 1"
                },
                new EventMarker
                {
                    FromTime = new TimeSpan(1, 10, 30),
                    ToTime = new TimeSpan(1, 15, 30),
                    Name = "Marker 2"
                },
                new EventMarker
                {
                    FromTime = new TimeSpan(0, 12, 30),
                    ToTime = new TimeSpan(0, 20, 30),
                    Name = "Marker 3"
                },
                new EventMarker
                {
                    FromTime = new TimeSpan(0, 15, 00),
                    ToTime = new TimeSpan(0, 16, 30),
                    Name = "Marker 4"
                },
                new EventMarker
                {
                    FromTime = new TimeSpan(0, 17, 30),
                    ToTime = new TimeSpan(0, 19, 30),
                    Name = "Marker 1"
                }
            };
        }

        public List<EventMarker> EventMarkers { get; set; }

        //public double ConvertTimeSpanToSliderValue(TimeSpan timeSpan)
        //{
        //    var sliderValuePerTick = MaxValue / MaxTimeSpan.Ticks;

        //    var sliderValue = timeSpan.Ticks * sliderValuePerTick;

        //    return sliderValue;
        //}

        public TimeSpan ConvertSliderValueToTimeSpan(double sliderValue)
        {
            var tickPerSliderValue = MaxTimeSpan.Ticks / MaxValue;

            var timeSpanTicks = tickPerSliderValue * sliderValue;

            var timeSpan = new TimeSpan((long)timeSpanTicks);

            return timeSpan;
        }

        private void InitializeMaxTimeSpan()
        {
            MaxTimeSpan = new TimeSpan(MaxHour, MaxMinute, MaxSecond);
        }
    }
}