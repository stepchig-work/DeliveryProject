import { Meal } from "./meal.model";
import { RestaurantImage } from "./restaurant-image.model";

export interface Restaurant {
  entityId: number;
  ownerId: number;
  name: string;
  description: string;
  meals: Meal[];
  image: RestaurantImage;
}

