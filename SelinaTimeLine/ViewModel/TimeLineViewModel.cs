using System;
using System.Collections.Generic;
using System.Windows;
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
        private bool _isLiveStreaming;
        private double _lowerValue;
        private double _upperValue;
        private bool _isRangeVisisble;

        public const double Tolerance = 0.00000001;


        public TimeSpan CurrentTimeSpan => _currentTimeSpan;

        public double CurrentTimeValue
        {
            get => _currentTimeSpan.TotalMilliseconds;
            set
            {
                var t = TimeSpan.FromMilliseconds(value);

                if (_currentTimeSpan == t)
                {
                    return;
                }

                _currentTimeSpan = t;

                RaisePropertyChanged();
                RaisePropertyChanged(() => CurrentTimeSpan);
            }
        }

        public List<PenaltyMarker> EventMarkers { get; set; }

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

        /// <summary>
        /// Connected to toggle button to show/hide range slider
        /// and reset Selected Range
        /// </summary>
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
                RaisePropertyChanged(() => LowerTimeSpan);
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
                RaisePropertyChanged(() => MaxValue);
            }
        }

        public double MaxValue => MaxTimeSpan.TotalMilliseconds;

        public double MinValue => 0;

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
                RaisePropertyChanged(() => UpperTimeSpan);
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

        public TimeLineViewModel()
        {
            //MaxValue = 200;
            MaxTimeSpan = new TimeSpan(3, 15, 30);

            CurrentValue = 30;
            IsLiveStreaming = false;

            EventMarkers = new List<PenaltyMarker>
            {
                new PenaltyMarker
                {
                    MarkerPosition = new TimeSpan(0, 10, 30),
                    Name = "Marker 1",
                },
                new PenaltyMarker
                {
                    MarkerPosition = new TimeSpan(1, 10, 30),
                    Name = "Marker 2",
                },
                new PenaltyMarker
                {
                    MarkerPosition = new TimeSpan(0, 12, 30),
                    Name = "Marker 3",
                },
                new PenaltyMarker
                {
                    MarkerPosition = new TimeSpan(0, 15, 00),
                    Name = "Marker 4",
                },
                new PenaltyMarker
                {
                    MarkerPosition = new TimeSpan(0, 17, 30),
                    Name = "Marker 5",
                }
            };
        }

        //public double ConvertTimeSpanToSliderValue(TimeSpan timeSpan)
        //{
        //    var sliderValuePerTick = MaxValue / MaxTimeSpan.Ticks;

        //    var sliderValue = timeSpan.Ticks * sliderValuePerTick;

        //    return sliderValue;
        //}

        #region Debug Stuff

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

        public TimeSpan LowerTimeSpan => TimeSpan.FromMilliseconds(LowerValue);

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

        public TimeSpan UpperTimeSpan => TimeSpan.FromMilliseconds(UpperValue);

        public Visibility LiveControlVisibility => IsLiveStreaming ? Visibility.Visible : Visibility.Collapsed;

        public Visibility TimeMarkerVisibility => IsLiveStreaming ? Visibility.Collapsed : Visibility.Visible;

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

        #endregion
    }

    public enum EventType
    {
        Goal,
        Foul
    }
}