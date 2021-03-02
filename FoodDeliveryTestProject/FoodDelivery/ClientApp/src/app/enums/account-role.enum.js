export var AccountRoles;
(function (AccountRoles) {
    AccountRoles[AccountRoles["RegularUser"] = 0] = "RegularUser";
    AccountRoles[AccountRoles["RestaurantOwner"] = 1] = "RestaurantOwner";
})(AccountRoles || (AccountRoles = {}));
export const AccountRoles2LabelMapping = {
    [AccountRoles.RegularUser]: 'Regular User',
    [AccountRoles.RestaurantOwner]: 'Restaurant Owner'
};
export const AccountRoleFromLabelMapping = {
    ['Regular User']: AccountRoles.RegularUser,
    ['Restaurant Owner']: AccountRoles.RestaurantOwner
};
//# sourceMappingURL=account-role.enum.js.map