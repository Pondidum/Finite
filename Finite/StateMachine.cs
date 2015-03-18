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
			CurrentState = _stateProvider.GetStateFor(typeof(TTarget));
		}

		public void TransitionTo<TTarget>() where TTarget : State<TSwitches>
		{
			var type = typeof(TTarget);
			var state = _stateProvider.GetStateFor(type);

			if (CurrentState == null)
			{
				throw new StateMachineException();
			}

			if (CurrentState.CanTransitionTo(_switches, state) == false)
			{
				throw new InvalidTransitionException(CurrentState.GetType(), type);
			}

			SetStateTo(state);
		}


		private void SetStateTo(State<TSwitches> target)
		{
			var stateChangeArgs = new StateChangeEventArgs<TSwitches>(_switches, CurrentState, target);

			OnLeaveState(stateChangeArgs);

			CurrentState = target;

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
