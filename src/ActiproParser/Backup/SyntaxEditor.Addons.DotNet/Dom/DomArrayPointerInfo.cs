using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Stores array and pointer information for an <see cref="IDomTypeReference"/>.
	/// </summary>
	internal class DomArrayPointerInfo {
		
		private int[]		arrayRanks;
		private int			pointerLevel;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>DomArrayPointerInfo</c> class.
		/// </summary>
		public DomArrayPointerInfo() : this(null, 0) {}

		/// <summary>
		/// Initializes a new instance of the <c>DomArrayPointerInfo</c> class.
		/// </summary>
		/// <param name="arrayRanks">The array ranks.</param>
		public DomArrayPointerInfo(int[] arrayRanks) : this(arrayRanks, 0) {}
		
		/// <summary>
		/// Initializes a new instance of the <c>DomArrayPointerInfo</c> class.
		/// </summary>
		/// <param name="pointerLevel">The pointer level.</param>
		public DomArrayPointerInfo(int pointerLevel) : this(null, 0) {}
		
		/// <summary>
		/// Initializes a new instance of the <c>DomArrayPointerInfo</c> class.
		/// </summary>
		/// <param name="arrayRanks">The array ranks.</param>
		/// <param name="pointerLevel">The pointer level.</param>
		public DomArrayPointerInfo(int[] arrayRanks, int pointerLevel) {
			// Initialize parameters
			this.arrayRanks = arrayRanks;
			this.pointerLevel = pointerLevel;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the array ranks.
		/// </summary>
		/// <value>The array ranks.</value>
		/// <remarks>
		/// <c>MyClass</c> is <see langword="null"/>.
		/// <c>MyClass[]</c> is <c>{ 1 }</c>.
		/// <c>MyClass[,]</c> is <c>{ 2 }</c>.
		/// <c>MyClass[][]</c> is <c>{ 1, 1 }</c>.
		/// </remarks>
		internal int[] ArrayRanks {
			get {
				return arrayRanks;
			}
			set {
				arrayRanks = value;
			}
		}

		/// <summary>
		/// Gets or sets the pointer level.
		/// </summary>
		/// <value>The pointer level.</value>
		internal int PointerLevel {
			get {
				return pointerLevel;
			}
			set {
				pointerLevel = value;
			}
		}


	}
}
