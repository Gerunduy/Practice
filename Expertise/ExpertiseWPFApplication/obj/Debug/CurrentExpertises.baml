   M S B A M L   `   `   `  ����R  NExpertiseWPFApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ExpertiseWPFApplicationExpertiseWPFApplication  5   	   /    )ExpertiseWPFApplication.CurrentExpertises   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:ExpertiseWPFApplication     5   	     � Title$)  #Текущие экспертизы��5   	   $	��400��6$   $
��1000��61   $	��200��6>   $
��1000��6N   .��5	      �  � RowDefinitions 5
   
   �� 5      $��20 �6   �� 5      $��1* �6   �� 5      $��20 �6    � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $��1* �6   �� 5      $��20 �6   .+�5   
   �� -   5        $��tblWait��5   1   $��1��6   $��1��6!   $	��100(�6@   $��Обработка...��6O    �� Padding$ 50,100,50,50q�6c    �� TextAlignment$ Center=�6z   #��� ��6�   � 5   
   -   5        $
��Grid��5   ,   $��1��6   $��1��6    5      �� 5      $
��auto �6    �� 5      $��2 �6    �� 5      $��1* �6     5      �� 5      $��3* �6#   �� 5      $��15 �6#   �� 5      $��1* �6#   �� 5      $��15 �6#   �� 5       $��1* �6#   .+�5"      �� $��0��6   $��0��6%   $��Экспертизы:��65   $
��Left=�6H   &   System.Windows.Controls.DataGrid  5#      -   5        $��dgExpertiseList��5#   4   $��2��6   $��0��6$      AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .     Columns 5$      0  *System.Windows.Controls.DataGridTextColumn  5%      ,  &System.Windows.Controls.DataGridColumn	   Header$		 №��6O   
   DisplayIndex$
 0��6Z      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6k   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   ��  �� Path$ id_expertise�6k     5&      $	 Статус��6I   $
 1��6Y   $
 auto 6j    6)   �� $ status�6j     5'      $	 Наиминование��6Q   $
 2��6g   $ 1* 6x    6)   �� $ name_expertise�6x     5(      $	 дата начала��6|   $
 3��6�   $
 auto 6�    6)   �� $ date_expertise� �� StringFormat$ {0:d}�� �� ConverterCulture$ ru��6�   4  .System.Windows.Controls.DataGridTemplateColumn  5)      $	  ��6-   $
 auto 67   $
 4��6D      CellTemplate 5*      �� 5+      .R�5,   "   �� $��btnGoToExpertiseCard��6>   -   5        $��В карточку��5,   )   6Z   5-      5/      51      � 52      $��0��6   $��2��6     53      �� 54      $��1* �6$    56      �� 57      $
��auto �6'   �� 58      $��1* �6'   �� 59      $
��auto �6'   .+�5;      �� $��0��6   $��0��6)   $��Проекты:��69   $
��Left=�6I   �� 5<      -   5        $��tblProjectCount��5<   9   $��0��6   $��2��6)   $��Количество: ��6P   $��Right=�6d   5=        5>      -   5        $��dgProjectList��5>   4   $��2��6   $��2��6$    .   .  .   5?        5@      $	 Наиминование��6O   $
 0��6e   $ 1* 6v    6)   �� $ name_project�6v   5B      �� 5C      $��0��6   $��4��6%   $,��&Экспертная комиссия:��65   $
��Left=�6Q     5D      -   5        $��dgExpertList��5D   4   $��2��6   $��4��6$    .   .  .   5E        5F      $	 ФИО��6=   $
 0��6J   $ 1* 6[    6)   �� 6[   5H      5I      5J      5K      