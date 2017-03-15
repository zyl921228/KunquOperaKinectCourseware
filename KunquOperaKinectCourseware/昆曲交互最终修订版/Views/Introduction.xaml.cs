namespace Microsoft.Samples.Kinect.BasicInteractions
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;
    using Microsoft.Samples.Kinect.BasicInteractions.Properties;

    /// <summary>
    /// Introduction.xaml 的交互逻辑
    /// </summary>
    public partial class Introduction : UserControl
    {
        public static readonly DependencyProperty IsHandRaisedProperty =
            DependencyProperty.Register("IsHandRaised", typeof(bool), typeof(Introduction), new UIPropertyMetadata(false));

        public Introduction()
        {
            this.InitializeComponent();
        }


        public bool IsHandRaised
        {
            get { return (bool)GetValue(IsHandRaisedProperty); }
            set { this.SetValue(IsHandRaisedProperty, value); }
        }

        /// <summary>
        /// 隐藏掉吸引界面,出现这个介绍界面,而且是以动画的形式渐入
        /// </summary>
        internal void FadeIn()
        {
            var animateOpacity = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(500));
            animateOpacity.Completed += (o, e) =>
                                {
                                    var mainWin = Application.Current.MainWindow as MainWindow;
                                    KinectController.AddHandRaisedHandler(this, new EventHandler<HandInputEventArgs>(OnHandRaised));
                                };

            this.Visibility = Visibility.Visible;
            Storyboard storyboard = (Storyboard)this.FindResource("onGoing");
            storyboard.Begin(this);
            this.BeginAnimation(Introduction.OpacityProperty, animateOpacity);
        }

        private void OnHandRaised(object sender, HandInputEventArgs args)
        {
            this.IsHandRaised = true;
            KinectController.RemoveHandRaisedHandler(this, this.OnHandRaised);
        }
        /// <summary>
        /// 第一个二级界面的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiButtonHoverClick(object sender, HandInputEventArgs e)
        {
            this.ShowCategorySelection();
        }
        /// <summary>
        /// 隐藏掉初始界面,打开第一个二级界面
        /// </summary>
        private void ShowCategorySelection()
        {
            var animateOpacity = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));
            animateOpacity.Completed += (o, e) =>
            {
                var mainWin = Application.Current.MainWindow as MainWindow;
                mainWin.ShowCategorySelection();
                this.Visibility = Visibility.Collapsed;
            };
            this.BeginAnimation(Introduction.OpacityProperty, animateOpacity);
            KinectController.RemoveHandRaisedHandler(this, new EventHandler<HandInputEventArgs>(this.OnHandRaised));
        }
        /// <summary>
        /// 第二个二级界面的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JuButtonHoverClick(object sender, HandInputEventArgs e)
        {
            this.ShowCategorySelection2();
        }
        /// <summary>
        /// 隐藏掉初始界面,打开第二个二级界面
        /// </summary>
        private void ShowCategorySelection2()
        {
            var animateOpacity = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));
            animateOpacity.Completed += (o, e) =>
            {
                var mainWin = Application.Current.MainWindow as MainWindow;
                mainWin.ShowCategorySelection2();
                this.Visibility = Visibility.Collapsed;
            };
            this.BeginAnimation(Introduction.OpacityProperty, animateOpacity);
            KinectController.RemoveHandRaisedHandler(this, new EventHandler<HandInputEventArgs>(this.OnHandRaised));
        }
        /// <summary>
        /// 第三个二级界面的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LianButtonHoverClick(object sender, HandInputEventArgs e)
        {
            this.ShowCategorySelection3();
        }
        /// <summary>
        /// 隐藏掉初始界面,打开第三个二级界面
        /// </summary>
        private void ShowCategorySelection3()
        {
            var animateOpacity = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));
            animateOpacity.Completed += (o, e) =>
            {
                var mainWin = Application.Current.MainWindow as MainWindow;
                mainWin.ShowCategorySelection3();
                this.Visibility = Visibility.Collapsed;
            };
            this.BeginAnimation(Introduction.OpacityProperty, animateOpacity);
            KinectController.RemoveHandRaisedHandler(this, new EventHandler<HandInputEventArgs>(this.OnHandRaised));
        }
        /// <summary>
        /// 第四个二级界面的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZuoButtonHoverClick(object sender, HandInputEventArgs e)
        {
            this.ShowCategorySelection4();
        }
        /// <summary>
        /// 隐藏掉初始界面,打开第四个二级界面
        /// </summary>
        private void ShowCategorySelection4()
        {
            var animateOpacity = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));
            animateOpacity.Completed += (o, e) =>
            {
                var mainWin = Application.Current.MainWindow as MainWindow;
                mainWin.ShowCategorySelection4();
                this.Visibility = Visibility.Collapsed;
            };
            this.BeginAnimation(Introduction.OpacityProperty, animateOpacity);
            KinectController.RemoveHandRaisedHandler(this, new EventHandler<HandInputEventArgs>(this.OnHandRaised));
        }
    }
}