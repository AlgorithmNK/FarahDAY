   M S B A M L   `   `   `  ����B  >OmniApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:OmniAppOmniApp  5          OmniApp.Setting   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:OmniApp     5        � 	Resources  5      0   *OmniApp.MessageSourceToBackgroundConverter &  "MessageSourceToBackgroundConverter  CustomListBoxItemStyle �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value��%u  &	        &	        5   
   63   �� 5   
   " ��6/   .�5      ��  � |� .  6*   �� 5       � ��
 ���� 6+   �� 5       � ��
 ���� 6,   �� 5       � ��$ 0q�60   �� 5       �@�FocusVisualStyle 61   Z� �� 5       �@J�IsHitTestVisible . 61   �� 5       � ��$ 0q�6(    5      P� 5       �@J�IsMouseOver . . �5      ��  � ��
 ���� 63   �� 5       � ��
 ���� 64   5      P� 5       �@��
IsSelected . . �5      ��  � ��
 ���� 63   �� 5       � ��
 ���� 64   5      5      5       .��5!      �� ���#b1c1e7 �� CornerRadius$ 40��6"   $	��570��64   ._�5"   
   � $	��570��6   $��Bottom=�6    � RowDefinitions 5#      �� 5$      $��50 �6    �� 5%      $��70 �6    �� 5&      $��* �6    �� 5'      $	��120 �6    .+�5)      �� $��1��6   ���#d4ddf1$��Stretch=�5+      $��Stretch=�5,      .+�5-      �� -   5        	 ��Name$	 ChatName��5-      $��Настройки��6.   $��40(�5.      $��Center=�5/      $��Center=�50      
 J� RenderTransformOrigin$
 0.504,2.009&�63   51      �� 53      -   5         ��Name$ scrollViewer��53      $��2��61   $
��Auto=�6>   $��Disabled=�6a   .��55      �� -   5         ��Name$ MessagesList��55      $��0q�60   $	��330��6D   ��56      �� 57      .R�58      �� 59      ��5;      �� 5<      .R�5=      �� $��5q�6%    �� Padding$ 10q�5>   !   $ 10��5?   !   
�������$��1q�5A   !   ��5B   !   �� *Source+ �� 	Converter# [  ._�5C   "   ��  �� 
IsReadOnly .  �� TextWrapping$
 Wrap=�5E   &   $��Stretch=�5F   &   $	��700��5G   &   
������ $��0q�5I   &   ��5C   *   �� *Text+5I   &   5K      5L      ��5N      �� 5O      " ��6     �� BasedOn# [ 69   5R      5S      �� 5X      $��Адрес сервера��6   $
��Left=�6,   $��	20,39,0,0q�6G   $��2��6Z   $	��Top=�6g   $��20(�6   �� 5Y      -   5         ��Name$ 
TextBoxUrl��5Y      $
 Wrap=�6*   $�� ��6>   $	��Top=�6F   ���#d4ddf1$��20(�6s   $��185,45,75,0q�6�   $��2��6�   
�������6�   �� 5[      $��Токен Vk��6   $
��Left=�6'   $��
20,0,0,190q�6B   $��2��6V   $��Bottom=�6c   $��20(�6~   G� 5\      -   5         G�Name$ PasswordBoxVkToken��5\      $��Bottom=�66   ���#d4ddf1$��20(�6f   $��185,0,75,190q�6t   $��2��6�   
�������6�   �� 5^      $��Токен Telegram��6   $
��Left=�6-   $��
20,0,0,120q�6H   $��2��6\   $��Bottom=�6i   $��20(�6�   G� 5_      -   5        $ PasswordBoxTgToken��5_      $��Bottom=�66   ���#d4ddf1$��20(�6f   $��185,0,75,120q�6t   $��2��6�   
�������6�   �� 5a      $��Адрес Gmail��6   $
��Left=�6*   $��	20,0,0,42q�6E   $��2��6X   $��Bottom=�6e   $��20(�6�   $��45��6�   $	��124��6�   �� 5b      -   5        $ TextBoxMailAddress��5b      $
 Wrap=�62   $�� ��6F   $��Bottom=�6N   ���#d4ddf1$��20(�6~   $��185,0,458,52q�6�   $��2��6�   
�������6�   �� 5c      -   5        $��Сохранить��5c   .   $��Center=�6B   $��3��6_   $��Center=�6l   $��40��6�   $	��204��6�   
�������$��20(�6�   
�������6�   �� 5d      $��Пароль��6   $
��Left=�6%   $��
423,0,0,44q�6@   $��2��6T   $��Bottom=�6a   $��20(�6|   $��45��6�   $��84��6�   G� 5e      -	   5        $ PasswordBoxMailPassword��5e      $��Bottom=�6;   ���#d4ddf1$��20(�6k   $��512,0,75,52q�6y   $��2��6�   
�������6�   �� 5f      -
   5        $	��→��5f   1   $
��Left=�6=   $��	626,0,0,0q�6X   $��3��6k   $��Center=�6x   $��40��6�   $��38��6�   $��20(�6�   ���#b1c1e7
�������6�   5g      5h      5i      