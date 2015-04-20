using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormProperty : UserControl, IPropertyForm, IEventSender
	{
		public bool EventRaisingDisabled { get; set; }

		public FormProperty()
		{
			InitializeComponent();

			buttonRemoveProperty.Click += (s, e) => RemoveProperty.RaiseEventEx(this);

			tbType.TextChanged += (s, e) =>
									{
										DatatypeChanged.RaiseEventEx(this);
										validationPropertyGrid1.SetAvailableOptions(
											ValidationOptions.GetApplicableValidationOptionsForType(tbType.Text));
									};
			tbName.TextChanged += (s, e) => PropertyNameChanged.RaiseEventEx(this);
			cbReadOnly.CheckedChanged += (s, e) => ReadOnlyChanged.RaiseEventEx(this);
			cbIsKeyProperty.CheckedChanged += (s, e) => IsKeyChanged.RaiseEventEx(this);

			tbType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			tbType.AutoCompleteSource = AutoCompleteSource.CustomSource;
			var collection = new AutoCompleteStringCollection();
			collection.AddRange(SQLServer.CLRTypeList.ToArray());
			tbType.AutoCompleteCustomSource = collection;
		}

		public void SetValidationOptions(ValidationOptions options)
		{
			validationPropertyGrid1.SetValidationOptions(options);
		}

		public void SetVirtualProperties(IEnumerable<IUserOption> virtualProperties)
		{
			virtualPropertyGrid1.SetVirtualProperties(virtualProperties);
		}

		public void RefreshVirtualProperties()
		{
			virtualPropertyGrid1.RefreshVisibilities();
		}

		public void Clear()
		{
			using (new EventDisabler(this))
			{
				Datatype = "";
				PropertyName = "";
				ReadOnly = false;
				SetValidationOptions(null);
			}
		}

		public bool ShouldShowReadOnly
		{
			get { return cbReadOnly.Visible; }
			set
			{
				cbReadOnly.Visible = value;
				labelReadOnly.Visible = value;
			}
		}

		public bool ShouldShowNullable
		{
			get { return false; }
			set { }
		}

		public bool ShouldShowIsKeyProperty
		{
			get { return cbIsKeyProperty.Visible; }
			set { cbIsKeyProperty.Visible = value; labelKeyProperty.Visible = value; }
		}

		public string Datatype
		{
			get { return tbType.Text; }
			set
			{
				using (new EventDisabler(this))
				{
					tbType.Text = value;
					validationPropertyGrid1.SetAvailableOptions(
						ValidationOptions.GetApplicableValidationOptionsForType(tbType.Text));
				}
			}
		}

		public string PropertyName
		{
			get { return tbName.Text; }
			set { using (new EventDisabler(this)) { tbName.Text = value; } }
		}

		public bool ReadOnly
		{
			get { return cbReadOnly.Checked; }
			set { using (new EventDisabler(this)) { cbReadOnly.Checked = value; } }
		}

		public bool IsKeyProperty
		{
			get { return cbIsKeyProperty.Checked; }
			set { using (new EventDisabler(this)) { cbIsKeyProperty.Checked = value; } }
		}

		private bool _isOveridden;
		public bool IsOveridden
		{
			get { return _isOveridden; }
			set { _isOveridden = value; SetUpInheritanceLabel(); }
		}

		private void SetUpInheritanceLabel()
		{
			if (_isOveridden)
			{
				labelInheritance.Text = "This Property overrides a property in the parent Entity";
			}
			else
			{
				labelInheritance.Text = "";
			}
		}

		public void StartBulkUpdate()
		{
			Slyce.Common.Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Slyce.Common.Utility.ResumePainting(this);
		}

		public event EventHandler DatatypeChanged;
		public event EventHandler PropertyNameChanged;
		public event EventHandler ReadOnlyChanged;
		public event EventHandler IsKeyChanged;
		public event EventHandler RemoveProperty;
	}
}
