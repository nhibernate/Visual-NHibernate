using System;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class ReferencePresenter : PresenterBase
	{
		private readonly IReferenceForm form;
		private Reference reference;

		public ReferencePresenter(IMainPanel panel, IReferenceForm form)
			: base(panel)
		{
			this.form = form;

			SetupForm();

			form.Entity1Changed += (sender, e) => { if (!Detached) reference.Entity1 = form.Entity1; };
			form.Entity2Changed += (sender, e) => { if (!Detached) reference.Entity2 = form.Entity2; };
			form.End1EnabledChanged += (sender, e) => { if (!Detached) reference.End1Enabled = form.End1Enabled; };
			form.End2EnabledChanged += (sender, e) => { if (!Detached) reference.End2Enabled = form.End2Enabled; };
			form.End1NameChanged += (sender, e) => { if (!Detached) reference.End1Name = form.End1Name; };
			form.End2NameChanged += (sender, e) => { if (!Detached) reference.End2Name = form.End2Name; };
			form.End1CardinalityChanged += (sender, e) => { if (!Detached) reference.Cardinality1 = form.End1Cardinality; };
			form.End2CardinalityChanged += (sender, e) => { if (!Detached) reference.Cardinality2 = form.End2Cardinality; };
			form.MappedTableChanged += form_MappedTableChanged;
			form.MappedRelationshipChanged += form_MappedRelationshipChanged;
			form.DeleteRelationship += form_DeleteRelationship;
		}

		void form_DeleteRelationship(object sender, EventArgs e)
		{
			if (Detached) return;

            //mainPanel.DisableDiagramRefresh();
		    var tempRef = reference;
		    var entitySet = reference.EntitySet;
            DetachFromModel();

            //mainPanel.ShowPropertyGrid(null);
			tempRef.DeleteSelf();
            //mainPanel.EnableDiagramRefresh();
            //mainPanel.ShowOnDiagram(entitySet);
		}

		void form_MappedRelationshipChanged(object sender, EventArgs e)
		{
			if (!Detached)
			{
				var relationship = form.MappedRelationship;
				reference.SetMappedRelationship(relationship);

				if(relationship != null)
					form.MappedTable = null;
			}
		}

		void form_MappedTableChanged(object sender, EventArgs e)
		{
			if (!Detached)
			{
				var table = form.MappedTable;
			 	reference.SetMappedTable(table);
			 	
				if(table != null)
					form.MappedRelationship = null;
			}
		}

		private void SetupForm()
		{
			if (Detached) return;

			form.Clear();
			form.EntityList = reference.EntitySet.Entities;
			form.Entity1 = reference.Entity1;
			form.Entity2 = reference.Entity2;
			form.End1Name = reference.End1Name;
			form.End2Name = reference.End2Name;
			form.End1Enabled = reference.End1Enabled;
			form.End2Enabled = reference.End2Enabled;
			form.End1Cardinality = reference.Cardinality1;
			form.End2Cardinality = reference.Cardinality2;

			form.SetVirtualProperties(reference.Ex);
			
			form.MappedTableList = reference.PossibleMappedTables();
			form.MappedTable = reference.MappedTable();

			form.MappedRelationshipList = reference.PossibleMappedRelationships();
			form.MappedRelationship = reference.MappedRelationship();

			CheckCardinalityAndSetEnabledControls();
		}

		private void CheckCardinalityAndSetEnabledControls()
		{
			if(ReferenceIsManyToMany())
			{
				form.MappedTableSelectionEnabled(true);
				form.MappedRelationshipSelectionEnabled(false);
			}
			else
			{
				form.MappedTableSelectionEnabled(false);
				form.MappedRelationshipSelectionEnabled(true);
			}
		}

		private bool ReferenceIsManyToMany()
		{
			return reference.Cardinality1 == Cardinality.Many && reference.Cardinality2 == Cardinality.Many;
		}

		public void AttachToModel(Reference @ref)
		{
			reference = @ref;
			Detached = false;
			reference.PropertyChanged += reference_PropertyChanged;
			SetupForm();
		}

		void reference_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (sender == reference)
			{
                form.RefreshVirtualProperties();

				switch (e.PropertyName)
				{
					case "Entity1":
						form.Entity1 = reference.Entity1;
						break;
					case "Entity2":
						form.Entity2 = reference.Entity2;
						break;
					case "End1Name":
						form.End1Name = reference.End1Name;
						break;
					case "End2Name":
						form.End2Name = reference.End2Name;
						break;
					case "End1Enabled":
						form.End1Name = reference.End1Name;
						break;
					case "End2Enabled":
						form.End2Enabled = reference.End2Enabled;
						break;
					case "Cardinality1":
						form.End1Cardinality = reference.Cardinality1;
						CheckCardinalityAndSetEnabledControls();
						break;
					case "Cardinality2":
						form.End2Cardinality = reference.Cardinality2;
						CheckCardinalityAndSetEnabledControls();
						break;
				}
			}
		}

		public override void DetachFromModel()
		{
			if (Detached || reference == null) return;

			reference.PropertyChanged -= reference_PropertyChanged;

			reference = null;
			Detached = true;
		}

		internal override void AttachToModel(object obj)
		{
			if (obj is Reference == false)
				throw new ArgumentException("Model must be a Reference!");
			AttachToModel((Reference)obj);
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}
