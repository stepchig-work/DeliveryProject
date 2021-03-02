import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AlertService } from "../alert-system/alert.service";


@Injectable()
export class AccountService {
  private banUserUrl: string = "api/account/banUser";
  private getUserNameUrl: string = "api/account/getUserName"

  options = {
    autoClose: true,
    keepAfterRouteChange: false
  };

  constructor(private readonly httpClient: HttpClient,
    private alertService: AlertService) { }

  public async getUserName(userId: number): Promise<string> {

    let params = new HttpParams();
    params = params.set('userId', userId.toString());
    return await this.httpClient.get<any>(this.getUserNameUrl, { params: params }).toPromise();
  }

  public async banUser(ownerId: number, username: string): Promise<void> {
    let banUserModel = {
      ownerId: ownerId,
      username: username
    };
    try {
      await this.httpClient.put(this.banUserUrl, banUserModel).toPromise();
    }
    catch (error) {
      this.alertService.error(error.error, this.options)
    }
  }


}
