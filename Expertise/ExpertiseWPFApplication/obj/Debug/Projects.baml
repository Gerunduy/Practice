   M S B A M L   `   `   `  ����R  NExpertiseWPFApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ExpertiseWPFApplicationExpertiseWPFApplication  5   	   &     ExpertiseWPFApplication.Projects   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:ExpertiseWPFApplication     5   	     � Title$  Проекты��5   	   $	��300��6   $
��1200��6&   $	��250��63   $	��900��6C   .��5	      � .+�5
   
   �� -   5        $��tblWait��5
   1   $��1��6   $��1��6!   $	��100(�6@   $��Обработка...��6O   $��10q�6c    �� Padding$ 315,50,200,200q�6o    �� TextAlignment$ Center=�6�   #��� ��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5         AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . $��10q�6l      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6%   	   Header$		 №��6Q   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 62   �� *
id_project+6Q     5      $ 1* 6%   $-	 'Наименование проекта��6Q   
 60   �� *name_project+6Q     5      $
 auto 6%   $	 Руководитель��6S   
 62   �� *lead_project+6S     5      $
 auto 6%   $	 Направление��6J   
 62   �� *fos+6J     5      $ 70 6%   $	 Код ГРНТИ��6R   
 60   �� *grnti_project+6R     5      $
 auto 6%   $	 Дата начала��6   
 62   �� *begin_project+ �� StringFormat$ {0:d}�� �� ConverterCulture$ ru��6     5      $
 auto 6%   $#	 Дата завершения��6}   
 62   �� *end_project+$ {0:d}��$ ru��6}     5      $
 auto 6%   $	 Экспертиза��6P   
 62   �� *	expertise+6P   4  .System.Windows.Controls.DataGridTemplateColumn  5      $
 auto 6)   $	  ��66      CellTemplate 5      �� 5      .R�5      �� -   5        $��В карточку��5   %   ��6;   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" �� �� Path$ ActualWidth�6�   5      5      5       5"      5#      