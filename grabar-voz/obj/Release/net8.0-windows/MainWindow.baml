   M S B A M L   `   `   `  ����   
grabar-voz    grabar_voz.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  
Grabar Voz��$	��200��$	��300�����#121212 � WindowStartupLocation$ Manual=� � WindowStyle$ SingleBorderWindow=� � Topmost$
 True�� � 
ResizeMode$ NoResize=� � 	Resources    RecordingColor �� Color  PausedColor  StoppedColor  ButtonDefaultColor  
TimerStyle �� 
TargetType �� Property	 �� Value  ButtonStyle
 �� 
TargetType �� CornerRadius �� Padding �� Triggers P� Property P� Value��%/  &	        &	      &	 &     &	 9     &	 L     &	 �     7  �� $ #D32F2F���� $ #FFEB3B���� $ #388E3C���� $ #1976D2���� " ��.���  � ��$	 18(���  � ��$
	 Bold"���  � ��	 �#FFFFFF��  � ��$	 Center=���  � ��$	 Center=���  � ��$	 0,5,0,5q��� " ��.���  � ��$	 50����  � ��$	 50����  � ��$	 5q���  � ��$	 14(���  � ��$
	 Bold"���  � ��8	   ��  � ��
	 ���� ��  � ��
	 �������  � ��$	 2q���  � ��	 �� "
 ��.R�� .+��� #��z��#��z��$��2q�$ 25��$ 0q�._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ���  � ��	 �#1565C0P�  � ��$
 True��. ���  � ��	 �#0D47A1P�  � {�$ False��. ���  � ��	 �#212121.���  � RowDefinitions �� $��35 ��� $
��Auto ��� $
��Auto �.+��� -   $��TimerTextBlock��$��0��$��00:00��#��[ ��  �� Orientation$ 
Horizontal=�$��Center=�$��Center=�$��1��$��0,0,0,5q�.+��� -   $��PauseRecording��#��[ ${�False��.���� $��Uniform=�$��/pausar.png��$��20��$��20���� -   $��StartRecording��#��[ .���� $��Uniform=�$��/iniciar.png��$��20��$��20���� -   $��StopRecording��#��[ .���� $��/detener.png��$��Uniform=�$��20��$��20���� -   $��SpectrumCanvas��$��2��$��10��$��5,5,5,5q�