using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using DevComponents.DotNetBar;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
	public partial class EntityDetailsEditor : UserControl
	{
		private class GeneratorParameterDisplayItem
		{
			public GeneratorParameterDisplayItem(string displayName, LabelX label, TextBox textbox)
			{
				DisplayName = displayName;
				Label = label;
				Textbox = textbox;
			}

			public TextBox Textbox { get; set; }
			public LabelX Label { get; set; }
			public string DisplayName { get; set; }
		}

		private Entity _Entity;
		private DetailsEditorHelper Helper;
		private List<GeneratorParameterDisplayItem> ParamDisplayItems = new List<GeneratorParameterDisplayItem>();

		public EntityDetailsEditor()
		{
			InitializeComponent();

			BackColor = Color.Black; //Color.FromArgb(40, 40, 40);
			ForeColor = Color.White;
			Helper = new DetailsEditorHelper(this, BackColor, ForeColor, superTooltip1);
			virtualPropertyGrid1.BackColor = BackColor;
			this.Controls.Remove(virtualPropertyGrid1);
		}
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}
		public Entity Entity
		{
			get { return _Entity; }
			set
			{
				_Entity = value;
				Populate();
			}
		}

		public void Clear()
		{
			//for (int i = 0; i < this.Controls.Count; i++)
			//{
			//    superTooltip1.SetSuperTooltip(this.Controls[i], null);
			//}
			//this.Controls.Clear();
			Helper.Clear();
		}

		private void Populate()
		{
			if (Entity == null)
				return;

			Slyce.Common.Utility.SuspendPainting(this);
			Clear();
			int maxLabelWidth = 0;

			LabelX label = new LabelX();
			Graphics g = Graphics.FromHwnd(label.Handle);
			maxLabelWidth = Math.Max(maxLabelWidth, Convert.ToInt32(g.MeasureString("ID Generator", label.Font).Width));
			maxLabelWidth = Math.Max(maxLabelWidth, Convert.ToInt32(g.MeasureString("2nd level cache", label.Font).Width));

			for (int i = 0; i < Entity.Ex.Count; i++)
				maxLabelWidth = Math.Max(maxLabelWidth, Convert.ToInt32(g.MeasureString(Entity.Ex[i].Name, label.Font).Width));

			maxLabelWidth += 10;

			int top = 5;

			//if (textBoxName == null)
			//{
			//textboxName = new TextBox();
			//tb.BackColor = this.BackColor;
			//tb.ForeColor = this.ForeColor;
			textBoxName.Top = top;
			textBoxName.Left = maxLabelWidth + 5 + 5;
			//this.Controls.Add(textboxName);
			//textboxName.TextChanged += new EventHandler(tb_TextChanged);

			top = Helper.AddLabel("Name", top, maxLabelWidth) + 5;

			#region ID Generator
			groupPanelIdGenerator.Top = top;
			groupPanelIdGenerator.Left = maxLabelWidth + 5 + 5;

			top = Helper.AddLabel("ID Generator", top + groupPanelIdGenerator.Height / 2 - labelGeneratorParam3.Height + 3, maxLabelWidth);
			top = groupPanelIdGenerator.Bottom + 5;
			#endregion

			#region Cache
			groupBoxCache.Top = top;
			groupBoxCache.Left = maxLabelWidth + 5 + 5;

			top = Helper.AddLabel("2nd level cache", top + groupBoxCache.Height / 2 - labelGeneratorParam3.Height + 3, maxLabelWidth);
			top = groupBoxCache.Bottom + 5;
			#endregion

			#region IsAbstract
			checkBoxIsAbstract.Top = top;
			checkBoxIsAbstract.Left = maxLabelWidth + 5 + 5;
			checkBoxIsAbstract.Checked = Entity.IsAbstract;

			top = Helper.AddLabel("Abstract", top + checkBoxIsAbstract.Height / 2 - labelGeneratorParam3.Height + 3, maxLabelWidth);
			top = checkBoxIsAbstract.Bottom;
			#endregion

			textBoxName.Text = Entity.Name;
			comboBoxIdGenerator.Items.Clear();
			comboBoxIdGenerator.Items.AddRange(GetGeneratorDisplayNames());
			comboBoxIdGenerator.Text = Entity.Generator.ClassName;

			for (int paramCounter = 0; paramCounter < Entity.Generator.Parameters.Count; paramCounter++)
			{
				switch (paramCounter)
				{
					case 0:
						labelGeneratorParam1.Text = Entity.Generator.Parameters[paramCounter].Name;
						textBoxGeneratorParam1.Text = Entity.Generator.Parameters[paramCounter].Value;
						break;
					case 1:
						labelGeneratorParam2.Text = Entity.Generator.Parameters[paramCounter].Name;
						textBoxGeneratorParam2.Text = Entity.Generator.Parameters[paramCounter].Value;
						break;
					case 2:
						labelGeneratorParam3.Text = Entity.Generator.Parameters[paramCounter].Name;
						textBoxGeneratorParam3.Text = Entity.Generator.Parameters[paramCounter].Value;
						break;
				}
			}
			comboBoxCacheInclude.Items.Clear();
			comboBoxCacheInclude.Items.AddRange(Enum.GetNames(typeof(Cache.IncludeTypes)));

			comboBoxCacheUsage.Items.Clear();
			comboBoxCacheUsage.Items.AddRange(Enum.GetNames(typeof(Cache.UsageTypes)));


			if (Entity.Cache == null)
			{
				comboBoxCacheInclude.Text = Cache.IncludeTypes.All.ToString();
				comboBoxCacheUsage.Text = Cache.UsageTypes.None.ToString();
			}
			else
			{
				comboBoxCacheInclude.Text = Entity.Cache.Include.ToString();
				comboBoxCacheUsage.Text = Entity.Cache.Usage.ToString();
				textBoxCacheRegion.Text = Entity.Cache.Region;
			}
			if (!this.Controls.Contains(virtualPropertyGrid1))
			{
				this.Controls.Add(virtualPropertyGrid1);
				virtualPropertyGrid1.BackColor = this.BackColor;
				virtualPropertyGrid1.ForeColor = this.ForeColor;
				virtualPropertyGrid1.Top = top;
				virtualPropertyGrid1.Left = 0;
				virtualPropertyGrid1.Width = Width - 5;
				virtualPropertyGrid1.Height = Height - virtualPropertyGrid1.Top;
				virtualPropertyGrid1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;// | AnchorStyles.Bottom;
				virtualPropertyGrid1.AutoSize = false;
				virtualPropertyGrid1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				this.Height = virtualPropertyGrid1.Bottom + 50;
			}
			virtualPropertyGrid1.SetVirtualProperties(Entity.Ex);
			Slyce.Common.Utility.ResumePainting(this);
		}

		private string[] GetParameters(string generatorName)
		{
			switch (generatorName)
			{
				case "increment":
				case "identity":
				case "uuid.string":
				case "guid":
				case "guid.comb":
				case "guid.native":
				case "trigger-identity":
				case "native":
				case "assigned":
				case "none":
					return new string[0];
				case "sequence":
					return new string[] { "sequence" };
				case "hilo":
					return new string[] { "table", "column", "max_lo" };
				case "seqhilo":
					return new string[] { "sequence" };
				case "uuid.hex":
					return new string[] { "format", "seperator" };
				case "select":
					return new string[] { "key" };
				case "sequence-identity":
					return new string[] { "sequence" };
				//case "counter":
				//    return "counter";
				case "foreign":
					return new string[] { "property" };
				default:
					return new string[] { "param 1", "param 2", "param 3" };
			}

		}

		private string[] GetGeneratorDisplayNames()
		{
			return new string[] {    "increment",
														"identity",
														"sequence",
														"hilo",
														"seqhilo",
														"uuid.hex",
														"uuid.string",
														"guid",
														"guid.comb",
														"guid.native",
														"select",
														"sequence-identity",
														"trigger-identity",
														//"counter",
														"native",
														"assigned",
														"foreign"};
		}

		private string GetGeneratorNameFromDisplayName(string name)
		{
			switch (name)
			{
				case "increment":
					return "increment";
				case "identity":
					return "identity";
				case "sequence":
					return "sequence";
				case "hilo":
					return "hilo";
				case "seqhilo":
					return "seqhilo";
				case "uuid.hex":
					return "uuid_hex";
				case "uuid.string":
					return "uuid_string";
				case "guid":
					return "guid";
				case "guid.comb":
					return "guid_comb";
				case "guid.native":
					return "guid_native";
				case "select":
					return "select";
				case "sequence-identity":
					return "sequence_identity";
				case "trigger-identity":
					return "trigger_identity";
				//case "counter":
				//    return "counter";
				case "native":
					return "native";
				case "assigned":
					return "assigned";
				case "foreign":
					return "foreign";
				default:
					throw new NotImplementedException("Generator name not handled yet: " + name);
			}
		}

		private string GetGeneratorDisplayNameFromGeneratorName(string name)
		{
			switch (name)
			{
				case "increment":
					return "increment";
				case "identity":
					return "identity";
				case "sequence":
					return "sequence";
				case "hilo":
					return "hilo";
				case "seqhilo":
					return "seqhilo";
				case "uuid_hex":
					return "uuid.hex";
				case "uuid_string":
					return "uuid.string";
				case "guid":
					return "guid";
				case "guid_comb":
					return "guid.comb";
				case "guid_native":
					return "guid.native";
				case "select":
					return "select";
				case "sequence_identity":
					return "sequence-identity";
				case "trigger_identity":
					return "trigger-identity";
				//case "counter":
				//    return "counter";
				case "native":
					return "native";
				case "assigned":
					return "assigned";
				case "foreign":
					return "foreign";
				default:
					throw new NotImplementedException("Generator name not handled yet: " + name);
			}
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			var originalName = Entity.Name;
			Entity.Name = ((TextBox)sender).Text;

			if (originalName != Entity.Name)
				ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
		}

		private void comboBoxIdGenerator_SelectedIndexChanged(object sender, EventArgs e)
		{
			PopulateParameterInputs(comboBoxIdGenerator.Text);
			Entity.Generator.ClassName = comboBoxIdGenerator.Text;
		}

		private void PopulateParameterInputs(string generatorName)
		{
			Slyce.Common.Utility.SuspendPainting(this);

			try
			{
				HideAllParams();
				//Entity.Generator.Parameters.Clear();
				ShowParams(GetParameters(generatorName));
			}
			finally
			{
				Slyce.Common.Utility.ResumePainting(this);
			}
		}

		private void HideAllParams()
		{
			labelGeneratorParam1.Visible = false;
			labelGeneratorParam2.Visible = false;
			labelGeneratorParam3.Visible = false;
			textBoxGeneratorParam1.Visible = false;
			textBoxGeneratorParam2.Visible = false;
			textBoxGeneratorParam3.Visible = false;
		}

		private void ShowParams(string[] paramNames)
		{
			for (int i = 0; i < paramNames.Length; i++)
			{
				switch (i)
				{
					case 0:
						labelGeneratorParam1.Text = paramNames[i];
						labelGeneratorParam1.Visible = true;
						textBoxGeneratorParam1.Visible = true;

						if (Entity.Generator.Parameters.Count < i + 1)
							Entity.Generator.Parameters.Add(new EntityGenerator.Parameter(paramNames[i], ""));
						break;
					case 1:
						labelGeneratorParam2.Text = paramNames[i];
						labelGeneratorParam2.Visible = true;
						textBoxGeneratorParam2.Visible = true;

						if (Entity.Generator.Parameters.Count < i + 1)
							Entity.Generator.Parameters.Add(new EntityGenerator.Parameter(paramNames[i], ""));
						break;
					case 2:
						labelGeneratorParam3.Text = paramNames[i];
						labelGeneratorParam3.Visible = true;
						textBoxGeneratorParam3.Visible = true;

						if (Entity.Generator.Parameters.Count < i + 1)
							Entity.Generator.Parameters.Add(new EntityGenerator.Parameter(paramNames[i], ""));
						break;
					default:
						throw new NotImplementedException("Not handled yet in ShowParams().");
				}
			}
		}

		private void comboBoxIdGenerator_TextChanged(object sender, EventArgs e)
		{
			var original = Entity.Generator.ClassName;
			PopulateParameterInputs(comboBoxIdGenerator.Text);
			Entity.Generator.ClassName = comboBoxIdGenerator.Text;

			if (original != Entity.Generator.ClassName)
				ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
		}

		private void textBoxGeneratorParam1_TextChanged(object sender, EventArgs e)
		{
			var originalName = Entity.Generator.Parameters[0].Name;
			var originalValue = Entity.Generator.Parameters[0].Value;

			Entity.Generator.Parameters[0].Name = labelGeneratorParam1.Text;
			Entity.Generator.Parameters[0].Value = textBoxGeneratorParam1.Text;

			if (originalName != Entity.Generator.Parameters[0].Name ||
				originalValue != Entity.Generator.Parameters[0].Value)
				ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
		}

		private void textBoxGeneratorParam2_TextChanged(object sender, EventArgs e)
		{
			var originalName = Entity.Generator.Parameters[1].Name;
			var originalValue = Entity.Generator.Parameters[1].Value;

			Entity.Generator.Parameters[1].Name = labelGeneratorParam2.Text;
			Entity.Generator.Parameters[1].Value = textBoxGeneratorParam2.Text;

			if (originalName != Entity.Generator.Parameters[0].Name ||
				originalValue != Entity.Generator.Parameters[0].Value)
				ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
		}

		private void textBoxGeneratorParam3_TextChanged(object sender, EventArgs e)
		{
			var originalName = Entity.Generator.Parameters[2].Name;
			var originalValue = Entity.Generator.Parameters[2].Value;

			Entity.Generator.Parameters[2].Name = labelGeneratorParam3.Text;
			Entity.Generator.Parameters[2].Value = textBoxGeneratorParam3.Text;

			if (originalName != Entity.Generator.Parameters[0].Name ||
				originalValue != Entity.Generator.Parameters[0].Value)
				ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
		}

		private void comboBoxCacheUsage_SelectedIndexChanged(object sender, EventArgs e)
		{
			var originalUsage = Entity.Cache.Usage;
			Entity.Cache.Usage = (Cache.UsageTypes)Enum.Parse(typeof(Cache.UsageTypes), comboBoxCacheUsage.Text, true);
			comboBoxCacheInclude.Enabled = textBoxCacheRegion.Enabled = Entity.Cache.Usage != Cache.UsageTypes.None;

			if (originalUsage != Entity.Cache.Usage)
				ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
		}

		private void comboBoxCacheInclude_SelectedIndexChanged(object sender, EventArgs e)
		{
			var originalInclude = Entity.Cache.Include;
			Entity.Cache.Include = (Cache.IncludeTypes)Enum.Parse(typeof(Cache.IncludeTypes), comboBoxCacheInclude.Text, true);

			if (originalInclude != Entity.Cache.Include)
				ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
		}

		private void textBoxCacheRegion_TextChanged(object sender, EventArgs e)
		{
			var original = Entity.Cache.Region;
			Entity.Cache.Region = textBoxCacheRegion.Text;

			if (original != Entity.Cache.Region)
				ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
		}

		private void checkBoxIsAbstract_CheckedChanged(object sender, EventArgs e)
		{
			var originalIsAbstract = Entity.IsAbstract;
			Entity.IsAbstract = checkBoxIsAbstract.Checked;

			if (originalIsAbstract != Entity.IsAbstract)
				ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
		}
	}
}
