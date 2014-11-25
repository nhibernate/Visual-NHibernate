using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI.Wizards;
using Slyce.Common;
using Slyce.Common.EventExtensions;
using Component=ArchAngel.Providers.EntityModel.Model.EntityLayer.Component;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormEntityKey : UserControl, IEntityKeyForm
	{
		private readonly List<Property> possibleProperties = new List<Property>();
		private readonly List<Component> possibleComponents = new List<Component>();
		private MappingSet mappingSet;

		public bool EventRaisingDisabled { get; set; }

		public FormEntityKey()
		{
			InitializeComponent();

			Clear();
		}

		public void Clear()
		{
			using (new EventDisabler(this))
			{
				comboBoxComponents.Items.Clear();
				listBoxProperties.Items.Clear();
				radioButtonNoKey.Checked = true;
				
				buttonAddProperty.Enabled = false;
				buttonDeleteProperty.Enabled = false;
				comboBoxComponents.Enabled = false;
				listBoxProperties.Enabled = false;
				convertKeyToComponentToolStripMenuItem.Enabled = false;
			}
		}

		public void SetButtonStatus()
		{
			buttonAddProperty.Enabled = radioButtonProperties.Checked;
			buttonDeleteProperty.Enabled = radioButtonProperties.Checked && listBoxProperties.SelectedIndex != -1;
			convertKeyToComponentToolStripMenuItem.Enabled = radioButtonProperties.Checked;
			comboBoxComponents.Enabled = radioButtonComponent.Checked;
			listBoxProperties.Enabled = radioButtonProperties.Checked;
		}

		public void StartBulkUpdate()
		{
			Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Utility.ResumePainting(this);
		}

		public EntityKeyType KeyType
		{
			get 
			{
				return GetKeyType();
			}
			set
			{
				using(new EventDisabler(this))
				{
					SetKeyType(value);
				}
			}
		}

		private void SetKeyType(EntityKeyType value)
		{
			if(InvokeRequired)
			{
				Invoke(new MethodInvoker(() => SetKeyType(value)));
				return;
			}

			switch(value)
			{
				case EntityKeyType.Component:
					radioButtonComponent.Checked = true;
					break;
				case EntityKeyType.Properties:
					radioButtonProperties.Checked = true;
					break;
				case EntityKeyType.Empty:
					radioButtonNoKey.Checked = true;
					break;
				default:
					throw new ArgumentOutOfRangeException("value");
			}
			SetButtonStatus();
		}

		private EntityKeyType GetKeyType()
		{
			if(InvokeRequired)
			{
				EntityKeyType keyType = EntityKeyType.Empty;
				Invoke(new MethodInvoker(() => keyType = GetKeyType()));
				return keyType;
			}

			if (radioButtonNoKey.Checked)
				return EntityKeyType.Empty;
			if (radioButtonComponent.Checked)
				return EntityKeyType.Component;
			if (radioButtonProperties.Checked)
				return EntityKeyType.Properties;

			return EntityKeyType.Empty;
		}

		public void SetProperties(IEnumerable<Property> properties)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => SetProperties(properties)));
				return;
			}

			listBoxProperties.Items.Clear();

			foreach(var property in properties)
			{
				listBoxProperties.Items.Add(new ComboBoxItemEx<Property>(property, p => p.Name));
			}
		}

		public Component Component
		{
			get { return GetComponent(); }
			set
			{
				using (new EventDisabler(this))
				{
					SetComponent(value);
				}
			}
		}

		private void SetComponent(Component component)
		{
			if(InvokeRequired)
			{
				Invoke(new MethodInvoker(() => SetComponent(component)));
				return;
			}

			comboBoxComponents.SetSelectedItem(component);
		}

		private Component GetComponent()
		{
			if (InvokeRequired)
			{
				Component comp = null;
				Invoke(new MethodInvoker(() => comp = GetComponent()));
				return comp;
			}

			return comboBoxComponents.GetSelectedItem<Component>();
		}

		public void SetParentEntityName(string name)
		{
			if(InvokeRequired)
			{
				Invoke(new MethodInvoker(() => SetParentEntityName(name)));
				return;
			}

			labelEntityName.Text = name;
		}

		public void SetPossibleProperties(IEnumerable<Property> properties)
		{
			possibleProperties.Clear();
			possibleProperties.AddRange(properties);
		}

		public void SetPossibleComponents(IEnumerable<Component> components)
		{
			possibleComponents.Clear();
			possibleComponents.AddRange(components);
			foreach(var component in components)
			{
				comboBoxComponents.Items.Add(new ComboBoxItemEx<Component>(component, c => c.Name));
			}
		}

		public void SetVirtualProperties(IEnumerable<IUserOption> options)
		{
			virtualPropertyGrid1.SetVirtualProperties(options);
		}

		private void buttonAddProperty_Click(object sender, EventArgs e)
		{
			FormSelect<Property> form = new FormSelect<Property>("Property", possibleProperties, p => p.Name);
			
			var result = form.ShowDialog(this);
			Property property = form.SelectedColumn;

			if(result == DialogResult.OK && property != null)
			{
				AddNewProperty.RaiseEventEx(this, new GenericEventArgs<Property>(property));
			}
		}

		private void buttonDeleteProperty_Click(object sender, EventArgs e)
		{
			var selectedProperty = listBoxProperties.GetSelectedItem<Property>();
			RemoveProperty.RaiseEventEx(this, new GenericEventArgs<Property>(selectedProperty));
		}

		private void listBoxProperties_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetButtonStatus();
		}

		private void comboBoxComponents_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComponentChanged.RaiseEventEx(this);
		}

		private void radioButtonNoKey_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonNoKey.Checked)
			{
				KeyTypeChanged.RaiseEventEx(this);
				SetButtonStatus();
			}
		}

		private void radioButtonComponent_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonComponent.Checked)
			{
				KeyTypeChanged.RaiseEventEx(this);
				SetButtonStatus();
			}
		}

		private void radioButtonProperties_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonProperties.Checked)
			{
				KeyTypeChanged.RaiseEventEx(this);
				SetButtonStatus();
			}
		}

		private void convertKeyToComponentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RunKeyConversionWizard.RaiseEventEx(this);
		}

		public event EventHandler<GenericEventArgs<Property>> AddNewProperty;
		public event EventHandler<GenericEventArgs<Property>> RemoveProperty;
		public event EventHandler ComponentChanged;
		public event EventHandler KeyTypeChanged;
		public event EventHandler RunKeyConversionWizard;
	}
}
