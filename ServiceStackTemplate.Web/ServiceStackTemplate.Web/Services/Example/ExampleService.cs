using System;
using System.Collections.Generic;
using ServiceStack;

namespace ServiceStackTemplate.Web.Services.Example
{
	public class ExampleService : Service
	{
		private static string GetCacheKey(ExampleRequest request)
		{
			return request.Key ?? string.Empty;
		}

		public ExampleService(IAppSettings settings)
		{
			Guard.ArgumentIsNotNull(settings, "settings");

			Settings = settings;
		}

		protected IAppSettings Settings { get; private set; }

		public object Get(ExampleRequest request)
		{
			Guard.ArgumentIsNotNull(request, "request");

			if (request.IgnoreCache)
			{
				return CreateResponse(request);
			}
			else
			{
				return Request.ToOptimizedResultUsingCache(Cache, GetCacheKey(request), () => CreateResponse(request));
			}
		}

		private ExampleResponse CreateResponse(ExampleRequest request)
		{
			var items = new List<string>();

			var itemCount = DateTime.Now.Second;

			var minimum = int.Parse(Settings.GetRequiredSetting("ExampleServiceMinItemCount"));

			if (itemCount < minimum)
				itemCount = minimum;

			for (int i = 0; i < itemCount; i++)
			{
				items.Add(Guid.NewGuid().ToString("N"));
			}

			return new ExampleResponse()
			{
				ItemCount = items.Count,
				Items = items.ToArray()
			};
		}
	}
}