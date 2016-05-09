using Autofac;
using ServiceStack.Configuration;

namespace ServiceStackTemplate.Web
{
	public sealed class AutofacContainerAdapter
		: IContainerAdapter
	{
		private readonly Funq.Container _container;

		public AutofacContainerAdapter(Funq.Container container)
		{
			Guard.ArgumentIsNotNull(container, "container");

			_container = container;
		}

		public T Resolve<T>()
		{
			return ActiveScope.Resolve<T>();
		}

		public T TryResolve<T>()
		{
			T result;

			if (ActiveScope.TryResolve<T>(out result))
			{
				return result;
			}

			return default(T);
		}

		private ILifetimeScope ActiveScope
		{
			get
			{
				return _container.Resolve<ILifetimeScope>();
			}
		}
	}
}
