using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ccxc.Core.Utils;

namespace shorturl_engine_backend.ShortUrlManager
{
    public static class ShortUrlLib
    {
        public static async Task<ShortUrlData> GetData(string id)
        {
            var redis = new RedisClient(Config.Config.Options.RedisConnStr);
            var basicKey = $"/MinyamiTools/ShortUrlManager/ShortUrl/{id}";

            var res = await redis.GetObject<ShortUrlData>(basicKey);
            return res;
        }

        public static async Task<string> SetDataAndReturnId(ShortUrlData data, long timeOut = -1L)
        {
            var redis = new RedisClient(Config.Config.Options.RedisConnStr);

            for (var length = 6; length <= 12; length++) //可用长度从6增加到12
            {
                for (var i = 0; i < 100; i++) //每个长度尝试生成100次
                {
                    var tempId = GetRandomId(length);

                    var basicKey = $"/MinyamiTools/ShortUrlManager/ShortUrl/{tempId}";
                    var exist = await redis.Exist(basicKey);
                    if(exist) continue; //生成的Key已存在，生成下个Key

                    data.Id = tempId;
                    await redis.PutObject(basicKey, data, timeOut);

                    return tempId;
                }
            }

            throw new Exception("无法生成指定的ID。");
        }

        private static Random Random { get; set; } = new Random();
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-_";

        private static string GetRandomId(int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var rs = Random.Next(0, 64);
                result.Append(Alphabet[rs]);
            }

            return result.ToString();
        }
    }
}
