// #define DEBUG_REPOSITORY

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Text;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a code repository for .NET assemblies that can be shared among multiple "projects" that are loaded.
	/// </summary>
	public class AssemblyCodeRepository {

		private static Hashtable	projectContents;
		private static object		threadSync			= new object();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INNER TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Represents options for loading an assembly in a <see cref="AssemblyCodeRepository"/>.
		/// </summary>
		[Serializable()]
		private class AssemblyCodeRepositoryOptions {

			internal string				CachePath;
			internal StringCollection	DependencySearchPaths;
			
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// OBJECT
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Initializes a new instance of the <c>AssemblyCodeRepositoryOptions</c> class.
			/// </summary>
			internal AssemblyCodeRepositoryOptions(DotNetProjectResolver projectResolver) {
				// Initialize properties
				this.CachePath				= projectResolver.CachePath;
				this.DependencySearchPaths	= projectResolver.GetDependencySearchPaths();
			}
		}

		/// <summary>
		/// Stores data about a project content reference.
		/// </summary>
		private class ProjectContentInfo {

			internal IProjectContent	ProjectContent;
			internal int				ReferenceCount;

		}

		/// <summary>
		/// Loads an <see cref="AssemblyProjectContent"/> from an assembly.
		/// </summary>
		private class ProjectContentLoader : MarshalByRefObject {

			private Assembly	assembly;
			private string		assemblyFullName;
			
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// NON-PUBLIC METHODS
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Occurs when an <see cref="Assembly"/> must be resolved.
			/// </summary>
			/// <param name="sender">Sender of the event.</param>
			/// <param name="e">An <c>IndicatorEventArgs</c> that contains the event data.</param>
			/// <returns>The resolved assembly.</returns>
			private Assembly AppDomain_AssemblyResolve(object sender, ResolveEventArgs e) {
				Assembly resolvedAssembly = null;

				// Get the assembly name
				string assemblyName = e.Name;
				int commaIndex = assemblyName.IndexOf(',');
				if (commaIndex != -1)
					assemblyName = assemblyName.Substring(0, commaIndex);

				// 2/7/2011 - Added null check moved try...catch up (353-14E298FD-26A2)
				if (!string.IsNullOrEmpty(assembly.Location)) {
					try {
						string path = Path.GetDirectoryName(assembly.Location);
						if ((path != null) && (Directory.Exists(path))) {
							// Update the path
							path = Path.Combine(path, assemblyName);

							// Try and load the assembly file
							if (File.Exists(path + ".dll"))
								resolvedAssembly = this.LoadAssemblyCore(path + ".dll");
						}
					}
					catch {
						// 8/23/2010 - Stopped throwing exceptions since it will prevent the AssemblyResolve event from raising in the future
					}
				}

				// NOTE: If there are any problems with this, maybe try to resolve to already-loaded assemblies

				// Try and load the assembly by name
				if (resolvedAssembly == null) {
					try {
						// 8/23/2010 - Added to skip over .resources requests that .NET 4 started asking for (1BB-1405466A-EB24)
						// 12/22/2010 - Commented back out because Microsoft posted a workaround for the initial problem in Connect (http://connect.microsoft.com/VisualStudio/feedback/details/586702/net4-error-loading-resources-in-assembly-references-of-csharpcodeprovider-compiled-assembly)
						//    NOT done yet but if this is done, add this rel hist item:  Updated AssemblyCodeRepository code that tries to resolve assemblies.  In the previous build code was added to try and skip over .resources assemblies passed to it starting in .NET 4.0 however this code change was causing slowdowns in load times in some scenarios so it has been backed out.  Add lines like this (with proper language specified and fallback indicated) to your .NET 4.0 projects instead: assembly: NeutralResourcesLanguageAttribute("en-US", UltimateResourceFallbackLocation.MainAssembly)]
						if (!assemblyName.EndsWith(".resources"))
							resolvedAssembly = this.LoadAssemblyCore(e.Name);
					}
					catch (FileNotFoundException) {
						assemblyName = e.Name;
						if (assemblyName.IndexOf(',') != -1) {
							// Update the assembly name to the next newer .NET framework version
							assemblyName = this.UpconvertAssemblyName(assemblyName);
							while (assemblyName != null) {
								try {
									// The specified assembly name was not found... maybe a newer version is in the GAC so check for that
									resolvedAssembly = this.LoadAssemblyCore(assemblyName);
									if (resolvedAssembly != null)
										break;
								}
								catch {}

								// Update the assembly name to the next newer .NET framework version
								assemblyName = this.UpconvertAssemblyName(assemblyName);
							}
						}

						// 7/6/2009 - Stopped throwing exceptions since it will prevent the AssemblyResolve event from raising in the future
						// Re-throw the original exception
						// if (resolvedAssembly == null) throw ex;
					}
					catch {
						// 7/6/2009 - Stopped throwing exceptions since it will prevent the AssemblyResolve event from raising in the future
					}
				}

				return resolvedAssembly;
			}

			/// <summary>
			/// Gets or sets the assembly that is loaded.
			/// </summary>
			/// <value>The assembly that is loaded.</value>
			/// <remarks>This property should only be used if using the loader in the main <see cref="AppDomain"/>.</remarks>
			internal Assembly Assembly {
				get {
					return assembly;
				}
				set {
					assembly = value;
				}
			}

			/// <summary>
			/// Gets the full name of the assembly.
			/// </summary>
			/// <value>The full name of the assembly.</value>
			internal string AssemblyFullName {
				get {
					return assemblyFullName;
				}
				set {
					assemblyFullName= value;
				}
			}

			/// <summary>
			/// Gets the location of the assembly.
			/// </summary>
			/// <value>The location of the assembly.</value>
			internal string AssemblyLocation {
				get {
					return assembly.Location;
				}
			}

			/// <summary>
			/// Creates an <see cref="AssemblyProjectContent"/> for the loaded assembly.
			/// </summary>
			/// <param name="options">The <see cref="AssemblyCodeRepositoryOptions"/> to use.</param>
			/// <param name="resolveDelegate">The custom <see cref="ResolveEventHandler"/> to use, if any.</param>
			/// <returns>
			/// The <see cref="AssemblyProjectContent"/> that was created.
			/// </returns>
			internal AssemblyProjectContent CreateProjectContent(AssemblyCodeRepositoryOptions options, ResolveEventHandler resolveDelegate) {
				if (assembly != null) {
					ResolveEventHandler handler = resolveDelegate ?? this.AppDomain_AssemblyResolve;

					try {
						// Attach to resolve event
						#if !NET11
						if (assembly.ReflectionOnly)
							AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += handler;
						else
						#endif
							AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(handler);
						
						return new AssemblyProjectContent(assembly, assemblyFullName, assembly.Location, options.CachePath, true);
					}
					finally {
						// Detach from resolve event
						#if !NET11
						if (assembly.ReflectionOnly)
							AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= handler;
						else
						#endif
							AppDomain.CurrentDomain.AssemblyResolve -= handler;
					}
				}
				
				return null;
			}

			/// <summary>
			/// Loads an assembly from an assembly full name or path.
			/// </summary>
			/// <param name="assemblyName">The full name, or path, of the assembly to add.</param>
			/// <returns>
			/// <c>true</c> if an assembly was loaded; otherwise, <c>false</c>.
			/// </returns>
			internal bool LoadAssembly(string assemblyName) {
				assembly = this.LoadAssemblyCore(assemblyName);
				if (assembly != null) {
					assemblyFullName = assembly.FullName;
					return true;
				}
				else
					return false;
			}

			/// <summary>
			/// Loads an assembly from an assembly full name or path.
			/// </summary>
			/// <param name="assemblyName">The full name, or path, of the assembly to add.</param>
			/// <returns>The assembly that was loaded, if any.</returns>
			internal Assembly LoadAssemblyCore(string assemblyName) {
				if (File.Exists(assemblyName)) {
					#if NET11
					return Assembly.LoadFile(assemblyName);
					#else
					return Assembly.ReflectionOnlyLoadFrom(assemblyName);
					#endif
				}
				else {
					#if NET11
					return Assembly.LoadWithPartialName(assemblyName);
					#else
					return Assembly.ReflectionOnlyLoad(assemblyName);
					#endif
				}
			}
			
			/// <summary>
			/// Upconverts the specified assembly name to a newer version.
			/// </summary>
			/// <param name="assemblyName">The assembly name to convert.</param>
			/// <returns>The updated assembly name.</returns>
			internal string UpconvertAssemblyName(string assemblyName) {
				const string v10 = "Version=1.0.3300.0";
				const string v11 = "Version=1.0.5000.0";
				const string v20 = "Version=2.0.0.0";
				const string v30 = "Version=3.0.0.0";
				const string v35 = "Version=3.5.0.0";
				const string v40 = "Version=4.0.0.0";

				if (assemblyName.IndexOf(v10) != -1)
					return assemblyName.Replace(v10, v11);
				else if (assemblyName.IndexOf(v11) != -1)
					return assemblyName.Replace(v11, v20);
				else if (assemblyName.IndexOf(v20) != -1)
					return assemblyName.Replace(v20, v30);
				else if (assemblyName.IndexOf(v30) != -1)
					return assemblyName.Replace(v30, v35);
				else if (assemblyName.IndexOf(v35) != -1)
					return assemblyName.Replace(v35, v40);
				else
					return null;
			}
			
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs immediately after an <see cref="AppDomain"/> is loaded, allowing for handling of additional resolving.
		/// </summary>
		/// <eventdata>
		/// The event handler receives an argument of type <c>AppDomainEventArgs</c> containing data related to this event.
		/// </eventdata>
		[
			Category("Action"),
			Description("Occurs immediately after an AppDomain is loaded, allowing for handling of additional resolving.")
		]
		public static event AppDomainEventHandler AppDomainCreated;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds an assembly to the code repository.
		/// </summary>
		/// <param name="assemblyName">The full name, or path, of the assembly to add.</param>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use.</param>
		/// <returns>
		/// The full assembly name of the assembly that was loaded; otherwise, <see langword="null"/>.
		/// </returns>
		/// <remarks>If the assembly already exists, the reference count to the assembly is increased.</remarks>
		public static string Add(string assemblyName, DotNetProjectResolver projectResolver) {
			if ((assemblyName == null) || (assemblyName.Length == 0))
				return null;
			
			#if DEBUG && DEBUG_REPOSITORY
			Trace.WriteLine(String.Format("AssemblyCodeRepository.Add: Adding assembly '{0}' by name/path...", assemblyName));
			#endif

			// Create options
			AssemblyCodeRepositoryOptions options = new AssemblyCodeRepositoryOptions(projectResolver);
			
			// If there is a cache path, load the assembly in a separate app domain
			ProjectContentLoader loader = null;
			string assemblyLocation = null;
			ProjectContentInfo info = null;
			if (options.CachePath != null) {
				// Generate the app domain setup
				AppDomainSetup setup = new AppDomainSetup();
				setup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;  // 2008-08-26 - Added ApplicationBase setting
				StringBuilder privateBinPath = new StringBuilder();
				if (assemblyName.IndexOf(',') == -1)
					privateBinPath.Append(Path.GetDirectoryName(assemblyName));
				StringCollection dependencySearchPaths = options.DependencySearchPaths;
				foreach (string path in dependencySearchPaths) {
					// NOTE: Only paths under the application base path will be searched, others will be ignored
					if (privateBinPath.Length > 0)
						privateBinPath.Append("; ");
					privateBinPath.Append(path);
				}
				setup.PrivateBinPath = privateBinPath.ToString();
				#if !NET11
				if (AppDomain.CurrentDomain.ApplicationTrust != null)
					setup.ApplicationTrust = AppDomain.CurrentDomain.ApplicationTrust;
				#endif

				// 4/2/2010 - Construct evidence... use null if is an empty evidence (http://www.actiprosoftware.com/Support/Forums/ViewForumTopic.aspx?ForumTopicID=4724#17468)
				Evidence evidence = AppDomain.CurrentDomain.Evidence;
				if ((evidence != null) && (evidence.Count > 0))
					evidence = new Evidence(evidence);
				else
					evidence = null;

				// Try and load an assembly in a separate app domain
				AppDomain loaderDomain = AppDomain.CreateDomain("SyntaxEditorReflectionLoader", evidence, setup);
				try {
					// Raise an event
					AppDomainEventArgs e = new AppDomainEventArgs(loaderDomain);
					if (AssemblyCodeRepository.AppDomainCreated != null)
						AssemblyCodeRepository.AppDomainCreated(null, e);

					// Create an instance of the loader
					loader = (ProjectContentLoader)loaderDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(ProjectContentLoader).FullName);
					if (loader == null) {
						#if DEBUG && DEBUG_REPOSITORY
						Trace.WriteLine(String.Format("AssemblyCodeRepository.Add: Failed to create a loader for assembly '{0}'", assemblyName));
						#endif
						return null;
					}

					// Try to load the assembly
					if (!loader.LoadAssembly(assemblyName)) {
						#if DEBUG && DEBUG_REPOSITORY
						Trace.WriteLine(String.Format("AssemblyCodeRepository.Add: Failed to load assembly '{0}'", assemblyName));
						#endif
						return null;
					}

					// Get the assembly full name and location
					assemblyName = loader.AssemblyFullName;
					assemblyLocation = loader.AssemblyLocation;

					// See if there already is a record for the assembly
					lock (threadSync) {
						if (projectContents != null)
							info = (ProjectContentInfo)projectContents[assemblyName.ToLower()];
					}
					if (info == null) {
						// There is no project content so create one
						if (loader.CreateProjectContent(options, null) == null) {
							#if DEBUG && DEBUG_REPOSITORY
							Trace.WriteLine(String.Format("AssemblyCodeRepository.Add: Failed to create a project content for assembly '{0}'", assemblyName));
							#endif
							return null;
						}
					}
				}
				finally {
					AppDomain.Unload(loaderDomain);
				}

				// Ensure there is a project content info record
				if (info == null) {
					info = new ProjectContentInfo();
					info.ProjectContent = new AssemblyProjectContent(assemblyName, assemblyLocation, options.CachePath, true);
				}
			}
			else {
				// Load the assembly in the main app domain
				loader = new ProjectContentLoader();

				// Try to load the assembly
				if (!loader.LoadAssembly(assemblyName)) {
					#if DEBUG && DEBUG_REPOSITORY
					Trace.WriteLine(String.Format("AssemblyCodeRepository.Add: Failed to load assembly '{0}'", assemblyName));
					#endif
					return null;
				}
				
				// Get the assembly full name and location
				assemblyName = loader.AssemblyFullName;
				assemblyLocation = loader.AssemblyLocation;

				// See if there already is a record for the assembly
				lock (threadSync) {
					if (projectContents != null)
						info = (ProjectContentInfo)projectContents[assemblyName.ToLower()];
				}
				if (info == null) {
					info = new ProjectContentInfo();
					info.ProjectContent = loader.CreateProjectContent(options, projectResolver.HostAppDomainResolver);
				}
			}

			// Increase the reference count and update the record
			info.ReferenceCount++;
			lock (threadSync) {
				// Ensure the project contents hashtable is initialized
				if (projectContents == null)
					projectContents	= new Hashtable();

				projectContents[assemblyName.ToLower()] = info;
			}

			#if DEBUG && DEBUG_REPOSITORY
			Trace.WriteLine(String.Format("AssemblyCodeRepository.Add: Added assembly '{0}' successfully", assemblyName));
			#endif
			return assemblyName;
		}
		
		/// <summary>
		/// Adds an assembly to the code repository.
		/// </summary>
		/// <param name="assembly">The assembly to add.</param>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use.</param>
		/// <remarks>If the assembly already exists, the reference count to the assembly is increased.</remarks>
		public static void Add(Assembly assembly, DotNetProjectResolver projectResolver) {
			AssemblyCodeRepository.Add(assembly, assembly.FullName, projectResolver);
		}

		/// <summary>
		/// Adds an assembly to the code repository, using the specified full name.
		/// </summary>
		/// <param name="assembly">The assembly to add.</param>
		/// <param name="assemblyFullName">The full name for the assembly, used as a key for the reflection data.</param>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use.</param>
		/// <remarks>If the assembly already exists, the reference count to the assembly is increased.</remarks>
		public static void Add(Assembly assembly, string assemblyFullName, DotNetProjectResolver projectResolver) {
			// Create options
			AssemblyCodeRepositoryOptions options = new AssemblyCodeRepositoryOptions(projectResolver);

			// See if there already is a record for the assembly
			ProjectContentInfo info = null;
			lock (threadSync) {
				if (projectContents != null)
					info = (ProjectContentInfo)projectContents[assemblyFullName.ToLower()];
			}
			if (info == null) {
				// There is no project content info so create one
				info = new ProjectContentInfo();

				// Create a loader and use it to load the project content
				ProjectContentLoader loader = new ProjectContentLoader();
				loader.Assembly = assembly;
				loader.AssemblyFullName = assemblyFullName;
				info.ProjectContent = loader.CreateProjectContent(options, projectResolver.HostAppDomainResolver);
			}
			
			// Increase the reference count and update the record
			info.ReferenceCount++;
			lock (threadSync) {
				// Ensure the project contents hashtable is initialized
				if (projectContents == null)
					projectContents	= new Hashtable();

				projectContents[assemblyFullName.ToLower()] = info;
			}

			#if DEBUG && DEBUG_REPOSITORY
			Trace.WriteLine(String.Format("AssemblyCodeRepository.Add: Directly added assembly '{0}' successfully", assemblyFullName));
			#endif
		}

		/// <summary>
		/// Gets the <see cref="ICollection"/> of assembly full names that are loaded.
		/// </summary>
		/// <value>The <see cref="ICollection"/> of assembly full names that are loaded.</value>
		public static ICollection AssemblyFullNames  {
			get {
				lock (threadSync) {
					if (projectContents != null)
						return projectContents.Keys;
					else
						return new ArrayList();
				}
			}
		}

		/// <summary>
		/// Removes all assemblies from the code repository.
		/// </summary>
		/// <remarks>
		/// All assemblies are removed from the code repository, regardless of their reference counts.
		/// Only call this method when closing out an application.
		/// </remarks>
		public static void Clear() {
			lock (threadSync) {
				if (projectContents != null) {
					#if DEBUG && DEBUG_REPOSITORY
					Trace.WriteLine(String.Format("AssemblyCodeRepository.Clear: Cleared the repository of {0} assemblies", projectContents.Count));
					#endif

					foreach (ProjectContentInfo info in projectContents.Values)
						info.ProjectContent.Dispose();

					projectContents.Clear();
					projectContents = null;
				}
			}
		}

		/// <summary>
		/// Gets the <see cref="IProjectContent"/> in the code repository with the specified assembly full name.
		/// </summary>
		/// <param name="assemblyFullName">The full name of the assembly to return.</param>
		/// <returns>
		/// The <see cref="IProjectContent"/> in the code repository with the specified assembly full name.
		/// </returns>
		public static IProjectContent GetProjectContent(string assemblyFullName) {
			lock (threadSync) {
				if (projectContents != null) {
					ProjectContentInfo info = (ProjectContentInfo)projectContents[assemblyFullName.ToLower()];
					if (info != null)
						return info.ProjectContent;
				}
			}
		
			return null;
		}
		
		/// <summary>
		/// Gets the <see cref="IProjectContent"/> in the code repository with the specified assembly partial name.
		/// </summary>
		/// <param name="assemblyPartialName">The partial name of the assembly to return.</param>
		/// <returns>
		/// The <see cref="IProjectContent"/> in the code repository with the specified assembly partial name.
		/// </returns>
		public static IProjectContent GetProjectContentWithPartialName(string assemblyPartialName) {
			assemblyPartialName = assemblyPartialName.ToLower();

			lock (threadSync) {
				if (projectContents != null) {
					foreach (string key in projectContents.Keys) {
						if (key.StartsWith(assemblyPartialName)) {
							ProjectContentInfo info = (ProjectContentInfo)projectContents[key];
							if (info != null)
								return info.ProjectContent;
						}
					}
				}
			}

			return null;
		}
		
		/// <summary>
		/// Gets the <see cref="ICollection"/> that contains the <see cref="IProjectContent"/> objects that are currently registered for reflection.
		/// </summary>
		/// <value>The <see cref="ICollection"/> that contains the <see cref="IProjectContent"/> objects that are currently registered for reflection.</value>
		public static ICollection ProjectContents {
			get {
				lock (threadSync) {
					if (projectContents != null)
						return projectContents.Values;
					else
						return new ArrayList();
				}
			}
		}
		
		/// <summary>
		/// Refreshes the code repository entry for a previously-loaded assembly, 
		/// which is useful for calling once an assembly is rebuilt with the exact same name and version information.
		/// </summary>
		/// <param name="assembly">The assembly to refresh.</param>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use.</param>
		/// <returns>
		/// <c>true</c> if the assembly is refreshed; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// The assembly is only refreshed if there is already an assembly loaded for the same assembly full name.
		/// </remarks>
		public static bool Refresh(Assembly assembly, DotNetProjectResolver projectResolver) {
			return AssemblyCodeRepository.Refresh(assembly, assembly.FullName, projectResolver);
		}

		/// <summary>
		/// Refreshes the code repository entry for a previously-loaded assembly, 
		/// which is useful for calling once an assembly is rebuilt with the exact same name and version information.
		/// </summary>
		/// <param name="assembly">The assembly to refresh.</param>
		/// <param name="assemblyFullName">The full name for the assembly, used as a key for the reflection data.</param>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use.</param>
		/// <returns>
		/// <c>true</c> if the assembly is refreshed; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// The assembly is only refreshed if there is already an assembly loaded for the same assembly full name.
		/// </remarks>
		public static bool Refresh(Assembly assembly, string assemblyFullName, DotNetProjectResolver projectResolver) {
			lock (threadSync) {
				if (projectContents != null) {
					ProjectContentInfo info = (ProjectContentInfo)projectContents[assemblyFullName.ToLower()];
					if (info != null) {
						// Create options
						AssemblyCodeRepositoryOptions options = new AssemblyCodeRepositoryOptions(projectResolver);

						// Create a loader and use it to update the project content
						ProjectContentLoader loader = new ProjectContentLoader();
						loader.Assembly = assembly;
						loader.AssemblyFullName = assemblyFullName;
						info.ProjectContent = loader.CreateProjectContent(options, projectResolver.HostAppDomainResolver);

						#if DEBUG && DEBUG_REPOSITORY
						Trace.WriteLine(String.Format("AssemblyCodeRepository.Refresh: Refreshed the assembly '{0}'", assemblyFullName));
						#endif
						return true;
					}
				}

				#if DEBUG && DEBUG_REPOSITORY
				Trace.WriteLine(String.Format("AssemblyCodeRepository.Refresh: Failed to refresh the assembly '{0}'", assemblyFullName));
				#endif
			}
			return false;
		}
		
		/// <summary>
		/// Removes an assembly from the code repository.
		/// </summary>
		/// <param name="assemblyFullName">The full name of the assembly to remove.</param>
		/// <returns>
		/// <c>true</c> if the assembly is removed; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// The assembly is only removed if the reference count is zero.
		/// </remarks>
		public static bool Remove(string assemblyFullName) {
			lock (threadSync) {
				if (projectContents != null) {
					ProjectContentInfo info = (ProjectContentInfo)projectContents[assemblyFullName.ToLower()];
					if (info != null) {
						info.ReferenceCount--;
						if (info.ReferenceCount <= 0) {
							info.ProjectContent.Dispose();
							projectContents.Remove(assemblyFullName.ToLower());

							#if DEBUG && DEBUG_REPOSITORY
							Trace.WriteLine(String.Format("AssemblyCodeRepository.Remove: Removed the assembly '{0}' successfully ({1} assemblies remaining)", assemblyFullName, projectContents.Count));
							#endif

							if (projectContents.Count == 0)
								projectContents = null;

							return true;
						}

						#if DEBUG && DEBUG_REPOSITORY
						Trace.WriteLine(String.Format("AssemblyCodeRepository.Remove: Didn't remove the assembly '{0}' because it still had {1} references ({2} assemblies remaining)", assemblyFullName, info.ReferenceCount, projectContents.Count));
						#endif
						return true;
					}
				}

				#if DEBUG && DEBUG_REPOSITORY
				Trace.WriteLine(String.Format("AssemblyCodeRepository.Remove: Failed to remove the assembly '{0}' ({1} assemblies remaining)", assemblyFullName, (projectContents != null ? projectContents.Count : 0)));
				#endif
			}
			return false;
		}
		
	}
}
