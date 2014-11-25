using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ActiproSoftware.ComponentModel;

namespace ActiproSoftware.SyntaxEditor.Addons.CSharp {
	
	/// <summary>
	/// Provides a store of formatting options for the the <c>C#</c> language.
	/// </summary>
	[TypeConverter(typeof(ExpandableNullableObjectConverter))]
	public class CSharpFormattingOptions {

		private bool indentBlockContents		= true;
		private bool indentCaseContents			= true;
		private bool indentCaseLabels			= true;
		private bool indentOpenAndCloseBraces	= false;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets whether to indent block contents.
		/// </summary>
		/// <value>
		/// <c>true</c> if block contents should be indented; otherwise, <c>false</c>.
		/// </value>
		[
			Category("Behavior"),
			Description("Indicates whether to indent block contents."),
			DefaultValue(true)
		]
		public bool IndentBlockContents {
			get { 
				return indentBlockContents; 
			}
			set { 
				indentBlockContents = value; 
			}
		}

		/// <summary>
		/// Gets or sets whether to indent case contents.
		/// </summary>
		/// <value>
		/// <c>true</c> if case contents should be indented; otherwise, <c>false</c>.
		/// </value>
		[
			Category("Behavior"),
			Description("Indicates whether to indent case contents."),
			DefaultValue(true)
		]
		public bool IndentCaseContents {
			get { 
				return indentCaseContents; 
			}
			set { 
				indentCaseContents = value; 
			}
		}

		/// <summary>
		/// Gets or sets whether to indent case labels.
		/// </summary>
		/// <value>
		/// <c>true</c> if case labels should be indented; otherwise, <c>false</c>.
		/// </value>
		[
			Category("Behavior"),
			Description("Indicates whether to indent case labels."),
			DefaultValue(true)
		]
		public bool IndentCaseLabels {
			get { 
				return indentCaseLabels; 
			}
			set { 
				indentCaseLabels = value; 
			}
		}

		/// <summary>
		/// Gets or sets whether to indent open and close braces.
		/// </summary>
		/// <value>
		/// <c>true</c> if open and close braces should be indented; otherwise, <c>false</c>.
		/// </value>
		[
			Category("Behavior"),
			Description("Indicates whether to indent open and close braces."),
			DefaultValue(false)
		]
		public bool IndentOpenAndCloseBraces {
			get { 
				return indentOpenAndCloseBraces; 
			}
			set { 
				indentOpenAndCloseBraces = value; 
			}
		}

	}
}
 
