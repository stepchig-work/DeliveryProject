import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';
import { Meal } from '../models/meal.model';
import { MealsService } from './meals.service';

describe('MealsService', () => {
  let service: MealsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [MealsService]
    });
    service = TestBed.inject(MealsService);
  });

  it('can load instance', () => {
    expect(service).toBeTruthy();
  });

    it('add meal api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const mealStub: Meal = <any>{};
      const accountId: number = 1;
      service.addMeal(mealStub, accountId).then(res => {
        expect(res).toEqual(mealStub);
      });
      const req = httpTestingController.expectOne('api/meals/addMeal');
      expect(req.request.method).toEqual('PUT');
      req.flush(mealStub);
      httpTestingController.verify();
    });

    it('update meal api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const mealStub: Meal = <any>{};
      const accountid: number = 1;
      service.updateMeal(mealStub, accountid).then(res => {
        expect(res).toEqual(mealStub);
      });
      const req = httpTestingController.expectOne('api/meals/updateMeal');
      expect(req.request.method).toEqual('POST');
      req.flush(mealStub);
      httpTestingController.verify();
    });

    it('get all meals api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      service.getAllElements().then(res => {
        expect(res).toEqual([]);
      });
      const req = httpTestingController.expectOne('api/meals/getAll');
      expect(req.request.method).toEqual('GET');
      req.flush([]);
      httpTestingController.verify();
    });

    it('get meal api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const mealid: number = 1;
      service.getMeal(mealid).then(res => {
        expect(res).toEqual([]);
      });
      const req = httpTestingController.expectOne('api/meals/getMeal?mealId=1');
      expect(req.request.method).toEqual('GET');
      req.flush([]);
      httpTestingController.verify();
    });

    it('get meals image api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const mealid: number = 1;
      service.getImage(mealid).then(res => {
        expect(res).toEqual([]);
      });
      const req = httpTestingController.expectOne('api/meals/getImage?mealId=1');
      expect(req.request.method).toEqual('GET');
      req.flush([]);
      httpTestingController.verify();
    });

    it('get all meals for restaurant api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const restaurantId: number = 1;
      service.getAllForRestaurant(restaurantId).then(res => {
        expect(res).toEqual([]);
      });
      const req = httpTestingController.expectOne('api/meals/getAllForRestaurant?restaurantId=1');
      expect(req.request.method).toEqual('GET');
      req.flush([]);
      httpTestingController.verify();
    });

    it('remove restaurant api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const mealid: number = 1;
      const accountid: number = 1;
      service.removeMeal(mealid, accountid).then(res => {
        expect(res).toEqual([]);
      });
      const req = httpTestingController.expectOne('api/meals/removeMeal');
      expect(req.request.method).toEqual('POST');
      req.flush([]);
      httpTestingController.verify();
    });
});
