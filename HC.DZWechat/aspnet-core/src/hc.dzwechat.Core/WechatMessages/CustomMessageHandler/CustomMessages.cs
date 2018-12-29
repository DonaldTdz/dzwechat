using Senparc.NeuChar.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.WechatMessages
{
    public class CustomMessages
    {
        public CustomMessages()
        {
            KeyWordsPic = new Dictionary<string, Article>();
            KeyWords = new Dictionary<string, string>();
        }

        #region 微信公众号关注 图文消息

        public virtual string SubscribeMsg { get; set; }
        public virtual Article SubscribeArticle { get; set; }

        #endregion

        #region 被动回复 图文消息    

        public virtual Dictionary<string, Article> KeyWordsPic { get; set; }
        public virtual Dictionary<string, string> KeyWords { get; set; }

        #endregion
    }
}
