using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Finite
{
	public abstract class State<TSwitches>
	{
		private readonly List<ILink<TSwitches>> _links;
		private readonly List<LinkBuilder<TSwitches>> _linkBuilders;
		private bool _configured;
		private StateMachine<TSwitches> _machine;

		protected State()
		{
			_links = new List<ILink<TSwitches>>();
			_linkBuilders = new List<LinkBuilder<TSwitches>>();
		}

		protected void LinkTo<TTarget>() where TTarget : State<TSwitches>
		{
			LinkTo<TTarget>(args => true);
		}

		protected void LinkTo<TTarget>(Expression<Func<TSwitches, bool>> condition) where TTarget : State<TSwitches>
		{
			LinkTo<TTarget>(new Link<TSwitches>(condition));
		}

		protected void LinkTo<TTarget>(ILink<TSwitches> link)
		{
			if (_configured)
				throw new InvalidOperationException("You can only call LinkTo in a state's constructor.");

			var builder = new LinkBuilder<TSwitches>
			{
				TargetState = typeof (TTarget),
				Link = link
			};

			_linkBuilders.Add(builder);
		}

		internal void Configure(StateMachine<TSwitches> stateMachine)
		{
			_machine = stateMachine;
			_links.AddRange(_linkBuilders.Select(config => config.CreateLink(stateMachine)));

			_configured = true;
		}

		public LinkIterator<TSwitches> Links
		{
			get { return new LinkIterator<TSwitches>(_machine, _links); }
		}

		public virtual void OnLeave(object sender, StateChangeEventArgs<TSwitches> stateChangeArgs)
		{
			//nothing
		}

		public virtual void OnEnter(object sender, StateChangeEventArgs<TSwitches> stateChangeArgs)
		{
			//nothing
		}

		public bool CanTransitionTo(State<TSwitches> target)
		{
			return _links
				.Where(l => l.IsActive(_machine.Switches))
				.Any(l => l.Target == target);
		}
	}
}
