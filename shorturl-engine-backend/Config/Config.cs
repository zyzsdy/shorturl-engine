using System;
using System.Collections.Generic;
using System.Text;
using Ccxc.Core.Utils;

namespace shorturl_engine_backend.Config
{
    public class Config
    {
        [OptionDescription("HTTP服务端口")]
        public int HttpPort { get; set; } = 34464;

        [OptionDescription("Redis服务器连接字符串")]
        public string RedisConnStr { get; set; } = "127.0.0.1:6379";

        [OptionDescription("调试模式：调试模式打开时，捕获的异常详情将通过HTTP直接返回给客户端，关闭时只返回简单错误消息和500提示码。True-打开 False-关闭，默认为False")]
        public bool DebugMode { get; set; } = false;

        [OptionDescription("缩短网址服务的主域名")]
        public string MainHostname { get; set; } = "ikp.yt";

        [OptionDescription("缩短网址服务的API域名")]
        public string ApiHostname { get; set; } = "api.ikp.yt";

        public static Config Options { get; set; } = SystemOption.GetOption<Config>("Config/ikp.xml");
    }
}
