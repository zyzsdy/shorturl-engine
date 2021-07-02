using Ccxc.Core.Utils.ExtensionFunctions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace shorturl_engine_backend.ShortUrlManager
{
    public class ShortUrlData
    {
        public string Id { get; set; }
        public string Url { get; set; }

        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime CreateTime { get; set; }
        public int RedirectMode { get; set; } = 302;
    }
}
