using System;

namespace ServiceStackTemplate.Web
{
	public static class Guard
	{
		public static class Messages
		{
			public const string ExpectedEnumType = "Expected Enum";
			public const string InvalidEnumValue = "Invalid Enum Value";
		}

		public static void IsValidEnumValue<T>(T value)
			where T : struct
		{
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException(Messages.ExpectedEnumType);
			}

			if (!Enum.IsDefined(typeof(T), value))
			{
				throw new ArgumentException(Messages.InvalidEnumValue);
			}
		}

		public static void IsAtLeast(long value, long minValue, string argument)
		{
			if (value < minValue)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} must be at least {1}", argument, minValue));
			}
		}

		public static void ArgumentIsNotDefault<T>(T value, string argument)
		{
			ArgumentIsNotNull(value, argument);

			var defaultValue = default(T);

			if (defaultValue.Equals(value))
			{
				throw new ArgumentException(string.Format("{0} must be assigned a non-default value({1}).", argument, defaultValue));
			}
		}

		public static void ArgumentIsNotNull(object value, string argument)
		{
			if (value == null)
			{
				throw new ArgumentNullException(argument);
			}
		}

		public static void ArgumentNotNullOrEmpty(string value, string argument)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentNullException(argument);
			}
		}
	}
}
