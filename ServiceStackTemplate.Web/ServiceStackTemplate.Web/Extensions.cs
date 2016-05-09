using System;

namespace ServiceStackTemplate.Web
{
	public static class Extensions
	{
		public static T[] Combine<T>(this T[] array1, T[] array2)
		{
			int array1OriginalLength = array1.Length;
			Array.Resize<T>(ref array1, array1OriginalLength + array2.Length);
			Array.Copy(array2, 0, array1, array1OriginalLength, array2.Length);
			return array1;
		}
	}
}