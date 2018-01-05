drop table Desk cascade constraints;
drop table Tabacco cascade constraints;
drop table Item cascade constraints;
drop table Shishatabacco cascade constraints;
drop table ShoppingCart cascade constraints;
drop table OrderingState cascade constraints;
drop table Booking cascade constraints;
drop table Bill cascade constraints;
drop table Reservation cascade constraints;
drop table DeskReservation cascade constraints;

create table Desk(
desk_id number,
capacity number,
primary key(desk_id));

create table Tabacco(
tabacco_id number,
description varchar2(50),
extant number(1,0),
primary key(tabacco_id));

create table Item(
item_id number,
description varchar2(50),
price number,
primary key(item_id));

create table Shishatabacco(
shishatabacco_id number,
shishaItem_id number,
tabacco_id number,
foreign key(shishaItem_id) references Item(item_id),
foreign key(tabacco_id) references Tabacco(tabacco_id),
primary key(shishatabacco_id));

create table OrderingState (
    state varchar2(10) CHECK( state IN ('done','not done') ),
    primary key(state)
);

create table ShoppingCart(
  shoppingCart_id number,
  addingDate date,
  addingTime date,
  item_id number,
  shishatabacco_id number,
  amountOfItem number,
  price number,
  foreign key(item_id) references Item(item_id),
  primary key(shoppingCart_id, addingDate, addingTime)
);

create table Booking(
order_id number,
orderingDate date,
orderingTime date,
desk_id number,
shoppingCart_id number,
state varchar2(10),
foreign key(desk_id) references Desk(desk_id),
foreign key(shoppingCart_id, orderingDate, orderingTime) references ShoppingCart(shoppingCart_id, addingDate, addingTime),
foreign key(state) references OrderingState(state),
primary key(order_id, orderingDate, orderingTime));

create table Bill(
bill_id number,
payingDate date,
payingTime date,
foreign key(bill_id, payingDate, payingTime) references Booking(order_id, orderingDate, orderingTime),
primary key(bill_id, payingDate, payingTime));

create table Reservation(
reservationDate date,
reservationTime date,
reserver varchar2(30),
amountOfPeople number,
primary key(reservationDate, reservationTime));

create table DeskReservation(
desk_id number,
reservationDate date,
reservationTime date,
foreign key(desk_id) references Desk(desk_id),
foreign key(reservationDate, reservationTime) references Reservation(reservationDate, reservationTime),
primary key(desk_id, reservationDate, reservationTime));