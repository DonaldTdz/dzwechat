using System;
using System.Collections.Generic;
using System.Text;

namespace HC.DZWechat.Dtos
{
    [Serializable]
    public class APIResultDto
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public Object Data { get; set; }
    }
}
