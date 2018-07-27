namespace SimpleChat
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using SimpleChat.Hubs;
    using SimpleChat.Infrastructure;
    using SimpleChat.Messaging.Base;
    using SimpleChat.Messaging.Database.Ef.Repository;
    using SimpleChat.Messaging.Entities;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityCore<User>();
            services.AddScoped<IUserStore<User>, UserStore>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IDatabaseSettings>(new DatabaseSettings { ConnectionString = "C:\\Logs\\test.db3" });

            services.AddAuthentication();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
            app.UseSignalR(routes => routes.MapHub<SimpleChatHub>("/SimpleChatHub"));
        }
    }
}
