using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class myDataBase
    {
        public static OleDbConnection myOleDbConnection = null;
        private System.Data.IDbCommand _selectCmd = null;
        private IDataReader _reader = null;
        private OleDbCommand command = null;

        private List<string> tables = null;
        public string _Statement = null;
        public string _StatementReservations = null;
        public string _StatementUsers = null;

        public myDataBase()
        {
            tables = new List<string>();
            command = new OleDbCommand();

            _Statement = "select t.x, t.y from TableBV tb, Table(SDO_Util.GetVertices(tb.position)) t";
            _StatementReservations = "select * from TableReservationsBV order by Datetime, IDTable";
            _StatementUsers = "select Username, Password from UserBV";
            Connect();
        }

        public void Connect()
        {
            try
            {
                string connectionString = "Provider=OraOLEDB.Oracle; " +
                "Data Source=192.168.128.152/ora11g; " +              // Intern
                //"Data Source=212.152.179.117/ora11g; " +                // Extern
                "User ID = d5a06; Password = d5a; OLEDB.NET=True;";

                myOleDbConnection = new OleDbConnection(connectionString);
                myOleDbConnection.Open();
            }

            catch (Exception ex)
            {
                throw new Exception("Can not connect to Database: " + ex.Message);
            }
        }

        public IDataReader getShapesReader()
        {
            _selectCmd = new OleDbCommand(_Statement, myOleDbConnection);
            _reader = _selectCmd.ExecuteReader();
            return _reader;
        }

        public IDataReader getReservationsReader()
        {
            _selectCmd = new OleDbCommand(_StatementReservations, myOleDbConnection);
            _reader = _selectCmd.ExecuteReader();
            return _reader;
        }

        public IDataReader getUsers()
        {
            _selectCmd = new OleDbCommand(_StatementUsers, myOleDbConnection);
            _reader = _selectCmd.ExecuteReader();
            return _reader;
        }
    }
}
