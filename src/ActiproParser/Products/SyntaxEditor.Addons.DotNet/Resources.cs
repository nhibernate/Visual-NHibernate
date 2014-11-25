using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ActiproSoftware.Products.SyntaxEditor.Addons.DotNet {

	/// <summary>
	/// Provides a class for accessing the resources of an assembly.
	/// </summary>
	public sealed class Resources : ActiproSoftware.Products.Resources {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>Resources</c> class.
		/// </summary>
		/// <remarks>
		/// The default constructor initializes all fields to their default values.
		/// </remarks>
		internal Resources() {
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets a <see cref="Image"/> from the resources.
		/// </summary>
		/// <param name="image">The type of the image resource to get.</param>
		/// <returns>The <see cref="Image"/> resource that was retrieved.</returns>
		public Image GetImage(ImageResource image) {
			string fullName = this.GetType().Namespace + ".Images." + image.ToString() + ".png";			
			Stream stream = this.GetType().Assembly.GetManifestResourceStream(fullName);
			return Image.FromStream(stream);
		}		

	}
}
