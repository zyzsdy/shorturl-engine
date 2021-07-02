using System;
using System.Collections.Generic;
using System.Text;
using Ccxc.Core.HttpServer;

namespace shorturl_engine_backend.Controllers.ShortUrl
{
    public class AddShortUrlRequest
    {
        [Required]
        public string to { get; set; }
    }

    public class AddShortUrlResponse : BasicResponse
    {
        public string id { get; set; }
        public string shorturl { get; set; }
    }
}
