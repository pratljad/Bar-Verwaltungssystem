   M S B A M L   `   `   `  ����N  JBarverwaltungClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:BarverwaltungClientBarverwaltungClient  5   	   )    #BarverwaltungClient.GUIReservierung   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -local!clr-namespace:BarverwaltungClient  5   	   X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	      MaterialDesignBody#���   5   	   $��Regular"�5	   	   $��13(�5
   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$ GUIReservierung��5   	   $��545.475��6!   $��682.222��62   .��5      � $��
0,0,0,-1.6q�6   $	��548��6   $	��Top=�6,   .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��8q�6   $��16q�6*   ���	#FF2196F3.��5      ��    MaterialDesignTitleTextBlock#���  6   $��Center=�6O   $��25(�6l   New Reservation6�   5      &   System.Windows.Controls.Calendar  5   
   -   5          Name$ DateUI��5      $
��Left=�6#   $��	30,94,0,0q�6>   $	��Top=�6Q   $	��300��6i   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5   
   -   5          Name$ TF_Tischnummer��5          	Watermark$ Enter table number��68   $��317,159,109,347q�6W   �� 5   
   -   5         ��Name$ LB_Tischnummern��5      $
��Left=�6+   $	��188��6F   $��317,206,0,0q�6S   $	��Top=�6h   $	��316��6�   �� 5   
   -   5         ��Name$ BTN_Add��5      $	��Add��6"   $
��Left=�60   $��572,159,0,0q�6K   $	��Top=�6`   $��61��6x   $��42��6�   ���	#FFDDDDDD6�   �� 5   
   -   5        $ BTN_Save��5      $
��Save��6#   $
��Left=�62   $��523,415,0,0q�6M   $	��Top=�6b   $	��110��6z   $��57��6�   ���	#FF2196F3
���   �
�������6�   )  #MaterialDesignThemes.Wpf.TimePicker  5   
   -   5          Name$ TimeUI��5   $   $��319,110,41,0q�64   6L   5      5      