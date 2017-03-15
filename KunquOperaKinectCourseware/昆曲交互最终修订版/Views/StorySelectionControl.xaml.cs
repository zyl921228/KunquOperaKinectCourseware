//------------------------------------------------------------------------------
// <copyright file="StorySelectionControl.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.BasicInteractions
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;
    using Microsoft.Samples.Kinect.BasicInteractions.Properties;

    /// <summary>
    /// CategoryControl.xaml 的交互逻辑
    /// </summary>
    public partial class StorySelectionControl : UserControl
    {
        // Using a DependencyProperty as the backing store for ScrollUpVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollUpVisibilityProperty =
            DependencyProperty.Register("ScrollUpVisibility", typeof(Visibility), typeof(StorySelectionControl), new UIPropertyMetadata(Visibility.Hidden));

        // Using a DependencyProperty as the backing store for ScrollUpVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollDownVisibilityProperty =
            DependencyProperty.Register("ScrollDownVisibility", typeof(Visibility), typeof(StorySelectionControl), new UIPropertyMetadata(Visibility.Hidden));


        private readonly DispatcherTimer scrollTimer;
        private double scrollingOffset;
        private bool scrollingUp;
        /// <summary>
        /// 在构造函数中设置好计时器的各项属性,并添加上下滚动的交互区域(UpScrollRegion DownScrollRegion),调用相应的方法.
        /// </summary>
        public StorySelectionControl()
        {
            this.InitializeComponent();

            if (Settings.Default.ShowScrollRegions)
            {
                this.scrollTimer = new DispatcherTimer();
                double scrollingTick = 100 - Settings.Default.ScrollSpeed;
                this.scrollTimer.Interval = TimeSpan.FromMilliseconds(scrollingTick);
                this.scrollTimer.Tick += this.ScrollTimer_Tick;
                KinectController.AddPreviewHandEnterHandler(this.UpScrollRegion, this.OnPreviewHandEnterUp);
                KinectController.AddPreviewHandLeaveHandler(this.UpScrollRegion, this.OnPreviewHandLeaveUp);
                KinectController.AddPreviewHandEnterHandler(this.DownScrollRegion, this.OnPreviewHandEnterDown);
                KinectController.AddPreviewHandLeaveHandler(this.DownScrollRegion, this.OnPreviewHandLeaveDown);
                ContentScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                this.ContentScrollViewer.ScrollChanged += new ScrollChangedEventHandler(this.ContentScrollViewer_ScrollChanged);
            }
            else
            {
            }
        }
        public Visibility ScrollDownVisibility
        {
            get { return (Visibility)GetValue(ScrollDownVisibilityProperty); }
            set { this.SetValue(ScrollDownVisibilityProperty, value); }
        }

        public Visibility ScrollUpVisibility
        {
            get { return (Visibility)GetValue(ScrollUpVisibilityProperty); }
            set { this.SetValue(ScrollUpVisibilityProperty, value); }
        }
        /// <summary>
        /// 计时器每隔一小段时间发挥作用,先判断是向上还是向下,然后进行滚动,同时加强健壮性.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            this.scrollingOffset += this.scrollingUp ? -20 : 20;
            if (this.scrollingOffset < 0)
            {
                this.scrollingOffset = 0;
            }

            this.ContentScrollViewer.ScrollToVerticalOffset(this.scrollingOffset);
        }

        private void ContentScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            this.ScrollUpVisibility = (this.ContentScrollViewer.VerticalOffset <= 0) ? Visibility.Hidden : Visibility.Visible;
            this.ScrollDownVisibility = (this.ContentScrollViewer.VerticalOffset + this.ContentScrollViewer.ViewportHeight < this.ContentScrollViewer.ExtentHeight) ? Visibility.Visible : Visibility.Hidden;
        }

        private void OnPreviewHandEnterUp(object sender, HandInputEventArgs args)
        {
            // start scrolling up
            this.UpScrollRegion.Background = Application.Current.Resources["ActiveScrollRegionBrush"] as Brush;
            this.scrollingOffset = this.ContentScrollViewer.VerticalOffset;
            this.scrollingUp = true;
            this.scrollTimer.Start();
            ContentScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        private void OnPreviewHandLeaveUp(object sender, HandInputEventArgs args)
        {
            // stop scrolling up
            this.UpScrollRegion.Background = Application.Current.Resources["InactiveScrollRegionBrush"] as Brush;
            this.scrollTimer.Stop();
            ContentScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }

        private void OnPreviewHandEnterDown(object sender, HandInputEventArgs args)
        {
            // start scrolling down
            this.DownScrollRegion.Background = Application.Current.Resources["ActiveScrollRegionBrush"] as Brush;
            this.scrollingOffset = this.ContentScrollViewer.VerticalOffset;
            this.scrollingUp = false;
            this.scrollTimer.Start();
            ContentScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        private void OnPreviewHandLeaveDown(object sender, HandInputEventArgs args)
        {
            // stop scrolling down
            this.DownScrollRegion.Background = Application.Current.Resources["InactiveScrollRegionBrush"] as Brush;
            this.scrollTimer.Stop();
            ContentScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }
        /// <summary>
        /// 利用StackPanel的特点,让功能先判断当前界面内容之后显示相应内容.
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <param name="duration"></param>
        public void AnimateIn(int selectedItem,Duration duration)
        {
            var sb = new Storyboard();
            var animateOpacity = new DoubleAnimation(0, 1, duration);
            Storyboard.SetTarget(animateOpacity, this);
            Storyboard.SetTargetProperty(animateOpacity, new PropertyPath("Opacity"));
            sb.Children.Add(animateOpacity);
            if (selectedItem == 1)
            {
                _3_1起源.Opacity = 1;
                _3_1起源.IsHitTestVisible = true;
                _3_2发展.Opacity = 0;
                _3_2发展.IsHitTestVisible = false;
                _3_3兴盛.Opacity = 0;
                _3_3兴盛.IsHitTestVisible = false;
                _3_4衰落.Opacity = 0;
                _3_4衰落.IsHitTestVisible = false;
                _3_5复兴.Opacity = 0;
                _3_5复兴.IsHitTestVisible = false;
                Storyboard storyboard = (Storyboard)this.FindResource("onGoing1");
                storyboard.Begin(this);
                Stack1.Visibility = Visibility.Visible;
                Stack2.Visibility = Visibility.Collapsed;
                Stack3.Visibility = Visibility.Collapsed;
                Stack4.Visibility = Visibility.Collapsed;
                Stack5.Visibility = Visibility.Collapsed;
                Stack6.Visibility = Visibility.Collapsed;
                Stack7.Visibility = Visibility.Collapsed;
                SpeechMediaElement.Source = new Uri(@"1_起源地.mp3", UriKind.Relative);
                SpeechMediaElement.Play();
            }
            else if (selectedItem == 2)
            {
                _3_1起源.Opacity = 0;
                _3_1起源.IsHitTestVisible = false;
                _3_2发展.Opacity = 1;
                _3_2发展.IsHitTestVisible = true;
                _3_3兴盛.Opacity = 0;
                _3_3兴盛.IsHitTestVisible = false;
                _3_4衰落.Opacity = 0;
                _3_4衰落.IsHitTestVisible = false;
                _3_5复兴.Opacity = 0;
                _3_5复兴.IsHitTestVisible = false;
                Storyboard storyboard = (Storyboard)this.FindResource("onGoing2");
                storyboard.Begin(this);
                Stack1.Visibility = Visibility.Collapsed;
                Stack2.Visibility = Visibility.Collapsed;
                Stack3.Visibility = Visibility.Collapsed;
                Stack4.Visibility = Visibility.Visible;
                Stack5.Visibility = Visibility.Collapsed;
                Stack6.Visibility = Visibility.Collapsed;
                Stack7.Visibility = Visibility.Collapsed;
                SpeechMediaElement.Source = new Uri(@"4_发展.mp3", UriKind.Relative);
                SpeechMediaElement.Play();
            }
            else if (selectedItem == 3)
            {
                _3_1起源.Opacity = 0;
                _3_1起源.IsHitTestVisible = false;
                _3_2发展.Opacity = 0;
                _3_2发展.IsHitTestVisible = false;
                _3_3兴盛.Opacity = 1;
                _3_3兴盛.IsHitTestVisible = true;
                _3_4衰落.Opacity = 0;
                _3_4衰落.IsHitTestVisible = false;
                _3_5复兴.Opacity = 0;
                _3_5复兴.IsHitTestVisible = false;
                Storyboard storyboard = (Storyboard)this.FindResource("onGoing3");
                storyboard.Begin(this);
                Stack1.Visibility = Visibility.Collapsed;
                Stack2.Visibility = Visibility.Collapsed;
                Stack3.Visibility = Visibility.Collapsed;
                Stack4.Visibility = Visibility.Collapsed;
                Stack5.Visibility = Visibility.Visible;
                Stack6.Visibility = Visibility.Collapsed;
                Stack7.Visibility = Visibility.Collapsed;
                SpeechMediaElement.Source = new Uri(@"5_兴盛.mp3", UriKind.Relative);
                SpeechMediaElement.Play();
            }
            else if (selectedItem == 4)
            {
                _3_1起源.Opacity = 0;
                _3_1起源.IsHitTestVisible = false;
                _3_2发展.Opacity = 0;
                _3_2发展.IsHitTestVisible = false;
                _3_3兴盛.Opacity = 0;
                _3_3兴盛.IsHitTestVisible = false;
                _3_4衰落.Opacity = 1;
                _3_4衰落.IsHitTestVisible = true;
                _3_5复兴.Opacity = 0;
                _3_5复兴.IsHitTestVisible = false;
                Storyboard storyboard = (Storyboard)this.FindResource("onGoing4");
                storyboard.Begin(this);
                Stack1.Visibility = Visibility.Collapsed;
                Stack2.Visibility = Visibility.Collapsed;
                Stack3.Visibility = Visibility.Collapsed;
                Stack4.Visibility = Visibility.Collapsed;
                Stack5.Visibility = Visibility.Collapsed;
                Stack6.Visibility = Visibility.Visible;
                Stack7.Visibility = Visibility.Collapsed;
                SpeechMediaElement.Source = new Uri(@"6_衰败.mp3", UriKind.Relative);
                SpeechMediaElement.Play();
            }
            else if (selectedItem == 5)
            {
                _3_1起源.Opacity = 0;
                _3_1起源.IsHitTestVisible = false;
                _3_2发展.Opacity = 0;
                _3_2发展.IsHitTestVisible = false;
                _3_3兴盛.Opacity = 0;
                _3_3兴盛.IsHitTestVisible = false;
                _3_4衰落.Opacity = 0;
                _3_4衰落.IsHitTestVisible = false;
                _3_5复兴.Opacity = 1;
                _3_5复兴.IsHitTestVisible = true;
                Storyboard storyboard = (Storyboard)this.FindResource("onGoing5");
                storyboard.Begin(this);
                Stack1.Visibility = Visibility.Collapsed;
                Stack2.Visibility = Visibility.Collapsed;
                Stack3.Visibility = Visibility.Collapsed;
                Stack4.Visibility = Visibility.Collapsed;
                Stack5.Visibility = Visibility.Collapsed;
                Stack6.Visibility = Visibility.Collapsed;
                Stack7.Visibility = Visibility.Visible;
                SpeechMediaElement.Source = new Uri(@"7_复兴.mp3", UriKind.Relative);
                SpeechMediaElement.Play();
            }
            this.ContentScrollViewer.ScrollToTop();
            this.IsHitTestVisible = true;
            sb.Begin();
        }
        /// <summary>
        /// 隐藏当前三级界面
        /// </summary>
        /// <param name="duration"></param>
        public void AnimateOut(Duration duration)
        {
            var sb = new Storyboard();

            var animateOpacity = new DoubleAnimation(0, duration);
            Storyboard.SetTarget(animateOpacity, this);
            Storyboard.SetTargetProperty(animateOpacity, new PropertyPath("Opacity"));
            sb.Children.Add(animateOpacity);
            sb.FillBehavior = FillBehavior.Stop;
            this.IsHitTestVisible = false;
            sb.Begin();
            SpeechMediaElement.Stop();
            SpeechMediaElement.Source = null;
        }
        private void Next1ButtonClick(object sender, HandInputEventArgs e)
        {
            this.AnimateIn(2, TimeSpan.FromSeconds(0.5));
        }
        private void Back2ButtonClick(object sender, HandInputEventArgs e)
        {
            this.AnimateIn(1, TimeSpan.FromSeconds(0.5));
        }
        private void Next2ButtonClick(object sender, HandInputEventArgs e)
        {
            this.AnimateIn(3, TimeSpan.FromSeconds(0.5));
        }
        private void Back3ButtonClick(object sender, HandInputEventArgs e)
        {
            this.AnimateIn(2, TimeSpan.FromSeconds(0.5));
        }
        private void Next3ButtonClick(object sender, HandInputEventArgs e)
        {
            this.AnimateIn(4, TimeSpan.FromSeconds(0.5));
        }
        private void Back4ButtonClick(object sender, HandInputEventArgs e)
        {
            this.AnimateIn(3, TimeSpan.FromSeconds(0.5));
        }
        private void Next4ButtonClick(object sender, HandInputEventArgs e)
        {
            this.AnimateIn(5, TimeSpan.FromSeconds(0.5));
        }
        private void Back5ButtonClick(object sender, HandInputEventArgs e)
        {
            this.AnimateIn(4, TimeSpan.FromSeconds(0.5));
        }
        private void BackAllButtonClick(object sender, HandInputEventArgs e)
        {
            this.AnimateOut(TimeSpan.FromSeconds(0.5));
        }
        private void StoryButtonClick(object sender, HandInputEventArgs e)
        {
        }

        private void HomeButton_HoverClick(object sender, HandInputEventArgs e)
        {
        }
        private void Stack1_HoverClick(object sender, HandInputEventArgs e)
        {
            Stack1.Visibility = Visibility.Visible;
            Stack2.Visibility = Visibility.Collapsed;
            Stack3.Visibility = Visibility.Collapsed;
            this.ContentScrollViewer.ScrollToTop();
            SpeechMediaElement.Source = new Uri(@"1_起源地.mp3", UriKind.Relative);
            SpeechMediaElement.Play();
        }
        private void Stack2_HoverClick(object sender, HandInputEventArgs e)
        {
            Stack2.Visibility = Visibility.Visible;
            Stack1.Visibility = Visibility.Collapsed;
            Stack3.Visibility = Visibility.Collapsed;
            this.ContentScrollViewer.ScrollToTop();
            SpeechMediaElement.Source = new Uri(@"2_鼻祖.mp3", UriKind.Relative);
            SpeechMediaElement.Play();
        }
        private void Stack3_HoverClick(object sender, HandInputEventArgs e)
        {
            Stack3.Visibility = Visibility.Visible;
            Stack2.Visibility = Visibility.Collapsed;
            Stack1.Visibility = Visibility.Collapsed;
            this.ContentScrollViewer.ScrollToTop();
            SpeechMediaElement.Source = new Uri(@"3_前身.mp3", UriKind.Relative);
            SpeechMediaElement.Play();
        }
        public void AllStop()
        {
            SpeechMediaElement.Stop();
            SpeechMediaElement.Source = null;
        }
    }
}