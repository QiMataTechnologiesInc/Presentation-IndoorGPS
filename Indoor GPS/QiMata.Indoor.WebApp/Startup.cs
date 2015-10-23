using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using QiMata.Indoor.WebApp;

[assembly:OwinStartup(typeof(Startup))]
namespace QiMata.Indoor.WebApp
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
