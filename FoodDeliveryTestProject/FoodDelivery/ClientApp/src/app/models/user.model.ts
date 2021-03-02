import { AccountRoles } from "../enums/account-role.enum";

export interface Account {
  entityId: number;
  name: string;
  surname: string;
  address: string;
  username: string;
  role: AccountRoles;
}

