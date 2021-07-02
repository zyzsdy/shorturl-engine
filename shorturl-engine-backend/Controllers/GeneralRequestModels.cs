﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ccxc.Core.HttpServer;

namespace shorturl_engine_backend.Controllers
{
    public class BasicResponse
    {
        /// <summary>
        /// 0-保留 1-成功 2-失败，message为错误提示 3-失败，并跳转location指定URL 4-失败并立即注销 13-成功并立即注销
        /// </summary>
        public int status { get; set; }
        public string message { get; set; }
    }

    public static class ResponseExtend
    {
        public static Task OK(this Response response)
        {
            return response.JsonResponse(200, new BasicResponse
            {
                status = 1,
                message = "ok"
            });
        }

        public static Task BadRequest(this Response response, string message)
        {
            return response.JsonResponse(400, new BasicResponse
            {
                status = 2,
                message = message
            });
        }

        public static Task Unauthorized(this Response response, string message)
        {
            return response.JsonResponse(401, new BasicResponse
            {
                status = 2,
                message = message
            });
        }
    }
}
