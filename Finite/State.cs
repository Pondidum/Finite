using System;
using System.Collections.Generic;
using System.Linq;

namespace Finite
{
	public abstract class State<T> : IStateConfiguration<T>
	{
		private readonly List<Link<T>> _links;
		private IStateProvider<T> _stateProvider;
		private Action<IStateConfiguration<T>> _configuration;

		protected State()
		{
			_links = new List<Link<T>>();
			_configuration = config => { };
		}

		internal void Configure(IStateProvider<T> stateProvider)
		{
			_stateProvider = stateProvider;
			_configuration.Invoke(this);
		}

		protected void Configure(Action<IStateConfiguration<T>> config)
		{
			_configuration = config;
		}

		ILinkConfigurationExpression<T> IStateConfiguration<T>.LinkTo<TTarget>()
		{
			var target = _stateProvider.GetStateFor(typeof(TTarget));
			var options = new Link<T>(target);

			_links.Add(options);

			return options;
		}

		public IEnumerable<Link<T>> Links
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

		public bool CanTransitionTo(T args, State<T> target)
		{
			return _links
				.Where(l => l.IsActive(args))
				.Any(l => l.Target == target);
		}
	}

	public interface IStateConfiguration<T>
	{
		ILinkConfigurationExpression<T> LinkTo<TTarget>() where TTarget : State<T>;
	}
}
