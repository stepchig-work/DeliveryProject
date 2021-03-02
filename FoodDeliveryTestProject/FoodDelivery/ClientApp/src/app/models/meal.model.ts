import { MealImage } from "./meal-image";

export interface Meal {
  entityId: number;
  restaurantId: number;
  name: string;
  description: string;
  price: number;
  image: MealImage
}

