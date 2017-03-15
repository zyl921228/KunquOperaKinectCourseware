namespace Microsoft.Samples.Kinect.BasicInteractions
{
    using System.ComponentModel;
    using System.Windows;
    using Microsoft.Samples.Kinect.BasicInteractions.Properties;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static KinectController controller;

        public App()
        {
        }


        public static KinectController Controller
        {
            get
            {
                if (controller == null)
                {
                    if (DesignerProperties.GetIsInDesignMode(Current.MainWindow) == false)
                    {
                        controller = new KinectController(Current.MainWindow);
                        controller.Initialize();
                    }
                }

                return controller;
            }
        }
    }
}