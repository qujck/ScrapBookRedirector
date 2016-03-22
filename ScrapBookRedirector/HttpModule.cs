using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Redirector
{
    public class HttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContextBase context = new HttpContextWrapper(application.Context);

            RedirectRequest(context);
        }

        private void RedirectRequest(HttpContextBase context)
        {
            if (context.Request.FilePath.TrimEnd('/').Length > 0
                && !File.Exists(context.Request.PhysicalPath))
            {
                context.Response.Redirect(
                    $"http://scrapbook.qujck.com{context.Request.FilePath}",
                    true);
            }
        }

        public void Dispose()
        {
        }
    }
}