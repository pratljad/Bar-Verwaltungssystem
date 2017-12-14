package data;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Properties;

public class ConnectionFactory {
    private static String user = null;
    private static String pwd = null;
    private static String url = null;
    private static String driver = null;
    private static String host = null;
    private static String db = null;
    private static Properties myProps = null;
    private static ConnectionFactory conn = new ConnectionFactory();

    public ConnectionFactory(){
        setProperties();
    }

    public Connection get() throws SQLException, ClassNotFoundException {
        try {
            Class.forName(driver);
        }

        catch(ClassNotFoundException ex) {
            throw new ClassNotFoundException("Driver " + driver + " not found!");
        }
        try {
            Connection c = DriverManager.getConnection(url,user,pwd);
            return c;
        }
        catch(Exception ex) {
            throw new SQLException("Couldn't connect with Url: <" + url + "> as User: " + user);
        }
    }

    public ConnectionFactory getConn() {
        return conn;
    }

    public boolean close(ResultSet rs){
        try{
            rs.close();
            return true;
        }
        catch(Exception ignore){
            return false;
        }
    }
    public boolean close(Connection con){
        try{
            con.close();
            return true;
        }
        catch(Exception ignore){
            return false;
        }
    }
    public boolean close(Statement stmt) {
        try{
            stmt.close();
            return true;
        }
        catch(Exception ignore){
            return false;
        }
    }

    public void setProperties() {
        driver = "oracle.jdbc.OracleDriver";
        url = "jdbc:oracle:thin:@212.152.179.117:1521:ora11g";
        user = "d5a06";
        pwd = "d5a";
        host = "aphrodite4";
        db = "";
    }

    public Properties getMyProps() {
        return myProps;
    }
    public String getUser() {
        return user;
    }
    public String getPwd() {
        return pwd;
    }
    public String getUrl() {
        return url;
    }
    public String getDriver() {
        return driver;
    }
    public String getHost() {
        return host;
    }
    public String getDb() {
        return db;
    }
}
