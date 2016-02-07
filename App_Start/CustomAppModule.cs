using CMS;
using CMS.DataEngine;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;
[assembly: RegisterModule(typeof(CustomAppModule))]
/// <summary>
/// Summary description for CustomApp
/// </summary>
public class CustomAppModule : CMS.DataEngine.Module
{
    public CustomAppModule()
        : base("CustomApp")
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void OnInit()
    {


        base.OnInit();
        var corsAttr = new EnableCorsAttribute("*", "*", "*");
        GlobalConfiguration.Configuration.EnableCors(corsAttr);
       GlobalConfiguration.Configuration.Routes.MapHttpRoute("customapi", "customapi/{controller}/{id}", new { id = RouteParameter.Optional });
       //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    }
}