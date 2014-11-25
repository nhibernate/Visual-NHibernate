using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.Controls
{
	public partial class FormScriptObject : Form
	{
		ScriptObject _scriptObject;
		readonly ScriptObject[] _scriptObjects;
		bool IsNew = false;
		internal Model.Database ParentDatabase;
		private readonly Color ErrorBackColor = Color.BlanchedAlmond;

		public bool UpdateTreeView
		{
			get { return checkBoxRenameRelatedObjects.Checked; }
		}

		public ScriptObject ScriptObject
		{
			get { return _scriptObject; }
		}

		public FormScriptObject(ScriptObject[] scriptObjects, Model.Database parentDatabase)
		{
			InitializeComponent();
			BackColor = Slyce.Common.Colors.BackgroundColor;
			ParentDatabase = parentDatabase;

			_scriptObjects = scriptObjects;
			ucHeading1.Text = "";
			Interfaces.Events.ShadeMainForm();
		}

		public FormScriptObject(ScriptObject scriptObject, ScriptObject[] scriptObjects)
		{
			InitializeComponent();
			BackColor = Slyce.Common.Colors.BackgroundColor;

			_scriptObject = scriptObject;
			_scriptObjects = scriptObjects;
			ucHeading1.Text = "";
			Interfaces.Events.ShadeMainForm();
		}

		private void FormScriptObject_Load(object sender, EventArgs e)
		{
			if (_scriptObject == null)
			{
				IsNew = true;
				_scriptObject = new ScriptObject("", true);
				_scriptObject.Database = ParentDatabase;

				if (_scriptObjects.GetType() == typeof(Table[]))
				{
					Text = "Add New Table";
				}
				if (_scriptObjects.GetType() == typeof(Model.View[]))
				{
					Text = "Add New View";
				}
				if (_scriptObjects.GetType() == typeof(StoredProcedure[]))
				{
					Text = "Add New Stored Procedure";
				}
				checkBoxRenameRelatedObjects.Enabled = false;
			}
			else
			{
				Text = "Edit " + _scriptObject.GetType().Name + " " + _scriptObject.Name;
			}
			textBoxName.Text = _scriptObject.Name;
			textBoxAlias.Text = _scriptObject.Alias;
			textBoxAliasPlural.Text = _scriptObject.AliasPlural;
            textBoxDescription.Text = _scriptObject.Description;

			if (!_scriptObject.IsUserDefined)
			{
				textBoxName.ReadOnly = true;
			}
            PopulateAttachedLookups();
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			for (int i = Controls.Count - 1; i >= 0; i--)
			{
				Control control = Controls[i];
				control.Focus();
				if (!Validate())
				{
					DialogResult = DialogResult.None;
					return;
				}
			}

			if (IsNew)
			{
				if (_scriptObjects.GetType() == typeof(Table[]))
				{
					_scriptObject = new Table(textBoxName.Text, true);
				}

				if (_scriptObjects.GetType() == typeof(Model.View[]))
				{
					_scriptObject = new Model.View(textBoxName.Text, true);
				}

				if (_scriptObjects.GetType() == typeof(StoredProcedure[]))
				{
					_scriptObject = new StoredProcedure(textBoxName.Text, true);
				}
				_scriptObject.Alias = textBoxAlias.Text;
				_scriptObject.AliasPlural = textBoxAliasPlural.Text;
                _scriptObject.Description = textBoxDescription.Text;
			}
			else
			{
				_scriptObject.Name = textBoxName.Text;
				_scriptObject.Alias = textBoxAlias.Text;
				_scriptObject.AliasPlural = textBoxAliasPlural.Text;
                _scriptObject.Description = textBoxDescription.Text;

				if (checkBoxRenameRelatedObjects.Checked)
				{
					RenameRelatedObjects();
				}
			}
		}

		private void textBoxName_KeyUp(object sender, KeyEventArgs e)
		{
			if (IsNew)
			{
				//textBoxAlias.Text = textBoxName.Text;
				_scriptObject.Name = textBoxName.Text;
				textBoxAlias.Text = _scriptObject.AliasDefault(_scriptObject);
				//textBoxAliasPlural.Text = Script.GetPlural(textBoxAlias.Text);
			}
		}

		private void RenameRelatedObjects()
		{
			//TODO:
			return;

/*
			foreach (ScriptObject scriptObject in _scriptObjects)
			{
				foreach (Relationship relationship in scriptObject.Relationships)
				{
					if (relationship.ForeignScriptObject != _scriptObject)
					{
						continue;
					}

					if (relationship.GetType() == typeof(OneToOneRelationship) ||
						relationship.GetType() == typeof(ManyToOneRelationship))
					{
						relationship.Alias = _scriptObject.Alias;
					}

					if (relationship.GetType() == typeof(OneToManyRelationship) ||
						relationship.GetType() == typeof(ManyToManyRelationship))
					{
						relationship.Alias = _scriptObject.AliasPlural;
					}
				}
			}

			if (_scriptObject.GetType() != typeof(ArchAngel.Providers.Database.Model.Table))
			{
				return;
			}

			ArchAngel.Providers.Database.Model.Table table = (ArchAngel.Providers.Database.Model.Table)_scriptObject;

			foreach (Index index in table.Indexes)
			{
				if (index.Type == DatabaseConstant.KeyType.Unique)
				{
					string filterAlias = ArchAngel.Providers.Database.BLL.Helper.GetFilterAlias(index);
					Filter filter = ArchAngel.Providers.Database.BLL.Search.GetFilter(table.Filters, index.Name);

					filter.Alias = filterAlias;
				}
			}

			foreach (Key key in table.Keys)
			{
				if (key.Type == DatabaseConstant.KeyType.Unique ||
					key.Type == DatabaseConstant.KeyType.Primary ||
					key.Type == DatabaseConstant.KeyType.Foreign)
				{
					string filterAlias = ArchAngel.Providers.Database.BLL.Helper.GetFilterAlias(key);
					Filter filter = ArchAngel.Providers.Database.BLL.Search.GetFilter(table.Filters, key.Name);

					filter.Alias = filterAlias;
				}
			}
*/
		}

		private void textBoxName_Validating(object sender, CancelEventArgs e)
		{
			errorProvider.Clear();
			string failReason;

			string originalName = _scriptObject.Name;
			_scriptObject.Name = textBoxName.Text.Trim();

			if (!_scriptObject.NameValidate(_scriptObject, out failReason))
			{
				_scriptObject.Name = originalName;
				errorProvider.SetError(textBoxName, failReason);
				textBoxName.BackColor = ErrorBackColor;
				e.Cancel = true;
				return;
			}
			_scriptObject.Name = originalName; // Reset, so we don't inadvertantly save
			textBoxName.BackColor = Color.White;
		}

		private void FormScriptObject_FormClosed(object sender, FormClosedEventArgs e)
		{
			Interfaces.Events.UnShadeMainForm();
		}

		private void textBoxAlias_TextChanged(object sender, EventArgs e)
		{
			errorProvider.Clear();
			string failReason;

			string originalAlias = _scriptObject.Alias;
			_scriptObject.Alias = textBoxAlias.Text.Trim();

			if (!_scriptObject.AliasValidate(_scriptObject, out failReason))
			{
				textBoxAliasPlural.Text = _scriptObject.AliasPluralDefault(_scriptObject);
				_scriptObject.Alias = originalAlias;
				errorProvider.SetError(textBoxAlias, failReason);
				textBoxAlias.BackColor = ErrorBackColor;
				//e.Cancel = true;
				return;
			}
			textBoxAliasPlural.Text = _scriptObject.AliasPluralDefault(_scriptObject);
			_scriptObject.Alias = originalAlias; // Reset, so we don't inadvertantly save
			textBoxAlias.BackColor = Color.White;
		}

		private void textBoxAliasPlural_TextChanged(object sender, EventArgs e)
		{
			errorProvider.Clear();
			string failReason;

			string originalAliasPlural = _scriptObject.AliasPlural;
			_scriptObject.AliasPlural = textBoxAliasPlural.Text.Trim();

			if (!_scriptObject.AliasPluralValidate(_scriptObject, out failReason))
			{
				_scriptObject.AliasPlural = originalAliasPlural;
				errorProvider.SetError(textBoxAliasPlural, failReason);
				textBoxAliasPlural.BackColor = ErrorBackColor;
				//e.Cancel = true;
				return;
			}
			_scriptObject.AliasPlural = originalAliasPlural; // Reset, so we don't inadvertantly save
			textBoxAliasPlural.BackColor = Color.White;
		}

        private void PopulateAttachedLookups()
        {
            listViewLookups.Items.Clear();

            foreach (Lookup lookup in ScriptObject.AttachedLookups)
            {
                listViewLookups.Items.Add(lookup.Alias);
            }
        }


	}
}