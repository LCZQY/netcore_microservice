using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using OcelotGateway.Aggregator;

namespace OcelotGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<LeaderAdvancedDependency>();
            services.AddOcelot()
                .AddPolly()//AddPolly() 配置熔断使用
                .AddConsul()
                .AddSingletonDefinedAggregator<LeaderAdvancedAggregator>();


            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //注册Identity4
            var identityBuilder = services.AddAuthentication();
            IdentityServerConfig identityServerConfig = new IdentityServerConfig();
            Configuration.Bind("IdentityServerConfig", identityServerConfig);
            if (identityServerConfig != null && identityServerConfig.Resources != null)
            {
                foreach (var resource in identityServerConfig.Resources)
                {
                    identityBuilder.AddIdentityServerAuthentication(resource.Key, options =>
                    {
                        options.Authority = $"http://{identityServerConfig.IP}:{identityServerConfig.Port}";
                        options.RequireHttpsMetadata = false;
                        options.ApiName = resource.Name;
                        options.SupportedTokens = SupportedTokens.Both;
                    });
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseAuthentication();

            app.UseMvc();
            app.UseOcelot().Wait();
        }
    }
}
