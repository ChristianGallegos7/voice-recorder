   M S B A M L   `   `   `  ����  
grabar-voz    grabar_voz.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
Grabar Voz��5   	   $	��160��5   	   $	��200��5   	   ���#F0F0F0 � WindowStartupLocation$ Manual=�5   	    � WindowStyle$
 None=�5	   	    � Topmost$
 True��5   	    � AllowsTransparency$
 True��5   	    � 
ResizeMode$ NoResize=�5   	    � 	Resources 5         CompactButtonStyle �� 
TargetType �� Property	 �� Value
 �� 
TargetType ��Name �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName��%`  &	        �� 5   
   " ��6+   .�5      ��  � ��$	 40��6&   �� 5       � ��$	 40��6'   �� 5       � ��$	 5q�6'   �� 5       � ��	 �#4CAF506+   �� 5       � ��
	 ���� 6,   �� 5       � ��	 5      �� 5      "
 ��6&   .R�5      �� $ border��6!   #��z��5   !   $ 4��5   !   $��0q�5   !   ._�5      �� $��Center=�5   !   $��Center=�5   !   5       5       P� 5!       �@J�IsMouseOver$
 True��6=   . �5"   "   �� $ border��5#   )    � ��	 �#45A0495$   )   5%      5'      5)      5*      5+      .��5-      � .+�5/   
   �� $��Center=�6   $��Center=�62    �� Orientation$ Vertical=�6M   $��10q�6d   .+�51      �� -   5        $��TimerTextBlock��51      $��00:00��52      $��18(�53      $
��Bold"�54      ���#333$��Center=�56      $��0,0,0,10q�57      �� 5:      -   5        $��AudioSpectrum��5:      $��Center=�5;      $��Center=�5<      $��0,0,0,10q�5=      ��5>      �� 5?      .R�5@      �� $ 
Horizontal=�6%   5A      ��5C      �� 5D      .R�5E      �� $��5��6$   $��1q�6A   ���#4CAF50��6.   �� 6L   5F      5H      �� 5K      $ 
Horizontal=�6   $��Center=�62   .+�5L      �� -   5        $��PauseRecording��5L      #��[  5N   	   ${�False��5O   	   .��5Q      �� $��Uniform=�6   $��/pausa-icon.png��6.   $��20��6G   $��20��6R   5R      �� 5S      -   5        $��StartRecording��5S      #��[  5U   	   .��5V      �� $��Uniform=�6   $��/grabar-icon.png��6.   $��20��6H   $��20��6S   5W      �� 5X      -   5        $��StopRecording��5X      #��[  5Z   	   ���#F44336${�False��5\   	   .��5^      �� $��/guardar-icon.png��6   $��Uniform=�67   $��20��6I   $��20��6T   5_      5a      5b      5c      5d      