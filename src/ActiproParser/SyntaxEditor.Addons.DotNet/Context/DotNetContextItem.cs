using System;
using System.Collections;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Context {

	/// <summary>
	/// Represents an item within a <see cref="DotNetContext"/>.
	/// </summary>
	public class DotNetContextItem {

		private string					argumentsText;
		private int[]					indexerParameterCounts;
		private IDomTypeReference[]		genericTypeArguments;
		private IDomTypeReference[]		resolvedArguments;
		private object					resolvedInfo;
		private string					text;
		private TextRange				textRange					= TextRange.Deleted;
		private DotNetContextItemType	type;
		private Expression[]			unresolvedArguments;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>DotNetContextItem</c> class.
		/// </summary>
		/// <param name="type">A <see cref="DotNetContextItemType"/> indicating the type of context item.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the item.</param>
		public DotNetContextItem(DotNetContextItemType type, TextRange textRange) : this(type, textRange, null) {}
		
		/// <summary>
		/// Initializes a new instance of the <c>DotNetContextItem</c> class.
		/// </summary>
		/// <param name="textRange">The <see cref="TextRange"/> of the item.</param>
		/// <param name="text">The text of the item.</param>
		public DotNetContextItem(TextRange textRange, string text) : this(DotNetContextItemType.Unknown, textRange, text) {}
		
		/// <summary>
		/// Initializes a new instance of the <c>DotNetContextItem</c> class.
		/// </summary>
		/// <param name="type">A <see cref="DotNetContextItemType"/> indicating the type of context item.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the item.</param>
		/// <param name="text">The text of the item.</param>
		public DotNetContextItem(DotNetContextItemType type, TextRange textRange, string text) {
			this.text		= text;
			this.textRange	= textRange;
			this.type		= type;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Appends a level to the indexer parameter count array.
		/// </summary>
		/// <param name="indexerParameterCounts">The indexer parameter count array to update.</param>
		/// <param name="parameterCount">The parameter count for the new level.</param>
		internal static void AppendIndexerParameterCountLevel(ref int[] indexerParameterCounts, int parameterCount) {
			if (indexerParameterCounts != null) {
				int[] oldIndexerParameterCounts = indexerParameterCounts;
				indexerParameterCounts = new int[indexerParameterCounts.Length + 1];
				Array.Copy(oldIndexerParameterCounts, indexerParameterCounts, oldIndexerParameterCounts.Length);
				indexerParameterCounts[indexerParameterCounts.Length - 1] = parameterCount;
			}
			else
				indexerParameterCounts = new int[] { parameterCount };
		}
		
		/// <summary>
		/// Gets or sets the text string of arguments that were specified to a member.
		/// </summary>
		/// <value>The text string of arguments that were specified to a member.</value>
		internal string ArgumentsText {
			get {
				return argumentsText;
			}
			set { 
				argumentsText = value;
			}
		}

		/// <summary>
		/// Gets the array of generic type argument <see cref="IDomTypeReference"/> objects of this item.
		/// </summary>
		/// <value>The array of generic type argument <see cref="IDomTypeReference"/> objects of this item.</value>
		internal IDomTypeReference[] GenericTypeArguments {
			get {
				return genericTypeArguments;
			}
			set {
				genericTypeArguments = value;
			}
		}

		/// <summary>
		/// Gets the array of resolved argument <see cref="IDomTypeReference"/> objects of this item.
		/// </summary>
		/// <value>The array of resolved argument <see cref="IDomTypeReference"/> objects of this item.</value>
		internal IDomTypeReference[] ResolvedArguments {
			get {
				return resolvedArguments;
			}
			set {
				resolvedArguments = value;
			}
		}

		/// <summary>
		/// Gets the array of unresolved argument <see cref="Expression"/> objects of this item.
		/// </summary>
		/// <value>The array of unresolved argument <see cref="Expression"/> objects of this item.</value>
		internal Expression[] UnresolvedArguments {
			get {
				return unresolvedArguments;
			}
			set {
				unresolvedArguments = value;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the array indexer parameter counts.
		/// </summary>
		/// <value>The array indexer parameter counts.</value>
		/// <remarks>
		/// <c>this</c> is <see langword="null"/>.
		/// <c>this[0]</c> is <c>{ 1 }</c>.
		/// <c>this[0, 1]</c> is <c>{ 2 }</c>.
		/// <c>this[0][1]</c> is <c>{ 1, 1 }</c>.
		/// </remarks>
		public int[] IndexerParameterCounts {
			get {
				return indexerParameterCounts;
			}
			set {
				indexerParameterCounts = value;
			}
		}

		/// <summary>
		/// Gets or sets an object that contains resolved information about the context item.
		/// </summary>
		/// <value>An object that contains resolved information about the context item.</value>
		public object ResolvedInfo {
			get {
				return resolvedInfo;
			}
			set {
				resolvedInfo = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the text of the item.
		/// </summary>
		/// <value>The text of the item.</value>
		public string Text {
			get {
				return text;
			}
			set {
				text = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="TextRange"/> of the item.
		/// </summary>
		/// <value>The <see cref="TextRange"/> of the item.</value>
		public TextRange TextRange {
			get {
				return textRange;
			}
			set {
				textRange = value;
			}
		}
		
		/// <summary>
		/// Gets or sets a <see cref="DotNetContextItemType"/> indicating the type of context item.
		/// </summary>
		/// <value>A <see cref="DotNetContextItemType"/> indicating the type of context item.</value>
		public DotNetContextItemType Type {
			get {
				return type;
			}
			set {
				type = value;
			}
		}
	}
}
 