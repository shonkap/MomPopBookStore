   M S B A M L   `   `   `  ����  	BookStore    BookStore.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Aav2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6G   -   5          �Name$  _window��5   	    � Title$ 
MainWindow��5   	   $	��424��6   $	��786��6)   .��5      � .+�5   
   �� $
��Left=�6   $	��271��6/   $��0,-2,0,0q�6<   
���   �$	��Top=�6]   $	��152��6u   ���	#FF26EAE56�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5      $��163,49,57,31q�6C      AutoGenerateColumns$ False��6Y      CanUserAddRows$
 True��6u   ��6%   �� *Books+   Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Book Name��6)      CellTemplate 5      �� 5      .R�5      �� ��6(   �� *Bname+5         CellEditingTemplate 5      �� 5      .R�5      �� 
���  ����67   �� *Bname+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=�5      5        5      $ 
Book Genre��6)    5       �� 5!      .R�5"      �� ��6(   �� *Gbook+5#       5%      �� 5&      .R�5'      �� 
���  ����67   �� *Gbook+$	 TwoWay=�$
 PropertyChanged=�5(      5*        5,      $ 
Book Price��6)    5-      �� 5.      .R�5/      �� ��6(   �� *Pbook+ �� StringFormat$ $ {0:C}��50       52      �� 53      .R�54      �� 
���  ����67   �� *Pbook+$	 TwoWay=�$
 PropertyChanged=�55      57        59      -   5        dataGridTemplateColumn��$ 
# in Stock��59   I    5:      �� 5;      .R�5<      �� ��6(   �� *	Stockbook+5=       5?      �� 5@      .R�5A      �� 
���  ����67   �� *	Stockbook+$	 TwoWay=�$
 PropertyChanged=�5B      5D        5Y       5Z      �� 5[      .R�5\      �� $��Delete��6�   ��6%   �� *DataContext.DeleteCommand+ �� ElementName$ _window����6h   �� 6�   5]       5_      �� 5`      .R�5a      �� $��Delete��6�   ��6%   �� *DataContext.DeleteCommand+$ _window����6h   �� 6�   5b      5d      5g      �� 5h   
   -   5         ��Name$ button_AddBook��5h      $��Add Book��6)   $
��Left=�6<   $��
61,357,0,0q�6W   $	��Top=�6k   $��65��6�   6�   �� 5i   
   -   5         ��Name$ book_search��5i      $
��Left=�6'   $��	22,49,0,0q�6B    �� TextWrapping$
 Wrap=�6U   $	��Top=�6i   $	��120��6�   6�   �� 5j   
   -   5        $ button_clearfilt��5j      $��Clear Search��6+   $
��Left=�6B   $��
38,238,0,0q�6]   $	��Top=�6q   $��86��6�   6�   �� 5k   
   -   5         ��Name$ label1��5k      $��	Book Name��6    $
��Left=�64   $��	22,23,0,0q�6O   $	��Top=�6b   �� 5l   
   -   5        $ genre_search��5l      $
��Left=�6(   $��	21,98,0,0q�6C   $
 Wrap=�6V   $	��Top=�6j   $	��120��6�   6�   �� 5m   
   -	   5        $ label2��5m      $��Genre��6    $
��Left=�60   $��	21,72,0,0q�6K   $	��Top=�6^   �� 5n   
   -
   5         ��Name$ price_dropdown��5n      $��
12,188,0,0q�6+    �� SelectedIndex$ 0��6?   $
��Left=�6�   $	��Top=�6�   .@�5o      �� $
��None��6   �� 5p      $��	Ascending��6   �� 5q      $��
Descending��6   5r      �� 5s   
   -   5        $ stock_dropdown��5s      $ 0��6+   $��
12,143,0,0q�6p   $	��Top=�6�   $
��Left=�6�   .@�5t      �� $
��None��6   �� 5u      $��	Ascending��6   �� 5v      $��
Descending��6   5w      �� 5x   
   -   5        $ label2_Copy��5x      $��Search by price��6%   $
��Left=�6?   $��
12,162,0,0q�6Z   $	��Top=�6n   �� 5y   
   -   5        $ label2_Copy1��5y      $��Search by in stock��6&   $
��Left=�6C   $��
12,117,0,0q�6^   $	��Top=�6r   �� 5z   
   -   5        $ label��5z      $��Search Column��6   $
��Left=�67   $��10,0,0,0q�6R   $	��Top=�6d   ���	#FF2959EC��6�   Z� 5}      5~      