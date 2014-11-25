using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents the method that will handle <see cref="AppDomain"/> events.
	/// </summary>
	/// <param name="sender">Sender of the event.</param>
	/// <param name="e">A <see cref="AppDomainEventArgs"/> containing event data.</param>
	/// <remarks>
	/// When you create a <c>AppDomainEventHandler</c> delegate, you identify the method that will handle the event. 
	/// To associate the event with your event handler, add an instance of the delegate to the event. 
	/// The event handler is called whenever the event occurs, unless you remove the delegate.
	/// </remarks>
	/// <seealso cref="AppDomainEventArgs" />
	public delegate void AppDomainEventHandler(object sender, AppDomainEventArgs e);
}