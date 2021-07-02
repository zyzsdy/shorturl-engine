using System;

namespace shorturl_engine_backend
{
    class Program
    {
        static void Main(string[] args)
        {
            var startUp = new StartUp();
            startUp.Run();
            startUp.Wait();
        }
    }
}
