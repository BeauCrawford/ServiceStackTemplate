using System;

namespace ServiceStackTemplate.Web
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			var host = new SimpleAppHost();
			host.Init();
		}
	}
}