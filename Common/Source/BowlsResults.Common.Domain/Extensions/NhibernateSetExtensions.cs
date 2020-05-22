using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Com.BinaryBracket.BowlsResults.Common.Domain.Extensions
{
	public static class NhibernateSetExtensions
	{
		public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IList<T> list)
		{
			return new ReadOnlyCollection<T>(list);
		}

		public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this ISet<T> set)
		{
			return new ReadOnlyCollection<T>(set.ToList());
		}
	}
}