import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';
import { Restaurant } from '../models/restaurant.model';
import { RestaurantsService } from './restaurants.service';

describe('RestaurantsService', () => {
  let service: RestaurantsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [RestaurantsService]
    });
    service = TestBed.inject(RestaurantsService);
  });

  it('can load instance', () => {
    expect(service).toBeTruthy();
  });

    it('add new restaurant api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const restaurantStub: Restaurant = <Restaurant>{};
      const accountId: number = 1;
      service.addRestaurant(restaurantStub, accountId).then(res => {
        expect(res).toEqual(restaurantStub);
      });
      const req = httpTestingController.expectOne(
        'api/restaurants/addRestaurant'
      );
      expect(req.request.method).toEqual('POST');
      req.flush(restaurantStub);
      httpTestingController.verify();
    });

    it('update restaurant api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const restaurantStub: Restaurant = <any>{};
      const accountId: number = 1;
      service.updateRestaurant(restaurantStub, accountId).then(res => {
        expect(res).toEqual(restaurantStub);
      });
      const req = httpTestingController.expectOne(
        'api/restaurants/updateRestaurant'
      );
      expect(req.request.method).toEqual('PUT');
      req.flush(restaurantStub);
      httpTestingController.verify();
    });

    it('get all restaurants api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      service.getAllElements().then(res => {
        expect(res).toEqual([]);
      });
      const req = httpTestingController.expectOne('api/restaurants/getAll');
      expect(req.request.method).toEqual('GET');
      req.flush([]);
      httpTestingController.verify();
    });

    it('get all restaurants for owner api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const accountid: number = 1;
      service.getAllRestaurantsForOwner(accountid).then(res => {
        expect(res).toEqual([]);
      });
      const req = httpTestingController.expectOne('api/restaurants/getAllRestaurantsForOwner?accountId=1');
      expect(req.request.method).toEqual('GET');
      req.flush([]);
      httpTestingController.verify();
    });


    it('get all restaurants for user api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const accountid: number = 1;
      service.getAllRestaurantsForUser(accountid).then(res => {
        expect(res).toEqual([]);
      });
      const req = httpTestingController.expectOne('api/restaurants/getAllRestaurantsForUser?accountId=1');
      expect(req.request.method).toEqual('GET');
      req.flush([]);
      httpTestingController.verify();
    });

    it('get restaurant image api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const restaurantid: number = 1;
      service.getImage(restaurantid).then(res => {
        expect(res).toEqual([]);
      });
      const req = httpTestingController.expectOne('api/restaurants/getImage?restaurantId=1');
      expect(req.request.method).toEqual('GET');
      req.flush([]);
      httpTestingController.verify();
    });

    it('get restaurant name api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const restaurantid: number = 1;
      service.getRestaurantName(restaurantid).then(res => {
        expect(res).toEqual(undefined);
      });
      const req = httpTestingController.expectOne('api/restaurants/getRestaurantName?restaurantId=1');
      expect(req.request.method).toEqual('GET');
      req.flush([]);
      httpTestingController.verify();
    });


    it('get restaurant api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const restaurantid: number = 1;
      const accountId: number = 1;
      service.getRestaurant(restaurantid, accountId).then(res => {
        expect(res).toEqual([]);
      });
      const req = httpTestingController.expectOne('api/restaurants/getRestaurant?restaurantId=1');
      expect(req.request.method).toEqual('GET');
      req.flush([]);
      httpTestingController.verify();
    });

    it('remove restaurant api call', () => {
      const httpTestingController = TestBed.inject(HttpTestingController);
      const restaurantid: number = 1;
      const accountId: number = 1;
      service.removeRestaurant(restaurantid, accountId).then(res => {
        expect(res).toEqual([]);
      });
      const req = httpTestingController.expectOne('api/restaurants/removeRestaurant');
      expect(req.request.method).toEqual('DELETE');
      req.flush([]);
      httpTestingController.verify();
    });
});
