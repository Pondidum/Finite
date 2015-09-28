using System;

namespace Finite
{
	internal class LinkWrapper<TSwitches> : ILink<TSwitches>
	{
		private readonly ILink<TSwitches> _other;
		private readonly Type _targeType;

		private Lazy<State<TSwitches>> _target;

		public LinkWrapper(ILink<TSwitches> other, Type targeType)
		{
			_other = other;
			_targeType = targeType;
		}

		public string ConditionDescription => _other.ConditionDescription;
		public State<TSwitches> Target => _target.Value;

		public bool IsActive(TSwitches switches)
		{
			return _other.IsActive(switches);
		}

		public void Configure(StateMachine<TSwitches> stateMachine)
		{
			_target = new Lazy<State<TSwitches>>(() => stateMachine.GetStateFor(_targeType));
		}
	}
}
