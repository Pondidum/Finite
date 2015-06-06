using System;
using System.Collections.Generic;
using System.Linq;

namespace Finite
{

	public class StateConfiguration<TSwitches>
	{
		public Type TargetState { get; set; }
		public Func<TSwitches, bool> Condition { get; set; }
	}

	public abstract class State<T>
	{
		private readonly List<Link<T>> _links;
		private readonly List<StateConfiguration<T>> _linkConfigurations;

		protected State()
		{
			_links = new List<Link<T>>();
			_linkConfigurations = new List<StateConfiguration<T>>();
		}

		protected void LinkTo<TTarget>() where TTarget : State<T>
		{
			LinkTo<TTarget>(args => true);
		}

		protected void LinkTo<TTarget>(Func<T, bool> condition) where TTarget : State<T>
		{
			var config = new StateConfiguration<T>
			{
				TargetState = typeof(TTarget),
				Condition = condition
			};

			_linkConfigurations.Add(config);
		}

		internal void Configure(States<T> stateProvider)
		{
			foreach (var config in _linkConfigurations)
			{
				var target = stateProvider.GetStateFor(config.TargetState);

				_links.Add(new Link<T>(target, config.Condition));
			}
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

		public bool CanTransitionTo(T switches, State<T> target)
		{
			return _links
				.Where(l => l.IsActive(switches))
				.Any(l => l.Target == target);
		}
	}
}
