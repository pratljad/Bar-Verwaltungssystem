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
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using BarverwaltungCL.Manager;
using BarverwaltungCL.Barverwaltung;
using System.Data.OleDb;
using DataBase;
using System.Data;
using BarverwaltungCL.IDManager;

namespace BarverwaltungClient
{
    /// <summary>
    /// Interaktionslogik für GUIReservierung.xaml
    /// </summary>
    public partial class GUIReservierung : Window
    {
        MainWindow mw;

        OleDbCommand command = new OleDbCommand();
        OleDbTransaction transaction = null;

        public GUIReservierung(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void BTN_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LB_Tischnummern.Items.Add(int.Parse(TF_Tischnummer.Text));
                TF_Tischnummer.Text = "";
                TF_Tischnummer.Watermark = "Enter table number";
            }
            catch (Exception)
            {
                TF_Tischnummer.Text = "";
                TF_Tischnummer.Watermark = "Value must be a number";
            }
        }

        private void BTN_Save_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Tischnummern.Items.Count != 0)
            {
                try
                {
                    BTN_Save.BorderBrush = Brushes.Black;
                    Tischreservierung tr = new Tischreservierung(new DateTime(DateUI.SelectedDate.Value.Year, DateUI.SelectedDate.Value.Month, DateUI.SelectedDate.Value.Day, TimeUI.SelectedTime.Value.Hour, TimeUI.SelectedTime.Value.Minute, 0));

                    foreach (int i in LB_Tischnummern.Items)
                    {
                        tr.addTischnummer(i);
                    }

                    insertToDatabase(tr);

                    ManagerReservierungen.addReservierung(tr);

                    mw.refreshReservierungen();

                    this.Close();
                }
                catch (Exception)
                {
                    BTN_Save.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                BTN_Save.BorderBrush = Brushes.Red;
            }
        }

        private void insertToDatabase(Tischreservierung t)
        {
            // Noch checken - Remove in Datenbank und Reservierung nicht möglich wenn gleicher Tisch und Datum (jedoch Uhrzeit anders)

            transaction = myDataBase.myOleDbConnection.BeginTransaction(IsolationLevel.Serializable);

            command.Connection = myDataBase.myOleDbConnection;
            command.Transaction = transaction;

            int ID = IDGeneratorReservierungen.getID();

            foreach(int tn in t.Tischnummern)
            {
                command.CommandText = "insert into TableReservationsBV values(" + ID + "," + tn + ", TO_DATE('" + t.TimestampReservierung.Year + "/" + t.TimestampReservierung.Month + "/" + t.TimestampReservierung.Day +
                    " " + t.TimestampReservierung.Hour + ":" + t.TimestampReservierung.Minute + ":" + t.TimestampReservierung.Second + "', 'yyyy/mm/dd hh24:mi:ss'))";

                command.ExecuteNonQuery();
            }

            transaction.Commit();
            
            transaction.Dispose();
        }
    }
}
