using System;
using System.Web;
using Autofac;
using ServiceStack;

namespace ServiceStackTemplate.Web
{
	public sealed partial class SimpleModule
		: Module
	{
		public SimpleModule(IAppSettings settings, Func<HttpContextBase> httpContextResolver)
		{
			Guard.ArgumentIsNotNull(settings, "settings");
			Guard.ArgumentIsNotNull(httpContextResolver, "httpContextResolver");

			_settings = settings;
			_httpContextResolver = httpContextResolver;
		}

		public SimpleModule()
			: this(new AppSettings(), () => new HttpContextWrapper(HttpContext.Current))
		{
		}

		private IAppSettings _settings;

		private Func<HttpContextBase> _httpContextResolver;

		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(c => _settings).As<IAppSettings>();
			builder.Register(c => _httpContextResolver()).As<HttpContextBase>();
			builder.RegisterAssemblyTypes(typeof(SimpleModule).Assembly).Where(c => typeof(Service).IsAssignableFrom(c)).InstancePerLifetimeScope();
		}
	}
}