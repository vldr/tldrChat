   M S B A M L   `   `   `  ����  tldr-client    tldr_client.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  	tldr chat��5   	   $��391.982��6   $	��432��6,   $��442.982��68   $	��506��6L    � AllowsTransparency .  � WindowStyle$
 None=�6u    � 
ResizeMode$ CanResizeWithGrip=�6�   ��6�   Z� .��5      � -   5        $��10q�5       J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         Opacity$	 0.3Q�6      ShadowDepth$ 0Q�6-      
BlurRadius$ 15Q�6=    � RowDefinitions 5	   
   �� 5
      .+�5   
   �� -   5        	 ��Name$	 txtInput��5      
 �� TextWrapping$

 Wrap=�6$    �� SelectionOpacity$	 0.1Q�68   $��0q�6P   ���	#99D1D1D1���	#FFF1F1F1���	#FF474747$
��3,10q�6�   $��15(�6�   $��
-1,381,0,0q�6�   �� 5   
   -   5         ��Name$ richTxtContent��5      $��
97,52,0,42q�6.   $��0q�6B   $��4q�6V    �� 
LineHeight$ 1��6b    �� 
IsReadOnly . " �� VerticalScrollBarVisibility$
 Auto=�6�    5        5      $	 0.1Q�6#   $ 0Q�61      	Direction$	 240Q�6A   . �5      /� 5      �� 5   
   -   5        $	 txtOnlineUsers��5      $��
-1,52,0,42q�6*   $��4q�6>   $

 Wrap=�6J   $
 Auto=�6^    .  � Cursor$ Arrow��5      $
��Left=�6!   $��98��6<   $��0q�6^   ���	#FF4F4F4F���	#FFF1F1F1��6G   Z� .��6�    5        5      $	 0.1Q�6#   $ 3Q�61   $	 190Q�6A   .��5      5      � 5   
   -   5         �Name$ 
gridHeader��5      $��	-1,-1,0,0q�6#   $��53��66   $	��Top=�6B   ���	#FFA8A5A5.+�5      �� -   5         ��Name$ lblTitle��5      $��	TLDR Chat��6&   $
��Left=�6:   $��5,13,0,0q�6U   $	��Top=�6g   
���������6�   Z� 5      �� 5   
   -   5        $ lblExit��5      $��×��6!   $��0,3,7,0q�6-   $	��Top=�6>   $��43��6V   
�������$
 Hand��6u   $��Right=�6�   $��25��6�   $��Microsoft YaHei Light)�6�   $��24(�6  ��6�   Z� 6  �� 5   
   -   5         ��Name$ border��5      ���	#FFA8A5A5$��1q�69   $��	-1,-1,0,0q�6M   5      5      