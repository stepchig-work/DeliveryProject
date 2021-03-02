import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { routing } from './app.routing'

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LogInComponent } from './authentication/log-in/log-in.component';
import { SignInComponent } from './authentication/sign-in/sign-in.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RestaurantsListComponent } from './restaurants-list/restaurants-list.component';
import { RestaurantsListItemComponent } from './restaurants-list/restaurant-list-component/restaurants-list-item.component';
import { RestaurantsMealItemComponent } from './restaurant-meals/restaurant-meal-item/restaurant-meal-item.component';
import { RestaurantComponent } from './restaurant-meals/restaurant.component';
import { AlertComponent } from './alert-system/alert.component';
import { BottomMenuComponent } from './bottom-menu/bottom-menu.component';
import { OrderComponent } from './order/order.component';
import { OrderMealComponent } from './order/order-meal/meal-for-order.component';

import { AuthenticationService } from './services/authentication.service';
import { RestaurantsService } from './services/restaurants.service';
import { MealsService } from './services/meals.service';
import { AlertService } from './alert-system/alert.service';
import { OrderCreationService } from './services/order-creation.service';
import { OrdersService } from './services/orders.service';
import { RestaurantUpdateCreationComponent } from './restaurants-list/restaurant-update-creation/restaurants-update-creation.component';
import { RestaurantMealUpdateCreationComponent } from './restaurant-meals/restaurant-meal-update-creation/restaurant-meal-update-creation.component';
import { AccountOrderComponent } from './account/account-order/account-order.component';
import { AccountComponent } from './account/account.component';
import { SignalRService } from './services/signalR.service';
import { OrderDescriptionComponent } from './order-description/order-description.component';
import { AccountService } from './services/account-service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    BottomMenuComponent,
    SignInComponent,
    LogInComponent,
    RestaurantsListComponent,
    RestaurantsListItemComponent,
    RestaurantComponent,
    RestaurantsMealItemComponent,
    RestaurantUpdateCreationComponent,
    RestaurantMealUpdateCreationComponent,
    OrderComponent,
    OrderMealComponent,
    AlertComponent,
    AccountComponent,
    AccountOrderComponent,
    OrderDescriptionComponent
  ],
  exports: [
    RestaurantsListItemComponent,
    RestaurantsMealItemComponent,
    OrderMealComponent,
    AlertComponent,
    AccountOrderComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    NgbModule,
    routing   
  ],
  providers: [
    AuthenticationService,
    RestaurantsService,
    MealsService,
    AlertService,
    OrderCreationService,
    OrdersService,
    SignalRService,
    AccountService],
  bootstrap: [AppComponent]
})
export class AppModule { }
