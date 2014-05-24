﻿using System;
using System.Web;

namespace Web.Modules
{
  public class XFrameOptionsModule : IHttpModule
  {
    public void Dispose() { }

    public void Init(HttpApplication context)
    {
      context.BeginRequest += OnBeginRequest;
    }

    private void OnBeginRequest(Object sender, EventArgs e)
    {
      HttpContext.Current.Response.AddHeader("X-Frame-Options", "Deny");
    }
  }
}