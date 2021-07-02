using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Text;
using Ccxc.Core.HttpServer;

namespace shorturl_engine_backend.Controllers
{
    public class ControllerRegister
    {
        [ImportMany]
        public IEnumerable<HttpController> Controllers { get; set; }

        public static void Regist()
        {
            var r = new ControllerRegister();
            r.Compose();
            if (r.Controllers == null) return;
            foreach (var controller in r.Controllers)
            {
                Server.RegisterController(controller);
            }
        }

        private void Compose()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}
