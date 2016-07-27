using BusinessCenter.Api.DependencyResolution;
using BusinessCenter.Api.Providers;
using BusinessCenter.Data;
using BusinessCenter.Identity;
using BusinessCenter.Identity.IdentityModels;
using CacheCow.Server;
using InteractivePreGeneratedViews;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using StructureMap;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiContrib.Formatting;

namespace BusinessCenter.Api
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; set; }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var cachingHandler = new CachingHandler(new HttpConfiguration(), new InMemoryEntityTagStore(), "Accept")
            {
                CacheControlHeaderProvider = (message, configuration) => new CacheControlHeaderValue()
                {
                    NoCache = false,
                    MaxAge = TimeSpan.FromMinutes(100)
                }
            };

            //Register for MVC

            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            ConfigureWebApi(config);
            ConfigureOAuth(app);
            var container = IoC.Initialize();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
            //Register for web api resolver
            config.DependencyResolver = new StructureMapDependencyResolver(container);
            Database.SetInitializer<ApplicationDbContext>(null);
            Database.SetInitializer<IdentityConnectionContext>(null);
            config.MessageHandlers.Add(cachingHandler);
            config.Formatters.Add(new ProtoBufFormatter());

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);

            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/authtoken"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}