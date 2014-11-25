using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle("Actipro SyntaxEditor .NET Languages Add-on")]
[assembly: AssemblyDescription("A .NET languages add-on for SyntaxEditor.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Actipro Software LLC")]
[assembly: AssemblyProduct(".NET Languages")]
[assembly: AssemblyCopyright("Copyright (c) 2001-2009 Actipro Software LLC.  All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]		
[assembly: CLSCompliant(true)]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion("4.0.289.0")]

#if NET11
[assembly: AssemblyKeyFile(@"..\Addons.DotNet\Addons.DotNet.snk")]
#endif

namespace ActiproSoftware.Products.SyntaxEditor.Addons.DotNet {

	/// <summary>
	/// Provides a class for retrieving information about the <c>ActiproSoftware.SyntaxEditor.Addons.DotNet</c> assembly.
	/// </summary>
	public sealed class AssemblyInfo : ActiproSoftware.Products.AssemblyInfo {

		private Resources resources;

		private static AssemblyInfo instance;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>AssemblyInfo</c> class.
		/// </summary>
		/// <remarks>
		/// The default constructor initializes all fields to their default values.
		/// </remarks>
		private AssemblyInfo() {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets an array containing the components that must be licensed to be used in production applications.
		/// </summary>
		/// <value>An array containing the components that must be licensed to be used in production applications.</value>
		public override Type[] Components { 
			get {
				return new Type[] {
					typeof(ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage),
					typeof(ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom.TypeMemberDropDownList)
					};
			}
		}

		/// <summary>
		/// Gets the instance of the <see cref="ActiproSoftware.Products.AssemblyInfo"/> class for this assembly.
		/// </summary>
		/// <value>The instance of the <see cref="ActiproSoftware.Products.AssemblyInfo"/> class for this assembly.</value>
		public static AssemblyInfo Instance {
			get {
				if (AssemblyInfo.instance == null)
					AssemblyInfo.instance = new AssemblyInfo();

				return AssemblyInfo.instance;
			}
		}

		/// <summary>
		/// Gets the product logo <see cref="Image"/> to display on the license form.
		/// </summary>
		/// <value>The <see cref="Image"/> resource that was retrieved.</value>
		public override Image LicenseFormProductLogo {
			get {
				return this.Resources.GetImage(ImageResource.LicenseFormProductLogo);
			}
		}

		/// <summary>
		/// Gets the type of license that is available for the assembly.
		/// </summary>
		/// <value>A <see cref="AssemblyLicenseType"/> specifying the type of license that is available for the assembly.</value>
		public override AssemblyLicenseType LicenseType { 
			get {
				#if BETA
					return AssemblyLicenseType.Beta;
				#elif PRERELEASE
					return AssemblyLicenseType.PreRelease;
				#else
					return AssemblyLicenseType.Full;
				#endif
			}
		}
		
		/// <summary>
		/// Gets the target platform for the assembly.
		/// </summary>
		/// <value>A <see cref="AssemblyPlatform"/> specifying the target platform for the assembly.</value>
		public override AssemblyPlatform Platform { 
			get {
				return AssemblyPlatform.WindowsForms;
			}
		}

		/// <summary>
		/// Gets the product code of the assembly.
		/// </summary>
		/// <value>A three-letter product code of the assembly.</value>
		public override string ProductCode { 
			get {
				return "SED";
			}
		}

		/// <summary>
		/// Gets an array containing the names of files that are permitted to be redistributed 
		/// with your projects after you have licensed the product.
		/// </summary>
		/// <value>
		/// An array containing the names of files that are permitted to be redistributed 
		/// with your projects after you have licensed the product
		/// </value>
		public override string[] RedistributableFiles { 
			get {
				return new string[] {
					"ActiproSoftware.SyntaxEditor.Addons.DotNet.Net20.dll",
					"ActiproSoftware.SyntaxEditor.Addons.DotNet.Net11.dll",
					};
			}
		}

		/// <summary>
		/// Gets a <see cref="ActiproSoftware.Products.SyntaxEditor.Resources"/> that provides access to the resources of the assembly.
		/// </summary>
		/// <value>
		/// A <see cref="ActiproSoftware.Products.SyntaxEditor.Resources"/> that provides access to the resources of the assembly.
		/// </value>
		public Resources Resources {
			get {
				if (resources == null)
					resources = new Resources();

				return resources;
			}
		}
	}
}
