using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ArchAngel.Interfaces;

namespace ArchAngel.Designer
{
	public partial class frmSelectModelObject : Form
	{
		private static frmSelectModelObject _Instance;


		public Type TypeOfObject { get; set; }

		public object SelectedObject;
		private object[] _RootPreviewObjects;
		private static readonly Dictionary<PropertyInfo, PropertyAttributes> PropertyAttributeValues = new Dictionary<PropertyInfo, PropertyAttributes>();
		private readonly List<DevComponents.AdvTree.Node> PopulatedCollectionNodes = new List<DevComponents.AdvTree.Node>();
		DevComponents.AdvTree.Node CurrentSelectedNode;
		private bool IsPopulated;

		public static frmSelectModelObject Instance
		{
			get
			{
				if (_Instance == null)
				{
					_Instance = new frmSelectModelObject();
				}
				return _Instance;
			}
		}

		private class PropertyAttributes
		{
			public bool IsCollection = false;
			public bool IsIterator = false;

			public PropertyAttributes(bool isCollection, bool isIterator)
			{
				IsCollection = isCollection;
				IsIterator = isIterator;
			}
		}

		private frmSelectModelObject()
		{
			InitializeComponent();
			ucHeading1.Text = "";
			treeList1.Nodes.Clear();
		}

		private PropertyAttributes GetPropertyAttributes(PropertyInfo property)
		{
			if (!PropertyAttributeValues.ContainsKey(property))
			{
				bool isCollection = false;
				bool isIterator = false;
				Type type = property.PropertyType;

				if (type.IsPrimitive || type == typeof(string))
				{ // leave everything as false.
				}
				else if (type.GetInterface("System.Collections.IEnumerable", false) != null)
				{
					isCollection = true;
				}
				else
				{
					object[] allAttributes = property.GetCustomAttributes(false);

					foreach (object att in allAttributes)
					{
						Type attType = att.GetType();

						if (Slyce.Common.Utility.StringsAreEqual(attType.Name, "ArchAngelEditorAttribute", true))
						{
							isIterator = (bool)attType.InvokeMember("IsGeneratorIterator", BindingFlags.GetProperty, null, att, null);
							break;
						}
					}
				}
				PropertyAttributeValues.Add(property, new PropertyAttributes(isCollection, isIterator));
			}
			return PropertyAttributeValues[property];
		}

		private object[] RootPreviewObjects
		{
			get
			{
				if (_RootPreviewObjects == null)
				{
					foreach (Assembly assembly in Project.Instance.ReferencedAssemblies)
					{
						if (ProviderInfo.IsProvider(assembly))
						{
							// Does this provider have the type we're looking for?
							if (assembly.GetType(TypeOfObject.FullName) != null)
							{
								if (Controller.PopulatedProviders.ContainsKey(assembly))
								{
									_RootPreviewObjects = Controller.PopulatedProviders[assembly].RootPreviewObjects.ToArray();
								}
								else
								{
									// It is possible I broke something here, as it used to keep searching if it couldn't
									// load the provider data. I didn't think that was necessary as this is definitly the
									// right Provider at this point.
									ProviderInfo providerInfo = ProviderInfo.GetProviderInfo(assembly);

									ProviderHelper.PopulateProviderFromProjectFile(providerInfo, Project.Instance.DebugProjectFile);
									_RootPreviewObjects = providerInfo.RootPreviewObjects.ToArray();
									Controller.PopulatedProviders.Add(assembly, providerInfo);
									break;

									// Unzip .aaproj file
									//string tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName().Replace(".", ""));
									//Directory.CreateDirectory(tempFolder);
									//Slyce.Common.Utility.UnzipFile(Project.Instance.DebugProjectFile, tempFolder);
									//string zipFile = Path.Combine(tempFolder, assembly.GetName().Name.Replace(".", "_") + "_data.zip");
									//string tempProviderFolder = Path.Combine(Path.GetTempPath(), assembly.GetName().Name.Replace(".", "_") + "Temp");

									//if (Directory.Exists(tempProviderFolder))
									//{
									//    Slyce.Common.Utility.DeleteDirectoryBrute(tempProviderFolder);
									//}
									//Directory.CreateDirectory(tempProviderFolder);

									//if (File.Exists(zipFile))
									//{
									//    Slyce.Common.Utility.UnzipFile(zipFile, tempProviderFolder);
									//    //provider.Open(tempProviderFolder);

									//    Interfaces.ProviderInfo providerInfo = ProviderInfo.GetProviderInfo(assembly);
									//    providerInfo.Open(tempProviderFolder);

									//    if (providerInfo == null)
									//    {
									//        throw new Exception("ProviderInfo type not found in assembly: " + assembly.GetName().Name);
									//    }
									//    _RootPreviewObjects = providerInfo.RootPreviewObjects;
									//    Controller.PopulatedProviders.Add(assembly, providerInfo);
									//    break;
									//}
								}
							}
						}
					}
				}
				return _RootPreviewObjects;
			}
		}

		private void SetDescription(string typeName)
		{
			//lblPageDescription.Text = string.Format("Select the {0} to use as a parameter.", typeName);
		}

		public void ShowObject(IWin32Window owner, Type type)
		{
			//bool sameAsssembly = false;

			//if (TypeOfObject != null && TypeOfObject.Assembly == type.Assembly)
			//{
			//    sameAsssembly = true;
			//}
			TypeOfObject = type;
			SetDescription(type.Name);

			Cursor = Cursors.WaitCursor;
			try
			{
				Slyce.Common.Utility.DisplayMessagePanel(treeList1, "Loading object model...", Slyce.Common.Controls.MessagePanel.ImageType.Hourglass);
				LoadObjectModel();
				Slyce.Common.Utility.HideMessagePanel(treeList1);
				PopulatePreviousSelections();
			}
			finally
			{
				Cursor = Cursors.Default;
			}
			ShowDialog();
		}

		private void PopulatePreviousSelections()
		{
			gridPrevSelections.Rows.Clear();
			gridPrevSelections.Rows.Add("  -- empty --");
			List<object> addedObjects = new List<object>();

			foreach (var funcParams in FunctionRunner.CachedParameters.Values)
			{
				foreach (object param in funcParams)
				{
					if (TypeOfObject.IsInstanceOfType(param))
					{
						bool found = false;

						foreach (object alreadyAddedObj in addedObjects)
						{
							if (alreadyAddedObj == param)
							{
								found = true;
								break;
							}
						}
						if (!found)
						{
							string name = string.Format("{0} [{1}]", ProviderInfo.GetDisplayName(param), param.GetType().Name);
							int index = gridPrevSelections.Rows.Add(name);
							gridPrevSelections.Rows[index].Tag = param;
							addedObjects.Add(param);
						}
					}
				}
			}
			gridPrevSelections.Sort(gridPrevSelections.Columns[0], ListSortDirection.Ascending);
		}

		private void LoadObjectModel()
		{
			//try
			//{
			LoadTreeView();
			//}
			//catch (Exception ex)
			//{
			//    Controller.ReportError(ex);
			//}
		}

		#region Edit Model

		public void LoadTreeView()
		{
			if (InvokeRequired)
			{
				MethodInvoker mi = delegate { LoadTreeView(); };
				Controller.SafeInvoke(this, mi);
				return;
			}
			treeList1.BeginUpdate();
			treeList1.Nodes.Clear();

			if (RootPreviewObjects != null)
			{
				foreach (object rootObject in RootPreviewObjects)
				{
					AddObjectToTreeview(rootObject, null);
				}
			}
			else
			{
				MessageBox.Show(this, string.Format("The following ArchAngel Provider ({0}) has not \nimplemented the RootPreviewObjects method correctly - it is returning 'null'. \nPlease contact the vendor of this provider.", Path.GetFileName(TypeOfObject.Assembly.CodeBase)), "Invalid ArchAngel Provider", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
			Slyce.Common.TreelistUtility.TreelistNodeComparer comparer = new Slyce.Common.TreelistUtility.TreelistNodeComparer(Slyce.Common.TreelistUtility.TreelistNodeComparer.SortDirection.Ascending, 0);
			Slyce.Common.TreelistUtility.SortNodes(treeList1, comparer);

			treeList1.EndUpdate();
			IsPopulated = true;
		}

		private void AddObjectToTreeview(object obj, DevComponents.AdvTree.Node parentNode)
		{
			if (InvokeRequired)
			{
				MethodInvoker mi = delegate { AddObjectToTreeview(obj, parentNode); };
				Controller.SafeInvoke(this, mi);
				return;
			}

			string displayName = ProviderInfo.GetDisplayName(obj);
			DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node();
			node.Text = displayName;
			node.Tag = obj;

			if (parentNode != null)
			{
				parentNode.Nodes.Add(node);
			}
			else
			{
				treeList1.Nodes.Add(node);
			}
			PropertyInfo[] properties = obj.GetType().GetProperties();

			if (properties.Length > 0)
			{
				DevComponents.AdvTree.Node tempNode = new DevComponents.AdvTree.Node();
				node.Nodes.Add(tempNode);
			}
			if (obj != null && TypeOfObject.IsInstanceOfType(obj))
			{
				node.Style = treeList1.Styles["elementStyleHighlighted"];
			}
			else
			{
				node.Style = treeList1.Styles["elementStyle1"];
			}
		}

		private void AddPropertiesToTreeview(DevComponents.AdvTree.Node parentNode)
		{
			treeList1.BeginUpdate();
			parentNode.Nodes.Clear();
			PopulatedCollectionNodes.Add(parentNode);
			object obj = parentNode.Tag;
			PropertyInfo[] properties = obj.GetType().GetProperties();

			foreach (PropertyInfo prop in properties)
			{
				PropertyAttributes atts = GetPropertyAttributes(prop);

				// We don't want strings to be treated as collections.
				if (atts.IsCollection)
				{
					DevComponents.AdvTree.Node collectionNode = null;

					foreach (object item in (System.Collections.IEnumerable)prop.GetValue(obj, null))
					{
						// All we want to do is determine whether the collection has any items. Seeing
						// as we are here, it obviously does
						collectionNode = new DevComponents.AdvTree.Node();
						collectionNode.Text = prop.Name;
						parentNode.Nodes.Add(collectionNode);
						//collectionNode.HasChildren = true;
						DevComponents.AdvTree.Node tempNode = new DevComponents.AdvTree.Node();
						collectionNode.Nodes.Add(tempNode);
						break;
					}
				}
				else if (atts.IsIterator)
				{
					object propObject = prop.GetValue(obj, null);

					if (propObject != null)
					{
						string displayName = ProviderInfo.GetDisplayName(propObject);
						DevComponents.AdvTree.Node propNode = new DevComponents.AdvTree.Node();
						propNode.Text = displayName;
						propNode.Tag = propObject;
						parentNode.Nodes.Add(propNode);
						DevComponents.AdvTree.Node tempNode = new DevComponents.AdvTree.Node();
						propNode.Nodes.Add(tempNode);
					}
				}
			}
			//Slyce.Common.TreelistUtility.TreelistNodeComparer comparer = new Slyce.Common.TreelistUtility.TreelistNodeComparer(Slyce.Common.TreelistUtility.TreelistNodeComparer.SortDirection.Ascending, 0);
			//Slyce.Common.TreelistUtility.SortNodes(treeList1, comparer);
			treeList1.EndUpdate();
		}

		private void AddChildrenToTreeview(DevComponents.AdvTree.Node collectionNode)
		{
			if (InvokeRequired)
			{
				MethodInvoker mi = delegate { AddChildrenToTreeview(collectionNode); };
				Controller.SafeInvoke(this, mi);
				return;
			}
			treeList1.BeginUpdate();
			collectionNode.Nodes.Clear();
			object parentObject = collectionNode.Parent.Tag;
			PropertyInfo collectionProperty = parentObject.GetType().GetProperty(collectionNode.Text);
			PopulatedCollectionNodes.Add(collectionNode);

			foreach (object item in (System.Collections.IEnumerable)collectionProperty.GetValue(parentObject, null))
			{
				if (item != null)
				{
					AddObjectToTreeview(item, collectionNode);
				}
			}
			//Slyce.Common.TreelistUtility.TreelistNodeComparer comparer = new Slyce.Common.TreelistUtility.TreelistNodeComparer(Slyce.Common.TreelistUtility.TreelistNodeComparer.SortDirection.Ascending, 0);
			//Slyce.Common.TreelistUtility.SortNodes(collectionNode, comparer);
			treeList1.EndUpdate();
		}

		private Color GetObjectTypeColor(Type objectType)
		{
			if (TypeOfObject == objectType)
			{
				return Color.Black;
			}
			else
			{
				return Color.Red;
			}
		}

		#endregion

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (CurrentSelectedNode == null)
			{
				MessageBox.Show(this, string.Format("No {0} has been selected.", TypeOfObject), "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			SelectedObject = CurrentSelectedNode.Tag;
			this.Visible = false;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			SelectedObject = null;
			this.Visible = false;
		}

		private void frmSelectModelObject_Paint(object sender, PaintEventArgs e)
		{
			this.BackColor = Slyce.Common.Colors.BackgroundColor;
		}

		private void frmSelectModelObject_FormClosing(object sender, FormClosingEventArgs e)
		{
			//if (InShowObject)
			//{
			//    e.Cancel = true;
			//}
			Controller.UnshadeMainForm();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			object[] args = (object[])e.Argument;

			switch (args[0].ToString())
			{
				case "LoadObjectModel":
					LoadObjectModel();
					break;
				case "AddChildrenToTreeview":
					AddChildrenToTreeview((DevComponents.AdvTree.Node)args[1]);
					break;
				default:
					throw new NotImplementedException("Not handled yet: " + e.Argument.ToString());
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Slyce.Common.Utility.HideMessagePanel(treeList1);
			Cursor = Cursors.Default;

			if (e.Error != null)
			{
				// Ensure that we get this reported to us.
				throw e.Error;
			}
		}

		private void treeList1_BeforeExpand(object sender, DevComponents.AdvTree.AdvTreeNodeCancelEventArgs e)
		{
			if (IsPopulated)
			{
				if (e.Node.Tag == null && !PopulatedCollectionNodes.Contains(e.Node))
				{
					Cursor = Cursors.WaitCursor;
					AddChildrenToTreeview(e.Node);
					Cursor = Cursors.Default;
				}
				else if (e.Node.Tag != null && !PopulatedCollectionNodes.Contains(e.Node))
				{
					Cursor = Cursors.WaitCursor;
					AddPropertiesToTreeview(e.Node);
					Cursor = Cursors.Default;
				}
			}
		}

		private void treeList1_NodeClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
		{
			DevComponents.AdvTree.Node node = e.Node;

			if (node.Tag != null && TypeOfObject.IsInstanceOfType(node.Tag))
			{
				CurrentSelectedNode = node;
			}
		}

		private void gridPrevSelections_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (gridPrevSelections.Rows[e.RowIndex].Tag == null)
			{
				return;
			}
			SelectedObject = gridPrevSelections.Rows[e.RowIndex].Tag;
			this.Visible = false;
			this.Close();
		}
	}
}