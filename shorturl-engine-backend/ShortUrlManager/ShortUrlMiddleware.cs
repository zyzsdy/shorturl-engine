using System;
using System.Collections.Generic;
using System.Text;
using Ccxc.Core.HttpServer;
using shorturl_engine_backend.Controllers;

namespace shorturl_engine_backend.ShortUrlManager
{
    public static class ShortUrlMiddleware
    {
        public static Server UseShortUrlManager(this Server server)
        {
            return server.Use(async (ctx, next) =>
            {
                var host = ctx.Request.Host.ToString();
                if (host != Config.Config.Options.MainHostname)
                {
                    await next.Invoke();
                    return;
                }

                var path = ctx.Request.Path.ToString().Substring(1);

                var shortUrlData = await ShortUrlLib.GetData(path);

                var response = new Response(ctx.Response);
                if (shortUrlData == null)
                {
                    await response.JsonResponse(404, new Controllers.BasicResponse
                    {
                        status = 2,
                        message = "未查询到指定的URL"
                    });
                    return;
                }

                switch (shortUrlData.RedirectMode)
                {
                    case 302:
                    {
                        response.SetHeader("Location", shortUrlData.Url);
                        await response.HeaderOnlyResponse(302);
                        return;
                    }
                    default:
                    {
                        await response.BadRequest("不支持的跳转模式。");
                        return;
                    }
                }
            });
        }
    }
}
