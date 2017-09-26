using System.Windows.Forms;

namespace Sample.Winforms.NewRequestEditor
{
	public interface INewRequestView
	{
		event EventAction SaveRequest;
		event EventAction SubmitRequest;
		event EventAction CancelRequest;

		decimal Amount { get; set; }
		string Justification { get; set; }

		bool SaveEnabled { get; set; }
		bool SubmitEnabled { get; set; }


		DialogResult DialogResult { get; set; }
		DialogResult ShowDialog();
		void Close();
	}
}
