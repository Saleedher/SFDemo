using Microsoft.Owin;

[assembly: OwinStartup(typeof(HelloWorldStatelessService.Startup))]
namespace HelloWorldStatelessService
{
    using Owin;
    using System.Web.Http;
    using System.Net.Http.Headers;
    using System.Web;
    using System.Web.SessionState;
    using Microsoft.Owin.Extensions;
    using Microsoft.Owin;
    using System.IO;
    using System.Threading.Tasks;
    using System;
    using System.Diagnostics;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.AspNet.Identity;

    public class Startup : IOwinAppBuilder
    {
        
        public void Configuration(IAppBuilder appBuilder)
        {
            

            HttpConfiguration config = new HttpConfiguration();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
            config.MapHttpAttributeRoutes();


            appBuilder.UseWebApi(config);


           
        }

        /// <summary>
        /// validate successful
        /// </summary>
        /// <param name="context"></param>
        private static void OnResponseSignIn(CookieResponseSignInContext context)
        {

            //set cookie domain
            context.Options.CookieDomain = context.OwinContext.Request.Uri.Host;
            //context.Options.CookieDomain ="www.website.com";
            //set cookie path
            context.Options.CookiePath = "/Business";

        }

        public static void RequireAspNetSession(IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                var httpContext = context.Get<HttpContextBase>(typeof(HttpContextBase).FullName);
                httpContext.SetSessionStateBehavior(SessionStateBehavior.Required);
                return next();
            });

            // To make sure the above `Use` is in the correct position:
            app.UseStageMarker(PipelineStage.MapHandler);
        }

           

        //public void Configuration(Owin.IAppBuilder app)
        //{
        //    // using Owin; you can use UseRequestScopeContext extension method.
        //    // enabled timing is according to Pipeline.
        //    // so I recommend enable as far in advance as possible.
        //    app.UseRequestScopeContext();

        //    //app.UseErrorPage();
        //    #region commented
        //    /*
        //    app.Run(async _ =>
        //    {
        //        // get global context like HttpContext.Current.
        //        var context = OwinRequestScopeContext.Current;

        //        // Environment is raw Owin Environment as IDictionary<string, object>.
        //        var __ = context.Environment;

        //        // optional:If you want to change Microsoft.Owin.OwinContext, you can wrap.
        //        new OwinContext(context.Environment);

        //        // Timestamp is request started(correctly called RequestScopeContextMiddleware timing).
        //        var ___ = context.Timestamp;

        //        // Items is IDictionary<string, object> like HttpContext#Items.
        //        // Items is threadsafe(as ConcurrentDictionary) by default.
        //        var ____ = context.Items;

        //        // DisposeOnPipelineCompleted can register dispose when request finished(correctly RequestScopeContextMiddleware underling Middlewares finished)
        //        // return value is cancelToken. If call token.Dispose() then canceled register.
        //        var cancelToken = context.DisposeOnPipelineCompleted(new TraceDisposable());

        //        // OwinRequestScopeContext over async/await also ConfigureAwait(false)
        //        context.Items["test"] = "foo";
        //        await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
        //        var _____ = OwinRequestScopeContext.Current.Items["test"]; // foo

        //        await Task.Run(() =>
        //        {
        //            // OwinRequestScopeContext over new thread/threadpool.
        //            var ______ = OwinRequestScopeContext.Current.Items["test"]; // foo
        //        });

        //        _.Response.ContentType = "text/plain";
        //        await _.Response.WriteAsync("Hello OwinRequestScopeContext! => ");
        //        await _.Response.WriteAsync(OwinRequestScopeContext.Current.Items["test"] as string); // render foo
        //    });
        //    */
        //    #endregion
        //    HttpConfiguration config = new HttpConfiguration();

        //    config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        //    config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
        //    config.MapHttpAttributeRoutes();


        //    app.UseWebApi(config);
        //}
    }
    
}
