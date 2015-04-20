using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Controls.ContentItems;
using ArchAngel.Providers.EntityModel.Controller.Validation;
using ArchAngel.Providers.EntityModel.Controller.Validation.Modules;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI;
using log4net;
using MappingSetDeserialisationScheme = ArchAngel.Providers.EntityModel.Model.MappingLayer.MappingSetDeserialisationScheme;
using MappingSetImpl = ArchAngel.Providers.EntityModel.Model.MappingLayer.MappingSetImpl;
using MappingSetSerialisationScheme = ArchAngel.Providers.EntityModel.Model.MappingLayer.MappingSetSerialisationScheme;
using ValidationResult = ArchAngel.Interfaces.ValidationResult;

namespace ArchAngel.Providers.EntityModel
{
	[DotfuscatorDoNotRename]
	public class ProviderInfo : Interfaces.ProviderInfo
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ProviderInfo));

		private MappingSet mappingSet;
		private const string DatabaseFilename = "database.xml";
		private const string EntitiesFilename = "entities.xml";
		private const string MappingsFilename = "mappings.xml";
		private static readonly Dictionary<string, Type> cachedTypes = new Dictionary<string, Type>();
		private EditModel editModelScreen;
		private string loadedDatabaseXML;
		private IUserInteractor customUserInteractor;
		private bool screensCreated;
		private ValidationRulesEngine _Engine = null;

		public ProviderInfo()
		{
			MappingSet = new MappingSetImpl(new Database("New Database", ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.SQLServer2005), new EntitySetImpl());

			Name = "Entity Provider";
			Description = "A Provider that allows mapping between Database Tables/Views/StoredProcedures and Entities";

			//if (ArchAngel.Interfaces.SharedData.CurrentProject.Options.SingleOrDefault(o => o.VariableName == "abc") == null)
			//{
			//    ArchAngel.Interfaces.SharedData.CurrentProject.Options.Add(new ArchAngel.Interfaces.TemplateInfo.Option()
			//    {
			//        Category = "C#",
			//        DefaultValue = "false",
			//        DefaultValueIsFunction = false,
			//        Description = "Include foreign-key?",
			//        DisplayToUserValue = true,
			//        Enabled = true,
			//        ResetPerSession = false,
			//        Text = "Include the foreign-key column in the model?",
			//        VarType = typeof(bool),
			//        VariableName = "IncludeForeignKeyColumn",
			//        IteratorName = "ArchAngel.Providers.EntityModel.Model.EntityLayer.Reference"
			//    });
			//}
			this.OptionForms.Add(new UI.PropertyGrids.FormPrefixes());
		}

		public bool IsDatabaseLoaded { get { return MappingSet.Database.IsEmpty == false; } }
		public bool IsEntitySetLoaded { get { return MappingSet.EntitySet.IsEmpty == false; } }

		public override void InitialisePreGeneration(ArchAngel.Interfaces.PreGenerationData data)
		{
		}

		public override void InitialiseScriptObjects(ArchAngel.Interfaces.PreGenerationData data)
		{
		}

		public IUserInteractor UserInteractor
		{
			get
			{
				if (customUserInteractor != null)
					return customUserInteractor;

				return editModelScreen;
			}
		}

		public override void CreateScreens()
		{
			if (screensCreated) return;

			// This hack prevents the debugger from loading the provider screens.
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA) return;

			editModelScreen = new EditModel { Provider = this };
			editModelScreen.SetMappingSet(MappingSet);

			Screens = new[] { (ContentItem)editModelScreen };

			screensCreated = true;
		}

		public MappingSet MappingSet
		{
			get
			{
				return mappingSet;
			}
			set
			{
				mappingSet = value;

				if (editModelScreen != null && value != null)
				{
					editModelScreen.Clear();
					editModelScreen.SetMappingSet(MappingSet);
					editModelScreen.SetFocusToFirstEntityNode();
				}
			}
		}

		public override IEnumerable<object> RootPreviewObjects
		{
			get
			{
				return new[] { MappingSet };
			}
		}

		public override void Save(string folder)
		{
			string xmlHeader = "<?xml version=\"1.0\" ?>" + Environment.NewLine;

			if (editModelScreen != null)
				editModelScreen.Save();

			string xml = new DatabaseSerialisationScheme().Serialise(MappingSet.Database);
			File.WriteAllText(Path.Combine(folder, DatabaseFilename), xmlHeader + xml);

			xml = new EntitySetSerialisationScheme().SerialiseEntitySet(MappingSet.EntitySet);
			File.WriteAllText(Path.Combine(folder, EntitiesFilename), xmlHeader + xml);

			xml = new MappingSetSerialisationScheme().SerialiseMappingSet(MappingSet);
			File.WriteAllText(Path.Combine(folder, MappingsFilename), xmlHeader + xml);
		}

		public override void Open(string folder)
		{
			string databaseFilePath = Path.Combine(folder, DatabaseFilename);
			string entityFilePath = Path.Combine(folder, EntitiesFilename);
			string mappingsFilePath = Path.Combine(folder, MappingsFilename);

			if (File.Exists(databaseFilePath) == false)
				throw new ArgumentException("Database definition file missing.");
			if (File.Exists(entityFilePath) == false)
				throw new ArgumentException("Entity definition file missing.");
			if (File.Exists(mappingsFilePath) == false)
				throw new ArgumentException("Mapping definition file missing.");

			string databaseXml = File.ReadAllText(databaseFilePath);
			string entitiesXml = File.ReadAllText(entityFilePath);
			string mappingsXml = File.ReadAllText(mappingsFilePath);


			try
			{
				var database = new DatabaseDeserialisationScheme().Deserialise(databaseXml);
				var entitySet = new EntitySetDeserialisationScheme().DeserialiseEntitySet(entitiesXml, database);

				MappingSet = new MappingSetDeserialisationScheme().DeserialiseMappingSet(mappingsXml, database, entitySet);

				// If everything loaded correctly, copy the database file away so we can use it to figure out what the user has changed
				// in this session.
				loadedDatabaseXML = databaseXml;
			}
			catch (Exception e)
			{
				log.ErrorFormat("Error opening Entity Model Provider files on disk");
				log.Error(e.Message);
#if DEBUG
				if (MessageBox.Show("An error occurred while opening the project files.\nDo you want to ignore the invalid model on disk? See logs for exception", "Error",
								MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
				{
					Clear();
				}
				else
				{
					throw;
				}
#endif
			}
		}

		internal string LoadedDatabaseXml { get { return loadedDatabaseXML; } }

		public override void Clear()
		{
			MappingSet = new MappingSetImpl(new Database("New Database", ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.Unknown), new EntitySetImpl());

			if (editModelScreen != null)
				editModelScreen.SetMappingSet(MappingSet);
		}

		public override bool IsValid(out string failReason)
		{
			failReason = "";
			return true;
		}

		public override IEnumerable<IScriptBaseObject> GetAllObjectsOfType(string typeName)
		{
			Type type = GetTypeFromTypeName(typeName);

			return GetAllObjectsOfType(type).ToArray();
		}

		public IEnumerable<IScriptBaseObject> GetAllObjectsOfType(Type type)
		{
			if (type == typeof(Database) || type == typeof(IDatabase))
				return new[] { MappingSet.Database };
			if (type == typeof(MappingSet) || type == typeof(MappingSetImpl))
				return new[] { MappingSet };
			if (type == typeof(EntitySet) || type == typeof(EntitySetImpl))
				return new[] { MappingSet.EntitySet };
			if (type == typeof(Mapping) || type == typeof(MappingImpl))
				return MappingSet.Mappings.ToArray();
			if (type == typeof(TableReferenceMapping) || type == typeof(TableReferenceMappingImpl))
				return MappingSet.ReferenceMappings.ToArray();
			if (type == typeof(RelationshipReferenceMapping) || type == typeof(RelationshipReferenceMappingImpl))
				return MappingSet.RelationshipMappings.ToArray();
			if (type == typeof(ComponentMapping) || type == typeof(ComponentMappingImpl))
				return MappingSet.ComponentMappings.ToArray();


			if (type == typeof(Entity) || type == typeof(EntityImpl))
				return MappingSet.EntitySet.Entities.ToArray();
			if (type == typeof(Reference) || type == typeof(ReferenceImpl))
				return MappingSet.EntitySet.References.ToArray();
			if (type == typeof(ComponentSpecification) || type == typeof(ComponentSpecificationImpl))
				return MappingSet.EntitySet.ComponentSpecifications.ToArray();

			if (type == typeof(Table) || type == typeof(ITable))
				return MappingSet.Database.Tables.ToArray();
			if (type == typeof(Relationship) || type == typeof(RelationshipImpl))
				return MappingSet.Database.Relationships.ToArray();

			// Complex object searches
			if (type == typeof(Column) || type == typeof(IColumn))
			{
				List<IColumn> columns = new List<IColumn>();
				foreach (var entityObject in MappingSet.Database.Tables)
					columns.AddRange(entityObject.Columns);
				return columns.ToArray();
			}

			if (type == typeof(Key) || type == typeof(IKey))
			{
				List<IKey> keys = new List<IKey>();
				foreach (var entityObject in MappingSet.Database.Tables)
					keys.AddRange(entityObject.Keys);
				return keys.ToArray();
			}

			if (type == typeof(Index) || type == typeof(IIndex))
			{
				List<IIndex> indexes = new List<IIndex>();
				foreach (var entityObject in MappingSet.Database.Tables)
					indexes.AddRange(entityObject.Indexes);
				return indexes.ToArray();
			}

			if (type == typeof(Property) || type == typeof(PropertyImpl))
			{
				List<Property> properties = new List<Property>();
				foreach (var entity in MappingSet.EntitySet.Entities)
					properties.AddRange(entity.Properties);
				return properties.ToArray();
			}

			if (type == typeof(Component) || type == typeof(ComponentImpl))
			{
				List<Component> components = new List<Component>();
				foreach (var entity in MappingSet.EntitySet.Entities)
					components.AddRange(entity.Components);
				return components.ToArray();
			}

			if (type == typeof(ComponentProperty) || type == typeof(ComponentPropertyImpl))
			{
				List<ComponentProperty> properties = new List<ComponentProperty>();
				foreach (var component in MappingSet.EntitySet.ComponentSpecifications)
					properties.AddRange(component.Properties);
				return properties.ToArray();
			}

			if (type == typeof(EntityKey) || type == typeof(EntityKeyImpl))
			{
				List<EntityKey> keys = new List<EntityKey>();
				foreach (var entity in MappingSet.EntitySet.Entities)
					keys.Add(entity.Key);
				return keys.ToArray();
			}

			throw new NotImplementedException("We do not currently handle " + type.FullName + " objects in GetAllObjectsOfType(Type type)");
		}

		public override IEnumerable<IScriptBaseObject> GetAllObjectsOfType(string typeName, IScriptBaseObject rootObject)
		{
			if (rootObject == MappingSet)
			{
				return GetAllObjectsOfType(typeName);
			}

			if (rootObject is ITable)
			{
				if (typeName == typeof(IColumn).FullName)
					return ((ITable)rootObject).Columns.ToArray();
				if (typeName == typeof(IKey).FullName)
					return ((ITable)rootObject).Keys.ToArray();
				if (typeName == typeof(IIndex).FullName)
					return ((ITable)rootObject).Indexes.ToArray();
			}

			if (rootObject is Entity)
			{
				if (typeName == typeof(Property).FullName)
					return ((Entity)rootObject).Properties.ToArray();
				if (typeName == typeof(EntityKey).FullName)
					return new[] { ((Entity)rootObject).Key };
				if (typeName == typeof(Component).FullName)
					return ((Entity)rootObject).Components.ToArray();
			}

			if (rootObject is IDatabase)
			{
				if (typeName == typeof(ITable).FullName)
				{
					return ((IDatabase)rootObject).Tables.ToArray();
				}
			}

			throw new NotImplementedException("We do not currently handle " + rootObject.GetType().FullName + " types as root objects or " + typeName + " as objects to get types of.");
		}

		public override void LoadFromNewProjectInformation(INewProjectInformation info)
		{
			if (info == null || info is TemplateSpecifiedNewProjectInformation == false) return;

			// At the moment, only NHibernate Helper has an inherited version of TemplateSpecifiedNewProjectInformation.
			// If we ever add inherit from TemplateSpecifiedNewProjectInformation in the Entity Provider,
			// then we'll need to re-think this code below:
			if (info.GetType() != typeof(TemplateSpecifiedNewProjectInformation))
				return;

			IUserInteractor userInteractor = customUserInteractor ?? editModelScreen;

			var newProjectInfo = (TemplateSpecifiedNewProjectInformation)info;
			newProjectInfo.RunCustomNewProjectLogic(this, userInteractor);
		}

		public override ValidationResult RunPreGenerationValidation()
		{
			if (MappingSet == null)
				return new ValidationResult();

			//var engine = new ValidationRulesEngine(MappingSet);
			//engine.AddModule(new ValidateModelModule());

			ValidationResults results = RunValidationRules();//engine);

			return results.HasAnyErrorsOrWorse ? new ValidationResult(true, editModelScreen) : new ValidationResult();
		}


		public ValidationRulesEngine Engine
		{
			get
			{
				if (_Engine == null)
				{
					_Engine = new ValidationRulesEngine(MappingSet);
					_Engine.AddModule(new ValidateModelModule());

					foreach (var provider in OtherProviders)
					{
						if (provider is IReturnValdationRules)
						{
							foreach (IValidationRule providerRule in ((IReturnValdationRules)provider).ValidationRules)
							{
								_Engine.AddRule(providerRule);
							}
						}
					}
				}
				return _Engine;
			}
		}

		public ValidationResults RunValidationRules()//ValidationRulesEngine engine)
		{
			var results = Engine.RunAllRules();

			if (editModelScreen != null && results.HasAnyIssues)
				editModelScreen.ShowValidationResults(results);

			return results;
		}

		private Type GetTypeFromTypeName(string name)
		{
			lock (cachedTypes)
			{
				if (cachedTypes.ContainsKey(name))
					return cachedTypes[name];

				Type type = GetType().Assembly.GetType(name);
				cachedTypes[name] = type;
				return type;
			}
		}
	}

	public abstract class TemplateSpecifiedNewProjectInformation : INewProjectInformation
	{
		public virtual Type ValidProviderType
		{
			get { return typeof(ProviderInfo); }
		}

		public abstract void RunCustomNewProjectLogic(ArchAngel.Interfaces.ProviderInfo providerInfo, IUserInteractor userInteractor);

		public string OutputPath { get; set; }
	}
}
