   M S B A M L   `   `   `  ����R  NExpertiseWPFApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ExpertiseWPFApplicationExpertiseWPFApplication  5   	   %    ExpertiseWPFApplication.Experts   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:ExpertiseWPFApplication     5   	     � Title$  Эксперты��5   	   $	��400��6   $
��1000��6'   $	��250��64   $	��900��6D   .��5	      � .+�5
   
   �� -   5        $��tblWait��5
   1   $��1��6   $��1��6!   $	��100(�6@   $��Обработка...��6O   $��10q�6c    �� Padding$ 220,100,200,200q�6o    �� TextAlignment$ Center=�6�   #��� ��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	dataGrid1��5         AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . $��10q�6l      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6%   	   Header$		 №��6P   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 62   �� *	id_expert+6P     5      $ 3* 6%   $	 ФИО��6J   
 60   �� *FIO+6J     5      $
 auto 6%   $	 Организация��6R   
 62   �� *
job_expert+6R     5      $
 auto 6%   $	 Должность��6S   
 62   �� *post_expert+6S     5      $
 auto 6%   $<	 6Ученая степень, ученое звание��6[   
 62   �� *degree_rank_expert+6[     5      $ * 6%   $;	 5Кол-во проведенных экспертиз��6T   
 6/   �� *countexpertise+6T     5      $
 auto 6%   $	 Статус��6O   
 62   �� *status+6O   4  .System.Windows.Controls.DataGridTemplateColumn  5      $
 auto 6)   $	  ��66      CellTemplate 5      �� 5      .R�5      �� -   5        $��В карточку��5   %   ��6;   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" �� �� Path$ ActualWidth�6�   5      5      5!      5#      5$      