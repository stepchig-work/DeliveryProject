import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Meal } from "../models/meal.model";


@Injectable()
export class MealsService {
  private addMealUrl: string = "api/meals/addMeal";
  private updateMealUrl: string = "api/meals/updateMeal";
  private getMealUrl: string = "api/meals/getMeal";
  private getAllUrl: string = "api/meals/getAll";
  private getImageUrl: string = "api/meals/getImage";
  private getAllForRestaurantUrl: string = "api/meals/getAllForRestaurant";
  private removeMealUrl: string = "api/meals/removeMeal";

  constructor(private readonly httpClient: HttpClient) { }

  async addMeal(meal: Meal, accountId: number) {
    let mealAddUpdateModel = {
      meal: meal,
      accountId: accountId
    };
    return await this.httpClient.put<any>(this.addMealUrl, mealAddUpdateModel).toPromise();
  }

  async updateMeal(meal: Meal, accountId: number) {
    let mealAddUpdateModel = {
      meal: meal,
      accountId: accountId
    };
    return await this.httpClient.post<any>(this.updateMealUrl, mealAddUpdateModel).toPromise();
  }

  async getMeal(mealId: number) {
    let params = new HttpParams();
    params = params.set('mealId', mealId.toString());
    return await this.httpClient.get<any>(this.getMealUrl, { params: params }).toPromise();
  }

  async getImage(mealId: number) {
    let params = new HttpParams();
    params = params.set('mealId', mealId.toString());
    return await this.httpClient.get<any>(this.getImageUrl, { params: params }).toPromise();
  }

  async getAllElements(): Promise<Meal[]> {
    return await this.httpClient.get<Meal[]>(this.getAllUrl).toPromise();
  }

  async getAllForRestaurant(restaurantId: number): Promise<Meal[]>{
    let params = new HttpParams();
    params = params.set('restaurantId', restaurantId.toString());
    return await this.httpClient.get<any>(this.getAllForRestaurantUrl, { params: params }).toPromise();
  }

  async removeMeal(mealId: number, accountId: number) {
    let removeMealModel = {
      mealId: mealId,
      accountId: accountId
    };
    return await this.httpClient.post<any>(this.removeMealUrl, removeMealModel).toPromise();
  }
}
