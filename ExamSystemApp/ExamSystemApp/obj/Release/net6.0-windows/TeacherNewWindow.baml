   M S B A M L   `   `   `  ���� H  DExamSystemApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ExamSystemAppExamSystemApp  $    ExamSystemApp.TeacherNewWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:ExamSystemApp       � Title$  TeacherNewWindow��$	��450��$	��800�� � WindowStartupLocation$ CenterScreen=�$	��600��$
��1000��.���  � ColumnDefinitions �� $��2* ��� $��3* �.+�� -   $��	InnerGrid�� � RowDefinitions �� $��35 ��� $��35 ��� $
��389* ��� $	��37* ��� $	��33* ��� $��35 �.+��� -   $��btnAddQ��$��Add Question��$��35��$��Center=��� -   $��btnRmvQ��$��Remove Question��$��1���� -   $��questionsLB��
�������$��2���� -   $��	btnSaveQS��$��Save Question On Local��$��3���� -   $��	btnLoasQS��$��Load Question From Local��$��4���� -   $��	btnSubmit��$��Submit��$��5��$��2��� $��1��$��0�� �� $��1* ��� $��3* � �� $	��77* ��� $
��100* ��� $	��55* ��� $
��122* ��� $��80 �.+��� -   $��lblQuestion��$��15(�$��10,10,10,10q�$��0��$��	Question:���� -	   $��quesViewTextIMG��$��IMAGE��$��1��$
��Left=�$��20��$��	20,45,0,0q�$	��Top=�$��40��
�������
���   ��� -
   $��quesViewImageIMG��$
��TEXT��$��1��$��Right=�$��20��$��	0,45,30,0q�$	��Top=�$��40��
�������
���   ��� -   $��txtName��$��10,10,10,10q�
�������$��1��$��2�� �� 
IsReadOnly$
 True���� -   $��
lblAnswers��$��15(�$��10,10,10,10q�$��2��$��0��$��Answers:��$��2����  �� Orientation$ 
Horizontal=�$��1��$��2��$��0,0,0,0q�$��Center=�.+��� -   $��	btnRmvAns��$��15(�$��Center=�$��10q�$��30��$��Remove��$	��100���� -   $��	btnAddAns��$��15(�$��Center=�$��10q�$��30��$	��Add��$	��100���� -   $��
btnEditAns��$��15(�$��Center=�$��10q�$��30��$
��Edit��$	��100���� -   $��	answersSP��$��1��$��3���� -    ��Name$ button_SaveExamLocal��$��1��$��Save Exam Local��$��Right=�$��4��$��Center=�