package sample;

import data.ConnectionFactory;
import javafx.application.Application;
import javafx.application.Platform;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.layout.StackPane;
import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;
import javafx.scene.text.Font;
import javafx.stage.Stage;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.scene.control.Alert;
import javafx.stage.Window;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.Statement;
import java.util.ArrayList;

public class Main extends Application {

    SelectionModel selectionModel;
    MouseHandler mouseHandler;
    Group objectLayer;

    ConnectionFactory cf = new ConnectionFactory();
    Connection conn;

    ArrayList<Rectangle> rectangleList = new ArrayList<Rectangle>();

    @Override
    public void start(Stage primaryStage) throws Exception{
        primaryStage.setTitle("DesignYourBar");

        try {
            Class.forName("oracle.jdbc.OracleDriver");

            conn = DriverManager.getConnection(cf.getUrl(), cf.getUser(), cf.getPwd());
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }

        Label labelHeader = new Label("    Design your own hookah bar!");
        labelHeader.setLayoutY(20);
        labelHeader.setFont(new Font("Algerian", 30));

        Button btnAdd = new Button();
        btnAdd.setLayoutX(170);
        btnAdd.setLayoutY(65);
        btnAdd.setScaleX(1.6);
        btnAdd.setScaleY(1.2);
        btnAdd.setText("Add Table");
        btnAdd.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) {
                addTable();
            }
        });

        Button btnRemove = new Button();
        btnRemove.setLayoutX(35);
        btnRemove.setLayoutY(65);
        btnRemove.setScaleX(1.2);
        btnRemove.setScaleY(1.2);
        btnRemove.setText("Remove Table");
        btnRemove.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) {
                removeTable();
            }
        });

        Button btnSend = new Button();
        btnSend.setLayoutX(1035);
        btnSend.setLayoutY(580);
        btnSend.setScaleX(2);
        btnSend.setScaleY(2);
        btnSend.setText("Save");
        btnSend.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) {
                saveTables();
            }
        });


        Group root = new Group();

        // object layer
        objectLayer = new Group();
        root.getChildren().add(objectLayer);

        // selection layer on top of object layer
        Group selectionLayer = new Group();
        root.getChildren().add(selectionLayer);

        selectionModel = new SelectionModel( selectionLayer);
        mouseHandler = new MouseHandler( selectionModel);

        Scene scene = new Scene( root, 1120, 640);      // gleiche Größe wie im C# Client
        scene.setFill(Color.LIGHTGRAY);
        // clear selection when user clicks outside of cell
        scene.setOnMousePressed(mouseEvent -> selectionModel.clear());

        scene.getStylesheets().add(getClass().getResource("application.css").toExternalForm());

        // Add Buttons, Labels etc.
        root.getChildren().add(btnAdd);
        root.getChildren().add(btnRemove);
        root.getChildren().add(btnSend);
        root.getChildren().add(labelHeader);

        primaryStage.setResizable(false);
        primaryStage.setScene( scene);
        primaryStage.show();
    }

    public void addTable() {
        // Add new "Table"
        Rectangle rect = new Rectangle(100,100);
        rect.setFill(Color.BROWN);
        rect.setStroke(Color.BLACK);
        rect.setStrokeWidth(2);
        rect.relocate(450,100);
        mouseHandler.makeDraggable(rect);

        rectangleList.add(rect);
        objectLayer.getChildren().addAll(rect);
    }

    public void removeTable() {
        // Remove last "Table" that got added
        if (rectangleList.size() != 0) {
            objectLayer.getChildren().remove(rectangleList.toArray()[rectangleList.size() - 1]);
            rectangleList.remove(rectangleList.size() - 1);
        }
    }

    public void saveTables() {
        if (rectangleList.size() != 0) {
            try {
                Statement stmt = conn.createStatement();

                for (int i = 0; i < rectangleList.size(); i++) {
                    Rectangle r = (Rectangle)rectangleList.toArray()[i];

                    String cmd = "INSERT INTO TableBV VALUES(" + (i+1) + ", SDO_GEOMETRY( 2003, NULL, NULL, SDO_ELEM_INFO_ARRAY(1,1003,1), SDO_ORDINATE_ARRAY(" + (int)r.getLayoutX() + "," + (int)r.getLayoutY() + ", " + (int)(r.getLayoutX() + r.getWidth()) + "," + (int)r.getLayoutY() + ", " + (int)(r.getLayoutX() + r.getWidth()) + "," + (int)(r.getLayoutY() + r.getHeight()) + ", " + (int)r.getLayoutX() + "," + (int)(r.getLayoutY() + r.getHeight()) + ", " + (int)r.getLayoutX() + "," + (int)r.getLayoutY() + ")))";

                    stmt.execute(cmd);
                }

                stmt.close();
                conn.close();

                Alert alert = new Alert(Alert.AlertType.CONFIRMATION);
                alert.setHeaderText("Tables got filled to the database correctly.");
                alert.getDialogPane().setExpandableContent(new ScrollPane(new TextArea("You can now use the cient containing your tables.")));
                alert.showAndWait();
                Platform.exit();
            } catch (Exception ex) {
                showAlert(ex.getMessage());
            }
        } else {
            showAlert("Add at least one table.");
        }
    }

    public void showAlert(String msg) {
        Alert alert = new Alert(Alert.AlertType.ERROR);
        alert.setHeaderText("An exception occurred!");
        alert.getDialogPane().setExpandableContent(new ScrollPane(new TextArea(msg)));
        alert.showAndWait();
    }

    public static void main(String[] args) {
        launch(args);
    }
}
