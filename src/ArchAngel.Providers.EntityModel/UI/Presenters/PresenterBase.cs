using ArchAngel.Providers.EntityModel.UI.PropertyGrids;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public abstract class PresenterBase
	{
		protected readonly IMainPanel mainPanel;
		public bool Detached { get; protected set; }

		protected PresenterBase(IMainPanel mainPanel)
		{
			this.mainPanel = mainPanel;
			Detached = true;
		}

        //protected void ShowPropertyGrid(IEditorForm userControl)
        //{
        //    mainPanel.ShowPropertyGrid(userControl);
        //}

		public abstract void DetachFromModel();

		/// <summary>
		/// Caution: This method will throw an ArgumentException if you give it anything
		/// but the correct Type of object expected. It is only provided to make it easy
		/// to work with Presenters in PresenterController. Everyone else should use the
		/// specific version of AttachToModel if possible.
		/// </summary>
		/// <param name="obj"></param>
		internal abstract void AttachToModel(object obj);

		public abstract void Show();
	}
}