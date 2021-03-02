import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core"
import { ActivatedRoute, Router } from "@angular/router";
import { BehaviorSubject, Observable, Subject } from "rxjs";
import { AccountRoles } from "../enums/account-role.enum";
import { Account } from "../models/user.model"


@Injectable()
export class AuthenticationService {

  private logInUrl: string = "api/authentication/logIn";
  private signUpUrl: string = "api/authentication/signUp"
  private currentUserSubject: BehaviorSubject<Account>;
  public currentUserObservable: Observable<Account>;


  private readonly onUserLoggedInSubject = new Subject<Account>();
  public readonly onUserLoggedIn = this.onUserLoggedInSubject.asObservable();

  private readonly onUserLoggedOutSubject = new Subject<void>();
  public readonly onUserLoggedOut = this.onUserLoggedOutSubject.asObservable();

  private get currentUser(): Account {
    return this.currentUserSubject.value;
  }

  public get isLoggedIn(): boolean {
    return this.currentUser != null;
  }

  public get getUser(): Account {
    return this.currentUser;
  }

  public get getUserId(): number {
    return this.currentUser.entityId;
  }

  public get getUserRole(): AccountRoles {
    return this.currentUser.role
  }

  public get isUserOwner(): boolean {
    return this.currentUser != null && this.currentUser.role == AccountRoles.RestaurantOwner;
  }

  constructor(private readonly httpClient: HttpClient,
    private route: ActivatedRoute,
    private router: Router) {
    this.currentUserSubject = new BehaviorSubject<Account>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUserObservable = this.currentUserSubject.asObservable();
  }

  public async logIn(userName: string, password: string): Promise<void> {
    let loginModel = {
      username: userName,
      password: password
    };
    try {
      let result = await this.httpClient.put<Account>(this.logInUrl, loginModel).toPromise() as Account;
      this.setCurrentUser(result);
    }
    catch (exeption) {
      throw exeption;
    }
  }

  public async logOut(): Promise<void> {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    location.href = location.origin;
  }

  public async signUp(account: Account, password): Promise<void> {
    try {
      let signinModel = {
        account: account,
        password: password
      };
      await this.httpClient.post<Account>(this.signUpUrl, signinModel).toPromise().then(user => {
        this.setCurrentUser(user);
      });

    }
    catch (exeption) {
      throw exeption;
    }
  }

  private setCurrentUser(user: Account) {
    localStorage.setItem('currentUser', JSON.stringify(user));
    this.currentUserSubject.next(user)
    this.router.navigate([""]);
  }
}
