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
using BarverwaltungCL.Manager;
using BarverwaltungCL.Barverwaltung;
using System.Collections;
using System.Data.OleDb;
using DataBase;
using System.Data;
using BarverwaltungCL.IDManager;

namespace BarverwaltungClient
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point startPointOfPolygon;
        public myDataBase myDb;      

        public List<Table> myTables = new List<Table>();

        public MainWindow()
        {
            InitializeComponent();
            init();

            myDb = new myDataBase();

            // Load data from database
            drawTables(myDb.getShapesReader());                 // Tables
            loadReservations(myDb.getReservationsReader());     // Reservations

            //Testweise
            addSongByCustomer(5, "Bombay", "Snipe");
            addSongByCustomer(2, "Whatever it takes", "Imagine Dragons");
            addSongByCustomer(7, "do re mi", "blackbear");


            // Testweise Bestellungen
            Bestellung b1 = new Bestellung(DateTime.Now);
            b1.addItem(new ItemBV(Typ.Essen, "Toast", 3.45));
            b1.addItem(new ItemBV(Typ.Getraenk, "Pago Erdbeer", 3.2));
            b1.addItem(new ItemBV(Typ.Getraenk, "Pago Himbeer", 3.2));
            b1.addItem(new ItemBV(Typ.Essen, "Snickers", 1));

            Bestellung b2 = new Bestellung(DateTime.Now);
            b2.addItem(new ItemBV(Typ.Getraenk, "Apfeltee", 5));
            b2.addItem(new ItemBV(Typ.Getraenk, "Früchtetee", 5));

            Bestellung b3 = new Bestellung(DateTime.Now);
            b3.addItem(new ItemBV(Typ.Getraenk, "Pago", 3.45));
            ItemBV i = new ItemBV(Typ.Shisha, "Normal Shisha", 10);
            i.addTabak(new Tabak("Hasso Kirsche"));
            i.addTabak(new Tabak("Fata Morgana"));
            b3.addItem(i);

            Bestellung b4 = new Bestellung(DateTime.Now);
            b4.addItem(new ItemBV(Typ.Getraenk, "Apfeltee", 5));
            b4.addItem(new ItemBV(Typ.Getraenk, "Früchtetee", 5));

            addBestellung(1, b1);
            addBestellung(2, b2);
            addBestellung(2, b3);
            addBestellung(3, b4);
        }

        public void init()
        {
            initPlaylist();
            initReservierungen();
            initZahlungen();
            initRechnungen();
        }

        private void drawTables(IDataReader reader)
        {
            Point currPoint = new Point();
            Point lastPoint = new Point();
            bool startDrawing = false;
            bool rTPy = false;

            double width = 0;
            double height = 0;
            double left = 0;
            double top = 0;

            int cnt = 0;

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (i % 2 == 0)
                        currPoint.X = Int32.Parse(reader[i].ToString());

                    else
                        currPoint.Y = Int32.Parse((string)reader[i].ToString());
                }

                if (cnt > 4)
                {
                    cnt = 0;
                }


                if (rTPy)
                {
                    if (lastPoint != startPointOfPolygon || !startDrawing)
                    {
                        if (cnt == 0)
                        {
                            width = currPoint.X - lastPoint.X;
                            left = lastPoint.X;
                            top = lastPoint.Y;
                        }
                        else if(cnt == 1)
                        {
                            height = currPoint.Y - lastPoint.Y;
                        }
                        else if(cnt == 2)
                        {
                            addTable(Convert.ToInt32(width), Convert.ToInt32(height), Convert.ToInt32(left), Convert.ToInt32(top));
                        }

                        startDrawing = true;
                    }


                    else
                    {
                        startPointOfPolygon = currPoint;
                        startDrawing = false;
                    }

                    cnt++;
                }

                else
                    startPointOfPolygon = currPoint;

                lastPoint = currPoint;
                rTPy = true;
            }
        }


        /// <summary>
        /// Song by customer gets added
        /// </summary>
        /// <param name="Tischnummer"></param>
        /// <param name="Titel"></param>
        /// <param name="Interpret"></param>
        public void addSongByCustomer(int Tischnummer, string Titel, string Interpret)  // ID ist dann bereits in DB vorhanden oder ID Manager machen bzw. überlegen ob ID notwendig
        {
            ManagerPlaylist.addSong(1, Tischnummer, Titel, Interpret);

            refreshPlaylist();
        }

        public void addReservierung(Tischreservierung t)
        {
            ManagerReservierungen.addReservierung(t);

            refreshReservierungen();
        }

        public void initRechnungen()
        {
            GridView gvRechnungen = new GridView();

            GridViewColumn gvcID = new GridViewColumn();
            gvcID.DisplayMemberBinding = new Binding("IDRechnung");
            gvcID.Width = 80;
            gvcID.Header = "ID";

            gvRechnungen.Columns.Add(gvcID);

            GridViewColumn gvcTimestamp = new GridViewColumn();
            gvcTimestamp.DisplayMemberBinding = new Binding("TimestampR");
            gvcTimestamp.Width = 300;
            gvcTimestamp.Header = "Timestamp";

            gvRechnungen.Columns.Add(gvcTimestamp);

            GridViewColumn gvcPreis = new GridViewColumn();
            gvcPreis.DisplayMemberBinding = new Binding("PreisRechnung");
            gvcPreis.Width = 300;
            gvcPreis.Header = "Price of Bill";

            gvRechnungen.Columns.Add(gvcPreis);

            LV_Rechnungen.View = gvRechnungen;
        }

        public void initZahlungen()
        {
            ManagerRechnungen.init();

            GridView gvZahlungen = new GridView();

            GridViewColumn gvcID = new GridViewColumn();
            gvcID.DisplayMemberBinding = new Binding("IDBestellung");
            gvcID.Width = 80;
            gvcID.Header = "ID";

            gvZahlungen.Columns.Add(gvcID);

            GridViewColumn gvcTimestamp = new GridViewColumn();
            gvcTimestamp.DisplayMemberBinding = new Binding("TimestampB");
            gvcTimestamp.Width = 200;
            gvcTimestamp.Header = "Timestamp";

            gvZahlungen.Columns.Add(gvcTimestamp);

            GridViewColumn gvcTischnummer = new GridViewColumn();
            gvcTischnummer.DisplayMemberBinding = new Binding("Tischnummer");
            gvcTischnummer.Width = 200;
            gvcTischnummer.Header = "Table number";

            gvZahlungen.Columns.Add(gvcTischnummer);

            GridViewColumn gvcPreis = new GridViewColumn();
            gvcPreis.DisplayMemberBinding = new Binding("PreisBestellung");
            gvcPreis.Width = 200;
            gvcPreis.Header = "Price of Order";

            gvZahlungen.Columns.Add(gvcPreis);

            LV_Zahlungen.View = gvZahlungen;
        }

        public void initPlaylist()
        {
            ManagerPlaylist.init();
    
            GridView gvPlaylist = new GridView();

            GridViewColumn gvcTischnummer = new GridViewColumn();
            gvcTischnummer.DisplayMemberBinding = new Binding("Tischnummer");
            gvcTischnummer.Width = 350;
            gvcTischnummer.Header = "Table number";

            gvPlaylist.Columns.Add(gvcTischnummer);

            GridViewColumn gvcTitel = new GridViewColumn();
            gvcTitel.DisplayMemberBinding = new Binding("Titel");
            gvcTitel.Width = 350;
            gvcTitel.Header = "Title";

            gvPlaylist.Columns.Add(gvcTitel);

            GridViewColumn gvcInterpret = new GridViewColumn();
            gvcInterpret.DisplayMemberBinding = new Binding("Interpret");
            gvcInterpret.Width = 350;
            gvcInterpret.Header = "Artist";

            gvPlaylist.Columns.Add(gvcInterpret);

            ListViewPlaylist.View = gvPlaylist;
        }

        public void initReservierungen()
        {
            ManagerReservierungen.init();

            GridView gvPlaylist = new GridView();

            GridViewColumn gvcTNummern = new GridViewColumn();
            gvcTNummern.DisplayMemberBinding = new Binding("TischnummernLV");
            gvcTNummern.Width = 500;
            gvcTNummern.Header = "Table numbers";

            gvPlaylist.Columns.Add(gvcTNummern);

            GridViewColumn gvcTS = new GridViewColumn();
            gvcTS.DisplayMemberBinding = new Binding("TimestampReservierung");
            gvcTS.Width = 500;
            gvcTS.Header = "Date and Time";

            gvPlaylist.Columns.Add(gvcTS);

            ListViewReservierungen.View = gvPlaylist;
        }

        public void refreshPlaylist()
        {
            ListViewPlaylist.Items.Clear();

            foreach(Song s in ManagerPlaylist.getPlaylist())
            {
                ListViewPlaylist.Items.Add(s);
            }
        }

        public void refreshRechnungen()
        {
            LV_Rechnungen.Items.Clear();

            foreach (Rechnung s in ManagerRechnungen.getRechnungen())
            {
                LV_Rechnungen.Items.Add(s);
            }
        }

        public void refreshReservierungen()
        {
            ListViewReservierungen.Items.Clear();

            foreach(Tischreservierung r in ManagerReservierungen.getReservierungen())
            {
                ListViewReservierungen.Items.Add(r);
            }
        }


        private void addTable(int width, int height, int left, int top)
        {
            int tableNumber = IDGeneratorTables.getID();

            Rectangle r = new Rectangle();
            r.Width = width;
            r.Height = height;

            r.MouseLeftButtonDown += (sender, e) => rectangle_MouseLeftButtonDown(sender, e, tableNumber);
        
            r.Stroke = Brushes.Gray;
            r.Fill = Brushes.WhiteSmoke;
            r.StrokeThickness = 4;

            // add Label
            Label l = new Label();
            l.Content = "Tisch " + tableNumber;
     
            Canvas.SetLeft(l, left);
            Canvas.SetTop(l, top);

            Canvas.SetLeft(r, left);
            Canvas.SetTop(r, top);

            myTables.Add(new Table(tableNumber, r));

            CanvasTables.Children.Add(r);
            CanvasTables.Children.Add(l);
        }

        private void rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, int TableNumber)
        {
            foreach(Table t in myTables)
            {
                if(t.getTableNumber() == TableNumber)
                {
                    if(t.getTableBestellungen().Count > 0)
                    {
                        OverviewOrder ov = new OverviewOrder(t, this);
                        ov.Show();
                    }
                    else
                    {
                        MessageBox.Show("No orders at table " + TableNumber + " at the moment.");
                    }
                }
            }
        }

        public void addBestellung(int Tischnummer, Bestellung b)
        {
            foreach (Table t in myTables)
            {
                if (t.getTableNumber() == Tischnummer)
                {
                    if (t.getTablePosition().Stroke == Brushes.Green)
                        t.getTablePosition().Stroke = Brushes.Red;
                    else if (t.getTablePosition().Stroke == Brushes.Gray)
                        t.getTablePosition().Stroke = Brushes.Blue;

                    b.Tischnummer = Tischnummer;
                    t.getTableBestellungen().Add(b);
                }
            }
        }

        private void BTN_Remove_Click(object sender, RoutedEventArgs e)
        {
            BTN_Remove.BorderBrush = Brushes.Black;

            if (ListViewPlaylist.SelectedItem != null)
                ListViewPlaylist.Items.Remove(ListViewPlaylist.SelectedItem);
            else
                BTN_Remove.BorderBrush = Brushes.Red;
        }

        private void BTN_Bill_Click(object sender, RoutedEventArgs e)
        {
            if(LV_Zahlungen.SelectedItem != null)
            {
                BTN_Bill.BorderBrush = Brushes.Black;  
                OverviewRechnung ov = new OverviewRechnung(this);
                ov.Show();
            }
            else
            {
                BTN_Bill.BorderBrush = Brushes.Red;
            }
        }

        private void BTN_Save_Click(object sender, RoutedEventArgs e)
        {
            if(this.LV_Rechnungen.Items.Count == 0)
                BTN_Save.BorderBrush = Brushes.Red;
            else
                ManagerRechnungen.saveToFiles();
        }

        public void loadReservations(IDataReader reader)
        {
            List<DateTime> datetimes = new List<DateTime>();
            ArrayList tableNumbers = new ArrayList();
            List<DateTime> allDT = new List<DateTime>();
            ArrayList structureTables = new ArrayList();
            Tischreservierung t = null;

            bool start = true;
            bool isUsed = false;
            bool declared = false;

            int j = 0;
            int y = 0;

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (i % 3 == 0)
                    {
                        structureTables.Add(Int32.Parse(reader[i].ToString()));
                    }

                    else if (i % 3 == 1)
                    {
                        tableNumbers.Add(Int32.Parse(reader[i].ToString()));
                    }

                    else if (i % 3 == 2)
                    {

                        string[] datetime = reader[i].ToString().Split(' ');
                        string[] date = datetime[0].Split('.');
                        string[] time = datetime[1].Split(':');

                        allDT.Add(new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]), Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2])));
                    }
                }
            }

            foreach (int i in structureTables)
            {
                if (start)
                {
                    j = i;                                
                }

                if (declared == false)
                {
                    if (start)
                        t = new Tischreservierung(allDT.ToArray()[y]);
                    else
                        t = new Tischreservierung(allDT.ToArray()[y - 1]);

                    declared = true;
                    start = false;
                }

                if (isUsed)
                {
                    if (y < tableNumbers.Count)
                        t.addTischnummer(Convert.ToInt32(tableNumbers.ToArray()[y - 1]));

                    isUsed = false;
                }

                if (j != i)
                {
                    isUsed = true;

                    declared = false;

                    ManagerReservierungen.addReservierung(getCopy(t));
                }
                else
                {
                    if (y < tableNumbers.Count) 
                        t.addTischnummer(Convert.ToInt32(tableNumbers.ToArray()[y]));
                }

                if (y == structureTables.Count - 1)
                {
                    if (Convert.ToInt32(structureTables.ToArray()[y - 1]) == Convert.ToInt32(structureTables.ToArray()[y]))
                    {
                        ManagerReservierungen.addReservierung(getCopy(t));
                    }
                    else
                    {
                        t = new Tischreservierung(allDT.ToArray()[y]);
                        t.addTischnummer(Convert.ToInt32(tableNumbers.ToArray()[y]));
                        ManagerReservierungen.addReservierung(getCopy(t));
                    }
                }

          
                j = i;
                y++;
            }

            refreshReservierungen();
        }

        private Tischreservierung getCopy(Tischreservierung t)
        {
            Tischreservierung tr = new Tischreservierung(t.TimestampReservierung);

            foreach(int i in t.Tischnummern)
            {
                tr.addTischnummer(i);
            }

            return tr;
        }

        private void BTN_AddReservation_Click(object sender, RoutedEventArgs e)
        {
            GUIReservierung g = new GUIReservierung(this);
            g.Show();
        }

        private void BTN_Remove_Reservation_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewReservierungen.SelectedItem != null)
                ListViewReservierungen.Items.Remove(ListViewReservierungen.SelectedItem);
        }
    }
}
