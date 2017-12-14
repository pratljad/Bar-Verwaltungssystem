using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace ReservierungGUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BTN_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LB_Tischnummern.Items.Add(int.Parse(TF_Tischnummer.Text));
                TF_Tischnummer.Text = "";
                TF_Tischnummer.Watermark = "Enter table number";
            }
            catch(Exception)
            {
                TF_Tischnummer.Text = "";
                TF_Tischnummer.Watermark = "Value must be a number";
            }
        }
    }
}
