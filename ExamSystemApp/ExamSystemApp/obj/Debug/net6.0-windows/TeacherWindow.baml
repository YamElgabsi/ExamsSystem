   M S B A M L   `   `   `  ����H  DExamSystemApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ExamSystemAppExamSystemApp  5   	   !    ExamSystemApp.TeacherWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ExamSystemApp     5   	     � Title$  TeacherWindow��5   	   $	��450��6   $	��800��6,    � WindowStartupLocation$ CenterScreen=�5	   	   $	��600��5
   	   $
��1000��5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��669* �6   �� 5      $
��131* �6    � RowDefinitions 5   
   �� 5      $��35 �6   �� 5      $	��88* �6   �� 5      $
��311* �6   .+�5   
   �� -   5        $��	navbarLBL��5      $��Exams System��6!   $��15(�69   
����ح�$��2��6^   �� 5   
   -   5        $��	searchLBL��5      $��Search exam:��6!   $
��Left=�68   $��30,0,0,0q�6S   $��Center=�6e   $��20(�6�   $��1��6�   $��36��6�   $	��126��6�   �� 5   
   -   5        $��searchTB��5      $��Right=�6"    �� TextWrapping$
 Wrap=�6>   $��0,0,20,0q�6R   $�� ��6d   $��Center=�6l   $	��480��6�   $��25��6�   $��1��6�   �� 5   
   -   5         ��Name$ SeachBTN��5      $��Seach��6#   $��Center=�63   $��0,0,0,0q�6P   $��1��6a   $��Center=�6n   $��60��6�   $��25��6�   $��1��6�   6�   �� 5   
   -   5        $��examsListBox��5   Y   
�������$��1��6*   $��0��67   $��0,88,0,0q�6G   $��2��6m   �� 5   
   -   5        $��editBTN��5      $��Edit selected exam��6    $��Center=�6=   $��0,50,0,0q�6Z   $��2��6l   $	��Top=�6y   $	��120��6�   $��25��6�   $��1��6�   6�   �� 5    
   -   5        $��addBTN��5       $��Add new exam��6   $��Center=�66   $��0,10,0,0q�6S   $��2��6e   $	��Top=�6r   $	��120��6�   $��25��6�   $��1��6�   6�   �� 5!   
   -   5        $��DeleteExamBtn��5!      $��Delete Exam��6&   $��Center=�6<   $��0,90,0,0q�6Y   $��2��6k   $	��Top=�6x   $	��120��6�   $��25��6�   $��1��6�   6�   �� 5"   
   -	   5        $��StatsBTN��5"      $��Show grades statistics��6!   $��Center=�6B   $��	0,130,0,0q�6_   $��2��6r   $	��Top=�6   $	��120��6�   $��25��6�   $��1��6�   6�   5%      5&      