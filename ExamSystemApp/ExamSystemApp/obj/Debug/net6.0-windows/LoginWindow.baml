   M S B A M L   `   `   `  ����H  DExamSystemApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ExamSystemAppExamSystemApp  5   	       ExamSystemApp.LoginWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ExamSystemApp     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�5	   	   $	��600��5
   	   $
��1000��5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $	��42* �6   �� 5      $	��85* �6   �� 5      $	��64* �6   �� 5      $	��68* �6   �� 5      $	��90* �6   �� 5      $	��85* �6   .+�5   
   �� -   5        $��	navbarLBL��5      $��Exams System��6!   $��10q�68   $��15(�6E   
����ح�$��2��6j   �� 5   
   -   5        $��SignLBL��5      $��Sign in��6   $��Center=�61   $��Center=�6O   $��30(�6j   $��1��6x   $��2��6�   �� 5   
   -   5        $��	userIdLBL��5      $	��ID:��6!   $��Center=�6/   $��Center=�6L   $��20(�6g   $��2��6u   �� 5    
   -   5        $��userIdTB��5       $��Center=�6"    �� TextWrapping$
 Wrap=�6@   $�� ��6T   $��Center=�6\   $	��200��6w   $��25��6�   $��2��6�   $��1��6�   �� 5"   
   -   5        $��userPasswordLBL��5"      $��	Password:��6'   $��Center=�6;   $��Center=�6Y   $��20(�6t   $��3��6�   �� 5#   
   -   5        $��userPasswordTB��5#      $��Center=�6(   $
 Wrap=�6F   $�� ��6Z   $��Center=�6b   $	��200��6}   $��25��6�   $��3��6�   $��1��6�   �� 5%   
   -   5         ��Name$ errorMessageTB��5%      $�� ��6,   $
��Wrap=�64   $��Right=�6I   $��70��6e   $	��300��6q   
���  ��$��4��6�   �� 5(   
   -   5        $��	signinBTN��5(      $��Sign in��6"   $��Center=�64   $��Center=�6Q   $��30��6l   $	��100��6x   $��4��6�   $��1��6�   5)      5*      