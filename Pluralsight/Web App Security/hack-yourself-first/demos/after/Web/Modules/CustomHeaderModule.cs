using System;
using System.Web;

namespace Web.Modules
{
  public class CustomHeaderModule : IHttpModule
  {
    public void Dispose() { }

    public void Init(HttpApplication context)
    {
      context.PreSendRequestHeaders += OnPreSendRequestHeaders;
    }

    void OnPreSendRequestHeaders(object sender, EventArgs e)
    {
      if (HttpContext.Current != null)
      {
        HttpContext.Current.Response.Headers.Remove("Server");
      }
    }
  }
}