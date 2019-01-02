using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HC.DZWechat.Configuration;
using HC.DZWechat.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HC.DZWechat.Web.Host.Controllers
{
    public class WeChatFileController : DZWechatControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public WeChatFileController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [RequestFormSizeLimit(valueCountLimit: 2147483647)]
        [HttpPost]
        public async Task<IActionResult> MarketingInfoPosts(IFormFile[] image, string fileName, Guid name)
        {
            //var files = Request.Form.Files;
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var imageName = "";
            foreach (var formFile in image)
            {
                if (formFile.Length > 0)
                {
                    string fileExt = Path.GetExtension(formFile.FileName); //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                    //var i = 0;
                    //fileName = fileName + new DateTime().ToString("yyMMddHH") + i++.ToString();
                    name = name == Guid.Empty ? Guid.NewGuid() : name;
                    string newName = name + fileExt; //新的文件名
                    var fileDire = webRootPath + string.Format("/upload/{0}/", fileName);
                    if (!Directory.Exists(fileDire))
                    {
                        Directory.CreateDirectory(fileDire);
                    }
                   var filePath = fileDire + newName;
                    ////2018-7-6 压缩后保存
                    //using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(imageByte))
                    //{
                    //    //如果高度大于200 就需要压缩
                    //    if (image.Height > 200)
                    //    {
                    //        var width = (int)((200 / image.Height) * image.Width);
                    //        image.Mutate(x => x.Resize(width, 200));
                    //    }
                    //    image.Save(filePath);
                    //}
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    imageName = filePath.Substring(webRootPath.Length);
                }
            }

            return Ok(new { imageName });
        }
    }
}