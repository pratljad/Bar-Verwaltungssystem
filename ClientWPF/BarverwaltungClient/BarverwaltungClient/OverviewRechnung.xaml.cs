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
using BarverwaltungCL.Barverwaltung;
using BarverwaltungCL.Manager;
using BarverwaltungCL.IDManager;

namespace BarverwaltungClient
{
    /// <summary>
    /// Interaktionslogik für OverviewRechnung.xaml
    /// </summary>
    public partial class OverviewRechnung : Window
    {
        MainWindow mw;
        Rechnung r;

        public OverviewRechnung(MainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;

            r = new Rechnung(DateTime.Now);

            initList();
            fillProperties();
        }

        private void initList()
        {
            GridView gvRechnungen = new GridView();

            GridViewColumn gvcID = new GridViewColumn();
            gvcID.DisplayMemberBinding = new Binding("IDBestellung");
            gvcID.Width = 80;
            gvcID.Header = "ID";

            gvRechnungen.Columns.Add(gvcID);

            GridViewColumn gvcTimestamp = new GridViewColumn();
            gvcTimestamp.DisplayMemberBinding = new Binding("TimestampB");
            gvcTimestamp.Width = 200;
            gvcTimestamp.Header = "Timestamp";

            gvRechnungen.Columns.Add(gvcTimestamp);

            GridViewColumn gvcPreis = new GridViewColumn();
            gvcPreis.DisplayMemberBinding = new Binding("PreisBestellung");
            gvcPreis.Width = 200;
            gvcPreis.Header = "Price Order";

            gvRechnungen.Columns.Add(gvcPreis);

            LV_OverviewBestellungen.View = gvRechnungen;
        }

        private void fillProperties()
        { 
            foreach (Bestellung b in mw.LV_Zahlungen.SelectedItems)
            {
                LV_OverviewBestellungen.Items.Add(b);
                r.addBestellung(b);
            }

            Label_Preis.Content = "Total price: " + r.PreisRechnung;
        }

        private void BTN_Payed_Click(object sender, RoutedEventArgs e)
        {
            int cnt = 0;

            foreach (Table t in mw.myTables)
            {
                foreach (Bestellung be in mw.LV_Zahlungen.SelectedItems)
                {
                    if (t.getTableNumber() == be.Tischnummer)
                    {
                        foreach (Bestellung b in mw.LV_Zahlungen.Items)
                        {
                            if (b.Tischnummer == be.Tischnummer)
                            {
                                cnt++;
                            }
                        }

                        if (cnt < 2 && t.getTablePosition().Stroke == Brushes.Green)
                        {
                            t.getTablePosition().Stroke = Brushes.Gray;
                        }
                        else if (cnt < 2 && t.getTablePosition().Stroke == Brushes.Red)
                        {
                            t.getTablePosition().Stroke = Brushes.Blue;
                        }
                        else if (cnt > 1 && t.getTablePosition().Stroke == Brushes.Red)
                        {
                            t.getTablePosition().Stroke = Brushes.Green;
                        }
                    }
                }
            }

            r.IDRechnung = IDGeneratorRechnungen.getID();

            List<int> itemsToDelete = new List<int>();

            int count = 0;

            foreach (Bestellung b in mw.LV_Zahlungen.SelectedItems)
            {
                itemsToDelete.Add(count);
            }

            foreach (int i in itemsToDelete)
            {
                mw.LV_Zahlungen.Items.RemoveAt(i);
            }

            ManagerRechnungen.addRechnung(r);
            mw.refreshRechnungen();

            this.Close();
        }
    }
}
