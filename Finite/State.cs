using System;
using System.Collections.Generic;
using System.Linq;

namespace Finite
{
	public abstract class State<T>
	{
		private readonly List<LinkOptions<T>> _links;

		protected State()
		{
			_links = new List<LinkOptions<T>>();
		}

		protected LinkOptions<T> LinkTo<TTarget>() where TTarget : State<T>
		{
			var target = typeof (TTarget);
			var options = new LinkOptions<T>(target);

			_links.Add(options);

			return options;
		}

		public IEnumerable<LinkOptions<T>> Links
		{
			get { return _links.AsEnumerable(); }
		}

		public virtual void OnExit(Type target)
		{
			//nothing
		}

		public virtual void OnEnter(Type previous)
		{
			//nothing
		}
	}
}
