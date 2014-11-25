namespace SchemaDiagrammer.View
{
	/// <summary>
	/// Common interface to all diagram entities.
	/// </summary>
	public interface IDiagramEntity
	{
		/// <summary>
		/// Gets or sets the unique identifier inside the document.
		/// </summary>
		/// <value>The UID.</value>
		string UID { get; set; }
		/// <summary>
		/// Gets or sets the surface.
		/// </summary>
		/// <value>The surface.</value>
		DiagramSurface Surface { get; set; }

		void RemoveFromSurface(DiagramSurface surface);
		void AddToSurface(DiagramSurface surface);

		bool Visible { get; set; }
	}
}