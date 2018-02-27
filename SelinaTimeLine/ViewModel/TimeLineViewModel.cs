using System;
using System.Windows;
using GalaSoft.MvvmLight;

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

        public TimeLineViewModel()
        {
            MaxValue = 200;
            MaxTimeSpan = new TimeSpan(0, 15, 30);

            CurrentValue = 30;
            IsLiveStreaming = false;
        }

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