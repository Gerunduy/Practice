   M S B A M L   `   `   `  ����R  NExpertiseWPFApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ExpertiseWPFApplicationExpertiseWPFApplication  5   	   #    ExpertiseWPFApplication.GRNTI   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:ExpertiseWPFApplication     5   	     � Title$%  Справочник ГРНТИ��5   	   $	��300��6"   $	��550��6/   $	��200��6;   $	��415��6K   .��5	      �  � RowDefinitions 5
   
   �� 5      $��30 �6   �� 5      $��1* �6   �� 5      $��20 �6    � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $��1* �6   �� 5      $��20 �6   .+�5   
   �� $��0��6   $��1��6!    �� TextAlignment$ Center=�61   $��12(�6H   $��Center=�6V   $r��lГосударственный рубрикатор научно-технической информации��6q   �� 5   
   -   5        $��tblWait��5   1   $��1��6   $��1��6!   $��80(�6@   $��Обработка...��6N    �� Padding$ 30,55,200,200q�6b   $ Center=�6z   #��� ��6�   � 5   
   -   5        $��gGrid��5   ,   $��1��6   $��1��6   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��	dataGrid1��5         AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Width5  /System.Windows.Controls.DataGridLengthConverter$
	 auto 6)   
   Header$
 Код��6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *
code_grnti+6V     5      $	 1* 6)   $
 Название��6U    64   �� *
name_grnti+6U   5      5      5      5      