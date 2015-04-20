using System;
using System.Collections.Generic;
using System.ComponentModel;
using ArchAngel.Interfaces;
using ArchAngel.NHibernateHelper.Validation;
using ArchAngel.Providers.EntityModel;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Controller.MappingLayer;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI;
using Slyce.Common;
using Slyce.Common.StringExtensions;

namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
	public class LoadExistingNHibernateProjectInfo : TemplateSpecifiedNewProjectInformation
	{
		public override Type ValidProviderType
		{
			get { return typeof(NHibernateHelper.ProviderInfo); }
		}

		public IDatabaseLoader DatabaseLoader { get; set; }

		public NHConfigFile NhConfigFile { get; set; }

		public string Filename { get; set; }

		public override void RunCustomNewProjectLogic(ArchAngel.Interfaces.ProviderInfo theProviderInfo, IUserInteractor userInteractor)
		{
			NHibernateHelper.ProviderInfo providerInfo = (NHibernateHelper.ProviderInfo)theProviderInfo;
			providerInfo.Clear();
			providerInfo.NhConfigFile = this.NhConfigFile;
			userInteractor.ShowWaitScreen("Loading from your existing NHibernate project");
			// Run this in its own thread
			BackgroundWorker worker = new BackgroundWorker();

			worker.DoWork
				+= (s, args) =>
				{
					try
					{
						var loader = new ProjectLoader(new FileController(), new IUserInteractorProgressUpdatorAdapter(userInteractor), userInteractor);
						var result = loader.LoadEntityModelFromCSProj(Filename, this.NhConfigFile);
						args.Result = new RunResult { MappingSet = result.MappingSet, DatabaseLoader = result.DatabaseLoader, NhConfigFile = result.NhConfigFile, CsProjFile = result.CsProjFile };
					}
					catch (NHibernateLoaderException e)
					{
						//var sb = new StringBuilder();
						//sb.Append("The HBM files [").Append(e.Filenames).AppendLine(
						//    "] could not be loaded due to the following errors:");
						//e.Errors.ForEach(err => sb.AppendLine(err.Message));
						//sb.AppendLine("Please send this file to support@slyce.com so we can make our HBM Loader better.");

						args.Result = new RunResult { ErrorOccurred = true, Exception = e };
					}
					catch (Exception e)
					{
						throw new NHibernateLoaderException(e.Message + Environment.NewLine + e.StackTrace, null, null);
					}
				};

			worker.RunWorkerCompleted
				+= (s, args) =>
					{
						if (args.Error != null)
						{
							userInteractor.RemoveWaitScreen();
							providerInfo.EntityProviderInfo.MappingSet = new MappingSetImpl();
							throw new Exception("An error occurred in RunCustomNewProjectLogic. The inner exception is: " + args.Error.StackTrace, args.Error);
							//System.Windows.Forms.Clipboard.SetText(args.Error.StackTrace);
							//userInteractor.ShowError("An Error Occurred", args.Error.Message + Environment.NewLine + Environment.NewLine + "The stacktrace has been copied to the clipboard. Please email to support@slyce.com");
							//providerInfo.EntityProviderInfo.MappingSet = new MappingSetImpl();
						}
						else
						{
							var result = (RunResult)args.Result;
							if (result.ErrorOccurred)
							{
								userInteractor.RemoveWaitScreen();
								providerInfo.EntityProviderInfo.MappingSet = new MappingSetImpl();

								string errorMessage = string.Format("Unsupported elements or Schema Validation Errors occurred. Please submit this error.\nFiles: {0}", result.Exception.Filenames);
								throw new Exception(errorMessage, result.Exception);
								//var form = new NHibernateHBMLoadErrorView();
								//form.Title = "Unsupported elements or Schema Validation Errors occurred";
								//form.NameOfFileWithError = result.Exception.Filename;
								//form.SetErrors(result.Exception.Errors);

								//userInteractor.ShowDialog(form);

							}
							else
							{
								// Set the MappingSet to the result of our work.
								providerInfo.EntityProviderInfo.MappingSet = result.MappingSet;
								providerInfo.NhConfigFile = result.NhConfigFile;
								providerInfo.CsProjFile = result.CsProjFile;

								if (!string.IsNullOrEmpty(providerInfo.CsProjFile.GetProjectGuid()))
									ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("ProjectGuid", providerInfo.CsProjFile.GetProjectGuid());

								ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("CacheProviderClass", providerInfo.NhConfigFile.cache_provider_class);

								// Then run the validation rules
								userInteractor.UpdateWaitScreen("Runnng Model Validation");
								//var rulesEngine = new ValidationRulesEngine(result.MappingSet);
								//var database = result.DatabaseLoader.LoadDatabase();
								//rulesEngine.AddModule(new NHibernateProjectLoaderModule(database));
								providerInfo.EntityProviderInfo.RunValidationRules();//rulesEngine);

								userInteractor.RemoveWaitScreen();
							}
						}
					};

			worker.RunWorkerAsync();
		}

		private class RunResult
		{
			public bool ErrorOccurred;
			public MappingSet MappingSet;
			public NHibernateLoaderException Exception;
			public IDatabaseLoader DatabaseLoader;
			public NHConfigFile NhConfigFile;
			public CSProjFile CsProjFile;
		}
	}

	public class IUserInteractorProgressUpdatorAdapter : IProgressUpdater
	{
		private readonly IUserInteractor interactor;

		public IUserInteractorProgressUpdatorAdapter(IUserInteractor interactor)
		{
			this.interactor = interactor;
		}

		public void SetCurrentState(string message, ProgressState type)
		{
			if (type == ProgressState.Normal)
			{
				interactor.UpdateWaitScreen(message);
			}
			else if (type == ProgressState.Error || type == ProgressState.Fatal)
			{
				interactor.RemoveWaitScreen();
				interactor.ShowError("Error Occurred", message);
			}
		}

		public void SetCurrentState(string message, int percentageProgress, ProgressState type)
		{

		}
	}

	public class LoadExistingDatabaseInfo : TemplateSpecifiedNewProjectInformation
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(LoadExistingDatabaseInfo));
		public IDatabaseLoader DatabaseLoader { get; set; }
		public ArchAngel.Providers.EntityModel.Helper.ConnectionStringHelper ConnStringHelper { get; set; }
		public List<string> TablePrefixes { get; set; }
		public List<string> ColumnPrefixes { get; set; }
		public List<string> TableSuffixes { get; set; }
		public List<string> ColumnSuffixes { get; set; }

		public override Type ValidProviderType
		{
			get { return typeof(NHibernateHelper.ProviderInfo); }
		}

		public override void RunCustomNewProjectLogic(ArchAngel.Interfaces.ProviderInfo theProviderInfo, IUserInteractor userInteractor)
		{
			//if (theProviderInfo is ArchAngel.Providers.EntityModel.ProviderInfo)
			//{
			//    ArchAngel.Providers.EntityModel.ProviderInfo providerInfo = (ArchAngel.Providers.EntityModel.ProviderInfo)theProviderInfo;
			//    providerInfo.Clear();
			//    return;
			//}

			try
			{
				log.Debug("Loading project...");
				userInteractor.UpdateWaitScreen("Loading project...");

				NHibernateHelper.ProviderInfo providerInfo = (NHibernateHelper.ProviderInfo)theProviderInfo;
				providerInfo.Clear();

				log.Debug("Loading database...");
				Database database = DatabaseLoader.LoadDatabase(DatabaseLoader.DatabaseObjectsToFetch, null);
				DatabaseProcessor dbProcessor = new DatabaseProcessor();
				dbProcessor.LogErrors = true;
				log.Debug("Creating relationships...");
				dbProcessor.CreateRelationships(database);

				if (dbProcessor.Errors.Count > 0)
				{
					log.Debug("Database errors exist..." + dbProcessor.Errors.Count.ToString());
					UI.FormErrors form = new UI.FormErrors("<b><font color='Red'>Note:</font></b> Database problems exist. Please <b>fix</b> these problems (or <b>omit the tables</b> in question) before trying again.", dbProcessor.Errors);
					form.ShowDialog();
					return;
				}
				log.Debug("Creating 1 to 1 mappings...");
				var mappingSet = new MappingProcessor(new OneToOneEntityProcessor())
					.CreateOneToOneMapping(database, this.TablePrefixes, this.ColumnPrefixes, this.TableSuffixes, this.ColumnSuffixes);

				foreach (var entity in mappingSet.EntitySet.Entities)
				{
					ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.ExistingPropertyNames = new List<string>();

					foreach (Property prop in entity.Properties)
					{
						IColumn mappedCol = prop.MappedColumn();

						ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn scriptCol = new Interfaces.Scripting.NHibernate.Model.IColumn()
						{
							IsNullable = mappedCol.IsNullable,
							//IsText = 
							Length = mappedCol.Size,
							Name = mappedCol.Name,
							ScriptObject = mappedCol,
							Type = mappedCol.OriginalDataType
						};
						prop.Name = ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.GetPropertyName(scriptCol);
					}
				}
				providerInfo.EntityProviderInfo.MappingSet = mappingSet;
				/////////////////////////////////
				providerInfo.EntityProviderInfo.Engine.AddModule(new NHibernateProjectLoaderModule(database));
				// Then run the validation rules
				log.Debug("Validating model...");
				userInteractor.UpdateWaitScreen("Validating model...");
				//var rulesEngine = new ValidationRulesEngine(mappingSet);
				//rulesEngine.AddModule(new NHibernateProjectLoaderModule(database));
				log.Debug("Running validation rules...");
				providerInfo.EntityProviderInfo.RunValidationRules();//rulesEngine);
			}
			//catch (Exception ex)
			//{
			//    MessageBox.
			//}
			finally
			{
				log.Debug("Removing wait screen...");
				userInteractor.RemoveWaitScreen();
			}
		}

		private class RunResult
		{
			public bool ErrorOccurred;
			public MappingSet MappingSet;
			public Exception Exception;
			public IDatabaseLoader DatabaseLoader;
		}
	}
}