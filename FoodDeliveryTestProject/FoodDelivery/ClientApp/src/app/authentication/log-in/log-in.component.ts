import { Component, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { AlertService } from '../../alert-system/alert.service';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html'
})

export class LogInComponent {

  @ViewChild('selfClosingAlert', { static: false }) alertMessage: NgbAlert;

  public submitted = false;

  get l() { return this.loginForm.controls; }

  loginForm = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  options = {
    autoClose: true,
    keepAfterRouteChange: false
  };

  constructor(private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
    private alertService: AlertService) {
     
  }

  async onSubmit(): Promise<void> {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }

    let username = this.l.username.value;
    let password = this.l.password.value;
    try {
      await this.authenticationService.logIn(username, password);
      this.alertService.success('Logged in', this.options)
    }
    catch (error) {
      this.alertService.error(error.error, this.options)
    }

  }

}

