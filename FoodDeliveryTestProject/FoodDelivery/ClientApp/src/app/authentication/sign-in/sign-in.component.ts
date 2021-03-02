import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MustMatch } from '../../helpers/must-match.validator';
import { PasswordStrengthValidator } from '../../helpers/password-strength.validator';
import { AuthenticationService } from '../../services/authentication.service';
import { Account } from '../../models/user.model';
import { AccountRoleFromLabelMapping, AccountRoles, AccountRoles2LabelMapping } from '../../enums/account-role.enum';
import { AlertService } from '../../alert-system/alert.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html'
})
export class SignInComponent {

  public submitted = false;

  public accountRoles2LabelMapping = AccountRoles2LabelMapping;
  public accountRoleFromLabelMapping = AccountRoleFromLabelMapping;
  public accountsRoles = Object.values(AccountRoles).filter(value => typeof value === 'number');

  options = {
    autoClose: true,
    keepAfterRouteChange: false
  };

  get s() { return this.signinForm.controls; }

  signinForm = this.formBuilder.group({
    username: ['', Validators.required],
    name: ['', Validators.required],
    surname: ['', Validators.required],
    address: ['', Validators.required],
    accountRole: [AccountRoles.RegularUser],
    password: ['', [Validators.required,
      PasswordStrengthValidator,
      Validators.minLength(8)]],
    confirmPassword: ['', Validators.required]
  }, {
    validator: MustMatch('password', 'confirmPassword')
  });

  constructor(private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
    private alertService: AlertService) {
  }


  async onSubmit() {
    this.submitted = true;
    if (this.signinForm.invalid) {
      return;
    }
    let password = this.s.password.value;
    let account: Account = {
      name: this.s.name.value,
      surname: this.s.surname.value,
      username: this.s.username.value,
      address: this.s.address.value,
      role: this.accountRoleFromLabelMapping[this.s.accountRole.value],
      entityId: 0
    };
    try {
      await this.authenticationService.signUp(account, password);
      this.alertService.success('Account created successfully', this.options)
    }
    catch (error) {
      this.alertService.error(error.error, this.options)
    }

  }
}
