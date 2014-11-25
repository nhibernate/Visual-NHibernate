using ArchAngel.Interfaces.Controls.ContentItems;

namespace ArchAngel.Workbench.OptionsItems
{
	public class OptionScreen : ContentItem
	{
		/// <summary>
		/// Saves the data currently on the screen.
		/// </summary>
		/// <returns>True if the data was saved, false if something didn't validate.</returns>
		public virtual bool OnSave()
		{
			return true;
		}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // OptionScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Name = "OptionScreen";
            this.ResumeLayout(false);

        }
	}
}