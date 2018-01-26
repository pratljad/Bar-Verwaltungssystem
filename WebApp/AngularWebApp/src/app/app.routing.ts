import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

/*import { DashboardComponent } from './dashboard/dashboard.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { TableListComponent } from './table-list/table-list.component';
import { TypographyComponent } from './typography/typography.component';
import { IconsComponent } from './icons/icons.component';
import { MapsComponent } from './maps/maps.component';
import { NotificationsComponent } from './notifications/notifications.component';*/
import { SnacksComponent } from './snacks/snacks.component';
import { ShishaComponent } from './shisha/shisha.component';
import { DrinksComponent } from './drinks/drinks.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';

const routes: Routes =[
    /*{ path: 'dashboard',      component: DashboardComponent },
    { path: 'user-profile',   component: UserProfileComponent },
    { path: 'table-list',     component: TableListComponent },
    { path: 'typography',     component: TypographyComponent },
    { path: 'icons',          component: IconsComponent },
    { path: 'maps',           component: MapsComponent },
    { path: 'notifications',  component: NotificationsComponent },*/
    { path: 'shopping-cart', component: ShoppingCartComponent },
    { path: 'snacks',         component: SnacksComponent },
    { path: 'drinks',         component: DrinksComponent },
    { path: 'shisha',         component: ShishaComponent },
    { path: '',               redirectTo: 'snacks', pathMatch: 'full' }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
  ],
})
export class AppRoutingModule { }
