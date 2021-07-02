using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using Ccxc.Core.HttpServer;

namespace shorturl_engine_backend.Controllers.ShortUrl
{
    [Export(typeof(HttpController))]
    public class ShortUrlController : HttpController
    {
        [HttpHandler("PUT", "/shorturl")]
        public async Task AddShortUrl(Request request, Response response)
        {
            var requestJson = request.Json<AddShortUrlRequest>();

            if (!Validation.Valid(requestJson, out string reason))
            {
                await response.BadRequest(reason);
                return;
            }

            var tempShortUrlData = new ShortUrlManager.ShortUrlData
            {
                CreateTime = DateTime.Now,
                RedirectMode = 302,
                Url = requestJson.to
            };

            var shortId = await ShortUrlManager.ShortUrlLib.SetDataAndReturnId(tempShortUrlData);
            var shortUrl = $"https://{Config.Config.Options.MainHostname}/{shortId}";

            await response.JsonResponse(200, new AddShortUrlResponse
            {
                status = 1,
                message = "ok",
                id = shortId,
                shorturl = shortUrl
            });
        }
    }
}
