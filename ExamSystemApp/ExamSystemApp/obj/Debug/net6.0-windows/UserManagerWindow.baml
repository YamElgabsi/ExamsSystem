   M S B A M L   `   `   `  ����H  DExamSystemApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ExamSystemAppExamSystemApp  5   	   %    ExamSystemApp.UserManagerWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ExamSystemApp     5   	     � Title$  UserManagerWindow��5   	   $	��450��6#   $	��800��60    � WindowStartupLocation$ CenterScreen=�5	   	   $	��600��5
   	   $
��1000��5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��3* �6   �� 5      $��1* �6   �� 5      $��3* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��2* �6   .+�5   
   �� -   5        $��	navbarLBL��5      $!��Exams System | User Manager��6!   $��10q�6G   $��15(�6T   
�������$��4��6y   �� 5!   
   $��Delete User��6   $��1��6&   $��2��63   $��Center=�6G   $��Center=�6d   6�   �� 5"   
   $��Add New User��6   $��1��6'   $��2��64   $��2��6D   $��Center=�6X   $��Center=�6u   6�   �� 5$   
   $	��ID:��6   $��2��6   $��Center=�6+   $��Center=�6H   6e   �� 5%   
   $	��ID:��6   $��2��6   $��2��6+   $��Center=�6;   $��Center=�6X   6u   �� 5'   
   -   5         ��Name$ textBox_ID_New��5'      $��2��6*   $��3��67   6I   �� 5(   
   -   5        $ textBox_ID_Delete��5(      $��2��6-   $��1��6:   6L   �� 5*   
   $��Name:��6   $��3��6    $��2��6-   $��Center=�6=   $��Center=�6Z   6w   �� 5+   
   -   5        $ textBox_Name��5+      $��3��6(   $��3��65   6G   �� 5-   
   $��	Password:��6   $��4��6$   $��2��61   $��Center=�6A   $��Center=�6^   6{   �� 5.   
   -   5        $ textBox_Password��5.      $��4��6,   $��3��69   6K   	� 50   
   -   5         	�Name$ radioButton_isTeacher��50      $��2��65   $��2��6E   $��Teacher��6Y   $��Center=�6k   $��5��6�   $��Center=�6�   �� 52   
   -   5         ��Name$ button_Delete��52      $��Delete��6(   $��2��6:   $��6��6N   $��Center=�6[   $��Center=�6x   $��80,10q�6�   6�   �� 53   
   -   5        $ button_Create��53      $��Create��6(   $��2��69   $��2��6I   $��6��6]   $��Center=�6j   $��Center=�6�   $��80,10q�6�   6�   �� 55   
   -	   5        $��label_Result��55      $��7��6$   $��4��61   $��Center=�6E   $��Center=�6`   6�   5:      5;      