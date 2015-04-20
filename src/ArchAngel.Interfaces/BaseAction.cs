using System;

namespace ArchAngel.Interfaces
{
	[ActionAttribute("BaseAction", typeof(BaseAction), "This should be used.", "")]
	[Serializable]
	public class BaseAction //: IBaseAction
	{
		private string _Description;

		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}

		/// <summary>
		/// Runs the Action
		/// </summary>
		/// <returns>True if successful, false otherwise.</returns>
		public virtual bool Run()
		{
			return true;
		}

		public virtual string SaveToXml()
		{
			return "";
		}

		public string DisplayName
		{
			get
			{
				return ((ArchAngel.Interfaces.ActionAttribute)this.GetType().GetCustomAttributes(false)[0]).DisplayName;
			}
		}

		public Type OwnerType
		{
			get
			{
				return ((ArchAngel.Interfaces.ActionAttribute)this.GetType().GetCustomAttributes(false)[0]).OwnerType;
			}
		}

		public virtual void ReadFromXml(string xml)
		{
		}

		public virtual void SetDefaultValues()
		{
		}

	}
}
