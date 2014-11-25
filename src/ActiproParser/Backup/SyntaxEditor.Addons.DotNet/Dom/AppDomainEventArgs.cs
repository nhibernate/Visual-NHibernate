using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Event arguments for <see cref="AppDomain"/> events.
	/// </summary>
	/// <remarks>
	/// This class is used with the <see cref="AppDomainEventHandler" /> delegate.
	/// </remarks>
	/// <seealso cref="AppDomainEventHandler" />
	public class AppDomainEventArgs : EventArgs {

		private AppDomain		domain;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>AppDomainEventArgs</c> class.
		/// </summary>
		/// <param name="domain">The <see cref="AppDomain"/> for which this event is raised.</param>
		public AppDomainEventArgs(AppDomain domain) {
			// Initialize parameters
			this.domain = domain;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="AppDomain"/> for which this event is raised.
		/// </summary>
		/// <value>The <see cref="AppDomain"/> for which this event is raised.</value>
		public AppDomain Domain {
			get {
				return domain;
			}
		}

	}
}
