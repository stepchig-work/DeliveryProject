export enum AccountRoles {
  RegularUser,
  RestaurantOwner 
}
export const AccountRoles2LabelMapping: Record<AccountRoles, string> = {
  [AccountRoles.RegularUser]: 'Regular User',
  [AccountRoles.RestaurantOwner]: 'Restaurant Owner'
};
export const AccountRoleFromLabelMapping: Record<string, AccountRoles> = {
  ['Regular User']: AccountRoles.RegularUser,
  ['Restaurant Owner']: AccountRoles.RestaurantOwner
}
