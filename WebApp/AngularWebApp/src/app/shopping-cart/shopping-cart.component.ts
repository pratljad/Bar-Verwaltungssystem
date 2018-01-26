import { Component, OnInit, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import {Item} from '../interfaces/item';
declare var $: any;

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ShoppingCartComponent implements OnInit {
    constructor() { }
    showNotification(from, align){
        const type = ['','info','success','warning','danger'];

        const color = Math.floor((Math.random() * 4) + 1);

        $.notify({
            icon: "shopping_cart",
            message: "Welcome to your <b>Shopping-Cart</b>"

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
    }
    

}
