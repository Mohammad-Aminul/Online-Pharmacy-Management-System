   M S B A M L   `   `   `  ����  wpftest    wpftest.customer_order   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Customer Order��5      $	��700��5   	   $
��1000��6    � WindowStartupLocation$ CenterScreen=�6#    � WindowState$ Normal=�6H    � 
ResizeMode$ CanMinimize=�6]    � WindowStyle$ SingleBorderWindow=�5   
   
���iii�$��15q�5       � Icon$ Images/ic.jpg��5      .��5
      � ���	#FFE5E5E5$��0,0,0,0q�6#   $��Center=�64   $
��1350��6Q   .+�5   
   �� $��Customer Orders��6   $��3q�6*   $��Center=�6>   
��� � �$��25(�6o   $��SemiBold"�6}   $��540,-2,409,0q�6�   $	��Top=�6�    J� RenderTransformOrigin$ 0.504,-0.577&�6�   $��Broadway)�6�   $	��401��6�   $��44��6	  �� 5   
   $��15(�6   $��SemiBold"�6   
��� � �$��Today's Order��6G   $
��Left=�6_   $��
579,89,0,0q�6z   $	��Top=�6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dg_today_order��5   '   
�������   AutoGenerateColumns . $��Center=�6X   $��	0,124,0,0q�6u   $	��Top=�6�   $	��218��6�   $	��905��6�   6�   �� 5   
   $��
All Orders��6   $��15(�6%   $��SemiBold"�63   
��� � �$
��Left=�6\   $��586,359,0,0q�6w   $	��Top=�6�     5   
   -   5        $��dg_all_orders��5   &   
�������$��Center=�6;   $��	0,391,0,0q�6X   $	��Top=�6k   $	��226��6�   $	��901��6�   6�   �� 5   
   
����� �$
��Left=�6'   $��
221,36,0,0q�6B   $	��Top=�6V   $	��904��6n   $��57��6z   .��5      �� $��0,0,0,0q�6   $��Center=�6*   $	��904��6G   .+�5      �� -   5         ��Name$ lbluname��5      $��14(�6+   $��SemiBold"�69   $��	20,11,0,0q�6O   $��Italic$�6b   $��Select Client ID:��6u   $
��Left=�6�   $��26��6�   $	��114��6�   �� 5      -   5        	 ��Name$	 txt_client_id��5      $
��Left=�61   $��26��6L   $��150,-24,0,0q�6X   
 �� TextWrapping$

 Wrap=�6m   $�� ��6�   $	��156��6�   {�.  6�   �� 5      -   5        $ lblpass��5      $��331,-30,0,0q�6)   $��14(�6>   $��SemiBold"�6L   $��Italic$�6b   $��Enter Order Status:��6u   $
��Left=�6�   $	��Top=�6�   $	��139��6�   �� 5      -   5         ��Name$ cb_order_status��5      $
��Left=�64   $��476,-27,0,0q�6O   $	��Top=�6d   $	��207��6|   $��29��6�   .@�5      �� $��
In Process��6#   6:   �� 5      $��
Fullfilled��6#   6:   �� 5      $��
Back Order��6#   6:   �� 5      $��	Cancelled��6#   69   5      �� 5      -   5         ��Name$ btnlogin��5   >   $��15(�6   $��SemiBold"�6(   $
��Left=�6Q   $��740,-33,0,0q�6l   $	��Top=�6�   $	��138��6�   $��39��6�    �� 	ClickMode$ Press=�6�   ��5      �� 5       �� Orientation$ 
Horizontal=�6%   .+�5      �� $��Center=�6$   $��Manage Order��6A   $��0,0,0,0q�6X   $��35��6i   5       5"      5#      5$      5'      5(      