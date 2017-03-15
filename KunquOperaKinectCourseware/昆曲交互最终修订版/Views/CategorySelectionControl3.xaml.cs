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
    /// CategorySelectionControl3.xaml 的交互逻辑
    /// </summary>
    public partial class CategorySelectionControl3 : UserControl
    {
        public CategorySelectionControl3()
        {
            InitializeComponent();
        }
        public void TransitionToGrid()
        {
            this.IsHitTestVisible = true;
            var animateOpacity = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(500));
            this.Visibility = Visibility.Visible;
            Storyboard storyboard = (Storyboard)this.FindResource("onGoingLian");
            storyboard.Begin(this);
            this.BeginAnimation(CategorySelectionControl3.OpacityProperty, animateOpacity);
            var mainWin = Application.Current.MainWindow as MainWindow;
            mainWin.VolumeDown();
            京剧文字底.Visibility = Visibility.Hidden;
            京剧1.Visibility = Visibility.Hidden;
            越剧文字底.Visibility = Visibility.Hidden;
            越剧1.Visibility = Visibility.Hidden;
        }

        private void BackHomeButtonClick(object sender, HandInputEventArgs e)
        {
            this.ShowIntro();
        }

        private void YueJuButtonClick(object sender, HandInputEventArgs e)
        {
            SpeechMediaElement.Source = new Uri(@"9_越剧比较.mp3", UriKind.Relative);
            SpeechMediaElement.Play();
            京剧文字底.Visibility = Visibility.Hidden;
            京剧1.Visibility = Visibility.Hidden;
            越剧文字底.Visibility = Visibility.Visible;
            越剧1.Visibility = Visibility.Visible;
            Storyboard storyboard = (Storyboard)this.FindResource("YueJuScale");
            storyboard.Begin(this);
        }

        private void JingJuButtonClick(object sender, HandInputEventArgs e)
        {
            SpeechMediaElement.Source = new Uri(@"8_京剧比较.mp3", UriKind.Relative);
            SpeechMediaElement.Play();
            越剧文字底.Visibility = Visibility.Hidden;
            越剧1.Visibility = Visibility.Hidden;
            京剧文字底.Visibility = Visibility.Visible;
            京剧1.Visibility = Visibility.Visible;
            Storyboard storyboard = (Storyboard)this.FindResource("JingJuScale");
            storyboard.Begin(this);
        }

        public void AllStop()
        {
            SpeechMediaElement.Stop();
            SpeechMediaElement.Source = null;
        }

        private void ShowIntro()
        {
            SpeechMediaElement.Stop();
            SpeechMediaElement.Source = null;
            京剧文字底.Visibility = Visibility.Hidden;
            京剧1.Visibility = Visibility.Hidden;
            越剧文字底.Visibility = Visibility.Hidden;
            越剧1.Visibility = Visibility.Hidden;
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
            this.BeginAnimation(CategorySelectionControl3.OpacityProperty, animateOpacity);
        }
    }
}
