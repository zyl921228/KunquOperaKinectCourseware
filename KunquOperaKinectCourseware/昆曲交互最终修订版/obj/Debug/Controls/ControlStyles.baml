   M S B A M L   `   `   `  ����P  LBasicInteractions-WPF, Version=0.0.0.0, Culture=neutral, PublicKeyToken=nullg8clr-namespace:Microsoft.Samples.Kinect.BasicInteractions*Microsoft.Samples.Kinect.BasicInteractions  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089A    ;Microsoft.Samples.Kinect.BasicInteractions.HoverDwellButton  �� 
TargetType �� Property �� Value �� 
TargetType   ScaleUp �� TargetProperty W� From	 W� To g� Duration  	ScaleDown  EnlargingHoverDwellButton �� Triggers	 P� Property
 P� Value N� EnterActions N� ExitActions  VerticalScrollViewerStyle �Name � ColumnDefinitions � RowDefinitions ��Name �� CanContentScroll �� CanHorizontallyScroll �� CanVerticallyScroll ��Name J� Opacity4  .System.Windows.Automation.AutomationProperties   AutomationId � Cursor �� ScrollableHeight � Maximum � Minimum* �� #ComputedVerticalScrollBarVisibility �� ViewportHeight �� ViewportSize  ScrollBarStyle � Value �� Mode  �� RelativeSource  HorizontalScrollViewerStyle! �� ScrollableWidth" �� Orientation,# �� %ComputedHorizontalScrollBarVisibility$ �� ViewportWidth  DisabledForegroundBrush% �� Color  NormalBrush  NormalBorderBrush 	 HorizontalNormalBrush 
 HorizontalNormalBorderBrush  
GlyphBrush  PressedBrush& �� 
StartPoint' �� EndPoint( � Color) � Offset  ScrollBarLineButton* ��Name+ �� CornerRadius, �� Triggers- �� 
TargetName  ScrollBarPageButton  DisabledColorBrush  ScrollBarThumb  HorizontalScrollBarThumb  MiniVerticalScrollBar. ]�Name/ � 	Resources0 ]� Thumb1 n�Name  ActiveScrollRegionBrush  MiniHorizontalScrollBar<   6Microsoft.Samples.Kinect.BasicInteractions.VideoPlayer2   Name3 J� IsHitTestVisible4 r�Name5 r� LoadedBehavior6 r� ScrubbingEnabled7 r� Source8 �� AncestorType9 ��Name�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    Blocal8clr-namespace:Microsoft.Samples.Kinect.BasicInteractions  5      .sys&clr-namespace:System;assembly=mscorlib  5      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      %`  '         &	  �     &	 �    &	 4    7   7  &	 �    &	 �    &	     &	 "    &	 @    &		 ^    &	
 |    &	 �    &	 �    &	 �    7  7  7  7  &	 P    7  &	 �    &	 8    &	 �    7  7  &	 �    7  7  &	 �    '  �    �� 5      "    6   .�5   
   ��  � �� 5      �� 5	      "   6"   .R�5
      �� #��z��6   #��z��6G   #��z��5      ._�5      �� 5      5      5      5      �� 5      .��5   
   W� $F @(local:HoverDwellButton.RenderTransform).(ScaleTransform.ScaleX)�6   $ 1X�5      $
 1.05X�6#   $ 0:0:0.5D�6-   W� 5   
   $F @(local:HoverDwellButton.RenderTransform).(ScaleTransform.ScaleY)�6   $ 1X�5      $
 1.05X�6#   $ 0:0:0.5D�6-   5      �� 5      .��5   
   W� $F @(local:HoverDwellButton.RenderTransform).(ScaleTransform.ScaleX)�6   $
 1.05X�5      $	 1.0X�6&   $ 0:0:0.5D�6/   W� 5   
   $F @(local:HoverDwellButton.RenderTransform).(ScaleTransform.ScaleY)�6   $
 1.05X�5      $	 1.0X�6&   $ 0:0:0.5D�6/   5      �� 5       "    6.   .�5!   
   ��  � �� 5"      �� 5#      "   6"   .R�5$      �� #��z��6   #��z��6G   #��z��5%      ._�5&      �� 5'      5(      5*      �� 5+   
    �@J�RenderTransformOrigin$ 0.5,0.5&�62    5,   
   P� 5-      	 �@  IsHoveredOver
 .  5.      �� 5/      8��  6&    51      �� 52      8�� 6&   54      57      �� 59      "  ��6.   .�5:   
   ��  � �� 5;      �� 5<      " ��6"   .R�5=      � $
 Grid��6   #��z��6)    5>      �� 5?      $��* �6/   �� 5@      $��72 �6/   �� 5A      $��2 �6/    5C      �� 5D      $��* �6,   .+�5F      �� $! PART_ScrollContentPresenter��61   # z��5G   1    .   .  #��z��5H   1   #��z��6e   $��0��6�   #��z��5I   1   $��0��6T   �� 5K      $ PART_VerticalScrollBar��6$   $	 0.6Q�5L   0   $ VerticalScrollBar��5M   0   $ Arrow��6f   $��0��6u   # z 5N   0   $ 0Q�6]   $��0��6i   $��2��6v   #y�z 5O   0   # z 5Q   0   $��760,10,10,0q�6`   #���  6u    5P   0   �� *VerticalOffset+$ OneWay=�  �� *TemplatedParent+5R      5T      5U      5W      5X      �� 5Z      "  ��60   .�5[   
   ��  � �� 5\      �� 5]      " ��6"   .R�5^      � $
 Grid��6   #��z��6)    5_      �� 5`      $��* �6/    5b      �� 5c      $��* �6,   �� 5d      $��72 �6,   �� 5e      $��2 �6,   .+�5g      �� $! PART_ScrollContentPresenter��61   # z��5h   1    .   .  #��z��5i   1   #��z��6e   $��0��6�   #��z��5j   1   $��0��6T   �� 5l      $ PART_HorizontalScrollBar��6$   $ HorizontalScrollBar��5m   0   $ Arrow��6h   $��0��6w   $��2��6�   # z! 5n   0   $ 0Q�6\   $" 
Horizontal=�6h   $��1��6�   #y�z# 5o   0   # z$ 5q   0   $��0,0,0,10q�6_   #���  6q    5p   0   �� *HorizontalOffset+$ OneWay=�  �� *TemplatedParent+5r      5s      5t      5v      5w      �� 5y      $
% #888��66   �� 5z      $
% #888��6*   �� 5{      $
% #888��60   �� 5|      $
% #888��64   �� 5}      $
% #888��6:   �� 5~      $
% #444��6)   �� 5      $	& 0,0&�6/   $	' 0,1&�6@   ��5�      � � 5�      $
( #BBB��6   $	) 0.0Q�6,   � 5�      $
( #EEE��6   $	) 0.1Q�6,   � 5�      $
( #EEE��6   $	) 0.9Q�6,   � 5�      $
( #FFF��6   $	) 1.0Q�6,   5�      5�      �� 5�      "  ��6(   .�5�   
   ��  �@J�SnapsToDevicePixels . 60   �� 5�   
    �@�OverridesDefaultStyle . 62   �� 5�   
    � |� .  6&   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      �� $* Border��6   $��1q�6-   $+ 2��68   ���	#FFD2D2D28��  $��1q�5�   N   ._�5�      F� $��Center=�6   $��Center=�6<   8�� ��5�   B   �� *	Content+  �� *TemplatedParent+5�      , 5�      P� 5�      	 � ��
 . . �5�      �� $- Border��6%   8   � ��6O   5�      P� 5�      	 � {�
 .  . �5�      �� 8   � ��6;   5�      5�      5�      5�      �� 5�      "  ��6(   .�5�   
   ��  �@J�SnapsToDevicePixels . 60   �� 5�   
    �@�OverridesDefaultStyle . 62   �� 5�   
    � �� .  6&   �� 5�   
    � |� .  6&   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      �� 8��  6   5�      5�      5�      �� 5�      "  n�6#   .�5�   
   ��  �@J�SnapsToDevicePixels . 60   �� 5�   
    � �� �#9696966'   �� 5�   
    �@�OverridesDefaultStyle . 62   �� 5�   
    � �� .  6&   �� 5�   
    � |� .  6&   �� 5�   
    � �� 5�      �� 5�      " n�6"   .R�5�      �� #��z��6   #��z��5�      $��1q�6I   ._�5�      � 5�      5�      5�      5�      5�      �� 5�      "  n�6-   .�5�   
   ��  �@J�SnapsToDevicePixels . 60   �� 5�   
    � �� �#9696966'   �� 5�   
    �@�OverridesDefaultStyle . 62   �� 5�   
    � �� .  6&   �� 5�   
    � |� .  6&   �� 5�   
    � �� 5�      �� 5�      " n�6"   .R�5�      �� #��z��6   #��z��5�      $��1q�6I   ._�5�      � 5�      5�      5�      5�      5�      �� 5�      " ��64   .R�5�   
   � $��7��6    5�      �� 5�      $��2��6    �� 5�      $��0.00001* �6    �� 5�      $��2��6    .+�5�      �� $��3��6   
������ 6&   ]� 5�      $. 
PART_Track��6   $��1��6(   ��. / 5�      ��X�5�      (��       6!   *31SystemParameters.VerticalScrollBarButtonHeightKey+)300Q�5�      5�      0 5�      n� 5�      $1 
Part_Thumb��6   $��0,0,0,0q�60   8��  8�� 5�      5�      5�      5�      �� 5�      " ��66   .R�5�   
   � $��0,0,0,0q�6   $��7��6     5�      �� 5�      $��2��6#   �� 5�      $��* �6#   �� 5�      $��2��6#   .+�5�      �� $��3��6   
������ 6)   ]� 5     $. 
PART_Track��6   $��1��6(   ��.  / 5     ��X�5     (��       6!   *42SystemParameters.HorizontalScrollBarButtonWidthKey+)300Q�5     5	     0 5     n� 5     $1 
Part_Thumb��6   $��0,0,0,0q�60   8��  8�� 5     5     5     5     �� 5     "  ��6#   .�5  
   ��  �@J�SnapsToDevicePixels . 60    5  
   P� 5     	 �@��Orientation$
 
Horizontal=�6-   . �5     ��  � ��$
 Auto��6*   �� 5      � ��$ 7��6+   �� 5      � ��# �  6-   5     P� 5     	 �@��Orientation$
 Vertical=�6-   . �5      ��  � ��$
 Auto��6+   �� 5!      � ��$ 20��6*   �� 5"      � ��# �  6-   5#     5%     �� 5(     "   6   .�5)  
   ��  � ��
 ���� 6'   �� 5*  
    � �� 5+     �� 5,     "  6"   .R�5-        $2 PART_PlayPauseButton��5/  )   #��z��5-  -   #��z��5.  )   #��z��6U   .��50     �� 
���   �3 .  .+�51     r� $4 
PART_Media��6+   $5 Manual=�6?   6 . 7 52  +   �� *Source+  �� *FindAncestor+"8  53  +   �� 54     $9 PART_PlayPause��6$   $ ��/Resources/Images/play.png��6<   $��70��6`   $��70��6k   $��Center=�55  $   56     57     , 58     P� 59     	 �@ 	IsPlaying
 . . �5:     �� $- PART_PlayPause��6%    � ��$! /Resources/Images/pause.png��6S   5;     5=     5?     5@     5A     