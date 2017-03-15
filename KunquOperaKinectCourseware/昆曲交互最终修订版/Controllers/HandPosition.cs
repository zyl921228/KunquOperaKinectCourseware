namespace Microsoft.Samples.Kinect.BasicInteractions
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Windows;
    using Microsoft.Samples.Kinect.BasicInteractions.Properties;

    public class HandPosition : INotifyPropertyChanged
    {
        private bool isInteracting;
        private bool isLeft;
        private int x;
        private int y;

        public HandPosition()
        {
            this.MagneticField = Settings.Default.MagneticField;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public double MagneticField { get; set; }

        /// <summary>
        /// 空气鼠标的使用者ID
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// 空气鼠标当前所悬浮于的元素,可以通过它引发路由事件向上路由到所需位置进行处理
        /// </summary>
        public IInputElement CurrentElement { get; set; }
        /// <summary>
        /// 空气鼠标是否已经被吸住的布尔值
        /// </summary>
        public bool Magnetized
        {
            get 
            { 
                return this.MagnetizedHorizontally || this.MagnetizedVertically; 
            }

            set
            {
                this.MagnetizedVertically = value;
                this.MagnetizedHorizontally = value;
                this.OnPropertyChanged(() => this.Magnetized);
            }
        }

        public bool MagnetizedHorizontally { get; set; }

        public bool MagnetizedVertically { get; set; }

        /// <summary>
        /// The X coordinate of the hand within the application, scaled to the screen size.
        /// 对于空气鼠标水平位置的改变。
        /// 当空气鼠标的位置在磁力区之内时，不会触发鼠标位置的改变，否则会使得鼠标位置改变并且取消磁力。
        /// </summary>
        public int X
        {
            get 
            { 
                return this.x; 
            }

            set
            {
                if (this.MagnetizedHorizontally)
                {
                    if (Math.Abs(value - this.x) < this.MagneticField)
                    {
                        return;
                    }
                }

                this.MagnetizedHorizontally = false;
                this.x = value;
                this.OnPropertyChanged(() => this.X);
            }
        }

        /// <summary>
        /// The Y coordinate of the hand within the application, scaled to the screen size.
        /// 对于空气鼠标垂直距离的改变。
        /// 当空气鼠标的位置在磁力区之内时，不会触发鼠标位置的改变，否则会使得鼠标位置改变并且取消磁力。
        /// </summary>
        public int Y
        {
            get 
            { 
                return this.y; 
            }

            set
            {
                if (this.MagnetizedVertically)
                {
                    if (Math.Abs(value - this.y) < this.MagneticField)
                    {
                        return;
                    }
                }

                this.MagnetizedVertically = false;
                this.y = value;
                this.OnPropertyChanged(() => this.Y);
            }
        }

        /// <summary>
        /// 用来设定空气鼠标是左手还是右手。 左手是True，右手是False。
        /// </summary>
        public bool IsLeft
        {
            get 
            { 
                return this.isLeft; 
            }

            set
            {
                this.isLeft = value;
                this.OnPropertyChanged(() => this.IsLeft);
            }
        }

        /// <summary>
        /// 设定空气鼠标是否正在与一个UI元素进行交互。
        /// </summary>
        public bool IsInteracting
        {
            get 
            { 
                return this.isInteracting; 
            }

            set
            {
                this.isInteracting = value;
                this.OnPropertyChanged(() => this.IsInteracting);
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as HandPosition;
            if (other != null)
            {
                return this.PlayerId.Equals(other.PlayerId) && this.IsLeft.Equals(other.IsLeft);
            }

            return false;
        }
        /// <summary>
        /// 获取使用者ID和使用者手的左右组合在一起的哈希代码
        /// </summary>
        public override int GetHashCode()
        {
            string hash = this.PlayerId.ToString(CultureInfo.InvariantCulture) + this.IsLeft.ToString();
            return hash.GetHashCode();
        }

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
    }
}