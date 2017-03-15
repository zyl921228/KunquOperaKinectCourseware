namespace Microsoft.Samples.Kinect.BasicInteractions
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;
    using Microsoft.Samples.Kinect.BasicInteractions.Properties;
    using Microsoft.Speech.Recognition;

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool musicOn=true;
        /// <summary>
        /// 构造函数中设定初始化界面组件；设定Kinect控制器的属性更改事件侦听、退出事件侦听、加载事件侦听、初始化背景音乐
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            App.Controller.PropertyChanged += this.KinectController_PropertyChanged;
            this.KeyUp += new System.Windows.Input.KeyEventHandler(this.MainWindow_KeyUp);
            this.Loaded += new RoutedEventHandler(this.MainWindow_Loaded);
            McMediaElement.Source = new Uri(@"MDTMusic.mp3", UriKind.Relative);
            McMediaElement.Volume = 1;
            McMediaElement.Play();
        }
        /// <summary>
        /// 控制背景音乐开关的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MusicButtonClick(object sender, HandInputEventArgs e)
        {
            if (musicOn == false)
            {
                McMediaElement.Source = new Uri(@"MDTMusic.mp3", UriKind.Relative);
                McMediaElement.Play();
                musicOn = true;
                MusicCX.Opacity = 0;
            }
            else if (musicOn == true)
            {
                McMediaElement.Stop();
                musicOn = false;
                MusicCX.Opacity = 1;
            }
        }
        /// <summary>
        /// 减小背景音乐音量
        /// </summary>
        public void VolumeDown()
        {
            var sb = new Storyboard();
            var animateVolume = new DoubleAnimation(1, 0.1, TimeSpan.FromMilliseconds(500));
            Storyboard.SetTarget(animateVolume, McMediaElement);
            Storyboard.SetTargetProperty(animateVolume, new PropertyPath("Volume"));
            sb.Children.Add(animateVolume);
            sb.Begin();
            sb.Completed += (o, e) =>
            {
                McMediaElement.Volume = 0.1;
            };
        }
        /// <summary>
        /// 增大背景音乐音量
        /// </summary>
        public void VolumeUp()
        {
            var sb = new Storyboard();
            var animateVolume = new DoubleAnimation(0.1, 1, TimeSpan.FromMilliseconds(500));
            Storyboard.SetTarget(animateVolume, McMediaElement);
            Storyboard.SetTargetProperty(animateVolume, new PropertyPath("Volume"));
            sb.Children.Add(animateVolume);
            sb.Begin();
            sb.Completed += (o, e) =>
            {
                McMediaElement.Volume = 1;
            };
        }
        /// <summary>
        /// 出现初始界面
        /// </summary>
        public void ShowIntro()
        {
            this.IntroScreen.FadeIn();
        }
        /// <summary>
        /// 第一个二级界面
        /// </summary>
        public void ShowCategorySelection()
        {
            this.CategorySelection.TransitionToGrid();
        }
        /// <summary>
        /// 第二个二级界面
        /// </summary>
        public void ShowCategorySelection2()
        {
            this.CategorySelection2.TransitionToGrid();
        }
        /// <summary>
        /// 第三个二级界面
        /// </summary>
        public void ShowCategorySelection3()
        {
            this.CategorySelection3.TransitionToGrid();
        }
        /// <summary>
        /// 第四个二级界面
        /// </summary>
        public void ShowCategorySelection4()
        {
            this.CategorySelection4.TransitionToGrid();
        }
        public void StopSpeechInCS1()
        {
            CategorySelection.CategoryContent1.AllStop();
        }
        /// <summary>
        /// 在失去骨架之后关掉CS2里的电影
        /// </summary>
        public void StopMovieInCS2()
        {
            CategorySelection2.AllStop();
        }
        /// <summary>
        /// 在失去骨架之后关掉CS3里的语音
        /// </summary>
        public void StopSpeechInCS3()
        {
            CategorySelection3.AllStop();
        }
        /// <summary>
        /// 在主界面加载时获取屏幕长和宽的参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // get the main screen size
            double height = System.Windows.SystemParameters.PrimaryScreenHeight;
            double width = System.Windows.SystemParameters.PrimaryScreenWidth;

            // if the main screen is not 1920 x 1080 then warn the user it is not the optimal experience 
            if (width != 1920 || height != 1080)
            {
                MessageBoxResult continueResult = MessageBox.Show("This screen is not 1920 x 1080.\nThis sample has been optimized for a screen resolution of 1920 x 1080.\nDo you wish to continue?", "Suboptimal Screen Resolution", MessageBoxButton.YesNo);
                if (continueResult == MessageBoxResult.No)
                {
                    this.Close();
                }
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            App.Controller.Dispose();
        }   
        /// <summary>
        /// 如果出现人物骨架，就隐藏吸引界面，使得初始界面出现。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KinectController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HasActiveSkeleton")
            {
                if (App.Controller.HasActiveSkeleton)
                {
                    var t = new DispatcherTimer();
                    t.Interval = TimeSpan.FromMilliseconds(Settings.Default.ShowSilhouetteTime);
                    t.Tick += (o, s) =>
                                  {
                                      t.Stop();
                                      var hideStoryboard = this.Resources["HideContent"] as Storyboard;
                                      hideStoryboard.Begin(this.AttractContent);

                                      if (Settings.Default.ShowIntro)
                                      {
                                          this.IntroScreen.FadeIn();
                                      }
                                      else
                                      {
                                          this.IntroScreen.IsHandRaised = true;
                                          this.ShowCategorySelection();
                                      }
                                  };
                    t.Start();
                }
                else
                {
                    // as this property might be changed from a different thread
                    // it is necessary to use dispatcher invoke to ensure the animation 
                    // occurs on the render thread, otherwise it will fail
                    this.Dispatcher.Invoke(new Action(() => 
                        {
                            this.StopSpeechInCS1();
                            this.StopMovieInCS2();
                            this.StopSpeechInCS3();
                            this.VolumeUp();
                                var showStoryboard =
                                    this.Resources["ShowContent"] as Storyboard;
                                showStoryboard.Begin(this.AttractContent);
                                this.IntroScreen.IsHandRaised = false;
                          }));
                }
            }
        }

        /// <summary>
        /// 按下Esc键关闭程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }
    }
}