using System;

namespace ArchAngel.Workbench.OptionsItems
{
	public partial class Formatting : OptionScreen
    {
        public Formatting()
        {
            Title = "Formatting";
            PageHeader = "Formatting";
            PageDescription = "Formatting settings.";
            Populate();
        }

        private void Populate()
        {
            InitializeComponent();
            DisplayTopPanel = true;
        }

		public override bool OnSave()
		{
			return true;
		}
    }
}
