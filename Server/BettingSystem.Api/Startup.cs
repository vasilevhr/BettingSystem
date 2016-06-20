using Microsoft.Owin;
using Owin;
using System.Threading;

[assembly: OwinStartup(typeof(BettingSystem.Api.Startup))]

namespace BettingSystem.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            new Thread(new ThreadStart(() =>
            {
                Parser.Parser parser = new Parser.Parser();

                while (true)
                {
                    parser.Run();
                    Thread.Sleep(60000);
                }
                
            })).Start();
        }
    }
}
