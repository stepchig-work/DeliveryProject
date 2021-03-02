import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';
import { AlertService } from '../alert-system/alert.service';
import { AccountService } from './account-service';

describe('AccountService', () => {
  let service: AccountService;

  beforeEach(() => {
    const alertServiceStub = () => ({ error: (error, options) => ({}) });
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        AccountService,
        { provide: AlertService, useFactory: alertServiceStub }
      ]
    });
    service = TestBed.inject(AccountService);
  });

  it('can load instance', () => {
    expect(service).toBeTruthy();
  });

  it('ban user api call', () => {
    const httpTestingController = TestBed.inject(HttpTestingController);
    const ownerid: number = 1;
    const username: string = 'owner';
    service.banUser(ownerid, username).then(res => {
      expect(res).toEqual();
    });
    const req = httpTestingController.expectOne('api/account/banUser');
    expect(req.request.method).toEqual('PUT');
    req.flush([]);
    httpTestingController.verify();
  });
});
