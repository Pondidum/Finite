using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Finite
{
	public class StateMachine<T>
	{
		private readonly IInstanceCreator _instanceCreator;

		private Dictionary<Type, State<T>> _states;
		private readonly MachineConfiguration<T> _configuration;

		private T _args;
		private State<T> _currentState;

		public StateMachine(IInstanceCreator instanceCreator )
		{
			_instanceCreator = instanceCreator;
			_configuration = new MachineConfiguration<T>();
		}

		public MachineConfiguration<T> Configuration
		{
			get { return _configuration; }
		}

		public Type CurrentState
		{
			get { return _currentState.GetType(); }
		}

		public void BindTo(T args)
		{
			_args = args;
		}

		public void InitialiseFromThis()
		{
			InitialiseFrom(Assembly.GetExecutingAssembly());
		}

		public void InitialiseFrom(Assembly assembly)
		{
			var types = assembly
				.GetTypes()
				.Where(t => t.IsAbstract == false)
				.Where(t => typeof (State<T>).IsAssignableFrom(t));

			InitialiseFrom(types);
		}

		public void InitialiseFrom(IEnumerable<Type> types)
		{
			_states = types
				.Where(t => typeof (State<T>).IsAssignableFrom(t))
				.ToDictionary(
					t =>t,
					t => _instanceCreator.Create<T>(t));
		}

		public void SetStateTo(State<T> state)
		{
			SetStateTo(state.GetType());
		}

		public void SetStateTo<TTarget>() where TTarget : State<T>
		{
			SetStateTo(typeof (TTarget));
		}
		
		public IEnumerable<Type> GetAllTargetStates()
		{
			return _currentState.Links.Select(l => l.Target);
		}

		public IEnumerable<Type> GetActiveTargetStates()
		{
			return _currentState.Links.Where(l => l.IsActive(_args)).Select(l => l.Target);
		}

		private void SetStateTo(Type target)
		{
			if (_currentState != null && GetActiveTargetStates().Contains(target) == false)
			{
				throw new InvalidTransitionException(_currentState.GetType(), target);
			}

			var previous = _currentState;

			OnLeaveState(target, previous);

			_currentState = _states[target];
			
			OnEnterState(target, previous);
		}

		private void OnEnterState(Type target, State<T> previous)
		{
			var prevType = previous != null ? previous.GetType() : null;

			_currentState.OnEnter(_args, prevType);
			_configuration.OnEnterState(_args, prevType, target);
		}

		private void OnLeaveState(Type target, State<T> previous)
		{
			var prevType = previous != null ? previous.GetType() : null;

			_configuration.OnLeaveState(_args, prevType, target);

			if (previous != null)
			{
				previous.OnLeave(_args, target);
			}
		}

		
	}
}