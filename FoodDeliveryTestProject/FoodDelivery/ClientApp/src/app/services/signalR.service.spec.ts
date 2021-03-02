import { TestBed } from '@angular/core/testing';
import { AuthenticationService } from './authentication.service';
import { SignalRService } from './signalR.service';

describe('SignalRService', () => {
  let service: SignalRService;

  beforeEach(() => {
    const authenticationServiceStub = () => ({ getUser: { entityId: {} } });
    TestBed.configureTestingModule({
      providers: [
        SignalRService,
        {
          provide: AuthenticationService,
          useFactory: authenticationServiceStub
        }
      ]
    });
    service = TestBed.inject(SignalRService);
  });

  it('can load instance', () => {
    expect(service).toBeTruthy();
  });

  it(`initialized has default value`, () => {
    expect(service.initialized).toEqual(false);
  });

  it(`initFailed has default value`, () => {
    expect(service.initFailed).toEqual(false);
  });
});
