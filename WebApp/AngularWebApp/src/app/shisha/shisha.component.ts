import { Component, OnInit, ViewEncapsulation, ViewChild, ElementRef } from '@angular/core';
import {Item} from '../interfaces/item';

declare var $: any;

@Component({
  selector: 'app-shisha',
  templateUrl: './shisha.component.html',
  styleUrls: ['./shisha.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class ShishaComponent implements OnInit {
    Item = { itemIcon: "wb_cloudy", itemName: "Shisha", itemDescription: "Fruchtige Shisha", itemPrice: 20, itemAvailable: true};
    
    @ViewChild('row') row: ElementRef;
    constructor() { }
    showNotification(from, align){
      const type = ['','info','success','warning','danger'];

      const color = Math.floor((Math.random() * 4) + 1);

      $.notify({
          icon: "wb_cloudy",
          message: "Shisha added to the cart."

      },{
          type: type[color],
          timer: 4000,
          placement: {
              from: from,
              align: align
          }
      });
  }
  ngOnInit() {
  }

  ngAfterViewInit() {
    this.row.nativeElement.insertAdjacentHTML('beforeend', '<div class="col-lg-4 col-md-6 col-sm-6" ><div class="card card-stats" ><div class="card-header" data-background-color="orange"><i class="material-icons">' + this.Item.itemIcon + '</i></div><div class="card-content"><p class="description" id="itemName">' + this.Item.itemName + '</p><h3 class="itemPrice" id="itemPrice">' + this.Item.itemPrice + '€</h3></div><div class="card-footer"><button class="md-button">Add</button></div></div></div>');
  }

}
