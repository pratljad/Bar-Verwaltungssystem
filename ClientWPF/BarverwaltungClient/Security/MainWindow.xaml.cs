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
using BarverwaltungClient;
using DataBase;
using System.Data;

namespace Security
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public myDataBase myDb;
        private List<KeyValuePair<string, string>> users = new List<KeyValuePair<string, string>>();

        public MainWindow()
        {
            InitializeComponent();

            myDb = new myDataBase();
            loadUsers(myDb.getUsers());

            PWBox.Focus();
        }


        private void BTN_Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (KeyValuePair <string, string> kvpair in users)
                {
                    if(kvpair.Key == CB_User.SelectedItem.ToString())
                    {
                        if(kvpair.Value == PWBox.Password)
                        {
                            BarverwaltungClient.MainWindow mw = new BarverwaltungClient.MainWindow();
                            mw.Show();

                            this.Close();
                        }
                    }
                }

                BTN_Login.BorderBrush = Brushes.Red;
            }
            catch (Exception)
            {
                MessageBox.Show("No access to the database.");
            }
        }

        public void loadUsers(IDataReader reader)
        {
            try
            {
                string username = "";

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (i % 2 == 0)
                        {
                            username = reader[i].ToString();
                            CB_User.Items.Add(username);
                        }
                        else
                        {
                            users.Add(new KeyValuePair<string, string>(username, reader[i].ToString()));
                        }
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("No access to the database.");
            }
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    BTN_Login_Click(sender, e);
                    break;
            }
        }
    }
}
