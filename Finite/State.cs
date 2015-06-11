using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Finite
{
	public abstract class State<T>
	{
		private readonly List<ILink<T>> _links;
		private readonly List<ILinkBuilder<T>> _linkBuilders;
		private bool _configured;

		protected State()
		{
			_links = new List<ILink<T>>();
			_linkBuilders = new List<ILinkBuilder<T>>();
		}

		protected void LinkTo<TTarget>() where TTarget : State<T>
		{
			LinkTo<TTarget>(args => true);
		}

		protected void LinkTo<TTarget>(Expression<Func<T, bool>> condition) where TTarget : State<T>
		{
			if (_configured)
				throw new InvalidOperationException("You can only call LinkTo in a state's constructor.");

			var builder = new StandardLinkBuilder<T>
			{
				TargetState = typeof(TTarget),
				Condition = condition
			};

			_linkBuilders.Add(builder);
		}

		internal void Configure(StateRespository<T> stateRepository)
		{
			_links.AddRange(_linkBuilders.Select(config => config.CreateLink(stateRepository)));

			_configured = true;
		}

		public IEnumerable<ILink<T>> Links
		{
			get { return _links.AsEnumerable(); }
		}

		public virtual void OnLeave(object sender, StateChangeEventArgs<T> stateChangeArgs)
		{
			//nothing
		}

		public virtual void OnEnter(object sender, StateChangeEventArgs<T> stateChangeArgs)
		{
			//nothing
		}

		public bool CanTransitionTo(T switches, State<T> target)
		{
			return _links
				.Where(l => l.IsActive(switches))
				.Any(l => l.Target == target);
		}
	}
}
