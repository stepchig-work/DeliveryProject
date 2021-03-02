import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { Account } from '../models/user.model';
import { AuthenticationService } from './authentication.service';
import { AccountRoles } from '../enums/account-role.enum';

describe('AuthenticationService', () => {
  let service: AuthenticationService;

  beforeEach(() => {
    const activatedRouteStub = () => ({});
    const routerStub = () => ({ navigate: array => ({}) });
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        AuthenticationService,
        { provide: ActivatedRoute, useFactory: activatedRouteStub },
        { provide: Router, useFactory: routerStub }
      ]
    });
    service = TestBed.inject(AuthenticationService);
    service['currentUserSubject'].next(null);
  });

  it('can load instance', () => {
    expect(service).toBeTruthy();
  });



  it('login authentication api call', () => {
    const httpTestingController = TestBed.inject(HttpTestingController);
    const userName: string = 'user';
    const password: string = 'password';
    service.logIn(userName, password).then(res => {
      expect(res).toEqual();
    });
    const req = httpTestingController.expectOne('api/authentication/logIn');
    expect(req.request.method).toEqual('PUT');
    req.flush([]);
    httpTestingController.verify();
  });

  it('signin authentication api call', () => {
    const httpTestingController = TestBed.inject(HttpTestingController);
    const user: Account = <Account>{};
    const password: string = 'password';
    service.signUp(user, password).then(res => {
      expect(res).toEqual();
    });
    const req = httpTestingController.expectOne('api/authentication/signUp');
    expect(req.request.method).toEqual('POSTs');
    req.flush([]);
    httpTestingController.verify();
  });

  it('get user test', () => {
    const account: Account = <Account>{};
    account.entityId = 1;
    account.role = AccountRoles.RegularUser;
    account.name = 'name';
    account.surname = 'surname';
    account.username = 'username';

    service['currentUserSubject'].next(account);

    expect(service.getUser).toEqual(account);
  });


  it('get user role test', () => {
    const account: Account = <Account>{};
    account.role = AccountRoles.RegularUser;
    service['currentUserSubject'].next(account);

    expect(service.getUserRole).toEqual(account.role);
  });

  it('is logged in empy test', () => {

    expect(service.isLoggedIn).toEqual(false);

  });

  it('is logged in  test', () => {


    const account: Account = <Account>{};
    service['currentUserSubject'].next(account);

    expect(service.isLoggedIn).toEqual(true);
  });

  it('is owner test empty account test', () => {

    expect(service.isUserOwner).toEqual(false);

  });

  it('is owner test regular user account test', () => {

    const account: Account = <Account>{};
    account.role = AccountRoles.RegularUser;
    service['currentUserSubject'].next(account);

    expect(service.isUserOwner).toEqual(false);
  });

  it('is owner test owner account test', () => {

    const account: Account = <Account>{};
    account.role = AccountRoles.RestaurantOwner;
    service['currentUserSubject'].next(account);

    expect(service.isUserOwner).toEqual(true);
  });


});
