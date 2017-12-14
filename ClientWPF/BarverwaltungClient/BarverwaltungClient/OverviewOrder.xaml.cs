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

namespace BarverwaltungClient
{
    /// <summary>
    /// Interaktionslogik für OverviewOrder.xaml
    /// </summary>
    public partial class OverviewOrder : Window
    {
        Table tableOrders;
        MainWindow mw;

        public OverviewOrder(Table tableOrders, MainWindow mw)
        {
            InitializeComponent();

            this.tableOrders = tableOrders;
            this.mw = mw;

            initLV();
            fillWithOrderDetails();
        }

        public void initLV()
        {
            Label_Order.Text = "Orders from Table " + tableOrders.getTableNumber();

            GridView gvItems = new GridView();

            GridViewColumn gvcTyp = new GridViewColumn();
            gvcTyp.DisplayMemberBinding = new Binding("TypItem");
            gvcTyp.Width = 200;
            gvcTyp.Header = "Type";

            gvItems.Columns.Add(gvcTyp);

            GridViewColumn gvcBezeichnung = new GridViewColumn();
            gvcBezeichnung.DisplayMemberBinding = new Binding("Bezeichnung");
            gvcBezeichnung.Width = 200;
            gvcBezeichnung.Header = "Description";

            gvItems.Columns.Add(gvcBezeichnung);

            GridViewColumn gvcPreisItem = new GridViewColumn();
            gvcPreisItem.DisplayMemberBinding = new Binding("PreisItem");
            gvcPreisItem.Width = 180;
            gvcPreisItem.Header = "Price Item";

            gvItems.Columns.Add(gvcPreisItem);

            GridViewColumn gvcTabak = new GridViewColumn();
            gvcTabak.DisplayMemberBinding = new Binding("allTabakLV");
            gvcTabak.Width = 200;
            gvcTabak.Header = "Tabacco";

            gvItems.Columns.Add(gvcTabak);

            LV_DetailsOrder.View = gvItems;
        }

        public void fillWithOrderDetails()
        {
            foreach(Bestellung b in tableOrders.getTableBestellungen())
            {
                CB_Orders.Items.Add("Order " + b.getIDBestellung());
            }

            CB_Orders.SelectedIndex = 0;

            Label_order_Details.Content = "Timestamp: " + tableOrders.getTableBestellungen()[0].getTimestamp().ToString();

            // Füllen der LB nicht nötig weil "CB_Orders_SelectionChanged" bei Start ausgeführt wird
        }

        public void CB_Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_Orders.Items.Count > 0)
            {
                if(CB_Orders.SelectedItem == null)
                {
                    CB_Orders.SelectedIndex = 0;
                }

                LV_DetailsOrder.Items.Clear();

                foreach (Bestellung b in tableOrders.getTableBestellungen())
                {
                    if ("Order " + b.getIDBestellung() == CB_Orders.SelectedItem.ToString())   
                    {
                        foreach (ItemBV i in b.allItems())
                        {
                            LV_DetailsOrder.Items.Add(i);
                        }
                    }
                }
            }
        }

        private void BTN_OrderDone_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int j = 0;
            bool isChecked = false;
            int cnt = 0;
            int number = 0;

            foreach (Bestellung b in tableOrders.getTableBestellungen())
            {
                if ("Order " + b.getIDBestellung() == CB_Orders.SelectedItem.ToString() && isChecked == false)    // checken ob selecteditem passt
                {
                    mw.LV_Zahlungen.Items.Add(b);
                    CB_Orders.Items.Remove("Order " + b.IDBestellung);

                    foreach(Table t in mw.myTables)
                    {
                        if(t.getTableNumber() == tableOrders.getTableNumber())
                        {
                            number = i;
                            cnt = j;
                        }

                        j++;
                    }

                    isChecked = true;
                }

                i++;
            }

            mw.myTables.ElementAt(cnt).removeBestellung(number);

            if (CB_Orders.Items.Count == 0)
            {
                foreach (Table t in mw.myTables)
                {
                    if (t.getTableNumber() == tableOrders.getTableNumber())
                    {
                        t.getTablePosition().Stroke = Brushes.Green;
                    }
                }

                this.Close();
            }
            else
            {
                foreach (Table t in mw.myTables)
                {
                    if (t.getTableNumber() == tableOrders.getTableNumber())
                    {
                        t.getTablePosition().Stroke = Brushes.Red;
                    }
                }
            }
        }
    }
}
