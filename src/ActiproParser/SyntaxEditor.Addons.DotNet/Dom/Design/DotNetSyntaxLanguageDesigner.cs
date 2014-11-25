using System;
using System.ComponentModel.Design;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom.Design {

	/// <summary>
	/// Extends design-time behavior for an <see cref="DotNetSyntaxLanguage"/>.
	/// </summary>
	internal class DotNetSyntaxLanguageDesigner : ActiproSoftware.WinUICore.Design.ActiproComponentDesignerBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENT HANDLERS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the user selects the About verb.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void verb_About(object sender, EventArgs e) {
			this.SyntaxLanguage.ShowAboutForm();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Implemented by inheritors to initialize the <see cref="ActiproSoftware.WinUICore.Design.ActiproControlDesignerBase.VerbsCore"/> property.
		/// </summary>
		protected override void InitializeVerbs() {
			this.Verbs.Add(new DesignerVerb("About...", new EventHandler(verb_About)));
		}

		/// <summary>
		/// Gets the <see cref="DotNetSyntaxLanguage"/> that is being designed.
		/// </summary>
		/// <value>The <see cref="DotNetSyntaxLanguage"/> that is being designed.</value>
		public DotNetSyntaxLanguage SyntaxLanguage {
			get {
				return this.Component as DotNetSyntaxLanguage;
			}
		}

	}
}
