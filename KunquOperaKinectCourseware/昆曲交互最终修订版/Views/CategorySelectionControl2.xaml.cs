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
    /// CategorySelectionControl2.xaml 的交互逻辑
    /// </summary>
    public partial class CategorySelectionControl2 : UserControl
    {
        public CategorySelectionControl2()
        {
            InitializeComponent();
        }

        public void TransitionToGrid()
        {
            this.IsHitTestVisible = true;
            var animateOpacity = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(500));
            this.Visibility = Visibility.Visible;
            Storyboard storyboard = (Storyboard)this.FindResource("onGoingJu");
            storyboard.Begin(this);
            this.BeginAnimation(CategorySelectionControl2.OpacityProperty, animateOpacity);
            var mainWin = Application.Current.MainWindow as MainWindow;
            mainWin.VolumeDown();
        }
        public void AllStop()
        {
            mediaElement.Stop();
            mediaElement.Source = null;
        }

        private void BackHomeButtonClick(object sender, HandInputEventArgs e)
        {
            mediaElement.Stop();
            mediaElement.Source = null;
            this.ShowIntro();
        }

        private void THSButtonClick(object sender, HandInputEventArgs e)
        {
            mediaElement.Source = new Uri("THSwmv.wmv", UriKind.Relative);
            mediaElement.Position = TimeSpan.FromSeconds(0);
            mediaElement.Play();
            //this.CategoryContent2.AnimateIn(1, TimeSpan.FromSeconds(0.5));

        }

        private void CSDButtonClick(object sender, HandInputEventArgs e)
        {
            mediaElement.Source = new Uri("CSDwmv.wmv", UriKind.Relative);
            mediaElement.Position = TimeSpan.FromSeconds(0);
            mediaElement.Play();
            //this.CategoryContent2.AnimateIn(2, TimeSpan.FromSeconds(0.5));

        }

        private void MDTButtonClick(object sender, HandInputEventArgs e)
        {
            mediaElement.Source = new Uri("MDTwmv.wmv", UriKind.Relative);
            mediaElement.Position = TimeSpan.FromSeconds(0);
            mediaElement.Play();
            //this.CategoryContent2.AnimateIn(3, TimeSpan.FromSeconds(0.5));

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
            this.BeginAnimation(CategorySelectionControl2.OpacityProperty, animateOpacity);
        }
    }
}
