using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using MyRewards.Providers;

namespace MyRewards
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureOAuth(app);

        }


    }
}
