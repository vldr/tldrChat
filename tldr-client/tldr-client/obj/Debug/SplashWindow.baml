   M S B A M L   `   `   `  ����  tldr-client    tldr_client.SplashWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  tldrChat��5   	   $��366.167��6   $��341.667��6+    � WindowStartupLocation$ CenterScreen=�6;    � 
ResizeMode$ NoResize=�6`    � AllowsTransparency .  � WindowStyle$
 None=�6�   ��6�   Z� .��5      � -   5        $��10q�5      ���	#FFE8E8E8 J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         Opacity$	 0.3Q�6      ShadowDepth$ 0Q�6-      
BlurRadius$ 15Q�6=   .+�5	   
   �� $��IP Address:��6   $
��Left=�6&   $��	20,76,0,0q�6A   $	��Top=�6T   ���	#FF5D5D5D$��15(�6�   �� 5
   
   -   5        	 ��Name$	 txtIP��5
      $��30��6!   $��25,107,21,0q�6-   
 �� TextWrapping$

 Wrap=�6B   $	��Top=�6V   $��5,5.5q�6n   $��0q�6~    J� RenderTransformOrigin$ 0.5,0.5&�6�   .��6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ 0.071Q�6&   T� 5      5      .��5   '    5        5      $	 0.1Q�6#   $ 0Q�61   $ 5Q�6A   .��5      5      �� 5   
   $��Name:��6   $
��Left=�6    $��
20,144,0,0q�6;   $	��Top=�6O   ���	#FF5D5D5D$��15(�6~   �� 5   
   -   5        $	 txtName��5      $��30��6#   $��25,174,21,0q�6/   $

 Wrap=�6D   $	��Top=�6X   $��5,5.5q�6p   $��0q�6�    �� 	MaxLength$ 12��6�   .��6�    5        5      $	 0.1Q�6#   $ 0Q�61   $ 5Q�6A   .��5      5      	� 5   
   -   5         	�Name$ radRed��5      $�� ��6&   $
��Left=�61   $��
29,249,0,0q�6L   $	��Top=�6`   ���	#FFFF3B3B$��0q�6�    d� 	IsChecked$
 TrueY�6�    5        5      $	 0.3Q�6#   $ 0Q�61   $ 5Q�6A   5!      	� 5"   
   -   5        $ 	radOrange��5"      $�� ��6)   $
��Left=�64   $��
53,249,0,0q�6O   $	��Top=�6c   ���	#FFFF943B$��0q�6�    5#        5$      $	 0.3Q�6#   $ 0Q�61   $ 5Q�6A   5&      	� 5'   
   -   5        $ radGreen��5'      $�� ��6(   $
��Left=�63   $��
77,249,0,0q�6N   $	��Top=�6b   ���	#FF51CF51$��0q�6�    5(        5)      $	 0.3Q�6#   $ 0Q�61   $ 5Q�6A   5+      	� 5,   
   -   5        $ radBlue��5,      $�� ��6'   $
��Left=�62   $��
99,249,0,0q�6M   $	��Top=�6a   ���	#FF51CFCF$��0q�6�    5-        5.      $	 0.3Q�6#   $ 0Q�61   $ 5Q�6A   50      �� 51   
   $��Color:��6   $
��Left=�6!   $��
22,214,0,0q�6<   $	��Top=�6P   ���	#FF5D5D5D$��15(�6   	� 52   
   -   5        $ 	radPurple��52      $�� ��6)   $
��Left=�64   $��121,249,0,0q�6O   $	��Top=�6d   ���	#FF7351CF$��0q�6�    53        54      $	 0.3Q�6#   $ 0Q�61   $ 5Q�6A   56      	� 57   
   -	   5        $ radPink��57      $�� ��6'   $
��Left=�62   $��143,249,0,0q�6M   $	��Top=�6b   ���	#FFCF51BE$��0q�6�    58        59      $	 0.3Q�6#   $ 0Q�61   $ 5Q�6A   5;      �� 5<   
   -
   5        $
��Join��5<      $
��Left=�6    $��
24,284,0,0q�6;   $	��Top=�6O   $	��136��6g   $��41��6s   $��0q�6   ���	#FFFDFDFD 5=        5>      $	 0.1Q�6#   $ 0Q�61   $ 5Q�6A   5@      � 5A   
   $��63��6   $	��Top=�6   
�������.+�5B      �� $
��Left=�6   $��34��6/   $��	16,15,0,0q�6;   $	��Top=�6N   $��75��6f   $��logo-text.png��6q   5C      �� 5D   
   -   5         ��Name$ lblExit��5D      $��×��6!   $	��Top=�6.   $��43��6F   
���   � � Cursor$
 Hand��6e   $��Right=�6�   $��25��6�   $��Microsoft YaHei Light)�6�   $��24(�6�   $��	0,10,12,0q�6�   ��6�   Z� 6�   �� 5E   
   ���	#FFBFBFBF$��1q�6)   5F      5G      