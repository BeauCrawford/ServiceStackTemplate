using System;
using System.Configuration;

namespace ServiceStackTemplate.Web
{
	public interface IAppSettings
	{
		string GetSetting(string key);

		Type GetTypeSetting(string key);

		string GetRequiredSetting(string key);
	}

	public class AppSettings : IAppSettings
	{
		public string GetSetting(string key)
		{
			var value = ConfigurationManager.AppSettings[key];
			return string.IsNullOrWhiteSpace(value) ? null : value;
		}

		public string GetRequiredSetting(string key)
		{
			var value = ConfigurationManager.AppSettings[key];

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new InvalidOperationException();
			}
			else
			{
				return value;
			}
		}

		public Type GetTypeSetting(string key)
		{
			var value = ConfigurationManager.AppSettings[key];

			if (string.IsNullOrWhiteSpace(value))
			{
				return null;
			}
			else
			{
				var type = Type.GetType(value);

				if (type == null)
				{
					throw new InvalidOperationException("Type does not exist: " + value);
				}
				else
				{
					return type;
				}
			}
		}
	}
}
