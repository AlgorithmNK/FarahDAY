   M S B A M L   `   `   `  ����B  >OmniApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:OmniAppOmniApp  5   	       OmniApp.Authorization   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:OmniApp     5   	   -   5        $	��Win��5   	     � Title$  Authorization��5   	   $
��1000��5	   	   $	��650��5
   	    � 
ResizeMode$ 	CanResize=�5   	    � AllowsTransparency .  � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������ .��5      �� -   5        $	��bor��5   2   
������� �� CornerRadius$ 40��6    $
��1000��6=   $	��650��6J   ._�5   
   (� .��5      � $	��650��6   $
��1000��6    $��Center=�6-   ���#b1c1e7 � ColumnDefinitions 5      �� 5      $
��195* �6'   �� 5      $
��502* �6'   �� 5      $
��503* �6'    � RowDefinitions 5      �� 5      $��40 �6$   �� 5      $��40 �6$   �� 5      �� 5      $	��120 �6$   .+�5!      �� $��2��6   $��3��6&   
�������._�5"      �� $��АВТОРИЗАЦИЯ��6   $��30(�62   $
��Left=�6@   $	��Top=�6[   $��392,-62,0,0q�6s   5#      �� 5$      $��2��6   $��3��6&   ._�5%      �� -   5        $��
UrlTextbox��5%       �� TextWrapping$
 Wrap=�60   $��30(�6E   $��400,68,150,344q�6S   ���#b1c1e7���#d4ddf16�   5&      �� 5'      $��2��6   $��3��6&   ._�5(      �� -   5        $��NameTextbox��5(      $
 Wrap=�61   $��30(�6F   $��400,130,150,282q�6T   ���#b1c1e7���#d4ddf16�   5)      �� 5*      $��2��6   $��3��6&   ._�5+      G� -   5        $��PasswordTextbox��5+   "   $��30(�69   $��400,187,150,225q�6G   ���#b1c1e7	 J� RenderTransformOrigin$	 0.334,-0.55&�6v   ���#d4ddf16�   5,      �� 5-      $��2��6   $��3��6&   $	 0.502,0.543&�6:   ._�5.      �� -   5        $��Button_Enter��5.      $��ВХОД��61   $
��Left=�6@   $��474,272,0,0q�6[   $��2��6p   $	��Top=�6}   $��2��6�   $��52��6�   $	��296��6�   ���#b1c1e7
�������$��25(�6�   $	 
0.459,0.38&�6�   
 � 	Resources
 5/       �� 
TargetType �� Property �� Value��%^   '��       �� 50      " ��6$   .�51   "   ��  �@��CornerRadius$ 6��6A   52      53      54      55      �� 57      -   5        $��CloseButton��57   �   $��2��6   $
��Left=�6)   $��	350,0,0,0q�6D   $	��Top=�6W   $��28��6o   ���#b1c1e7$��28��6�   ���#b1c1e7.��58      �� $��/Assets/Images/Close2.png��6   $��25��6?   $��25��6K   59      �� 5;      -   5        $��MinimizeButton��5;   �   $��2��6   $	��—��6)   $
��Left=�65   $��	290,2,0,0q�6P   $	��Top=�6c   $��30��6{   ���#b1c1e7$��25��6�   ���#b1c1e7$��15(�6�   5<      �� 5>      -	   5         ��Name$ MaximizeButton��5>   �   $��2��6   $
��Left=�6*   $��	320,2,0,0q�6E   $	��Top=�6X   $��30��6p   ���#b1c1e7$��25��6�   ���#b1c1e7$��15(�6�   .��5?      �� $��19��6    
���   �$��19��6;   5@      5B      �� 5C      $&�� Имя пользователя:��6   $
��Left=�64   $��145,126,0,0q�6O   $��2��6d   $	��Top=�6q   $��25(�6�   $��2��6�   �� 5D      $��Пароль:��6   $
��Left=�6*   $��108,185,0,0q�6E   $��2��6Z   $	��Top=�6g   $��1��6   $��25(�6�   �� 5E      -
   5        $��	ErrorText��5E      $J��DНеверное имя пользователя или пароль��6*   $
��Left=�6Y   $��232,158,0,0q�6t   $��2��6�   $	��Top=�6�   $��1��6�   $��20(�6�   $��2��6�   $	��522��6�   
���  ��$y�Hidden=�6�   �� 5F      $y�Hidden=�6   $��Регистрация��6-   $
��Left=�6C   $��
276,10,0,0q�6^   $��3��6r   $	��Top=�6   $��1��6�   $	��143��6�   $��2��6�   $��20(�6�   ���#b1c1e7���#b1c1e76�   �� 5G      $y�Hidden=�6   $ ��Забыли пароль?��6-   $
��Left=�6F   $��	256,0,0,0q�6a   $��3��6t   $��Center=�6�   $��1��6�   $	��188��6�   $��2��6�   $��20(�6�   ���#b1c1e7���#b1c1e76�   �� 5H      -   5         ��Name$ CheckBoxRememberUser��5H      $��Запомнить��69   $
��Left=�6M   $��108,277,0,0q�6h   $��2��6}   $	��Top=�6�   $��16��6�   $��10(�6�   $��1��6�   ���#b1c1e7$	 	0.257,0.4&�6�   $��78��6   � LayoutTransform 5I      �� 5J       �� ScaleX$ 2Q�6)    �� ScaleY$ 2Q�64   5M      �� 5N      $��URL сервера:��6   $
��Left=�6/   $��	50,63,0,0q�6J   $��2��6]   $	��Top=�6j   $��25(�6�   $��1��6�   5O      5P      5Q      5S      