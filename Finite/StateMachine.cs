using System;
using System.Collections.Generic;
using System.Linq;
using Finite.Configurations;

namespace Finite
{
	public class StateMachine<TSwitches>
	{
		private readonly MachineConfiguration<TSwitches> _configuration;
		private readonly States<TSwitches> _states;
		private readonly TSwitches _switches;

		public StateMachine(Func<States<TSwitches>, States<TSwitches>> stateProvider, TSwitches switches)
			: this(null, stateProvider, switches)
		{
		}

		public StateMachine(Action<MachineConfiguration<TSwitches>> customiseConfiguration, Func<States<TSwitches>, States<TSwitches>> stateProvider, TSwitches switches)
		{
			_switches = switches;
			_configuration = new MachineConfiguration<TSwitches>();

			if (customiseConfiguration != null)
				customiseConfiguration(_configuration);

			var states = stateProvider.Invoke(new States<TSwitches>());
			states.InitialiseStates(_configuration.InstanceCreator);

			_states = states;
		}

		public State<TSwitches> CurrentState { get; private set; }

		public IEnumerable<State<TSwitches>> AllTargetStates
		{
			get { return CurrentState.Links.Select(l => l.Target); }
		}

		public IEnumerable<State<TSwitches>> ActiveTargetStates
		{
			get { return CurrentState.Links.Where(l => l.IsActive(_switches)).Select(l => l.Target); }
		}

		public IEnumerable<State<TSwitches>> InactiveTargetStates
		{
			get { return CurrentState.Links.Where(l => l.IsActive(_switches) == false).Select(l => l.Target); }
		}

		public void ResetTo<TTarget>() where TTarget : State<TSwitches>
		{
			CurrentState = _states.GetStateFor<TTarget>();
		}

		public void TransitionTo<TTarget>() where TTarget : State<TSwitches>
		{
			var type = typeof(TTarget);
			var targetState = _states.GetStateFor<TTarget>();

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
