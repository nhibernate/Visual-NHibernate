using System;
using System.Collections;
using System.Reflection;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a delegate declaration.
	/// </summary>
	public class DelegateDeclaration : TypeDeclaration, IDomType {

		/// <summary>
		/// Gets the context ID for a return type AST node.
		/// </summary>
		/// <value>The context ID for a return type AST node.</value>
		public const byte ReturnTypeContextID = TypeDeclaration.TypeDeclarationContextIDBase;

		/// <summary>
		/// Gets the context ID for a parameter AST node.
		/// </summary>
		/// <value>The context ID for a parameter AST node.</value>
		public const byte ParameterContextID = TypeDeclaration.TypeDeclarationContextIDBase + 1;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>DelegateDeclaration</c> class. 
		/// </summary>
		/// <param name="modifiers">The modifiers.</param>
		/// <param name="name">The name.</param>
		public DelegateDeclaration(Modifiers modifiers, QualifiedIdentifier name) : base(modifiers, name) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="IDomTypeReference"/> to the base type.
		/// </summary>
		/// <value>The <see cref="IDomTypeReference"/> to the base type.</value>
		IDomTypeReference IDomType.BaseType {
			get {
				return new TypeReference("System.MulticastDelegate", TextRange.Deleted);
			}
		}
		
		/// <summary>
		/// Returns the access <see cref="Modifiers"/> of the type's constructors.
		/// </summary>
		/// <returns>The access <see cref="Modifiers"/> of the type's constructors.</returns>
		Modifiers IDomType.GetConstructorAccessModifiers() {
			return Modifiers.None;
		}
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates a member that represents the <c>BeginInvoke</c> method.
		/// </summary>
		/// <returns>The <see cref="MethodDeclaration"/> that represents the invoke method.</returns>
		private MethodDeclaration CreateBeginInvokeMethod() {
			MethodDeclaration method = new MethodDeclaration(Modifiers.Public | Modifiers.Virtual, new QualifiedIdentifier("BeginInvoke"));
			method.ReturnType = new TypeReference("System.IAsyncResult", TextRange.Deleted);

			ParameterDeclaration newParameter;
			IAstNodeList parameters = this.Parameters;
			if (parameters != null) {

				foreach (IAstNode node in parameters) {
					ParameterDeclaration parameter = node as ParameterDeclaration;
					if (parameter != null) {
						newParameter = new ParameterDeclaration(parameter.Modifiers, parameter.Name);
						if (parameter.ParameterType != null)
							newParameter.ParameterType = new TypeReference(parameter.ParameterType.Name, parameter.ParameterType.IsGenericParameter);
						method.Parameters.Add(newParameter);
					}
				}
			}

			newParameter = new ParameterDeclaration(ParameterModifiers.None, "callback");
			newParameter.ParameterType = new TypeReference("System.AsyncCallback", TextRange.Deleted);
			method.Parameters.Add(newParameter);

			newParameter = new ParameterDeclaration(ParameterModifiers.None, "object");
			newParameter.ParameterType = new TypeReference("System.Object", TextRange.Deleted);
			method.Parameters.Add(newParameter);
			
			return method;
		}

		/// <summary>
		/// Creates a member that represents the <c>EndInvoke</c> method.
		/// </summary>
		/// <returns>The <see cref="MethodDeclaration"/> that represents the invoke method.</returns>
		private MethodDeclaration CreateEndInvokeMethod() {
			MethodDeclaration method = new MethodDeclaration(Modifiers.Public | Modifiers.Virtual, new QualifiedIdentifier("EndInvoke"));

			ParameterDeclaration newParameter = new ParameterDeclaration(ParameterModifiers.None, "result");
			newParameter.ParameterType = new TypeReference("System.IAsyncResult", TextRange.Deleted);
			method.Parameters.Add(newParameter);

			return method;
		}

		/// <summary>
		/// Creates a member that represents the <c>Invoke</c> method.
		/// </summary>
		/// <returns>The <see cref="MethodDeclaration"/> that represents the invoke method.</returns>
		private MethodDeclaration CreateInvokeMethod() {
			MethodDeclaration method = new MethodDeclaration(Modifiers.Public | Modifiers.Virtual, new QualifiedIdentifier("Invoke"));
			if (this.ReturnType != null) {
				method.ReturnType = new TypeReference(this.ReturnType.Name, this.ReturnType.IsGenericParameter);
			}
			IAstNodeList parameters = this.Parameters;
			if (parameters != null) {
				foreach (IAstNode node in parameters) {
					ParameterDeclaration parameter = node as ParameterDeclaration;
					if (parameter != null) {
						ParameterDeclaration newParameter = new ParameterDeclaration(parameter.Modifiers, parameter.Name);
						if (parameter.ParameterType != null)
							newParameter.ParameterType = new TypeReference(parameter.ParameterType.Name, parameter.ParameterType.IsGenericParameter);
						method.Parameters.Add(newParameter);
					}
				}
			}

			return method;
		}

		/// <summary>
		/// Generates the invoke members.
		/// </summary>
		internal void GenerateInvokeMembers() {
			this.Members.Clear();
			this.Members.Add(this.CreateBeginInvokeMethod());
			this.Members.Add(this.CreateEndInvokeMethod());
			this.Members.Add(this.CreateInvokeMethod());
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Accepts the specified visitor for visiting this node.
		/// </summary>
		/// <param name="visitor">The visitor to accept.</param>
		/// <remarks>This method is part of the visitor design pattern implementation.</remarks>
		protected override void AcceptCore(AstVisitor visitor) {
			if (visitor.OnVisiting(this)) {
				// Visit children
				if (this.ChildNodeCount > 0)
					this.AcceptChildren(visitor, this.ChildNodes);
			}
			visitor.OnVisited(this);
		}
		
		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public override int ImageIndex {
			get {
				return AssemblyDomType.GetReflectionImageIndex(DomTypeType.Delegate, this.AccessModifiers);
			}
		}

		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.DelegateDeclaration;
			}
		}

		/// <summary>
		/// Gets the collection of parameters.
		/// </summary>
		/// <value>The collection of parameters.</value>
		public IAstNodeList Parameters {
			get {
				return new AstNodeListWrapper(this, DelegateDeclaration.ParameterContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the return type.
		/// </summary>
		/// <value>The return type.</value>
		public TypeReference ReturnType {
			get {
				return this.GetChildNode(DelegateDeclaration.ReturnTypeContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, DelegateDeclaration.ReturnTypeContextID);
			}
		}
		
	}
}
