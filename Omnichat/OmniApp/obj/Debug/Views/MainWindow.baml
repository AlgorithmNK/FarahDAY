   M S B A M L   `   `   `  ����B  >OmniApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:OmniAppOmniApp  5   	       OmniApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $ucclr-namespace:OmniApp     5   	   -   5        $	��650��5   	   $
��1200��5	   	     � 
ResizeMode$  	CanResize=�5
   	    � AllowsTransparency .  � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � Icon$ /Assets/Images/file.ico��5   	   .��5      �� -   5        $	��bor��5   9   $	��650��6   $
��1200��6    �� CornerRadius$ 20��6'   ���#ffffff._�5   
   (�  (� Stretch$ Uniform=�6   .��5      � -   5         �Name$
 Main��5   -   $	��650��6   $
��1200��6     � RowDefinitions 5      �� 5      $��60 �6$   �� 5      $��* �6$   	 � ColumnDefinitions	 5      �� 5      $��50 �6'   �� 5      $	��280 �6'   �� 5      $��* �6'   .+�5      �� 
�������$��0��6,   $ 20��5       $��0��5!      ._�5"      � .+�5#      �� -   5        $	��Bt1��5#   :   
������ 
�������
 � 	Resources
 5%       �� 
TargetType �� Property �� Value��%_   '��       �� 5&   "   " ��6(   .�5'   &   ��  �@��CornerRadius$ 18��6E   5(   #   5)      .��5*      � .+�5+   "   �� $��_��6,   ���#5676c0$��50(�6J   $��13, -33 10 0q�5,      $
��Bold"�6.   �� 5-   "   $��_��6,   ���#5676c0$��50(�6J   $��13 -43 10 0q�5.      �� 5/   "   $��_��6,   ���#5676c0$��50(�6J   $��13 -53 10 0q�50      51      52      53      54      � 55      $��1��6   $��1��6'   $��Stretch=�56       57      �� 58      $	��77* �6(   �� 59      $
��514* �6(   .+�5;      �� -   5        $
��grid��5;   4   $��2��6#   .��5<      �� -   5        $��	MenuPlace��5<   =   $	��Top=�6%   5=      5>      �� 5?      -   5        $	��img��5?   w   $��/Assets/Images/Fon.png��6    J� RenderTransformOrigin$ 0.036,-1.434&�6<   $��-51,0,-878,0q�6a   $��5��6�   z�5@      V� 5A      .~�5B   "   ��  �� ScaleX$ -1Q�61   �� 5C   "   �� 5D   "   T� 5E   "   5F      5H      �� 5I      -   5        $��label1��5I   f   $��Недавние��6   $��15(�6/   
�������$��8,64,-12,448q�6P   $��3��6t   �� 5J      -	   5        $��label2��5J   h   $��Настройки��6   $��15(�60   
�������$��10,34,-14,438q�6Q   $��1��6v   $��2��6�   5K      � 5L      -
   5        $ MainGrid��5L      $��2��6)   $��1��69   
�������.+�5O         OmniApp.WithoutChat  -   5          Name$ WithoutChat��5O   %   $y�Visible=�6:      OmniApp.ChatWindow  5P      -   5          Name$ 
ChatWindow��5P   $   $y�	Collapsed=�68      OmniApp.Setting  5Q      -   5          Name$ SettingWindow��5Q   !   $y�	Collapsed=�68      OmniApp.Setting2  5R      -   5          Name$ SettingWindow2��5R   "   $y�	Collapsed=�6:   �� 5S      ._�5T      �� -   5        $��ProfilPlace��5T   =   $	��Top=�6%   $��560,10,40,0q�6P   5U      5V      5X      �� 5Y      $��1��6   $
��Left=�6)   $��	60,10,0,5q�6D   $	��Top=�6W   
�������
�������.��5Z      �� $!��/Assets/Images/Messages.png��6   6C   5[      � 5\      -   5        $ grid1��5\      $��0��6&   $��1��66   ���#5676c0$��	0,0,0,-42q�6X   .+�5^      �� $	��Top=�6!   $��0 50 0 0q�69   .+�5_      �� $��50��6   
������ ���#5676c0.��5`   
   �� -   5         ��Name$ 
textblock1��5`      $
��🕓��6(   $��46��62   $��46��6>   
������� �� TextAlignment$ Center=�5a      $��32(�5b      ���#5676c0$ 0.511,-0.48&�5c   !   5d      �� 5e      -   5        $��50��5e      
������ ���#5676c0.��5f   
   �� $��⚙️��6   $��46��6   $��46��6*   
�������$ Right=�5g      $��32(�5h      ���#5676c0$ 0.561,-0.648&�5i   !   5j      5l      5m      �� 5n      -   5        $��CloseButton��5n   �   $��2��6   $
��Left=�6)   $��	826,3,0,0q�6D   $	��Top=�6W   $��28��6o   
�������$��28��6�   
�������.��5o      �� $��/Assets/Images/Close.png��6   $��25��6>   $��25��6J   5p      �� 5r      -   5        $��MinimizeButton��5r   �   $��2��6   $	��—��6)   $
��Left=�65   $��	764,5,0,0q�6P   $	��Top=�6c   $��30��6{   
�������$��25��6�   
�������$��15(�6�   5s      �� 5u      -   5         ��Name$ MaximizeButton��5u   �   $��2��6   $
��Left=�6*   $��	796,5,0,0q�6E   $	��Top=�6X   $��30��6p   
�������$��25��6�   
�������$��15(�6�   .��5v      �� $��19��6    
���   �$��19��6;   5w      5y      5{      5|      5}      5~      