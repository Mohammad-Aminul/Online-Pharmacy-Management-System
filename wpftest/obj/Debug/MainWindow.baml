   M S B A M L   `   `   `  ����  wpftest    wpftest.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $��	win_login��5   	     � Title$   Pharmecy Management System��5   	    � WindowStartupLocation$ CenterScreen=�6,    � WindowStyle$ SingleBorderWindow=�6Q    � WindowState$ 	Maximized=�5   	    � 
ResizeMode$ CanMinimize=�5   	   
���iii�$��20q�5      $	��800��5      $	��600��6&    � Icon$ Images/ic.jpg��5
      .��5      � $��0,0,0,0q�6   .+�5   
   �� $��UniformToFill=�6   $��Images/pharmacy2.jpg��6(   $��0,0,0,0q�6F   �� 5   
   $��Welcome to Drug Museum.��6   
����� �$��40(�6D   $
��Bold"�6R   $��ExtraCondensed&�6d   $��Center=�6�   $	��Top=�6�   $	��665��6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5   
   ���	#FFE5E5E5
��� � �$��2q�6>   $��Center=�6U   $��Center=�6s   $	��326��6�   $	��472��6�   ��5      �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� $��Images/login.png��6   $��0,0,0,0q�66   $��83��6G   $��87��6S   �� 5      $��30(�6   $��Admin Panel��6*   $��0,20,0,0q�6@   5      .��5      �� $��0,0,0,0q�6   .+�5      �� -   5         ��Name$ txt_please_wait��5   f   
���  ��$8��2For login,it will take a few seconds. Please wait.��6)   $��30��6   $
��Auto��6�   $y�Visible=�6�   $��
153,0,48,0q�6�   �� 5      -   5        $ lbluname��5      $��14(�6+   $��SemiBold"�69   $��	25,35,0,0q�6O   $��Italic$�6b   $��Enter Username:��6u   $
��Left=�6�   $��26��6�   $	��120��6�   �� 5      	 +� FocusedElement	 6   �� 
 �� ElementName$
 txtuname��.+�5       �� -   5         ��Name$ txtuname��5       $
��Left=�60   $��27��6K   $��150,-26,0,0q�6W    �� TextWrapping$
 Wrap=�6l   $�� ��6�   $	��250��6�   5!      �� 5"      -   5        $ lblpass��5"      $��	25,30,0,0q�6)   $��14(�6<   $��SemiBold"�6J   $��Italic$�6`   $��Enter Password:��6s   $
��Left=�6�   $	��Top=�6�   $	��116��6�   G� 5#      -   5         G�Name$ txt_pass��5#      $
��Left=�60   $��150,-30,0,0q�6K   $	��Top=�6`   $	��250��6x   $��29��6�   �� 5$      -   5         ��Name$ btnlogin��5$   P   
��� � �$��15(�6,   $��SemiBold"�6:   $
��Left=�6c   $��
90,30,0,30q�6~   $	��Top=�6�   $	��138��6�   $��41��6�    �� 	ClickMode$ Press=�6�   .��5%      �� $ 
Horizontal=�6!   .+�5&      �� $��Images/Login3.png��6    $��	-30,0,0,0q�6;   $��47��6N   $��36��6Y   �� 5'      $��Login��6    $��	10,0,0,-6q�60   $��52��6C   5(      5)      �� 5+      -   5        $��190,-103,0,0q�5+      $	��125��6/   $��40��6;   
���  ��.��5,      �� $ 
Horizontal=�6!   .+�5-      �� $��	-40,0,0,0q�6    $��Images/quit3.jpg��63   $��64��6M   $��35��6X   �� 5.      $��SemiBold"�6    $��0,0,0,0q�66   $
��QUIT��6G   $��26��6V   50      51      52      53      �� 54   
   $��aminul��6   $��Brush Script MT)�6!   $��15(�6>   $
��Left=�6L   $��
58,466,0,0q�6g   $	��Top=�6{   $��25��6�   $��70��6�   58      59      