using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ActiproSoftware.Drawing;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a control that displays two drop-down lists, one for .NET types and another for .NET members within the selected type.
	/// </summary>
	[
		ToolboxBitmap(typeof(TypeMemberDropDownList)),
		ToolboxItem(true)
	]
	public class TypeMemberDropDownList : Control {

		private int				contextOffset;
		private bool			ignoreMemberChange;
		private bool			ignoreTypeChange;
		private ImageList		imageList;
		private ComboBox		memberDropDownList;
		private int				oldContextOffset;
		private SyntaxEditor	syntaxEditor;
		private ComboBox		typeDropDownList;

		private const int Space = 2;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the value of the <see cref="SelectedMember"/> property is changed.
		/// </summary>
		/// <eventdata>
		/// The event handler receives an argument of type <c>EventArgs</c> containing data related to this event.
		/// </eventdata>
		/// <remarks>This event does not need to be handled if the <see cref="SyntaxEditor"/> property is set.</remarks>
		[
			Category("Action"),
			Description("Occurs when the value of the SelectedMember property is changed.")
		]
		public event EventHandler SelectedMemberChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="SelectedType"/> property is changed.
		/// </summary>
		/// <eventdata>
		/// The event handler receives an argument of type <c>EventArgs</c> containing data related to this event.
		/// </eventdata>
		/// <remarks>This event does not need to be handled if the <see cref="SyntaxEditor"/> property is set.</remarks>
		[
			Category("Action"),
			Description("Occurs when the value of the SelectedType property is changed.")
		]
		public event EventHandler SelectedTypeChanged;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INNER TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Stores data about an AST node.
		/// </summary>
		internal class AstNodeItem : IComparable {

			private AstNode		node;
			private string		text;
			
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// OBJECT
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			
			/// <summary>
			/// Initializes a new instance of the <c>AstNodeItem</c> class. 
			/// </summary>
			/// <param name="node">The <see cref="AstNode"/> that is wrapped by this item.</param>
			/// <param name="text">The text for the item.</param>
			internal AstNodeItem(AstNode node, string text) {
				this.node = node;
				this.text = text;
			}
				
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// INTERFACE IMPLEMENTATION
			/////////////////////////////////////////////////////////////////////////////////////////////////////
				
			/// <summary>
			/// Compares the current instance with another object of the same type.
			/// </summary>
			/// <param name="obj">An object to compare with this instance.</param>
			/// <returns>A 32-bit signed integer that indicates the relative order of the comparands.</returns>
			int IComparable.CompareTo(object obj) {
				return ((AstNodeItem)this).Text.CompareTo(((AstNodeItem)obj).Text);
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////
				
			/// <summary>
			/// Gets the key of the node.
			/// </summary>
			/// <value>The key of the node.</value>
			public string Key {
				get {
					return node.Key;
				}
			}

			/// <summary>
			/// Gets the <see cref="AstNode"/> that is wrapped by this item.
			/// </summary>
			/// <value>The <see cref="AstNode"/> that is wrapped by this item.</value>
			public AstNode Node {
				get {
					return node;
				}
			}

			/// <summary>
			/// Gets the text representing the node.
			/// </summary>
			/// <value>The text representing the node.</value>
			public string Text {
				get {
					return text;
				}
			}
				
			/// <summary>
			/// Creates and returns a string representation of the current object.
			/// </summary>
			/// <returns>A string representation of the current object.</returns>
			public override string ToString() {
				return text;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>TypeMemberDropDownList</c> class.
		/// </summary>
		public TypeMemberDropDownList() {
			// Get the imagelist
			imageList = SyntaxEditor.ReflectionImageList;

			// Create the type drop-down list
			typeDropDownList = new ComboBox();
			typeDropDownList.DisplayMember = "Text";
			typeDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
			typeDropDownList.DrawMode = DrawMode.OwnerDrawFixed;
			typeDropDownList.ItemHeight = 16;
			typeDropDownList.MaxDropDownItems = 30;
			typeDropDownList.Sorted = true;
			typeDropDownList.TabIndex = 1;
			typeDropDownList.TabStop = false;
			typeDropDownList.ValueMember = "Key";
			typeDropDownList.DrawItem += new DrawItemEventHandler(typeDropDownList_DrawItem);
			#if NET11
			typeDropDownList.SelectedIndexChanged += new EventHandler(typeDropDownList_SelectedIndexChanged);
			#else
			typeDropDownList.SelectionChangeCommitted += new EventHandler(typeDropDownList_SelectionChangeCommitted);
			#endif

			// Create the member drop-down list
			memberDropDownList = new ComboBox();
			memberDropDownList.DisplayMember = "Text";
			memberDropDownList.DropDownStyle = ComboBoxStyle.DropDownList;
			memberDropDownList.DrawMode = DrawMode.OwnerDrawFixed;
			memberDropDownList.ItemHeight = 16;
			memberDropDownList.MaxDropDownItems = 30;
			memberDropDownList.Sorted = true;
			memberDropDownList.TabIndex = 1;
			memberDropDownList.TabStop = false;
			memberDropDownList.ValueMember = "Key";
			memberDropDownList.DrawItem += new DrawItemEventHandler(memberDropDownList_DrawItem);
			#if NET11
			memberDropDownList.SelectedIndexChanged += new EventHandler(memberDropDownList_SelectedIndexChanged);
			#else
			memberDropDownList.SelectionChangeCommitted += new EventHandler(memberDropDownList_SelectionChangeCommitted);
			#endif
			
			typeDropDownList.Parent = this;
			memberDropDownList.Parent = this;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENT HANDLERS
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when an item needs to be drawn.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">A <c>DrawItemEventArgs</c> that contains the event data.</param>
		private void memberDropDownList_DrawItem(object sender, DrawItemEventArgs e) {
			this.DrawItem(e, memberDropDownList);
		}
		
		/// <summary>
		/// Occurs when the selected index is changed.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		private void memberDropDownList_SelectedIndexChanged(object sender, EventArgs e) {
			memberDropDownList_SelectionChangeCommitted(sender, e);
		}
		
		/// <summary>
		/// Occurs when a selected change is committed.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		private void memberDropDownList_SelectionChangeCommitted(object sender, EventArgs e) {
			if (!ignoreMemberChange) {
				if (syntaxEditor != null) {
					// Navigate to the selected member
					if (this.SelectedMember != null) {
						syntaxEditor.Caret.Offset = this.SelectedMember.NavigationOffset;
						syntaxEditor.SelectedView.EnsureVisible(syntaxEditor.Caret.Offset, true);
					}
					syntaxEditor.Focus();
				}

				// Raise an event
				this.OnSelectedMemberChanged(EventArgs.Empty);
			}
		}
		
		/// <summary>
		/// Occurs when the document changes.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void syntaxEditor_DocumentChanged(object sender, EventArgs e) {
			// Update the type/member drop down list 
			if ((syntaxEditor != null) && (syntaxEditor.Document != null))
				this.BindToCompilationUnit(syntaxEditor.Document.SemanticParseData as CompilationUnit, syntaxEditor.Caret.Offset);
		}
		
		/// <summary>
		/// Occurs when the semantic parser data changes.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void syntaxEditor_DocumentSemanticParseDataChanged(object sender, EventArgs e) {
			// Update the type/member drop down list 
			if ((syntaxEditor != null) && (syntaxEditor.Document != null))
				this.BindToCompilationUnit(syntaxEditor.Document.SemanticParseData as CompilationUnit, syntaxEditor.Caret.Offset);
		}
		
		/// <summary>
		/// Occurs when the document's syntax language changes.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void syntaxEditor_DocumentSyntaxLanguageLoaded(object sender, SyntaxLanguageEventArgs e) {
			// Update the type/member drop down list 
			if ((syntaxEditor != null) && (syntaxEditor.Document != null))
				this.BindToCompilationUnit(syntaxEditor.Document.SemanticParseData as CompilationUnit, syntaxEditor.Caret.Offset);
		}
		
		/// <summary>
		/// Occurs when the selection changes.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void syntaxEditor_SelectionChanged(object sender, SelectionEventArgs e) {
			// Update the selection in the type member drop-down list
			this.UpdateSelection(syntaxEditor.Document.SemanticParseData as CompilationUnit, syntaxEditor.Caret.Offset);
		}

		/// <summary>
		/// Occurs when an item needs to be drawn.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">A <c>DrawItemEventArgs</c> that contains the event data.</param>
		private void typeDropDownList_DrawItem(object sender, DrawItemEventArgs e) {
			this.DrawItem(e, typeDropDownList);
		}
		
		/// <summary>
		/// Occurs when the selected index is changed.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		private void typeDropDownList_SelectedIndexChanged(object sender, EventArgs e) {
			typeDropDownList_SelectionChangeCommitted(sender, e);
		}
		
		/// <summary>
		/// Occurs when a selected change is committed.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		private void typeDropDownList_SelectionChangeCommitted(object sender, EventArgs e) {
			if (!ignoreTypeChange) {
				// Update the member drop-down
				this.BindToType((typeDropDownList.SelectedIndex != -1 ? ((AstNodeItem)typeDropDownList.SelectedItem).Node as TypeDeclaration : null), null);

				if (syntaxEditor != null) {
					// Navigate to the selected type
					if (this.SelectedType != null) {
						syntaxEditor.Caret.Offset = this.SelectedType.NavigationOffset;
						syntaxEditor.SelectedView.EnsureVisible(syntaxEditor.Caret.Offset, true);
					}
					syntaxEditor.Focus();
				}

				// Raise an event
				this.OnSelectedTypeChanged(EventArgs.Empty);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Rebinds the member drop-down to the specified <see cref="TypeDeclaration"/>.
		/// </summary>
		/// <param name="typeDeclaration">The <see cref="TypeDeclaration"/> to examine.</param>
		/// <param name="contextNode">The context <see cref="AstNode"/> to select.</param>
		private void BindToType(TypeDeclaration typeDeclaration, AstNode contextNode) {
			// Get the language type
			DotNetLanguage languageType = DotNetLanguage.CSharp;
			CompilationUnit compilationUnit = null;
			if (typeDeclaration != null) {
				compilationUnit = typeDeclaration.FindAncestor(typeof(CompilationUnit)) as CompilationUnit;
				if (compilationUnit != null)
					languageType = compilationUnit.SourceLanguage;
			}

			// Begin update
			memberDropDownList.BeginUpdate();

			memberDropDownList.Items.Clear();
			if (typeDeclaration != null) {
				// Add the non-type members
				foreach (TypeMemberDeclaration memberDeclaration in typeDeclaration.Members) {
					if ((memberDeclaration.HasStartOffset) && (!(memberDeclaration is TypeDeclaration))) {
						if (memberDeclaration is IVariableDeclarationSection) {
							foreach (IAstNode declarator in ((IVariableDeclarationSection)memberDeclaration).Variables) {
								if ((declarator.HasStartOffset) && (declarator is AstNode))
									memberDropDownList.Items.Add(new AstNodeItem((AstNode)declarator, ((AstNode)declarator).GetDisplayText(languageType, DisplayTextDetailLevel.Simple)));
							}
						}
						else
							memberDropDownList.Items.Add(new AstNodeItem(memberDeclaration, memberDeclaration.GetDisplayText(languageType, DisplayTextDetailLevel.Simple)));
					}
				}
			}
			memberDropDownList.DropDownWidth = Math.Max(16, Math.Max(memberDropDownList.Width, this.IdealWidthForMembers));

			// Select a member list item
			this.UpdateMemberSelection(contextNode);

			// End update
			memberDropDownList.EndUpdate();
		}

		/// <summary>
		/// Draws an item.
		/// </summary>
		/// <param name="e">A <c>DrawItemEventArgs</c> that contains the event data.</param>
		/// <param name="comboBox">The <see cref="ComboBox"/> being drawn.</param>
		private void DrawItem(DrawItemEventArgs e, ComboBox comboBox) {
			bool isFocused = ((e.State & DrawItemState.Focus) == DrawItemState.Focus);

			// Draw the background
			SimpleBorder.Draw(e.Graphics, new Rectangle(e.Bounds.Left + 16, e.Bounds.Top, e.Bounds.Width - 16, e.Bounds.Height), SimpleBorderStyle.Solid, SystemColors.Window);
			SolidColorBackgroundFill.Draw(e.Graphics, new Rectangle(e.Bounds.Left + 17, e.Bounds.Top + 1, e.Bounds.Width - 18, e.Bounds.Height - 2), 
				(isFocused ? SystemColors.Highlight : SystemColors.Window));
			if (e.Index == -1)
				return;
			
			// Draw the focus rectangle
			if (isFocused)
				ControlPaint.DrawFocusRectangle(e.Graphics, new Rectangle(e.Bounds.Left + 16, e.Bounds.Top, e.Bounds.Width - 16, e.Bounds.Height));

			AstNodeItem nodeItem = comboBox.Items[e.Index] as AstNodeItem;

			// Draw the icon
			SolidColorBackgroundFill.Draw(e.Graphics, new Rectangle(e.Bounds.Left, e.Bounds.Top, 16, e.Bounds.Height), SystemColors.Window);
			DrawingHelper.DrawImage(e.Graphics, imageList.Images[nodeItem.Node.ImageIndex], e.Bounds.Left, e.Bounds.Top, 1.0f, RotateFlipType.RotateNoneFlipNone);

			// Draw the text
			StringFormat format = DrawingHelper.GetStringFormat(StringAlignment.Near, StringAlignment.Center, StringTrimming.Character, false, false);
			DrawingHelper.DrawString(e.Graphics, nodeItem.Text, comboBox.Font, 
				(isFocused ? SystemColors.HighlightText : (((e.State & DrawItemState.ComboBoxEdit) != DrawItemState.ComboBoxEdit) ||
					(nodeItem.Node.IntersectsWith(contextOffset)) ? SystemColors.ControlText : SystemColors.GrayText)), 
				new Rectangle(e.Bounds.Left + 18, e.Bounds.Top, e.Bounds.Width - 19, e.Bounds.Height), format);
			format.Dispose();
		}

		/// <summary>
		/// Gets the ideal width for the list based on the current items.
		/// </summary>
		/// <value>The ideal width for the list based on the current items.</value>
		private int IdealWidthForMembers {
			get {
				int width = 0;

				// Get a Graphics
				Graphics g = this.CreateGraphics();
				StringFormat format = DrawingHelper.GetStringFormat(StringAlignment.Near, StringAlignment.Center, StringTrimming.Character, false, false);

				// Loop through each item and measure it
				foreach (AstNodeItem nodeItem in memberDropDownList.Items)
					width = Math.Max(width, DrawingHelper.MeasureString(g, nodeItem.Text, memberDropDownList.Font, format).Width);

				// Dispose the Graphics
				format.Dispose();
				g.Dispose();

				// Add on spacers
				width += 22;

				return width;
			}
		}

		/// <summary>
		/// Gets the ideal width for the list based on the current items.
		/// </summary>
		/// <value>The ideal width for the list based on the current items.</value>
		private int IdealWidthForTypes {
			get {
				int width = 0;

				// Get a Graphics
				Graphics g = this.CreateGraphics();
				StringFormat format = DrawingHelper.GetStringFormat(StringAlignment.Near, StringAlignment.Center, StringTrimming.Character, false, false);

				// Loop through each item and measure it
				foreach (AstNodeItem nodeItem in typeDropDownList.Items)
					width = Math.Max(width, DrawingHelper.MeasureString(g, nodeItem.Text, typeDropDownList.Font, format).Width);

				// Dispose the Graphics
				format.Dispose();
				g.Dispose();

				// Add on spacers
				width += 22;

				return width;
			}
		}

		/// <summary>
		/// Updates the member drop-down list selection.
		/// </summary>
		/// <param name="contextNode">The context <see cref="AstNode"/> to select.</param>
		private void UpdateMemberSelection(AstNode contextNode) {
			// Select a member list item
			if (memberDropDownList.Items.Count > 0) {
				// Get whether the current member selection contains the context offset
				bool didIntersectWithContextOffset = (memberDropDownList.SelectedIndex != -1 ? ((AstNodeItem)memberDropDownList.SelectedItem).Node.IntersectsWith(oldContextOffset) : false);

				// Change the member drop-down list's selected item
				bool memberSelectionChanged = false;
				ignoreMemberChange = true;
				try {
					if (contextNode != null) {
						string key = contextNode.Key;
						foreach (AstNodeItem nodeItem in memberDropDownList.Items) {
							if (nodeItem.Key == key) {
								if (memberDropDownList.SelectedItem != nodeItem) {
									memberDropDownList.SelectedItem = nodeItem;
									memberSelectionChanged = true;
								}
								break;
							}
						}
					}
					if (memberDropDownList.SelectedIndex == -1) {
						memberDropDownList.SelectedIndex = 0;
						memberSelectionChanged = true;
					}
				}
				finally {
					ignoreMemberChange = false;
				}

				if (!memberSelectionChanged) {
					// Repaint the type list if the node now intersects with the context offset
					if (didIntersectWithContextOffset != ((AstNodeItem)memberDropDownList.SelectedItem).Node.IntersectsWith(contextOffset))
						memberDropDownList.Invalidate();
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Rebinds the drop-downs to the specified <see cref="CompilationUnit"/>.
		/// </summary>
		/// <param name="compilationUnit">The <see cref="CompilationUnit"/> to examine.</param>
		/// <param name="contextOffset">The offset at which the caret is located.</param>
		/// <remarks>This method does not need to be called if the <see cref="SyntaxEditor"/> property is set.</remarks>
		public void BindToCompilationUnit(CompilationUnit compilationUnit, int contextOffset) {
			if (compilationUnit == null) {
				// Clear the drop-downs
				typeDropDownList.DropDownWidth = Math.Max(16, typeDropDownList.Width);
				typeDropDownList.Items.Clear();
				memberDropDownList.DropDownWidth = Math.Max(16, memberDropDownList.Width);
				memberDropDownList.Items.Clear();
				return;
			}

			// Begin update
			typeDropDownList.BeginUpdate();

			// Rebind the type list
			typeDropDownList.Items.Clear();
			foreach (AstNode node in compilationUnit.Types)
				typeDropDownList.Items.Add(new AstNodeItem(node, (node != null ? node.GetDisplayText(compilationUnit.SourceLanguage, DisplayTextDetailLevel.SimpleFullyQualified) : null)));
			typeDropDownList.DropDownWidth = Math.Max(16, Math.Max(typeDropDownList.Width, this.IdealWidthForTypes));

			// Clear the member list
			memberDropDownList.Items.Clear();

			// Update the selection
			this.UpdateSelection(compilationUnit, contextOffset);

			// End update
			typeDropDownList.EndUpdate();
		}
		
		/// <summary>
		/// Disposes any resources used by the object.
		/// </summary>
		/// <param name="disposing">Whether the object is being disposed.</param>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				// Clear the SyntaxEditor reference
				this.SyntaxEditor = null;
			}

			// Call the base method
			base.Dispose(disposing);
		}

		/// <summary>
		/// Raises the <c>Layout</c> event.
		/// </summary>
		/// <param name="e">A <c>LayoutEventArgs</c> that contains the event data.</param>
		/// <remarks>
		/// The <c>OnLayout</c> method also allows derived classes to handle the event without attaching a delegate. 
		/// This is the preferred technique for handling the event in a derived class.
		/// <para>
		/// When overriding <c>OnLayout</c> in a derived class, be sure to call the base class's <c>OnLayout</c> 
		/// method so that registered delegates receive the event.
		/// </para>
		/// </remarks>
		protected override void OnLayout(LayoutEventArgs e) {	
			// Call the base method
			base.OnLayout(e);

			int x = this.Width / 2;
			typeDropDownList.Bounds = new Rectangle(this.ClientRectangle.Left, TypeMemberDropDownList.Space, 
				x - TypeMemberDropDownList.Space, typeDropDownList.Height);
			memberDropDownList.Bounds = new Rectangle(x + TypeMemberDropDownList.Space, TypeMemberDropDownList.Space, 
				this.ClientRectangle.Right - x - TypeMemberDropDownList.Space, typeDropDownList.Height);
		}
		
		/// <summary>
		/// Raises the <c>Resize</c> event.
		/// </summary>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		/// <remarks>
		/// The <c>OnResize</c> method also allows derived classes to handle the event without attaching a delegate. 
		/// This is the preferred technique for handling the event in a derived class.
		/// <para>
		/// When overriding <c>OnResize</c> in a derived class, be sure to call the base class's <c>OnResize</c> 
		/// method so that registered delegates receive the event.
		/// </para>
		/// </remarks>
		protected override void OnResize(EventArgs e) {			
			// Ensure the proper height
			if (this.Height != typeDropDownList.Height + 2 * TypeMemberDropDownList.Space) {
				this.Height = typeDropDownList.Height + 2 * TypeMemberDropDownList.Space;
				return;
			}

			// Call the base method
			base.OnResize(e);
		}
		
		/// <summary>
		/// Raises the <c>SelectedMemberChanged</c> event.
		/// </summary>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		protected virtual void OnSelectedMemberChanged(EventArgs e) {
			if (this.SelectedMemberChanged != null) 
				this.SelectedMemberChanged(this, e);
		}

		/// <summary>
		/// Raises the <c>SelectedTypeChanged</c> event.
		/// </summary>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		protected virtual void OnSelectedTypeChanged(EventArgs e) {
			if (this.SelectedTypeChanged != null) 
				this.SelectedTypeChanged(this, e);
		}

		/// <summary>
		/// Gets the selected <see cref="AstNode"/> in the member drop-down list.
		/// </summary>
		/// <value>The selected <see cref="AstNode"/> in the member drop-down list.</value>
		[
			Browsable(false),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		]
		public AstNode SelectedMember {
			get {
				if (memberDropDownList.SelectedIndex != -1)
					return ((AstNodeItem)memberDropDownList.SelectedItem).Node;
				else
					return null;
			}
		}

		/// <summary>
		/// Gets the selected <see cref="AstNode"/> in the type drop-down list.
		/// </summary>
		/// <value>The selected <see cref="AstNode"/> in the type drop-down list.</value>
		[
			Browsable(false),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
		]
		public AstNode SelectedType {
			get {
				if (typeDropDownList.SelectedIndex != -1)
					return ((AstNodeItem)typeDropDownList.SelectedItem).Node;
				else
					return null;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="ActiproSoftware.SyntaxEditor.SyntaxEditor"/> to which the drop-down list is currently bound.
		/// </summary>
		/// <value>The <see cref="ActiproSoftware.SyntaxEditor.SyntaxEditor"/> to which the drop-down list is currently bound.</value>
		/// <remarks>
		/// This property automates the drop-down list functionality.  When this property is set to a valid <see cref="ActiproSoftware.SyntaxEditor.SyntaxEditor"/>
		/// control, the drop-down list listens to selection change events and looks for document semantic parse data updates.
		/// It also automatically navigates the SyntaxEditor caret to the type or member in the drop-down when a selection is made by the end user.
		/// </remarks>
		[
			Category("Behavior"),
			Description("Specifies the SyntaxEditor to which the drop-down list is currently bound."),
			DefaultValue(null)
		]
		public SyntaxEditor SyntaxEditor {
			get {
				return syntaxEditor;
			}
			set {
				// Quit if the value is already set
				if (syntaxEditor == value)
					return;

				if (syntaxEditor != null) {
					// Detach from events
					try {
						syntaxEditor.DocumentChanged -= new EventHandler(syntaxEditor_DocumentChanged);
						syntaxEditor.DocumentSemanticParseDataChanged -= new EventHandler(syntaxEditor_DocumentSemanticParseDataChanged);
						syntaxEditor.DocumentSyntaxLanguageLoaded -= new SyntaxLanguageEventHandler(syntaxEditor_DocumentSyntaxLanguageLoaded);
						syntaxEditor.SelectionChanged -= new SelectionEventHandler(syntaxEditor_SelectionChanged);
					}
					catch {}
				}

				// Set the new value
				syntaxEditor = value;

				if (syntaxEditor != null) {
					// Attach to events
					syntaxEditor.DocumentChanged += new EventHandler(syntaxEditor_DocumentChanged);
					syntaxEditor.DocumentSemanticParseDataChanged += new EventHandler(syntaxEditor_DocumentSemanticParseDataChanged);
					syntaxEditor.DocumentSyntaxLanguageLoaded += new SyntaxLanguageEventHandler(syntaxEditor_DocumentSyntaxLanguageLoaded);
					syntaxEditor.SelectionChanged += new SelectionEventHandler(syntaxEditor_SelectionChanged);
				}
			}
		}

		/// <summary>
		/// Updates the selection.
		/// </summary>
		/// <param name="compilationUnit">The <see cref="CompilationUnit"/> to examine.</param>
		/// <param name="contextOffset">The offset at which the caret is located.</param>
		/// <remarks>This method does not need to be called if the <see cref="SyntaxEditor"/> property is set.</remarks>
		public void UpdateSelection(CompilationUnit compilationUnit, int contextOffset) {
			// Save the context offset
			this.contextOffset = contextOffset;

			// Select a type list item
			if (compilationUnit == null) {
				// Clear the list if there is no compilation unit
				if (typeDropDownList.Items.Count > 0)
					typeDropDownList.Items.Clear();
				if (memberDropDownList.Items.Count > 0)
					memberDropDownList.Items.Clear();
			}
			else if (typeDropDownList.Items.Count > 0) {
				// Get whether the current type selection contains the context offset
				bool didIntersectWithContextOffset = (typeDropDownList.SelectedIndex != -1 ? ((AstNodeItem)typeDropDownList.SelectedItem).Node.IntersectsWith(this.oldContextOffset) : false);

				// Get the context node
				AstNode contextNode = compilationUnit.GetClosestTypeOrMember(contextOffset);

				// Determine the key of the parent type declaration
				string key = null;
				if (contextNode != null) {
					switch (contextNode.NodeCategory) {
						case DotNetNodeCategory.TypeDeclaration:
							key = contextNode.Key;
							break;
						case DotNetNodeCategory.TypeMemberDeclaration:
						case DotNetNodeCategory.TypeMemberDeclarationSection: {
							TypeDeclaration parentTypeDeclaration = contextNode.ParentTypeDeclaration;
							if (parentTypeDeclaration != null)
								key = parentTypeDeclaration.Key;
							break;
						}
					}
				}

				// Change the type drop-down list's selected item
				ignoreTypeChange = true;
				bool typeSelectionChanged = false;
				try {
					if (key != null) {
						foreach (AstNodeItem nodeItem in typeDropDownList.Items) {
							if (nodeItem.Key == key) {
								if (typeDropDownList.SelectedItem != nodeItem) {
									typeDropDownList.SelectedItem = nodeItem;
									typeSelectionChanged = true;
								}
								break;
							}
						}
					}
					if (typeDropDownList.SelectedIndex == -1) {
						typeDropDownList.SelectedItem = compilationUnit.Types[0];
						typeSelectionChanged = true;
					}
				}
				finally {
					ignoreTypeChange = false;
				}

				if (typeSelectionChanged) {
					// Rebind the member drop-down list
					this.BindToType((typeDropDownList.SelectedIndex != -1 ? ((AstNodeItem)typeDropDownList.SelectedItem).Node as TypeDeclaration : null), contextNode);
				}
				else {
					// Repaint the type list if the node now intersects with the context offset
					if (didIntersectWithContextOffset != ((AstNodeItem)typeDropDownList.SelectedItem).Node.IntersectsWith(contextOffset))
						typeDropDownList.Invalidate();

					// Update the selection only
					this.UpdateMemberSelection(contextNode);
				}
			}

			// Save the old context offset
			oldContextOffset = contextOffset;
		}

	}
}