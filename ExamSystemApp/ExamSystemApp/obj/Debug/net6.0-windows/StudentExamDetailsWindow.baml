   M S B A M L   `   `   `  ����H  DExamSystemApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ExamSystemAppExamSystemApp  5   	   ,    &ExamSystemApp.StudentExamDetailsWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ExamSystemApp     5   	     � Title$  StudentExamDetailsWindow��5   	   $	��450��6*   $	��800��67    � WindowStartupLocation$ CenterScreen=�5	   	   $	��600��5
   	   $
��1000��5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $	��75* �6   �� 5      $	��61* �6   �� 5      $	��59* �6   �� 5      $	��71* �6   �� 5      $
��111* �6   �� 5      $	��57* �6   .+�5   
   �� $��
Exam Name:��6   $��Center=�6%   $��1��6C   $��Center=�6P   �� 5   
   -   5        $��examNameLbl��5      $��########��6#   $��Center=�66   $��1��6T   $��1��6a   $��Center=�6q   �� 5   
   $��Teacher Name:��6   $��Center=�6(   $��2��6F   $��Center=�6S   �� 5   
   -   5        $��teacherNameLbl��5      $��########��6&   $��Center=�69   $��2��6W   $��1��6d   $��Center=�6t   �� 5!   
   $��Minutes:��6   $��Center=�6#   $��3��6A   $��Center=�6N   �� 5"   
   -   5        $��
minutesLbl��5"      $��########��6"   $��Center=�65   $��3��6S   $��1��6`   $��Center=�6p   �� 5$   
   -   5        $��beckBtn��5$      $��Go Beck��6    $��4��62   $��30q�6?   6c   �� 5%   
   -   5        $��enterBtn��5%      $��Enter��6!   $��4��61   $��1��6>   $��30q�6N   6s   5(      5)      