using System;
using System.Collections.Generic;
using System.Linq;

namespace Finite
{
	public abstract class State<T>
	{
		private readonly List<Link<T>> _links;

		protected State()
		{
			_links = new List<Link<T>>();
		}

		protected ILinkConfigurationExpression<T> LinkTo<TTarget>() where TTarget : State<T>
		{
			var target = typeof (TTarget);
			var options = new Link<T>(target);

			_links.Add(options);

			return options;
		}

		public IEnumerable<Link<T>> Links
		{
			get { return _links.AsEnumerable(); }
		}

		public virtual void OnLeave(Type target)
		{
			//nothing
		}

		public virtual void OnEnter(Type previous)
		{
			//nothing
		}
	}
}
