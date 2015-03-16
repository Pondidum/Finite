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

		public StateMachine(IMachineConfiguration<T> configuration, IStateProvider<T> stateProvider, T args)
		{
			_configuration = configuration ?? new NullConfiguration<T>();
			_stateProvider = stateProvider;
			_args = args;
		}

		public State<T> CurrentState { get; private set; }

		public void SetStateTo<TTarget>() where TTarget : State<T>
		{
			SetStateTo(_stateProvider.GetStateFor( typeof (TTarget)));
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

			var previous = CurrentState;

			OnLeaveState(target, previous);

			CurrentState = target;
			
			OnEnterState(target, previous);
		}

		private void OnEnterState(State<T> target, State<T> previous)
		{
			CurrentState.OnEnter(_args, previous);
			_configuration.OnEnterState(_args, previous, target);
		}

		private void OnLeaveState(State<T> target, State<T> previous)
		{
			_configuration.OnLeaveState(_args, previous, target);

			if (previous != null)
			{
				previous.OnLeave(_args, target);
			}
		}

		
	}
}