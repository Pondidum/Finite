using System.Text;

namespace Finite.Renderers
{
	public class GraphvizRenderer
	{
		private readonly StringBuilder _sb;

		public GraphvizRenderer()
		{
			_sb = new StringBuilder();
		}

		public string Output { get { return _sb.ToString(); } }

		public void Render<TSwitches>(StateMachine<TSwitches> machine)
		{
			_sb.Clear();

			_sb.AppendLine("digraph {");

			foreach (var state in machine.States)
			{
				var fromName = state.GetType().Name;

				foreach (var link in state.Links)
				{
					var toName = link.Target.GetType().Name;

					_sb.AppendFormat("\t{0} -> {1};", fromName, toName).AppendLine();

				}
			}

			_sb.AppendLine("}");
		}
	}
}
