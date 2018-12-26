using Microsoft.AspNetCore.Antiforgery;
using hc.dzwechat.Controllers;

namespace hc.dzwechat.Web.Host.Controllers
{
    public class AntiForgeryController : dzwechatControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
