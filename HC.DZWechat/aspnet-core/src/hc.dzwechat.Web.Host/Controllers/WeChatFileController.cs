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
using Senparc.NeuChar.Entities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using HC.DZWechat.Dtos;

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
        //第一个参数的名字需要与前端控件的name保持一致（针对图片上传控件）
        public async Task<IActionResult> MarketingInfoPosts(IFormFile[] image, string fileName)
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
                    var i = 0;
                    //var name = new DateTime().ToString("yyMMddHH") + i++.ToString();
                    var name = Guid.NewGuid();
                    string newName = name + fileExt; //新的文件名
                    var fileDire = webRootPath + string.Format("/upload/{0}/", fileName);
                    if (!Directory.Exists(fileDire))
                    {
                        Directory.CreateDirectory(fileDire);
                    }
                    var filePath = fileDire + newName;
                    //2018-7-6 压缩后保存
                    using (Image<Rgba32> image2 = SixLabors.ImageSharp.Image.Load(formFile.OpenReadStream()))
                    {
                        //如果高度大于200 就需要压缩
                        if (image2.Height > 200)
                        {
                            var width = (int)((200 / image2.Height) * image2.Width);
                            image2.Mutate(x => x.Resize(width, 200));
                        }
                        image2.Save(filePath);
                    }
                    imageName = filePath.Substring(webRootPath.Length);
                }
            }
            return await Task.FromResult((IActionResult)Json(new APIResultDto() { Code = 0, Msg = "上传数据成功", Data = imageName }));
            //return Ok(new { imageName });
        }
    }
}