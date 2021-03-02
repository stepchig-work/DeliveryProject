import { HttpClient, HttpParams } from "@angular/common/http";
import { Byte } from "@angular/compiler/src/util";
import { Injectable } from "@angular/core";
import { Restaurant } from "../models/restaurant.model";


@Injectable()
export class RestaurantsService {
  private addRestaurantUrl: string = "api/restaurants/addRestaurant";
  private updateRestaurantUrl: string = "api/restaurants/updateRestaurant";
  private getAllUrl: string = "api/restaurants/getAll";
  private getAllRestaurantsForUserUrl: string = "api/restaurants/getAllRestaurantsForUser";
  private getAllRestaurantsForOwnerUrl: string = "api/restaurants/getAllRestaurantsForOwner";
  private getRestaurantUrl: string = "api/restaurants/getRestaurant";
  private getImageUrl: string = "api/restaurants/getImage";
  private getRestaurantNameUrl: string = "api/restaurants/getRestaurantName";
  private removeRestaurantUrl: string = "api/restaurants/removeRestaurant";

  constructor(private readonly httpClient: HttpClient) { }

  async addRestaurant(restaurant: Restaurant, accountId: number) {
    let restaurantAddUpdateModel = {
      restaurant: restaurant,
      accountId: accountId
    };
    return await this.httpClient.post<any>(this.addRestaurantUrl, restaurantAddUpdateModel).toPromise();
  }

  async updateRestaurant(restaurant: Restaurant, accountId: number) {
    let restaurantAddUpdateModel = {
      restaurant: restaurant,
      accountId: accountId
    };
    return await this.httpClient.put<any>(this.updateRestaurantUrl, restaurantAddUpdateModel).toPromise();
  }

  async getImage(restaurantId: number) {
    let params = new HttpParams();
    params = params.set('restaurantId', restaurantId.toString());
    return await this.httpClient.get<any>(this.getImageUrl, { params: params }).toPromise();
  }

  async getAllRestaurantsForUser(restaurantId: number): Promise<any> {
    let params = new HttpParams();
    params = params.set('accountId', restaurantId.toString());
    let result = await this.httpClient.get<any>(this.getAllRestaurantsForUserUrl, { params: params }).toPromise();
    return result;
  }
  async getAllRestaurantsForOwner(restaurantId: number): Promise<any> {
    let params = new HttpParams();
    params = params.set('accountId', restaurantId.toString());
    let result = await this.httpClient.get<any>(this.getAllRestaurantsForOwnerUrl, { params: params }).toPromise();
    return result;
  }

  async getAllElements(): Promise<Restaurant[]> {
    return await this.httpClient.get<Restaurant[]>(this.getAllUrl).toPromise();
  }

  async getRestaurant(restaurantId: number, accountId: number): Promise<any> {
    
    let params = new HttpParams();
    params = params.set('restaurantId', restaurantId.toString());
    params = params.set('accountId', accountId.toString());
    return await this.httpClient.get<any>(this.getRestaurantUrl, { params: params }).toPromise();
     
  }

  async getRestaurantName(restaurantId: number): Promise<any> {
    let params = new HttpParams();
    params = params.set('restaurantId', restaurantId.toString());
    let result = await this.httpClient.get<any>(this.getRestaurantNameUrl, { params: params }).toPromise();
    return result.name;
  }

  async removeRestaurant(restaurantId: number, accountId: number) {
    let params = new HttpParams();
    params = params.set('restaurantId', restaurantId.toString());
    params = params.set('accountId', accountId.toString());
    return await this.httpClient.delete<any>(this.removeRestaurantUrl, { params: params }).toPromise();
  }

}
