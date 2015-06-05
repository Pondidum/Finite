using System;
using System.Collections.Generic;
using System.Linq;
using Finite.Configurations;

namespace Finite.StateProviders
{
	public class ScanningStateProvider<TSwitches> : IStateProvider<TSwitches>
	{
		private readonly IInstanceCreator _instanceBuilder;
		private readonly List<Type> _types;

		public ScanningStateProvider() : this(null)
		{
		}

		public ScanningStateProvider(IInstanceCreator instanceCreator)
		{
			_instanceBuilder = instanceCreator ?? new DefaultInstanceCreator();
			_types = typeof(TSwitches)
				.Assembly
				.GetTypes()
				.Where(t => t.IsAbstract == false)
				.Where(t => typeof(State<TSwitches>).IsAssignableFrom(t))
				.ToList();
		}

		public IEnumerable<Type> KnownTypes {  get { return _types; }}

		public IEnumerable<State<TSwitches>> Execute()
		{
			return _types
				.Select(t => _instanceBuilder.Create<TSwitches>(t))
				.ToList();
		}
	}
}
