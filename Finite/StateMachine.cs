using System.Collections.Generic;
using System.Linq;
using Finite.Configurations;

namespace Finite
{
	public class StateMachine<TSwitches>
	{
		private readonly MachineConfiguration<TSwitches> _configuration;
		private readonly IStateProvider<TSwitches> _stateProvider;
		private readonly TSwitches _switches;

		public StateMachine(IStateProvider<TSwitches> stateProvider, TSwitches switches)
			: this(null, stateProvider, switches)
		{
		}

		public StateMachine(MachineConfiguration<TSwitches> configuration, IStateProvider<TSwitches> stateProvider, TSwitches switches)
		{
			_configuration = configuration ?? new MachineConfiguration<TSwitches>();
			_stateProvider = stateProvider;
			_switches = switches;

			_stateProvider.InitialiseStates(_configuration.InstanceCreator);
		}

		public State<TSwitches> CurrentState { get; private set; }

		public IEnumerable<State<TSwitches>> GetAllTargetStates()
		{
			return CurrentState.Links.Select(l => l.Target);
		}

		public void ResetTo<TTarget>() where TTarget : State<TSwitches>
		{
			CurrentState = _stateProvider.GetStateFor<TTarget>();
		}

		public void TransitionTo<TTarget>() where TTarget : State<TSwitches>
		{
			var type = typeof(TTarget);
			var targetState = _stateProvider.GetStateFor<TTarget>();

			if (CurrentState == null)
			{
				throw new StateMachineException();
			}

			if (CurrentState.CanTransitionTo(_switches, targetState) == false)
			{
				throw new InvalidTransitionException(CurrentState.GetType(), type);
			}

			var stateChangeArgs = new StateChangeEventArgs<TSwitches>(_switches, CurrentState, targetState);

			OnLeaveState(stateChangeArgs);

			CurrentState = targetState;

			OnEnterState(stateChangeArgs);
		}

		private void OnEnterState(StateChangeEventArgs<TSwitches> stateChangeArgs)
		{
			CurrentState.OnEnter(this, stateChangeArgs);

			_configuration.StateChangedHandler.OnEnterState(this, stateChangeArgs);
		}

		private void OnLeaveState(StateChangeEventArgs<TSwitches> stateChangeArgs)
		{
			_configuration.StateChangedHandler.OnLeaveState(this, stateChangeArgs);

			if (stateChangeArgs.Previous != null)
			{
				stateChangeArgs.Previous.OnLeave(this, stateChangeArgs);
			}
		}
	}
}
