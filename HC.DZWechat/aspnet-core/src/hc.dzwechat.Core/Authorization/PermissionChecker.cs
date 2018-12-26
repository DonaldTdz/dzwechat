using Abp.Authorization;
using HC.DZWechat.Authorization.Roles;
using HC.DZWechat.Authorization.Users;

namespace HC.DZWechat.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

