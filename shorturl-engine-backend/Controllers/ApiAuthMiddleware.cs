using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ccxc.Core.HttpServer;
using Ccxc.Core.Utils;

namespace shorturl_engine_backend.Controllers
{
    public static class ApiAuthMiddleware
    {
        private static readonly HashSet<string> PreSetTokens = new HashSet<string>
        {
            "6decfe8e-626c-420a-a015-3d76f12acb78"
        };

        public static Server UseApiAuth(this Server server)
        {
            return server.Use(async (ctx, next) =>
            {
                var response = new Response(ctx.Response);

                var headers = new Dictionary<string, string>();
                foreach (var (key, value) in ctx.Request.Headers)
                {
                    if (!headers.ContainsKey(key.ToLower()))
                    {
                        headers.Add(key.ToLower(), value);
                    }
                }

                if (!headers.ContainsKey("user-token"))
                {
                    await response.BadRequest("请求格式不完整：User-Token 不可为空。");
                    return;
                }

                var token = headers["user-token"].ToString();

                if (!PreSetTokens.Contains(token))
                {
                    await response.Unauthorized("User-Token 认证未通过。");
                    return;
                }

                await next.Invoke();
            });
        }
    }
}
