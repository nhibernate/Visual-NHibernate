using System;
using System.ComponentModel;
using System.Linq;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class RelationshipPresenter : PresenterBase
	{
		private Relationship relationship;
		private readonly IRelationshipForm form;

		public RelationshipPresenter(IMainPanel mainPanel, IRelationshipForm form) : base(mainPanel)
		{
			this.form = form;
			SetupForm();

			form.RelationshipNameChanged += (sender, e) => { if (!Detached) relationship.Name = form.RelationshipName; };
			form.PrimaryKeyChanged += form_Key1Changed;
			form.ForeignKeyChanged += form_Key2Changed;
			form.DeleteRelationship += form_DeleteRelationship;
		}

		void form_Key1Changed(object sender, EventArgs e)
		{
			if (Detached) return;

			var key = form.PrimaryKey;
			relationship.SetPrimaryEnd(key);
		}

		void form_Key2Changed(object sender, EventArgs e)
		{
			if (Detached) return;

			var key = form.ForeignKey;
			relationship.SetForeignEnd(key);
		}

		void form_DeleteRelationship(object sender, EventArgs e)
		{
			if (Detached) return;

            //mainPanel.DisableDiagramRefresh();
            var tempRelationship = relationship;
            var database = relationship.Database;
            DetachFromModel();

			//mainPanel.ShowPropertyGrid(null);
			tempRelationship.DeleteSelf();
            //mainPanel.EnableDiagramRefresh();
            //mainPanel.ShowOnDiagram(database);
		}

		private void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            form.RefreshVirtualProperties();

			if (sender == relationship)
			{
				switch (e.PropertyName)
				{
					case "Name":
						form.RelationshipName = relationship.Name;
						break;
					case "PrimaryKey":
						form.PrimaryKey = relationship.PrimaryKey;
						break;
					case "ForeignKey":
						form.ForeignKey = relationship.ForeignKey;
						break;
				}
			}
		}

		private void SetupForm()
		{
			if (Detached)
				return;
			
			form.Clear();
			form.RelationshipName = relationship.Name;

			var allKeys = relationship.Database.GetAllKeys().ToList();
			var primaryKeys = allKeys.Where(k => k.Keytype == DatabaseKeyType.Primary);
			var foreignKeys = allKeys.Where(k => k.Keytype == DatabaseKeyType.Foreign);

			form.SetPossiblePrimaryKeys(primaryKeys);
			form.SetPossibleForeignKeys(foreignKeys);

			form.PrimaryKey = relationship.PrimaryKey;
			form.ForeignKey = relationship.ForeignKey;

			form.SetVirtualProperties(relationship.Ex);
		}

		public void AttachToModel(Relationship rel)
		{
			if (!Detached) DetachFromModel();

			relationship = rel;
			relationship.PropertyChanged += Entity_PropertyChanged;
			Detached = false;

			SetupForm();
		}

		public override void DetachFromModel()
		{
			if (Detached || relationship == null) return;

			relationship.PropertyChanged -= Entity_PropertyChanged;
			relationship = null;
			Detached = true;
			SetupForm();
		}

		internal override void AttachToModel(object obj)
		{
			if (obj is Relationship == false)
				throw new ArgumentException("Model must be a Relationship!");
			AttachToModel((Relationship)obj);
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}
