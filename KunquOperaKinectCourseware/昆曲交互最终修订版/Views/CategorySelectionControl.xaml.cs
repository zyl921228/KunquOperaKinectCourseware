
namespace Microsoft.Samples.Kinect.BasicInteractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using Microsoft.Samples.Kinect.BasicInteractions.Properties;

    /// <summary>
    /// Interaction logic for CategorySelectionControl.xaml
    /// </summary>
    public partial class CategorySelectionControl : UserControl
    {
        public CategorySelectionControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 出现当前这个二级界面
        /// </summary>
        public void TransitionToGrid()
        {
            this.IsHitTestVisible = true;
            var animateOpacity = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(500));
            this.Visibility = Visibility.Visible;
            Storyboard storyboard = (Storyboard)this.FindResource("onGoingShi");
            storyboard.Begin(this);
            this.BeginAnimation(CategorySelectionControl2.OpacityProperty, animateOpacity);
            var mainWin = Application.Current.MainWindow as MainWindow;
            mainWin.VolumeDown();
        }
        /// <summary>
        /// 起源按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YuanButton_HoverClick(object sender, HandInputEventArgs e)
        {
            this.CategoryContent1.AnimateIn(1,TimeSpan.FromSeconds(0.5));
        }
        /// <summary>
        /// 发展按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FaButton_HoverClick(object sender, HandInputEventArgs e)
        {
            this.CategoryContent1.AnimateIn(2,TimeSpan.FromSeconds(0.5));
        }
        /// <summary>
        /// 兴盛按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XingButton_HoverClick(object sender, HandInputEventArgs e)
        {
            this.CategoryContent1.AnimateIn(3,TimeSpan.FromSeconds(0.5));
        }
        /// <summary>
        /// 衰败按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShuaiButton_HoverClick(object sender, HandInputEventArgs e)
        {
            this.CategoryContent1.AnimateIn(4,TimeSpan.FromSeconds(0.5));
        }
        /// <summary>
        /// 复兴按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FuButton_HoverClick(object sender, HandInputEventArgs e)
        {
            this.CategoryContent1.AnimateIn(5,TimeSpan.FromSeconds(0.5));
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackHomeButtonClick(object sender, HandInputEventArgs e)
        {
            this.ShowIntro();
        }
        /// <summary>
        /// 返回到上一级初始界面
        /// </summary>
        private void ShowIntro()
        {
            var animateOpacity = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));
            animateOpacity.Completed += (o, e) =>
            {
                var mainWin = Application.Current.MainWindow as MainWindow;
                mainWin.ShowIntro();
                mainWin.VolumeUp();
                this.IsHitTestVisible = false;
                this.Visibility = Visibility.Collapsed;
            };
            this.BeginAnimation(CategorySelectionControl.OpacityProperty, animateOpacity);
        }
    }
}