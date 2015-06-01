using System;
using System.IO;
using System.Text;

namespace Finite.Renderers
{
	public class GraphvizRenderer<TSwitches> : IMachineRenderer<TSwitches>
	{
		private readonly StringBuilder _sb;

		public GraphvizRenderer()
		{
			_sb = new StringBuilder();
		}

		public string Output { get { return _sb.ToString(); } }

		public void Render(States<TSwitches> states)
		{
			_sb.Clear();

			_sb.AppendLine("digraph {");

			foreach (var state in states.AsEnumerable())
			{
				var fromName = state.GetType().Name;

				foreach (var link in state.Links)
				{
					var toName = link.Target.GetType().Name;

					_sb.AppendFormat("\t{0} -> {1};\n", fromName, toName);

				}
			}

			_sb.AppendLine("}");
		}
	}
}
