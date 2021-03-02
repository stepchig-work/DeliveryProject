import { TestBed } from '@angular/core/testing';
import { Alert } from './alert.model';
import { AlertService } from './alert.service';

describe('AlertService', () => {
  let service: AlertService;

  beforeEach(() => {
    TestBed.configureTestingModule({ providers: [AlertService] });
    service = TestBed.inject(AlertService);
  });

  it('can load instance', () => {
    expect(service).toBeTruthy();
  });
});
