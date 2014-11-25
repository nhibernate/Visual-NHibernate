using System;
using System.ComponentModel;
using ArchAngel.Interfaces;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.Model
{
	public abstract class ModelObject : ScriptBaseObject, IModelObject
	{
		/// <summary>
		/// This identifier is set when the object is constructed in the current application instance.
		/// It will change every time the model is loaded up, and should not be relied on for anything
		/// but to identify the object within a single session.
		/// </summary>
		public Guid InternalIdentifier { get; private set; }

		public abstract string DisplayName { get; }

		protected ModelObject()
		{
			InternalIdentifier = Guid.NewGuid();
		}

		protected void RaisePropertyChanged(string property)
		{
			PropertyChanged.RaiseEventEx(this, property);
		}

		public override int GetHashCode()
		{
			return InternalIdentifier.GetHashCode();
		}

		public bool EventRaisingDisabled { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
	}

	public interface IModelObject : INotifyPropertyChanged, IEventSender, IScriptBaseObject
	{
		Guid InternalIdentifier { get; }
		string DisplayName { get; }
	}
}
