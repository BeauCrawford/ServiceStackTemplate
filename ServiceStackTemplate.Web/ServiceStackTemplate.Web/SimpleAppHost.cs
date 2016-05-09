using System.Reflection;
using Autofac;
using Funq;
using ServiceStack;
using ServiceStack.Text;

namespace ServiceStackTemplate.Web
{
	public class SimpleAppHost : AppHostBase
	{
		public SimpleAppHost(params Assembly[] assemblies)
			: base("Simple App Host Example", assemblies.Combine(new Assembly[] { typeof(SimpleAppHost).Assembly }))
		{
		}

		public override void Configure(Container container)
		{
			JsConfig.EmitCamelCaseNames = true;
			JsConfig.DateHandler = DateHandler.ISO8601;
			JsConfig.IncludeNullValues = false;
			JsConfig.TreatEnumAsInteger = true;

			SetConfig(new HostConfig
			{
				DebugMode = true
			});

			Routes.Add(typeof(ServiceStackTemplate.Web.Services.Example.ExampleRequest), "/Example", "GET");

			/*
			var connectionString = "AZURE_REDIS_CONNECTION_STRING_HERE";
			container.Register<ServiceStack.Redis.IRedisClientsManager>(c => new ServiceStack.Redis.PooledRedisClientManager(connectionString));
			container.Register(c => c.Resolve<ServiceStack.Redis.IRedisClientsManager>().GetCacheClient());
			*/

			container.Register<ServiceStack.Caching.ICacheClient>(new ServiceStack.Caching.MemoryCacheClient());

			ConfigureContainer(container);
		}

		private void ConfigureContainer(Funq.Container container)
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new SimpleModule());
			var autofacContainerRoot = builder.Build();

			container.Register<ILifetimeScope>(c => autofacContainerRoot.BeginLifetimeScope()).ReusedWithin(ReuseScope.Request);
			container.Adapter = new AutofacContainerAdapter(container);
		}
	}
}