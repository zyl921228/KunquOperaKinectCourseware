   M S B A M L   `   `   `  ����P  LBasicInteractions-WPF, Version=0.0.0.0, Culture=neutral, PublicKeyToken=nullg8clr-namespace:Microsoft.Samples.Kinect.BasicInteractions*Microsoft.Samples.Kinect.BasicInteractions  5   @   ;    5Microsoft.Samples.Kinect.BasicInteractions.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    6I   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jlocal8clr-namespace:Microsoft.Samples.Kinect.BasicInteractions     6@   -   5        
�������  � Title$#  KunQu Interaction with Kinect��5   	    � WindowStyle$
 None=�6L    � WindowState$ 	Maximized=�6_    � Icon$/ )/BasicInteractions-WPF;component/Icon.ico��6w    � 	Resources 5         BackGroundAnimation �� TargetProperty �� 
TargetName g� RepeatBehavior8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime	 �� Value  HideContent9  3System.Windows.Media.Animation.EasingDoubleKeyFrame
 O� KeyTime O� Value  ShowContent W� From	 W� To g� Duration��%�  &	        &	     &	 $    �� 5   
   .��5      �� $N H(Panel.Background).(GradientBrush.GradientStops)[2].(GradientStop.Color)�6+   $ 
LayoutRoot��6�   $ Forever��6�   .g�5        $ 0:0:3��6&   $	 	#E1373737��66     5      $ 0:0:6��6&   $	 	#FF373737��66   5	      5
      �� 5   
   .��5      U� $; 5(UserControl.RenderTransform).(ScaleTransform.ScaleX)�5      .\�5        $
 0��6'   $ 1Q�63     5      $
 0:0:0.5��6'   $	 0.1Q�69   5      U� 5      $; 5(UserControl.RenderTransform).(ScaleTransform.ScaleY)�5      .\�5        $
 0��6'   $ 1Q�63     5      $
 0:0:0.5��6'   $	 0.1Q�69   5      U� 5      $ Opacity�6,   .\�5        $
 0��6'   $ 1Q�63     5      $
 0:0:0.5��6'   $ 0Q�69   5      5      �� 5   
   .��5      W� $; 5(UserControl.RenderTransform).(ScaleTransform.ScaleX)�6   $	 0.1X�5      $ 1X�6)   $ 0:0:0.5D�60   W� 5      $; 5(UserControl.RenderTransform).(ScaleTransform.ScaleY)�6   $	 0.1X�5      $ 1X�6)   $ 0:0:0.5D�60   W� 5       $ Opacity�6   $ 0X�6B   $ 1X�6K   $ 	00:00:0.5D�6R   5!      5"       � Triggers 5#      :� 5$   
    :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5%      �� #��[  6   5&      .��5(      � -   5         �Name$ 
LayoutRoot��5(      ��5)   
   �� 5*       �� EndPoint$ 0.5,1&�6"    � MappingMode$ RelativeToBoundingBox=�63    �� 
StartPoint$ 0.5,0&�6W   .��5+      �  � Color$ Black��6    � Offset$ 0Q�6-   � 5,      $ Black��6   $ 1Q�6-   � 5-      $ 	#FF373737��6   $ 0.452Q�61   5.      .+�50   
   (� .��51      � $
��1024��6   $	��655��6    .+�52      � y�53      �� *Controller.HasActiveSkeleton+ �� Current �� Source# Z   System.Windows.Visibility   Hidden �� FallbackValue# Z   VisibilityConverter �� 	Converter# [ .+�54      J   DMicrosoft.Samples.Kinect.BasicInteractions.CategorySelectionControl4  -   5          Name$ CategorySelection4��54   6   $y�Hidden=�6R   J   DMicrosoft.Samples.Kinect.BasicInteractions.CategorySelectionControl3  55      -   5          Name$ CategorySelection3��55   6   $y�Hidden=�6R   J   DMicrosoft.Samples.Kinect.BasicInteractions.CategorySelectionControl2  56      -   5          Name$ CategorySelection2��56   6   $y�Hidden=�6R   I   CMicrosoft.Samples.Kinect.BasicInteractions.CategorySelectionControl  57      -   5          Name$ CategorySelection��57   5   $y�Hidden=�6P   =   7Microsoft.Samples.Kinect.BasicInteractions.Introduction  58      -   5           Name$  IntroScreen��58   )   ! J� Opacity$! 0Q�6>   $y�	Collapsed=�6J   59      ?	   9Microsoft.Samples.Kinect.BasicInteractions.AttractControl	  5:      -   5        " 	 Name$" AttractContent��5:   '   $��Center=�6?   $��Center=�6Z   # J� IsHitTestVisible# .  5;      �� 5=      $��Uniform=�6m   # .  ��6   �� $ �� Path$$ Controller.Silhouette�# Z ��5>      �� 5?      % �� 
TargetType"% ��6    .�5@      �� & �� Property& �@J�Opacity' �� Value$' 0Q�68   ( �� Triggers( 5A      �� 5B   "   ) �� Binding) 6.   �� * �� ElementName$* IntroScreen��$$ IsHandRaised�+ �� Value	+ False6m   , N� EnterActions, 5C   &   �� 5D   *   #��[ 6:   - N� ExitActions- 5F   &   �� 5G   *   #��[ 6:   5I   #   5K      5M      5N      5O      �� 5P   
   .+�5Q   
   A
   ;Microsoft.Samples.Kinect.BasicInteractions.HoverDwellButton
  -	   5        . 
 Name$. MusicButton��5Q   !   A   ;Microsoft.Samples.Kinect.BasicInteractions.HoverDwellButton/   
HoverClick$/ MusicButtonClick��66   0 
  SoundOnEnter$0 slot_roll_on.wav��6T   # . 1 
  SoundOnClick$1 snd_buttonselect.wav��5R   )   $��76��6M   $��48��6Y   .��5T      � .+�5U      �� -
   5        2 ��Name$2 MusicC��5U      $��76��6(   $B��</BasicInteractions-WPF;component/3-0历史_Images\喇叭.png��64   $��46��6r   �� 5V      -   5        $2 MusicCX��5V   $   $! 0Q�6   $��62��65   $H��B/BasicInteractions-WPF;component/3-0历史_Images\喇叭取消.png��6A   $��48��6�   5W      5X      5Y      r� 5Z   
   -   5        3 r�Name$3 McMediaElement��5Z      4 r� LoadedBehavior$4 Manual=�60   ?   9Microsoft.Samples.Kinect.BasicInteractions.HandVisualizer  5[   
   # .  5   Hand5 68   �� $$ Controller.ActiveHand�# Z ��5\      �� 5]      "%  6   .�5^      �� & �@J�Opacity$' 0Q�60   ( 5_      �� 5`      ) 6&   �� $* IntroScreen��$$ IsHandRaised�+ True6e   , 5a      �� 5b   "   #��[ 62   - 5d      �� 5e   "   #��[ 62   5g      5i      5k      5l      5m      