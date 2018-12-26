using Abp.Authorization;
using hc.dzwechat.Authorization.Roles;
using hc.dzwechat.Authorization.Users;

namespace hc.dzwechat.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
