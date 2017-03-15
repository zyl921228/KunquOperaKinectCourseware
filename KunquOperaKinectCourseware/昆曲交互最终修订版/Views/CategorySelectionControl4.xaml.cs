namespace Microsoft.Samples.Kinect.BasicInteractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Media.Animation;
    using Microsoft.Samples.Kinect.BasicInteractions.Properties;
    /// <summary>
    /// CategorySelectionControl4.xaml 的交互逻辑
    /// </summary>
    public partial class CategorySelectionControl4 : UserControl
    {
        public CategorySelectionControl4()
        {
            InitializeComponent();
        }
        public void TransitionToGrid()
        {
            this.videoPlayer.Source = new Uri(@"YJwmv.wmv", UriKind.Relative);
            videoPlayer.IsPlaying = true;
            this.IsHitTestVisible = true;
            var animateOpacity = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(500));
            this.Visibility = Visibility.Visible;
            Storyboard storyboard = (Storyboard)this.FindResource("onGoingZuo");
            storyboard.Begin(this);
            this.BeginAnimation(CategorySelectionControl4.OpacityProperty, animateOpacity);
            var mainWin = Application.Current.MainWindow as MainWindow;
            mainWin.VolumeDown();
        }

        private void BackHomeButtonClick(object sender, HandInputEventArgs e)
        {
            this.ShowIntro();
            videoPlayer.IsPlaying = false;
            videoPlayer.Source = null;
        }

        private void YueJuButtonClick(object sender, HandInputEventArgs e)
        {
            if (this.videoPlayer.Source==(new Uri(@"YJwmv.wmv", UriKind.Relative)))
            {
                Storyboard storyboard = (Storyboard)this.FindResource("AnswerRight");
                storyboard.Begin(this);
            }
            else 
            {
                Storyboard storyboard = (Storyboard)this.FindResource("AnswerWrong");
                storyboard.Begin(this);
            }
        }

        private void JingJuButtonClick(object sender, HandInputEventArgs e)
        {
            if (this.videoPlayer.Source==(new Uri(@"JJwmv.wmv", UriKind.Relative)))
            {
                Storyboard storyboard = (Storyboard)this.FindResource("AnswerRight");
                storyboard.Begin(this);
            }
            else
            {
                Storyboard storyboard = (Storyboard)this.FindResource("AnswerWrong");
                storyboard.Begin(this);
            }
        }

        private void KunQuButtonClick(object sender, HandInputEventArgs e)
        {
            if (this.videoPlayer.Source==(new Uri(@"MDTwmv.wmv", UriKind.Relative)))
            {
                Storyboard storyboard = (Storyboard)this.FindResource("AnswerRight");
                storyboard.Begin(this);
            }
            else
            {
                Storyboard storyboard = (Storyboard)this.FindResource("AnswerWrong");
                storyboard.Begin(this);
            }
        }

        private void NextQuButtonClick(object sender, HandInputEventArgs e)
        {
            if (this.videoPlayer.Source==(new Uri(@"YJwmv.wmv", UriKind.Relative)))
            {
                this.videoPlayer.Source = new Uri(@"JJwmv.wmv", UriKind.Relative);
            }
            else if (this.videoPlayer.Source==(new Uri(@"JJwmv.wmv", UriKind.Relative)))
            {
                this.videoPlayer.Source = new Uri(@"MDTwmv.wmv", UriKind.Relative);
            }
            else if (this.videoPlayer.Source==(new Uri(@"MDTwmv.wmv", UriKind.Relative)))
            {
                this.videoPlayer.Source = new Uri(@"YJwmv.wmv", UriKind.Relative);
            }
        }

        private void ShowIntro()
        {
            // hide this screen and show the intro screen
            var animateOpacity = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));
            animateOpacity.Completed += (o, e) =>
            {
                var mainWin = Application.Current.MainWindow as MainWindow;
                mainWin.ShowIntro();
                mainWin.VolumeUp();
                this.IsHitTestVisible = false;
                this.Visibility = Visibility.Collapsed;
            };
            this.BeginAnimation(CategorySelectionControl4.OpacityProperty, animateOpacity);
        }
    }
}
