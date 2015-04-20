
namespace ArchAngel.Interfaces
{
	public interface IProgressUpdater
	{
		void SetCurrentState(string message, ProgressState type);
		void SetCurrentState(string message, int percentageProgress, ProgressState type);
	}

	public class NullProgressUpdater : IProgressUpdater
	{
		public void SetCurrentState(string message, ProgressState type)
		{
		}

		public void SetCurrentState(string message, int percentageProgress, ProgressState type)
		{
		}
	}

	public enum ProgressState
	{
		Normal, Error, Fatal
	}
}