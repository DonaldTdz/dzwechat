using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Timing;
using HC.DZWechat.Controllers;

namespace HC.DZWechat.Web.Host.Controllers
{
    public class HomeController : DZWechatControllerBase
    {
        private readonly INotificationPublisher _notificationPublisher;

        public HomeController(INotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }

        public IActionResult Index()
        {
            /*string word = "1088257684991377408,1544";
            Helpers.RSAHelper rsa = new Helpers.RSAHelper(Helpers.RSAType.RSA2, System.Text.Encoding.ASCII, Helpers.RSAHelper.PrivateKeyRsa2, Helpers.RSAHelper.PublicKeyRsa2);
            var mword = rsa.Encrypt(word);
            var dword = rsa.Decrypt(mword);*/

            //return Redirect("/swagger");
            return Redirect("/dzadmin/index.html");
        }

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ActionResult> TestNotification(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            var defaultTenantAdmin = new UserIdentifier(1, 2);
            var hostAdmin = new UserIdentifier(null, 1);

            await _notificationPublisher.PublishAsync(
                "App.SimpleMessage",
                new MessageNotificationData(message),
                severity: NotificationSeverity.Info,
                userIds: new[] { defaultTenantAdmin, hostAdmin }
            );

            return Content("Sent notification: " + message);
        }
    }
}

