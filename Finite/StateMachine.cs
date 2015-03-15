using System;
using System.Collections.Generic;
using System.Linq;

namespace Finite
{
	public class StateMachine<T>
	{
		private readonly IStateProvider<T> _stateProvider;
		private readonly T _args;

		public StateMachine(IStateProvider<T> stateProvider, T args)
		{
			_stateProvider = stateProvider;
			_args = args;
			Configuration = new MachineConfiguration<T>();
		}

		public MachineConfiguration<T> Configuration { get; private set; }
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
			Configuration.OnEnterState(_args, previous, target);
		}

		private void OnLeaveState(State<T> target, State<T> previous)
		{
			Configuration.OnLeaveState(_args, previous, target);

			if (previous != null)
			{
				previous.OnLeave(_args, target);
			}
		}

		
	}
}