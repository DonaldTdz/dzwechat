using Microsoft.AspNetCore.Antiforgery;
using HC.DZWechat.Controllers;

namespace HC.DZWechat.Web.Host.Controllers
{
    public class AntiForgeryController : DZWechatControllerBase
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

