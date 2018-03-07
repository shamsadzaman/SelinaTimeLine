using System;
using System.Windows;
using System.Windows.Controls;

namespace SelinaTimeLine.Controls
{
    /// <summary>
    /// Interaction logic for RangeSlider.xaml
    /// </summary>
    public partial class RangeSlider : UserControl
    {
        public RangeSlider()
        {
            InitializeComponent();
            Loaded += RangeSlider_Loaded;
            LayoutUpdated += OnLayoutUpdated;
        }

        private void OnLayoutUpdated(object sender, EventArgs eventArgs)
        {
            //SetProgressBorder();
        }

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(0d));

        public double LowerValue
        {
            get => (double)GetValue(LowerValueProperty);
            set => SetValue(LowerValueProperty, value);
        }

        public static readonly DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(0d));

        public double UpperValue
        {
            get => (double)GetValue(UpperValueProperty);
            set => SetValue(UpperValueProperty, value);
        }

        public static readonly DependencyProperty UpperValueProperty =
            DependencyProperty.Register("UpperValue", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(0d));

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(RangeSlider), new UIPropertyMetadata(1d));

        void RangeSlider_Loaded(object sender, RoutedEventArgs e)
        {
            LowerSlider.ValueChanged += LowerSlider_ValueChanged;
            UpperSlider.ValueChanged += UpperSlider_ValueChanged;
        }

        private void LowerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpperSlider.Value = Math.Max(UpperSlider.Value, LowerSlider.Value);
            //SetProgressBorder();
        }

        private void UpperSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LowerSlider.Value = Math.Min(UpperSlider.Value, LowerSlider.Value);
            //SetProgressBorder();
        }

        //private void SetProgressBorder()
        //{
        //    var lowerPoint = (this.ActualWidth * (LowerValue - Minimum)) / (Maximum - Minimum);
        //    var upperPoint = (this.ActualWidth * (UpperValue - Minimum)) / (Maximum - Minimum);
        //    upperPoint = this.ActualWidth - upperPoint;
        //    RangeBorder.Margin = new Thickness(lowerPoint, 0, upperPoint, 0);
        //    //RangeBorder.Width = upperPoint;
        //}
    }
}
