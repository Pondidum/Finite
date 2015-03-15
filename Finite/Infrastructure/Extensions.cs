using System;
using System.Collections.Generic;

namespace Finite.Infrastructure
{
	public static class Extensions
	{
		public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
		{
			foreach (var item in self)
			{
				action(item);
			}
		}

		public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			items.ForEach(self.Add);
		}

	}
}
