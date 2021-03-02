import { RouterModule, Routes } from "@angular/router";
import { AccountComponent } from "./account/account.component";
import { LogInComponent } from "./authentication/log-in/log-in.component";
import { SignInComponent } from "./authentication/sign-in/sign-in.component";
import { OrderDescriptionComponent } from "./order-description/order-description.component";
import { OrderComponent } from "./order/order.component";
import { RestaurantMealUpdateCreationComponent } from "./restaurant-meals/restaurant-meal-update-creation/restaurant-meal-update-creation.component";
import { RestaurantComponent } from "./restaurant-meals/restaurant.component";
import { RestaurantUpdateCreationComponent } from "./restaurants-list/restaurant-update-creation/restaurants-update-creation.component";
import { RestaurantsListComponent } from "./restaurants-list/restaurants-list.component";

const routes: Routes = [
  { path: '', component: RestaurantsListComponent},
  { path: 'signin', component: SignInComponent },
  { path: 'login', component: LogInComponent },
  { path: 'restaurant/:id', component: RestaurantComponent },
  { path: 'account', component: AccountComponent },
  { path: 'order', component: OrderComponent },
  { path: 'order/:id', component: OrderComponent }  ,
  { path: 'restaurant-update-creation', component: RestaurantUpdateCreationComponent },
  { path: 'restaurant-update-creation/:id', component: RestaurantUpdateCreationComponent },
  { path: 'restaurant-meal-update-creation/:rid', component: RestaurantMealUpdateCreationComponent },
  { path: 'restaurant-meal-update-creation/:rid/:mid', component: RestaurantMealUpdateCreationComponent },
  { path: 'order-description/:id', component: OrderDescriptionComponent },
  
]; 

export const routing = RouterModule.forRoot(routes, { onSameUrlNavigation: "reload" }); 
