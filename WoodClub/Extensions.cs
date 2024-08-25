using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Linq extensions. I'm not sure of what magic these work but they work.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Check if BindingSource.Current has data
		/// </summary>
		/// <param name="sender"></param>
		/// <returns></returns>
		public static bool CurrentRowIsValid(this BindingSource sender)
		{
			return (sender.Current != null);
		}

		/// <summary>
		/// Used to filter data via a generic object using lambda
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="filterParam"></param>
		/// <returns></returns>
		public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Func<T, bool> filterParam)
		{
			return list.Where(filterParam);
		}

		/// <summary>
		/// Provides functionality to get a distinct list of items used in a lambda Distint call.
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="sender"></param>
		/// <param name="keySelector"></param>
		/// <returns></returns>
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> sender, Func<TSource, TKey> keySelector)
		{
			HashSet<TKey> knownKeys = new HashSet<TKey>();

			foreach (TSource element in sender)
			{
				if (knownKeys.Add(keySelector(element)))
				{
					yield return element;
				}
			}
		}
	}
}
