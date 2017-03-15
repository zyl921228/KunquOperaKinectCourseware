namespace Microsoft.Samples.Kinect.BasicInteractions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq.Expressions;
    using System.Timers;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Kinect;
    using Microsoft.Speech.Recognition;

    public class KinectController : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// 鼠标进入一个UI元素的路由事件
        /// </summary>
        public static readonly RoutedEvent HandEnterEvent = EventManager.RegisterRoutedEvent(
            "HandEnter",
            RoutingStrategy.Bubble,
            typeof(EventHandler<HandInputEventArgs>),
            typeof(KinectController));

        /// <summary>
        /// 空气鼠标在一个UI元素上面移动的路由事件
        /// </summary>
        public static readonly RoutedEvent HandMoveEvent = EventManager.RegisterRoutedEvent(
            "HandMove", 
            RoutingStrategy.Bubble, 
            typeof(EventHandler<HandInputEventArgs>),
            typeof(KinectController));

        /// <summary>
        /// 空气鼠标离开一个UI元素的路由事件
        /// </summary>
        public static readonly RoutedEvent HandLeaveEvent = EventManager.RegisterRoutedEvent(
            "HandLeave", 
            RoutingStrategy.Bubble, 
            typeof(EventHandler<HandInputEventArgs>),
            typeof(KinectController));

        /// <summary>
        /// 当一只手高于胳膊肘时引发的路由事件
        /// </summary>
        public static readonly RoutedEvent HandRaisedEvent = EventManager.RegisterRoutedEvent(
            "HandRaised", 
            RoutingStrategy.Tunnel, 
            typeof(EventHandler<HandInputEventArgs>),
            typeof(KinectController));

        private const int RedIndex = 2;
        private const int GreenIndex = 1;
        private const int BlueIndex = 0;
        private static readonly int Bgra32BytesPerPixel = (PixelFormats.Bgra32.BitsPerPixel + 7) / 8;
        private readonly Window parentWindow;
        private KinectSensor sensor;
        private HandPosition activeHand;
        private byte[] depthFrame32;
        private byte[] convertedDepthBits;
        private DepthImageFormat lastImageFormat;
        private short[] pixelData;
        private Skeleton trackedSkeleton;
        private Skeleton[] skeletons;
        private Timer trackedSkeletonTimer;
        private bool hasActiveSkeleton = false;
        private WriteableBitmap silhouette;
        /// <summary>
        /// 该类的构造函数将会获取当前Kinect控制器所在窗口
        /// </summary>
        /// <param name="parent"></param>
        public KinectController(Window parent)
        {
            this.parentWindow = parent;
            this.ActiveHand = new HandPosition();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Kinect控制器所依赖的空气鼠标
        /// </summary>
        public HandPosition ActiveHand
        {
            get 
            { 
                return activeHand; 
            }

            set
            {
                activeHand = value;
                OnPropertyChanged(() => ActiveHand);//属性变化时控件会自动更新
            }
        }
        /// <summary>
        /// 是否有活动的骨骼点的布尔值
        /// </summary>
        public bool HasActiveSkeleton
        {
            get 
            { 
                return this.hasActiveSkeleton; 
            }

            private set
            {
                if (this.hasActiveSkeleton != value)
                {
                    this.hasActiveSkeleton = value;
                    this.OnPropertyChanged(() => this.HasActiveSkeleton);
                }
            }
        }

        
        /// <summary>
        /// Kinect所依赖的体感器,在设置的同时要启动体感器,为了增强健壮性还应该测试在启动时是否遇到错误,并给出错误提示
        /// </summary>
        public KinectSensor Sensor
        {
            get
            { 
                return this.sensor; 
            }

            private set
            {
                if (this.sensor != null)
                {
                    this.sensor.Stop();
                }

                this.sensor = value;
                if (this.sensor != null)
                {
                    try
                    {
                        this.sensor.Start();
                        this.SetupSensor();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message, BasicInteractions.Properties.Resources.KinectInitializeError);
                        Application.Current.Shutdown();
                    }
                }
            }
        }

        
        /// <summary>
        /// Kinect控制器所依赖的人物轮廓，用来给用户一个提示，说明这个程序是体感的。
        /// </summary>
        public WriteableBitmap Silhouette
        {
            get 
            { 
                return this.silhouette; 
            }

            private set
            {
                if (this.silhouette != value)
                {
                    this.silhouette = value;
                    this.OnPropertyChanged(() => this.Silhouette);
                }
            }
        }


        public static void AddPreviewHandEnterHandler(DependencyObject sender, EventHandler<HandInputEventArgs> handler)
        {
            var element = sender as UIElement;
            if (element != null)
            {
                element.AddHandler(HandEnterEvent, handler);
            }
        }

        public static void RemovePreviewHandEnterHandler(DependencyObject sender, EventHandler<HandInputEventArgs> handler)
        {
            var element = sender as UIElement;
            if (element != null)
            {
                element.RemoveHandler(HandEnterEvent, handler);
            }
        }


        public static void AddPreviewHandMoveHandler(DependencyObject sender, EventHandler<HandInputEventArgs> handler)
        {
            var element = sender as UIElement;
            if (element != null)
            {
                element.AddHandler(HandMoveEvent, handler);
            }
        }

        public static void RemovePreviewHandMoveHandler(DependencyObject sender, EventHandler<HandInputEventArgs> handler)
        {
            var element = sender as UIElement;
            if (element != null)
            {
                element.RemoveHandler(HandMoveEvent, handler);
            }
        }

        public static void AddPreviewHandLeaveHandler(DependencyObject sender, EventHandler<HandInputEventArgs> handler)
        {
            var element = sender as UIElement;
            if (element != null)
            {
                element.AddHandler(HandLeaveEvent, handler);
            }
        }

        public static void RemovePreviewHandLeaveHandler(DependencyObject sender, EventHandler<HandInputEventArgs> handler)
        {
            var element = sender as UIElement;
            if (element != null)
            {
                element.RemoveHandler(HandLeaveEvent, handler);
            }
        }

        public static void AddHandRaisedHandler(DependencyObject sender, EventHandler<HandInputEventArgs> handler)
        {
            var element = sender as UIElement;
            if (element != null)
            {
                element.AddHandler(HandRaisedEvent, handler);
            }
        }

        public static void RemoveHandRaisedHandler(DependencyObject sender, EventHandler<HandInputEventArgs> handler)
        {
            var element = sender as UIElement;
            if (element != null)
            {
                element.RemoveHandler(HandRaisedEvent, handler);
            }
        }

        public void Initialize()
        {
            foreach (KinectSensor kinect in KinectSensor.KinectSensors)
            {
                if (kinect.Status == KinectStatus.Connected)
                {
                    this.Sensor = kinect;
                    break;
                }
            }

            if (this.Sensor == null)
            {
                MessageBox.Show("Unable to detect an available Kinect Sensor.\nPlease make sure you have a Kinect sensor plugged in.\nThis sample will now close.");
                Application.Current.Shutdown();
            }
            //监听体感器的状态变化
            KinectSensor.KinectSensors.StatusChanged += new EventHandler<StatusChangedEventArgs>(KinectSensors_StatusChanged);

        }


        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.trackedSkeletonTimer != null)
                {
                    this.trackedSkeletonTimer.Dispose();
                    this.trackedSkeletonTimer = null;
                }

                if (this.sensor != null)
                {
                    this.sensor.Stop();
                    this.sensor = null;
                }
            }
        }

        #endregion
        /// <summary>
        /// 将骨骼点中的Y坐标转换为全屏幕范围内的坐标,其实它是以像素为单位的
        /// </summary>
        /// <param name="joint"></param>
        /// <returns></returns>
        private static double ScaleY(Joint joint)
        {
            double y = ((SystemParameters.PrimaryScreenHeight / 0.4) * -joint.Position.Y) +
                       (SystemParameters.PrimaryScreenHeight / 2);
            return y;
        }
        /// <summary>
        /// 将骨骼点中的X和Y坐标转换为全屏幕范围内的以像素为单位的坐标并输出(out),对于X坐标要判断是左手还是右手控制
        /// </summary>
        /// <param name="shoulderCenter"></param>
        /// <param name="rightHand"></param>
        /// <param name="joint"></param>
        /// <param name="scaledX"></param>
        /// <param name="scaledY"></param>
        private static void ScaleXY(Joint shoulderCenter, bool rightHand, Joint joint, out int scaledX, out int scaledY)
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;

            double x = 0;
            double y = ScaleY(joint);

            // 如果是右手控制那么就把肩膀中间点放在屏幕的左边
            // 否则把肩膀中间点放在屏幕的右边
            if (rightHand) 
            {
                x = (joint.Position.X - shoulderCenter.Position.X) * screenWidth * 2;
            }
            else 
            {
                x = screenWidth - ((shoulderCenter.Position.X - joint.Position.X) * (screenWidth * 2));
            }


            if (x < 0)
            {
                x = 0;
            }
            else if (x > screenWidth - 5)
            {
                x = screenWidth - 5;
            }

            if (y < 0)
            {
                y = 0;
            }

            scaledX = (int)x;
            scaledY = (int)y;
        }
        /// <summary>
        /// 当体感器发生状态变化时,判断其状态,增强健壮性.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            if (e.Sensor == this.sensor)
            {
                switch (e.Status)
                {
                    case KinectStatus.Connected:
                    case KinectStatus.DeviceNotGenuine:
                    case KinectStatus.Initializing:
                        break;
                    default:
                        MessageBox.Show("Unable to detect an available Kinect Sensor.\nPlease make sure you have a Kinect sensor plugged in.\nThis sample will now close.");
                        Application.Current.Shutdown();
                        break;
                }
            }
        }
        /// <summary>
        /// 在设置体感器的时候打开它的骨骼点数据流(带参数的,加入平滑,从而增强用户体验)和深度数据流,并加入骨骼数据侦听.
        /// </summary>
        private void SetupSensor()
        {
            var parameters = new TransformSmoothParameters
                                 {
                                     Correction = 0.5f,
                                     Prediction = 0.5f,
                                     Smoothing = 0.05f,
                                     JitterRadius = 0.8f,
                                     MaxDeviationRadius = 0.2f
                                 };

            Sensor.SkeletonStream.Enable(parameters);
            Sensor.DepthStream.Enable();
            Sensor.SkeletonFrameReady += this.Sensor_SkeletonFrameReady;
        }

        private void Sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                //判断当骨骼点图像存在并且骨架总数大于0时,执行一系列操作
                if (skeletonFrame != null && skeletonFrame.SkeletonArrayLength > 0)
                {
                    //如果骨架数量为空或者骨架数量和当前数量不同时重设骨架;否则同样要清空所有现有的骨架
                    if (skeletons == null || skeletons.Length != skeletonFrame.SkeletonArrayLength)
                    {
                        skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    }
                    else
                    {
                        for (int i = 0; i < skeletons.Length; i++)
                        {
                            skeletons[i] = null;
                        }
                    }
                    //把当前数据流中的骨架信息传递给储存的骨架
                    skeletonFrame.CopySkeletonDataTo(skeletons);

                    int playerIndex = 0;
                    Skeleton updatedSkeleton = null;
                    //保证现在的使用者是骨骼点的控制者
                    if (this.trackedSkeleton != null)
                    {
                        for (int i = 0; i < skeletons.Length; i++)
                        {
                            if (skeletons[i].TrackingId == this.trackedSkeleton.TrackingId)
                            {
                                playerIndex = i;
                                updatedSkeleton = skeletons[i];
                                break;
                            }
                        }
                    }
                    //当不存在使用者时将画面内可见的的被跟踪的使用者的骨架赋值给当前程序依赖的骨架
                    if (updatedSkeleton == null)
                    {
                        double closestX = 1;
                        for (int i = 0; i < skeletons.Length; i++)
                        {
                            Skeleton newSkeleton = skeletons[i];
                            if (newSkeleton.TrackingState != SkeletonTrackingState.NotTracked &&
                                Math.Abs(newSkeleton.Position.X) < closestX)
                            {
                                playerIndex = i;
                                updatedSkeleton = skeletons[i];
                            }
                        }
                    }

                    this.trackedSkeleton = updatedSkeleton;

                    if (updatedSkeleton != null && this.sensor != null)
                    {
                        // set the silhouette
                        using (DepthImageFrame depthFrame = this.sensor.DepthStream.OpenNextFrame(15))
                        {
                            this.GetPlayerSilhouette(depthFrame, playerIndex + 1);
                        }
                        //停止"骨架消失计时器"
                        if (this.trackedSkeletonTimer != null)
                        {
                            this.trackedSkeletonTimer.Stop();
                            this.trackedSkeletonTimer = null;
                        }

                        this.HasActiveSkeleton = true;
                        this.SkeletonUpdated(updatedSkeleton);
                        return;
                    }
                }

                this.trackedSkeleton = null;
                this.Silhouette = null;
                //2秒内没有使用者就会使得HasActiveSkeleton属性变为False
                if (this.trackedSkeletonTimer == null)
                {
                    this.trackedSkeletonTimer = new Timer(2000);
                    this.trackedSkeletonTimer.Elapsed += (o, s) =>
                        {
                            this.HasActiveSkeleton = false;
                        };
                    this.trackedSkeletonTimer.Start();
                }
            }
        }

        
        /// <summary>
        /// 获取使用者的外形轮廓
        /// </summary>
        /// <param name="depthFrame"></param>
        /// <param name="playerIndex"></param>
        private void GetPlayerSilhouette(DepthImageFrame depthFrame, int playerIndex)
        {
            if (depthFrame != null)
            {
                bool haveNewFormat = this.lastImageFormat != depthFrame.Format;

                if (haveNewFormat)
                {
                    this.pixelData = new short[depthFrame.PixelDataLength];
                    this.depthFrame32 = new byte[depthFrame.Width * depthFrame.Height * Bgra32BytesPerPixel];
                    this.convertedDepthBits = new byte[this.depthFrame32.Length];
                }

                depthFrame.CopyPixelDataTo(this.pixelData);

                for (int i16 = 0, i32 = 0; i16 < pixelData.Length && i32 < this.depthFrame32.Length; i16++, i32 += 4)
                {
                    int player = pixelData[i16] & DepthImageFrame.PlayerIndexBitmask;
                    if (player == playerIndex)
                    {
                        convertedDepthBits[i32 + RedIndex] = 0x44;
                        convertedDepthBits[i32 + GreenIndex] = 0x23;
                        convertedDepthBits[i32 + BlueIndex] = 0x59;
                        convertedDepthBits[i32 + 3] = 0x66;
                    }
                    else if (player > 0)
                    {
                        convertedDepthBits[i32 + RedIndex] = 0xBC;
                        convertedDepthBits[i32 + GreenIndex] = 0xBE;
                        convertedDepthBits[i32 + BlueIndex] = 0xC0;
                        convertedDepthBits[i32 + 3] = 0x66;
                    }
                    else
                    {
                        convertedDepthBits[i32 + RedIndex] = 0x0;
                        convertedDepthBits[i32 + GreenIndex] = 0x0;
                        convertedDepthBits[i32 + BlueIndex] = 0x0;
                        convertedDepthBits[i32 + 3] = 0x0;
                    }
                }
                //如果人物轮廓不存在,就创建一个新的
                if (this.Silhouette == null || haveNewFormat)
                {
                    this.Silhouette = new WriteableBitmap(
                        depthFrame.Width,
                        depthFrame.Height,
                        96,
                        96,
                        PixelFormats.Bgra32,
                        null);
                }

                this.Silhouette.WritePixels(
                    new Int32Rect(0, 0, depthFrame.Width, depthFrame.Height),
                    convertedDepthBits,
                    depthFrame.Width * Bgra32BytesPerPixel,
                    0);

                this.lastImageFormat = depthFrame.Format;
            }
        }

        /// <summary>
        /// 当骨骼点升级之后引发ActiveHand的X,Y发生变化.
        /// 并且判断当前使用者使用的的是左手还是右手
        /// </summary>
        /// <param name="skeleton"></param>
        private void SkeletonUpdated(Skeleton skeleton)
        {
            if (this.ActiveHand == null)
            {
                this.ActiveHand = new HandPosition();
            }

            int leftX, leftY, rightX, rightY;
            Joint leftHandJoint = skeleton.Joints[JointType.HandLeft];
            Joint rightHandJoint = skeleton.Joints[JointType.HandRight];
            Joint hipCenterJoint = skeleton.Joints[JointType.HipCenter];
            Joint shoulderCenterJoint = skeleton.Joints[JointType.ShoulderCenter];
            float leftZ = leftHandJoint.Position.Z;
            float rightZ = rightHandJoint.Position.Z;

            ScaleXY(skeleton.Joints[JointType.ShoulderCenter], false, leftHandJoint, out leftX, out leftY);
            ScaleXY(skeleton.Joints[JointType.ShoulderCenter], true, rightHandJoint, out rightX, out rightY);
            if (leftHandJoint.TrackingState == JointTrackingState.Tracked && leftZ < rightZ && leftY < SystemParameters.PrimaryScreenHeight)
            {
                this.ActiveHand.IsLeft = true;
                this.ActiveHand.X = leftX;
                this.ActiveHand.Y = leftY; 
                this.HitTestHand(this.ActiveHand);
            }
            else if (rightHandJoint.TrackingState == JointTrackingState.Tracked && rightY < SystemParameters.PrimaryScreenHeight)
            {
                this.ActiveHand.IsLeft = false;
                this.ActiveHand.X = rightX;
                this.ActiveHand.Y = rightY;
                this.HitTestHand(this.ActiveHand);
            }
            else
            {
                //如果在空气鼠标上有元素交互,那么应当在停止交互后再将空气鼠标设置为null
                if (this.activeHand != null && this.activeHand.CurrentElement != null)
                {
                    this.ActiveHand.X = -1;
                    this.ActiveHand.Y = -1;
                    HandInputEventArgs args = new HandInputEventArgs(HandLeaveEvent, this.ActiveHand.CurrentElement, this.ActiveHand);
                    this.activeHand.CurrentElement.RaiseEvent(args);
                    this.activeHand.CurrentElement = null;
                }

                this.ActiveHand = null;
            }

            if (this.ActiveHand == null)
            {
                return;
            }

            if (this.ActiveHand.CurrentElement != null)
            {
                if (ScaleY(skeleton.Joints[JointType.ElbowLeft]) > ScaleY(skeleton.Joints[JointType.HandLeft])
                    || ScaleY(skeleton.Joints[JointType.ElbowRight]) > ScaleY(skeleton.Joints[JointType.HandRight]))
                {
                    HandInputEventArgs args = new HandInputEventArgs(HandRaisedEvent, this.ActiveHand.CurrentElement, this.ActiveHand);
                    this.ActiveHand.CurrentElement.RaiseEvent(args);
                }
            }
        }
        /// <summary>
        /// 将空气鼠标ActiveHand的“进入”、“离开”事件激发（RaiseEvent）
        /// </summary>
        /// <param name="hand"></param>
        private void HitTestHand(HandPosition hand)
        {
            var pt = new Point(hand.X, hand.Y);
            IInputElement input = this.parentWindow.InputHitTest(pt);
            if (hand.CurrentElement != input)
            {
                var inputObject = input as DependencyObject;
                var currentObject = hand.CurrentElement as DependencyObject;

                // If the new input is a child of the current element then don't fire the leave event.
                // It will be fired later when the current input moves to the parent of the current element.
                if (hand.CurrentElement != null && Utility.IsElementChild(currentObject, inputObject) == false)
                {
                    // Raise the HandLeaveEvent on the CurrentElement, which at this point is the previous element the hand was over.
                    hand.CurrentElement.RaiseEvent(new HandInputEventArgs(HandLeaveEvent, hand.CurrentElement, hand));
                }

                // If the current element is the parent of the new input element then don't
                // raise the entered event as it has already been fired.
                if (input != null && Utility.IsElementChild(inputObject, currentObject) == false)
                {
                    input.RaiseEvent(new HandInputEventArgs(HandEnterEvent, input, hand));
                }

                hand.CurrentElement = input;
            }
            else if (hand.CurrentElement != null)
            {
                hand.CurrentElement.RaiseEvent(new HandInputEventArgs(HandMoveEvent, hand.CurrentElement, hand));
            }
        }

        

        #region INotifyPropertyChanged Members

        private void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (this.PropertyChanged == null)
            {
                return;
            }

            var body = (MemberExpression)expression.Body;
            string propertyName = body.Member.Name;
            var args = new PropertyChangedEventArgs(propertyName);
            this.PropertyChanged(this, args);
        }

        #endregion
    }
}