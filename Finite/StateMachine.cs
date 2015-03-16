using System;
using System.Collections.Generic;
using System.Linq;
using Finite.Configurations;

namespace Finite
{
	public class StateMachine<T>
	{
		private readonly IMachineConfiguration<T> _configuration;
		private readonly IStateProvider<T> _stateProvider;
		private readonly T _args;

		public StateMachine(IStateProvider<T> stateProvider, T args)
			: this(null, stateProvider, args)
		{
		}

		public StateMachine(IMachineConfiguration<T> configuration, IStateProvider<T> stateProvider, T args)
		{
			_configuration = configuration ?? new NullConfiguration<T>();
			_stateProvider = stateProvider;
			_args = args;
		}

		public State<T> CurrentState { get; private set; }

		public void SetStateTo<TTarget>() where TTarget : State<T>
		{
			SetStateTo(_stateProvider.GetStateFor(typeof(TTarget)));
		}

		public IEnumerable<State<T>> GetAllTargetStates()
		{
			return CurrentState.Links.Select(l => l.Target);
		}

		private void SetStateTo(State<T> target)
		{
			if (CurrentState != null && CurrentState.CanTransitionTo(_args, target) == false)
			{
				throw new InvalidTransitionException(CurrentState.GetType(), target.GetType());
			}

			var stateChangeArgs = new StateChangeEventArgs<T>(_args, CurrentState, target);

			OnLeaveState(stateChangeArgs);

			CurrentState = target;

			OnEnterState(stateChangeArgs);
		}

		private void OnEnterState(StateChangeEventArgs<T> stateChangeArgs)
		{
			CurrentState.OnEnter(this, stateChangeArgs);
			_configuration.OnEnterState(this, stateChangeArgs);
		}

		private void OnLeaveState(StateChangeEventArgs<T> stateChangeArgs)
		{
			_configuration.OnLeaveState(this, stateChangeArgs);

			if (stateChangeArgs.Previous != null)
			{
				stateChangeArgs.Previous.OnLeave(this, stateChangeArgs);
			}
		}


	}
}