//using SchemaDiagrammer;

namespace ArchAngel.Providers.EntityModel.UI
{
	partial class EditModel
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditModel));
			this.advTree1 = new DevComponents.AdvTree.AdvTree();
			this.elementStyle4 = new DevComponents.DotNetBar.ElementStyle();
			this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextMenuNewTable = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuNewBlankEntity = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuNewEntityFromTable = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuNewEntitiesFromManyTables = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuAddReference = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuNewComponent = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuAddComponentToEntity = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuNewRelationship = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuCreate1to1EntityMappings = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuNewDatabaseFromEntities = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuCheckModel = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuShowDBChanges = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRefreshDatabaseSchema = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMergeEntity = new System.Windows.Forms.ToolStripMenuItem();
			this.TreeImages = new System.Windows.Forms.ImageList(this.components);
			this.node1 = new DevComponents.AdvTree.Node();
			this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.grayedStyle = new DevComponents.DotNetBar.ElementStyle();
			this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
			this.textBoxSearch = new System.Windows.Forms.TextBox();
			this.tbddSearchBar = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.dockContainerItem1 = new DevComponents.DotNetBar.DockContainerItem();
			this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
			this.diagrammer = new ArchAngel.Providers.EntityModel.UI.PropertyGrids.EntityForm2();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.expandableSplitter2 = new DevComponents.DotNetBar.ExpandableSplitter();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.panelSearch = new System.Windows.Forms.Panel();
			this.btnClearSearchBox = new System.Windows.Forms.PictureBox();
			this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
			this.validationFailureView1 = new ArchAngel.Providers.EntityModel.UI.ValidationFailureView();
			((System.ComponentModel.ISupportInitialize)(this.advTree1)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			this.panelEx3.SuspendLayout();
			this.panelSearch.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnClearSearchBox)).BeginInit();
			this.panelEx2.SuspendLayout();
			this.SuspendLayout();
			// 
			// advTree1
			// 
			this.advTree1.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
			this.advTree1.AllowDrop = true;
			this.advTree1.BackColor = System.Drawing.Color.Black;
			// 
			// 
			// 
			this.advTree1.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.advTree1.BackgroundStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.advTree1.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.advTree1.BackgroundStyle.Class = "TreeBorderKey";
			this.advTree1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.advTree1.CellStyleMouseOver = this.elementStyle4;
			this.advTree1.CellStyleSelected = this.elementStyle2;
			this.advTree1.ContextMenuStrip = this.contextMenuStrip;
			this.advTree1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.advTree1.DragDropEnabled = false;
			this.advTree1.DragDropNodeCopyEnabled = false;
			this.advTree1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.advTree1.GridLinesColor = System.Drawing.Color.LightGray;
			this.advTree1.HotTracking = true;
			this.advTree1.ImageList = this.TreeImages;
			this.advTree1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.advTree1.Location = new System.Drawing.Point(0, 58);
			this.advTree1.Name = "advTree1";
			this.advTree1.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
			this.advTree1.NodesColumnsBackgroundStyle = this.elementStyle2;
			this.advTree1.NodesConnector = this.nodeConnector1;
			this.advTree1.NodeStyle = this.elementStyle1;
			this.advTree1.NodeStyleMouseOver = this.elementStyle2;
			this.advTree1.NodeStyleSelected = this.elementStyle2;
			this.advTree1.PathSeparator = ";";
			this.advTree1.Size = new System.Drawing.Size(200, 487);
			this.advTree1.Styles.Add(this.elementStyle1);
			this.advTree1.Styles.Add(this.grayedStyle);
			this.advTree1.Styles.Add(this.elementStyle2);
			this.advTree1.Styles.Add(this.elementStyle3);
			this.advTree1.Styles.Add(this.elementStyle4);
			this.advTree1.TabIndex = 0;
			this.advTree1.Text = "advTree1";
			this.advTree1.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.advTree1_AfterNodeSelect);
			// 
			// elementStyle4
			// 
			this.elementStyle4.BackColor = System.Drawing.Color.White;
			this.elementStyle4.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(240)))));
			this.elementStyle4.BackColorGradientAngle = 90;
			this.elementStyle4.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle4.BorderBottomWidth = 1;
			this.elementStyle4.BorderColor = System.Drawing.Color.DarkGray;
			this.elementStyle4.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle4.BorderLeftWidth = 1;
			this.elementStyle4.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle4.BorderRightWidth = 1;
			this.elementStyle4.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle4.BorderTopWidth = 1;
			this.elementStyle4.Class = "";
			this.elementStyle4.CornerDiameter = 4;
			this.elementStyle4.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle4.Description = "Gray";
			this.elementStyle4.Name = "elementStyle4";
			this.elementStyle4.PaddingBottom = 1;
			this.elementStyle4.PaddingLeft = 1;
			this.elementStyle4.PaddingRight = 1;
			this.elementStyle4.PaddingTop = 1;
			this.elementStyle4.TextColor = System.Drawing.Color.Black;
			// 
			// elementStyle2
			// 
			this.elementStyle2.BackColor = System.Drawing.Color.White;
			this.elementStyle2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(240)))));
			this.elementStyle2.BackColorGradientAngle = 90;
			this.elementStyle2.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle2.BorderBottomWidth = 1;
			this.elementStyle2.BorderColor = System.Drawing.Color.DarkGray;
			this.elementStyle2.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle2.BorderLeftWidth = 1;
			this.elementStyle2.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle2.BorderRightWidth = 1;
			this.elementStyle2.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle2.BorderTopWidth = 1;
			this.elementStyle2.Class = "";
			this.elementStyle2.CornerDiameter = 4;
			this.elementStyle2.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle2.Description = "Gray";
			this.elementStyle2.Name = "elementStyle2";
			this.elementStyle2.PaddingBottom = 1;
			this.elementStyle2.PaddingLeft = 1;
			this.elementStyle2.PaddingRight = 1;
			this.elementStyle2.PaddingTop = 1;
			this.elementStyle2.TextColor = System.Drawing.Color.Black;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuNewTable,
            this.contextMenuNewBlankEntity,
            this.contextMenuNewEntityFromTable,
            this.contextMenuNewEntitiesFromManyTables,
            this.contextMenuDelete,
            this.contextMenuAddReference,
            this.contextMenuNewComponent,
            this.contextMenuAddComponentToEntity,
            this.contextMenuNewRelationship,
            this.contextMenuCreate1to1EntityMappings,
            this.contextMenuNewDatabaseFromEntities,
            this.contextMenuCheckModel,
            this.contextMenuShowDBChanges,
            this.mnuRefreshDatabaseSchema,
            this.mnuMergeEntity});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(254, 356);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// contextMenuNewTable
			// 
			this.contextMenuNewTable.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuNewTable.Image")));
			this.contextMenuNewTable.Name = "contextMenuNewTable";
			this.contextMenuNewTable.Size = new System.Drawing.Size(253, 22);
			this.contextMenuNewTable.Text = "New table";
			this.contextMenuNewTable.Click += new System.EventHandler(this.contextMenuNewTable_Click);
			// 
			// contextMenuNewBlankEntity
			// 
			this.contextMenuNewBlankEntity.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuNewBlankEntity.Image")));
			this.contextMenuNewBlankEntity.Name = "contextMenuNewBlankEntity";
			this.contextMenuNewBlankEntity.Size = new System.Drawing.Size(253, 22);
			this.contextMenuNewBlankEntity.Text = "New blank entity";
			this.contextMenuNewBlankEntity.Click += new System.EventHandler(this.contextMenuNewBlankEntity_Click);
			// 
			// contextMenuNewEntityFromTable
			// 
			this.contextMenuNewEntityFromTable.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuNewEntityFromTable.Image")));
			this.contextMenuNewEntityFromTable.Name = "contextMenuNewEntityFromTable";
			this.contextMenuNewEntityFromTable.Size = new System.Drawing.Size(253, 22);
			this.contextMenuNewEntityFromTable.Text = "New entity from this table";
			this.contextMenuNewEntityFromTable.Click += new System.EventHandler(this.contextMenuNewEntityFromTable_Click);
			// 
			// contextMenuNewEntitiesFromManyTables
			// 
			this.contextMenuNewEntitiesFromManyTables.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuNewEntitiesFromManyTables.Image")));
			this.contextMenuNewEntitiesFromManyTables.Name = "contextMenuNewEntitiesFromManyTables";
			this.contextMenuNewEntitiesFromManyTables.Size = new System.Drawing.Size(253, 22);
			this.contextMenuNewEntitiesFromManyTables.Text = "New entities from many tables";
			this.contextMenuNewEntitiesFromManyTables.Click += new System.EventHandler(this.contextMenuNewEntitiesFromManyTables_Click);
			// 
			// contextMenuDelete
			// 
			this.contextMenuDelete.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuDelete.Image")));
			this.contextMenuDelete.Name = "contextMenuDelete";
			this.contextMenuDelete.Size = new System.Drawing.Size(253, 22);
			this.contextMenuDelete.Text = "Delete";
			this.contextMenuDelete.Click += new System.EventHandler(this.contextMenuDelete_Click);
			// 
			// contextMenuAddReference
			// 
			this.contextMenuAddReference.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuAddReference.Image")));
			this.contextMenuAddReference.Name = "contextMenuAddReference";
			this.contextMenuAddReference.Size = new System.Drawing.Size(253, 22);
			this.contextMenuAddReference.Text = "Add reference";
			this.contextMenuAddReference.Click += new System.EventHandler(this.contextMenuAddReference_Click);
			// 
			// contextMenuNewComponent
			// 
			this.contextMenuNewComponent.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuNewComponent.Image")));
			this.contextMenuNewComponent.Name = "contextMenuNewComponent";
			this.contextMenuNewComponent.Size = new System.Drawing.Size(253, 22);
			this.contextMenuNewComponent.Text = "New component";
			this.contextMenuNewComponent.Click += new System.EventHandler(this.contextMenuNewComponent_Click);
			// 
			// contextMenuAddComponentToEntity
			// 
			this.contextMenuAddComponentToEntity.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuAddComponentToEntity.Image")));
			this.contextMenuAddComponentToEntity.Name = "contextMenuAddComponentToEntity";
			this.contextMenuAddComponentToEntity.Size = new System.Drawing.Size(253, 22);
			this.contextMenuAddComponentToEntity.Text = "Add component to entity";
			this.contextMenuAddComponentToEntity.Click += new System.EventHandler(this.contextMenuAddComponentToEntity_Click);
			// 
			// contextMenuNewRelationship
			// 
			this.contextMenuNewRelationship.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuNewRelationship.Image")));
			this.contextMenuNewRelationship.Name = "contextMenuNewRelationship";
			this.contextMenuNewRelationship.Size = new System.Drawing.Size(253, 22);
			this.contextMenuNewRelationship.Text = "New relationship";
			this.contextMenuNewRelationship.Click += new System.EventHandler(this.contextMenuNewRelationship_Click);
			// 
			// contextMenuCreate1to1EntityMappings
			// 
			this.contextMenuCreate1to1EntityMappings.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuCreate1to1EntityMappings.Image")));
			this.contextMenuCreate1to1EntityMappings.Name = "contextMenuCreate1to1EntityMappings";
			this.contextMenuCreate1to1EntityMappings.Size = new System.Drawing.Size(253, 22);
			this.contextMenuCreate1to1EntityMappings.Text = "Create 1:1 entity mapping";
			this.contextMenuCreate1to1EntityMappings.Click += new System.EventHandler(this.create11EntityMappingToolStripMenuItem_Click);
			// 
			// contextMenuNewDatabaseFromEntities
			// 
			this.contextMenuNewDatabaseFromEntities.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuNewDatabaseFromEntities.Image")));
			this.contextMenuNewDatabaseFromEntities.Name = "contextMenuNewDatabaseFromEntities";
			this.contextMenuNewDatabaseFromEntities.Size = new System.Drawing.Size(253, 22);
			this.contextMenuNewDatabaseFromEntities.Text = "Create new database from entities";
			this.contextMenuNewDatabaseFromEntities.Click += new System.EventHandler(this.contextMenuNewDatabaseFromEntities_Click);
			// 
			// contextMenuCheckModel
			// 
			this.contextMenuCheckModel.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuCheckModel.Image")));
			this.contextMenuCheckModel.Name = "contextMenuCheckModel";
			this.contextMenuCheckModel.Size = new System.Drawing.Size(253, 22);
			this.contextMenuCheckModel.Text = "Check model";
			this.contextMenuCheckModel.Click += new System.EventHandler(this.contextMenuCheckModel_Click);
			// 
			// contextMenuShowDBChanges
			// 
			this.contextMenuShowDBChanges.Image = ((System.Drawing.Image)(resources.GetObject("contextMenuShowDBChanges.Image")));
			this.contextMenuShowDBChanges.Name = "contextMenuShowDBChanges";
			this.contextMenuShowDBChanges.Size = new System.Drawing.Size(253, 22);
			this.contextMenuShowDBChanges.Text = "Show DB changes";
			this.contextMenuShowDBChanges.Click += new System.EventHandler(this.contextMenuShowDBChanges_Click);
			// 
			// mnuRefreshDatabaseSchema
			// 
			this.mnuRefreshDatabaseSchema.Image = ((System.Drawing.Image)(resources.GetObject("mnuRefreshDatabaseSchema.Image")));
			this.mnuRefreshDatabaseSchema.Name = "mnuRefreshDatabaseSchema";
			this.mnuRefreshDatabaseSchema.Size = new System.Drawing.Size(253, 22);
			this.mnuRefreshDatabaseSchema.Text = "Refresh database schema";
			this.mnuRefreshDatabaseSchema.Click += new System.EventHandler(this.mnuRefreshDatabaseSchema_Click);
			// 
			// mnuMergeEntity
			// 
			this.mnuMergeEntity.Image = ((System.Drawing.Image)(resources.GetObject("mnuMergeEntity.Image")));
			this.mnuMergeEntity.Name = "mnuMergeEntity";
			this.mnuMergeEntity.Size = new System.Drawing.Size(253, 22);
			this.mnuMergeEntity.Text = "Merge into X";
			// 
			// TreeImages
			// 
			this.TreeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeImages.ImageStream")));
			this.TreeImages.TransparentColor = System.Drawing.Color.Magenta;
			this.TreeImages.Images.SetKeyName(0, "db16.png");
			this.TreeImages.Images.SetKeyName(1, "db_register_16.png");
			this.TreeImages.Images.SetKeyName(2, "TableHS.png");
			this.TreeImages.Images.SetKeyName(3, "sql_script_new16.png");
			this.TreeImages.Images.SetKeyName(4, "print_preview_16.png");
			this.TreeImages.Images.SetKeyName(5, "RadialChartHS.png");
			this.TreeImages.Images.SetKeyName(6, "indexes16.png");
			this.TreeImages.Images.SetKeyName(7, "PrimaryKeyHS.png");
			this.TreeImages.Images.SetKeyName(8, "square_light_blue_16.png");
			this.TreeImages.Images.SetKeyName(9, "VSObject_Field.bmp");
			this.TreeImages.Images.SetKeyName(10, "folder_closed_16.png");
			this.TreeImages.Images.SetKeyName(11, "folder_open_16.png");
			// 
			// node1
			// 
			this.node1.Expanded = true;
			this.node1.Name = "node1";
			this.node1.Text = "node1";
			// 
			// nodeConnector1
			// 
			this.nodeConnector1.LineColor = System.Drawing.Color.LightGray;
			// 
			// elementStyle1
			// 
			this.elementStyle1.Class = "";
			this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle1.Name = "elementStyle1";
			this.elementStyle1.TextColor = System.Drawing.Color.White;
			// 
			// grayedStyle
			// 
			this.grayedStyle.Class = "";
			this.grayedStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.grayedStyle.Name = "grayedStyle";
			this.grayedStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(195)))), ((int)(((byte)(198)))));
			// 
			// elementStyle3
			// 
			this.elementStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
			this.elementStyle3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(168)))), ((int)(((byte)(228)))));
			this.elementStyle3.BackColorGradientAngle = 90;
			this.elementStyle3.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderBottomWidth = 1;
			this.elementStyle3.BorderColor = System.Drawing.Color.DarkGray;
			this.elementStyle3.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderLeftWidth = 1;
			this.elementStyle3.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderRightWidth = 1;
			this.elementStyle3.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderTopWidth = 1;
			this.elementStyle3.Class = "";
			this.elementStyle3.CornerDiameter = 4;
			this.elementStyle3.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle3.Description = "Blue";
			this.elementStyle3.Name = "elementStyle3";
			this.elementStyle3.PaddingBottom = 1;
			this.elementStyle3.PaddingLeft = 1;
			this.elementStyle3.PaddingRight = 1;
			this.elementStyle3.PaddingTop = 1;
			this.elementStyle3.TextColor = System.Drawing.Color.Black;
			// 
			// textBoxSearch
			// 
			this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSearch.Location = new System.Drawing.Point(4, 4);
			this.textBoxSearch.Name = "textBoxSearch";
			this.textBoxSearch.Size = new System.Drawing.Size(171, 20);
			this.textBoxSearch.TabIndex = 3;
			this.textBoxSearch.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// tbddSearchBar
			// 
			this.tbddSearchBar.DisplayMember = "Text";
			this.tbddSearchBar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.tbddSearchBar.FormattingEnabled = true;
			this.tbddSearchBar.ItemHeight = 14;
			this.tbddSearchBar.Location = new System.Drawing.Point(4, 4);
			this.tbddSearchBar.Name = "tbddSearchBar";
			this.tbddSearchBar.Size = new System.Drawing.Size(74, 20);
			this.tbddSearchBar.TabIndex = 2;
			this.tbddSearchBar.Visible = false;
			this.tbddSearchBar.TextChanged += new System.EventHandler(this.tbddSearchBar_TextChanged);
			// 
			// dockContainerItem1
			// 
			this.dockContainerItem1.Name = "dockContainerItem1";
			this.dockContainerItem1.Text = "dockContainerItem1";
			// 
			// panelEx3
			// 
			this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx3.Controls.Add(this.diagrammer);
			this.panelEx3.Controls.Add(this.labelX1);
			this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx3.Location = new System.Drawing.Point(206, 0);
			this.panelEx3.Name = "panelEx3";
			this.panelEx3.Size = new System.Drawing.Size(808, 545);
			this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx3.Style.BackColor1.Color = System.Drawing.Color.Black;
			this.panelEx3.Style.BackColor2.Color = System.Drawing.Color.Black;
			this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx3.Style.GradientAngle = 90;
			this.panelEx3.TabIndex = 2;
			// 
			// diagrammer
			// 
			this.diagrammer.BackColor = System.Drawing.Color.Black;
			this.diagrammer.ComponentSpecification = null;
			this.diagrammer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.diagrammer.Entity = null;
			this.diagrammer.Location = new System.Drawing.Point(0, 30);
			this.diagrammer.Name = "diagrammer";
			this.diagrammer.Size = new System.Drawing.Size(808, 515);
			this.diagrammer.TabIndex = 1;
			this.diagrammer.Table = null;
			// 
			// labelX1
			// 
			this.labelX1.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			this.labelX1.BackgroundStyle.BackColor2 = System.Drawing.Color.Black;
			this.labelX1.BackgroundStyle.BackColorGradientAngle = 90;
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelX1.Location = new System.Drawing.Point(0, 0);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(808, 30);
			this.labelX1.TabIndex = 8;
			// 
			// expandableSplitter2
			// 
			this.expandableSplitter2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
			this.expandableSplitter2.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter2.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandableSplitter2.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
			this.expandableSplitter2.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter2.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter2.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter2.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter2.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter2.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
			this.expandableSplitter2.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter2.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
			this.expandableSplitter2.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
			this.expandableSplitter2.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
			this.expandableSplitter2.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
			this.expandableSplitter2.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
			this.expandableSplitter2.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter2.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter2.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter2.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
			this.expandableSplitter2.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter2.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
			this.expandableSplitter2.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter2.Location = new System.Drawing.Point(200, 0);
			this.expandableSplitter2.Name = "expandableSplitter2";
			this.expandableSplitter2.Size = new System.Drawing.Size(6, 545);
			this.expandableSplitter2.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
			this.expandableSplitter2.TabIndex = 0;
			this.expandableSplitter2.TabStop = false;
			// 
			// labelX2
			// 
			this.labelX2.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			this.labelX2.BackgroundStyle.BackColor2 = System.Drawing.Color.Black;
			this.labelX2.BackgroundStyle.BackColorGradientAngle = 90;
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelX2.Location = new System.Drawing.Point(0, 0);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(200, 30);
			this.labelX2.TabIndex = 8;
			// 
			// panelSearch
			// 
			this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
			this.panelSearch.Controls.Add(this.btnClearSearchBox);
			this.panelSearch.Controls.Add(this.textBoxSearch);
			this.panelSearch.Controls.Add(this.tbddSearchBar);
			this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelSearch.Location = new System.Drawing.Point(0, 30);
			this.panelSearch.Name = "panelSearch";
			this.panelSearch.Size = new System.Drawing.Size(200, 28);
			this.panelSearch.TabIndex = 0;
			// 
			// btnClearSearchBox
			// 
			this.btnClearSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClearSearchBox.Image = ((System.Drawing.Image)(resources.GetObject("btnClearSearchBox.Image")));
			this.btnClearSearchBox.Location = new System.Drawing.Point(181, 6);
			this.btnClearSearchBox.Name = "btnClearSearchBox";
			this.btnClearSearchBox.Size = new System.Drawing.Size(16, 16);
			this.btnClearSearchBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.btnClearSearchBox.TabIndex = 4;
			this.btnClearSearchBox.TabStop = false;
			this.btnClearSearchBox.Click += new System.EventHandler(this.btnClearSearchBox_Click);
			// 
			// panelEx2
			// 
			this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx2.Controls.Add(this.advTree1);
			this.panelEx2.Controls.Add(this.panelSearch);
			this.panelEx2.Controls.Add(this.labelX2);
			this.panelEx2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelEx2.Location = new System.Drawing.Point(0, 0);
			this.panelEx2.Name = "panelEx2";
			this.panelEx2.Size = new System.Drawing.Size(200, 545);
			this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx2.Style.GradientAngle = 90;
			this.panelEx2.TabIndex = 0;
			this.panelEx2.Text = "panelEx2";
			// 
			// validationFailureView1
			// 
			this.validationFailureView1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.validationFailureView1.Location = new System.Drawing.Point(0, 545);
			this.validationFailureView1.Name = "validationFailureView1";
			this.validationFailureView1.Size = new System.Drawing.Size(1014, 232);
			this.validationFailureView1.TabIndex = 0;
			this.validationFailureView1.Visible = false;
			// 
			// EditModel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelEx3);
			this.Controls.Add(this.expandableSplitter2);
			this.Controls.Add(this.panelEx2);
			this.Controls.Add(this.validationFailureView1);
			this.Name = "EditModel";
			this.Size = new System.Drawing.Size(1014, 777);
			((System.ComponentModel.ISupportInitialize)(this.advTree1)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			this.panelEx3.ResumeLayout(false);
			this.panelSearch.ResumeLayout(false);
			this.panelSearch.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnClearSearchBox)).EndInit();
			this.panelEx2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.AdvTree.AdvTree advTree1;
		private DevComponents.AdvTree.Node node1;
		private DevComponents.AdvTree.NodeConnector nodeConnector1;
		private DevComponents.DotNetBar.ElementStyle elementStyle1;
		//private MainWindow mainWindow1;
		private DevComponents.DotNetBar.Controls.ComboBoxEx tbddSearchBar;
		private DevComponents.DotNetBar.ElementStyle grayedStyle;
		private System.Windows.Forms.ImageList TreeImages;
		private DevComponents.DotNetBar.DockContainerItem dockContainerItem1;
		private ValidationFailureView validationFailureView1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem contextMenuNewTable;
		private System.Windows.Forms.ToolStripMenuItem contextMenuNewBlankEntity;
		private System.Windows.Forms.ToolStripMenuItem contextMenuNewEntityFromTable;
		private System.Windows.Forms.ToolStripMenuItem contextMenuNewEntitiesFromManyTables;
		private System.Windows.Forms.ToolStripMenuItem contextMenuDelete;
		private System.Windows.Forms.ToolStripMenuItem contextMenuAddReference;
		private System.Windows.Forms.ToolStripMenuItem contextMenuNewComponent;
		private System.Windows.Forms.ToolStripMenuItem contextMenuAddComponentToEntity;
		private System.Windows.Forms.ToolStripMenuItem contextMenuNewRelationship;
		private System.Windows.Forms.ToolStripMenuItem contextMenuCreate1to1EntityMappings;
		private ArchAngel.Providers.EntityModel.UI.PropertyGrids.EntityForm2 diagrammer;
		private DevComponents.DotNetBar.ElementStyle elementStyle2;
		private System.Windows.Forms.ToolStripMenuItem contextMenuNewDatabaseFromEntities;
		private DevComponents.DotNetBar.PanelEx panelEx3;
		private DevComponents.DotNetBar.ElementStyle elementStyle4;
		private DevComponents.DotNetBar.ElementStyle elementStyle3;
		private System.Windows.Forms.ToolStripMenuItem contextMenuCheckModel;
		private System.Windows.Forms.TextBox textBoxSearch;
		private System.Windows.Forms.ToolStripMenuItem contextMenuShowDBChanges;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter2;
		private DevComponents.DotNetBar.LabelX labelX2;
		private System.Windows.Forms.Panel panelSearch;
		private DevComponents.DotNetBar.PanelEx panelEx2;
		private System.Windows.Forms.PictureBox btnClearSearchBox;
		private System.Windows.Forms.ToolStripMenuItem mnuRefreshDatabaseSchema;
		private System.Windows.Forms.ToolStripMenuItem mnuMergeEntity;
	}
}
