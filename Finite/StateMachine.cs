using System;
using System.Collections.Generic;
using System.Linq;
using Finite.Configurations;
using Finite.Infrastructure;

namespace Finite
{
	public class StateMachine<TSwitches>
	{
		private readonly MachineConfiguration<TSwitches> _configuration;
		private readonly Dictionary<Type, State<TSwitches>> _states;
		private readonly TSwitches _switches;

		public StateMachine(IStateProvider<TSwitches> stateProvider, TSwitches switches)
			: this(null, stateProvider, switches)
		{
		}

		public StateMachine(MachineConfiguration<TSwitches> configuration, IStateProvider<TSwitches> stateProvider, TSwitches switches)
		{
			_switches = switches;
			_configuration = configuration ?? new MachineConfiguration<TSwitches>();
			_states = stateProvider
				.Execute()
				.ToDictionary(s => s.GetType(), s => s);

			_states.Values.ForEach(state => state.Configure(this));
		}

		public State<TSwitches> CurrentState { get; private set; }


		public IEnumerable<State<TSwitches>> States
		{
			get { return _states.Values; }
		}

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
			ResetTo(typeof(TTarget));
		}

		public void ResetTo(Type target)
		{
			var targetState = GetStateFor(target);
			var stateChangeArgs = new StateChangeEventArgs<TSwitches>(_switches, CurrentState, targetState);

			CurrentState = targetState;

			_configuration.StateChangedHandler.OnResetState(this, stateChangeArgs);
		}

		public void TransitionTo<TTarget>() where TTarget : State<TSwitches>
		{
			TransitionTo(typeof(TTarget));
		}

		public void TransitionTo(Type target)
		{
			var targetState = GetStateFor(target);

			if (CurrentState == null)
			{
				throw new StateMachineException();
			}

			if (CurrentState.CanTransitionTo(_switches, targetState) == false)
			{
				throw new InvalidTransitionException(CurrentState.GetType(), target);
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

		public State<TSwitches> GetStateFor(Type stateType)
		{
			if (_states.ContainsKey(stateType) == false)
			{
				throw new UnknownStateException(stateType);
			}

			return _states[stateType];
		}
	}
}
