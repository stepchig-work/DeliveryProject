import { Component } from '@angular/core';
import { async, Subscription } from 'rxjs';
import { Account } from '../models/user.model';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-bottom-menu',
  templateUrl: './bottom-menu.component.html'
})
export class BottomMenuComponent {

  
  public get ifBuyingAllowed(): boolean {
    return !this.authenticationService.isUserOwner;
  }

  constructor(public authenticationService: AuthenticationService) {
  }

}
