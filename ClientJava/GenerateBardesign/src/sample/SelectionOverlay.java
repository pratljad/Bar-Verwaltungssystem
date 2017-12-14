package sample;

import javafx.beans.property.ReadOnlyObjectProperty;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.event.EventHandler;
import javafx.geometry.Bounds;
import javafx.scene.Cursor;
import javafx.scene.Node;
import javafx.scene.control.Control;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.Region;
import javafx.scene.shape.Rectangle;

public class SelectionOverlay extends Region{
    boolean selectionRectangleVisible = true;

    /**
     * Drag handles visibility. In addition to this boolean the cell must implement ResizableI
     */
    boolean dragHandlesVisible = true;

    /**
     * The shape (cell) to which the overlay has been assigned.
     */
    final Node monitoredShape;

    private ChangeListener<Bounds> boundsChangeListener;

    /**
     * Drag handle size
     */
    double diameter = 6;

    /**
     * Drag handle half size, just to avoid / 2.0 of radius everywhere
     */
    double radius = diameter / 2.0;

    /**
     * Selection rectangle around the shape / cell
     */
    Rectangle selectionRectangle = new Rectangle();

    // Drag handles
    DragHandle dragHandleNW;
    DragHandle dragHandleNE;
    DragHandle dragHandleSE;
    DragHandle dragHandleSW;
    DragHandle dragHandleN;
    DragHandle dragHandleS;
    DragHandle dragHandleE;
    DragHandle dragHandleW;

    Node cell;

    public SelectionOverlay(final Node shape) {

        this.cell = shape;


        // mouse events only on our drag objects, but not on this node itself
        // note that the selection rectangle is only for visuals and is set to being mouse transparent
        setPickOnBounds(false);

        // the rectangle is only for visuals, we don't want any mouse events on it
        selectionRectangle.setMouseTransparent(true);

        // drag handles: drag handles must be enabled AND the cell must implement ResizableI
        dragHandlesVisible = dragHandlesVisible && (shape instanceof Node);

        if( selectionRectangleVisible) {

            // set style
            selectionRectangle.getStyleClass().add("selection_rectangle");

            getChildren().add(selectionRectangle);

        }

        if( dragHandlesVisible) {
            dragHandleNW = new DragHandle( diameter, Cursor.NW_RESIZE);
            dragHandleNE = new DragHandle( diameter, Cursor.NE_RESIZE);
            dragHandleSE = new DragHandle( diameter, Cursor.SE_RESIZE);
            dragHandleSW = new DragHandle( diameter, Cursor.SW_RESIZE);

            dragHandleN = new DragHandle( diameter, Cursor.N_RESIZE);
            dragHandleS = new DragHandle( diameter, Cursor.S_RESIZE);
            dragHandleE = new DragHandle( diameter, Cursor.E_RESIZE);
            dragHandleW = new DragHandle( diameter, Cursor.W_RESIZE);

            getChildren().addAll(dragHandleNW, dragHandleNE, dragHandleSE, dragHandleSW, dragHandleN, dragHandleS, dragHandleE, dragHandleW);
        }

        monitoredShape = shape;

        monitorBounds();

    }

    /**
     * Set bounds listener for the overlay.
     */
    private void monitorBounds() {

        // determine the shape's
        final ReadOnlyObjectProperty<Bounds> bounds = monitoredShape.boundsInParentProperty();

        // set the overlay based upon the new bounds and keep it in sync
        updateBoundsDisplay(bounds.get());

        // keep the overlay based upon the new bounds in sync
        boundsChangeListener = new ChangeListener<Bounds>() {
            @Override public void changed(ObservableValue<? extends Bounds> observableValue, Bounds oldBounds, Bounds newBounds) {
                updateBoundsDisplay(newBounds);
            }
        };

        bounds.addListener(boundsChangeListener);
    }

    /**
     * Update this overlay to match a new set of bounds.
     * @param newBounds
     */
    private void updateBoundsDisplay(Bounds newBounds) {

        if( selectionRectangleVisible) {
            selectionRectangle.setX(newBounds.getMinX());
            selectionRectangle.setY(newBounds.getMinY());
            selectionRectangle.setWidth(newBounds.getWidth());
            selectionRectangle.setHeight(newBounds.getHeight());
        }

        if( dragHandlesVisible) {
            dragHandleNW.setX(newBounds.getMinX() - radius);
            dragHandleNW.setY(newBounds.getMinY() - radius);

            dragHandleNE.setX(newBounds.getMaxX() - radius);
            dragHandleNE.setY(newBounds.getMinY() - radius);

            dragHandleSE.setX(newBounds.getMaxX() - radius);
            dragHandleSE.setY(newBounds.getMaxY() - radius);

            dragHandleSW.setX(newBounds.getMinX() - radius);
            dragHandleSW.setY(newBounds.getMaxY() - radius);

            dragHandleN.setX(newBounds.getMinX() + newBounds.getWidth() / 2.0 - radius);
            dragHandleN.setY(newBounds.getMinY() - radius);

            dragHandleS.setX(newBounds.getMinX() + newBounds.getWidth() / 2.0 - radius);
            dragHandleS.setY(newBounds.getMaxY() - radius);

            dragHandleE.setX(newBounds.getMaxX() - radius);
            dragHandleE.setY(newBounds.getMinY() + newBounds.getHeight() / 2.0 - radius);

            dragHandleW.setX(newBounds.getMinX() - radius);
            dragHandleW.setY(newBounds.getMinY() + newBounds.getHeight() / 2.0 - radius);

        }
    }

    // make a node movable by dragging it around with the mouse.
    private void enableDrag( final DragHandle dragHandle) {

        final Delta dragDelta = new Delta();

        dragHandle.setOnMousePressed(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent mouseEvent) {

                // record a delta distance for the drag and drop operation.
                dragDelta.x = dragHandle.getX() - mouseEvent.getX();
                dragDelta.y = dragHandle.getY() - mouseEvent.getY();

                dragDelta.minX = cell.getBoundsInParent().getMinX();
                dragDelta.maxX = cell.getBoundsInParent().getMaxX();
                dragDelta.minY = cell.getBoundsInParent().getMinY();
                dragDelta.maxY = cell.getBoundsInParent().getMaxY();

                getScene().setCursor(dragHandle.getDragCursor());

                mouseEvent.consume();
            }
        });
        dragHandle.setOnMouseReleased(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent mouseEvent) {
                getScene().setCursor(Cursor.DEFAULT);

                mouseEvent.consume();
            }
        });
        dragHandle.setOnMouseDragged(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent mouseEvent) {

                Node rCell = (Node) cell;

                double newX = mouseEvent.getX() + dragDelta.x;
                double newY = mouseEvent.getY() + dragDelta.y;

                dragHandle.setX(newX);
                dragHandle.setY(newY);

                if( dragHandle == dragHandleN) {

                    setHeight( rCell, dragDelta.maxY - newY - radius);
                    rCell.relocate( dragDelta.minX, newY + radius);

                }
                else if( dragHandle == dragHandleNE) {

                    setWidth( rCell, newX - dragDelta.minX + radius);
                    setHeight( rCell, dragDelta.maxY - newY - radius);
                    rCell.relocate( dragDelta.minX, newY + radius);

                }
                else if( dragHandle == dragHandleE) {

                    setWidth( rCell, newX - dragDelta.minX + radius);

                }
                else if( dragHandle == dragHandleSE) {

                    setWidth( rCell, newX - dragDelta.minX + radius);
                    setHeight( rCell, newY - dragDelta.minY + radius);

                }
                else if( dragHandle == dragHandleS) {

                    setHeight( rCell, newY - dragDelta.minY + radius);

                }
                else if( dragHandle == dragHandleSW) {

                    setWidth( rCell, dragDelta.maxX - newX - radius);
                    setHeight( rCell, newY - dragDelta.minY + radius);
                    rCell.relocate( newX + radius, dragDelta.minY);
                }
                else if( dragHandle == dragHandleW) {

                    setWidth( rCell, dragDelta.maxX - newX - radius);
                    rCell.relocate( newX + radius, dragDelta.minY);

                }
                else if( dragHandle == dragHandleNW) {

                    setWidth( rCell, dragDelta.maxX - newX - radius);
                    setHeight( rCell, dragDelta.maxY - newY - radius);
                    rCell.relocate( newX + radius, newY + radius);

                }

                mouseEvent.consume();
            }
        });
        dragHandle.setOnMouseEntered(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent mouseEvent) {
                if (mouseEvent.isPrimaryButtonDown()) {
                    return;
                }

                getScene().setCursor(dragHandle.getDragCursor());

            }
        });
        dragHandle.setOnMouseExited(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent mouseEvent) {
                if (!mouseEvent.isPrimaryButtonDown()) {
                    getScene().setCursor(Cursor.DEFAULT);
                }
            }
        });

    }

    private void setWidth( Node node, double value) {

        if( node instanceof Rectangle) {

            Rectangle shape = (Rectangle) node;
            shape.setWidth(value);

        } else if (node instanceof Control) {

            Control control = (Control) node;
            control.setPrefWidth(value);

        } else if (node instanceof Region) {

            Region region = (Region) node;
            region.setPrefWidth(value);

        }

    }

    private void setHeight( Node node, double value) {

        if( node instanceof Rectangle) {

            Rectangle shape = (Rectangle) node;
            shape.setHeight(value);

        } else if (node instanceof Control) {

            Control control = (Control) node;
            control.setPrefHeight(value);

        } else if (node instanceof Region) {

            Region region = (Region) node;
            region.setPrefHeight(value);

        }

    }

    /**
     * Drag handle
     */
    private class DragHandle extends Rectangle {

        Cursor dragCursor;

        public DragHandle( double size, Cursor dragCursor) {

            this.dragCursor = dragCursor;

            setWidth(size);
            setHeight(size);

            getStyleClass().add("selection_drag_handle");

            enableDrag( this);
        }

        public Cursor getDragCursor() {
            return dragCursor;
        }
    }

    // records relative x and y co-ordinates.
    private class Delta {
        double x;
        double y;
        double minX;
        double maxX;
        double minY;
        double maxY;
    }
}
