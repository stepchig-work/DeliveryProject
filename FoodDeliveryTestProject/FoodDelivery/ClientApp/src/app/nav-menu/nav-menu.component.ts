import { Component } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {


  constructor(public authenticationService: AuthenticationService) {
  }
  public logOut() {
    this.authenticationService.logOut();
  }
}
