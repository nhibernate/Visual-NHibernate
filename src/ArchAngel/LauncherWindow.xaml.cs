using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using ArchAngel.Common;
using ArchAngel.Interfaces;
using ArchAngel.NHibernateHelper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using Slyce.Common;
using Utility=ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Utility;

namespace ArchAngel
{
	/// <summary>
	/// Interaction logic for LauncherWindow.xaml
	/// </summary>
	public partial class LauncherWindow
	{
		public LauncherWindow()
		{
			InitializeComponent();
		}

		private void buttonStartWorkbench_Click(object sender, RoutedEventArgs e)
		{
            Workbench.Program.Main(new string[0]);
			Close();
		}

		private void buttonStartWorkbenchNH_Click(object sender, RoutedEventArgs e)
		{
			Workbench.Program.Main(new[] { "-brand", ApplicationBrand.VisualNHibernate.ToString() });
			Close();
		}

		private void buttonStartDesigner_Click(object sender, RoutedEventArgs e)
		{
            Designer.Program.Main(new string[0]);
			Close();
		}

		private void buttonRunHBM2ModelTransform_Click(object sender, RoutedEventArgs args)
		{
			var loader = new EntityLoader(new FileController());
			var database = new Database("DB1", ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.SQLServer2005);
			var table = new Table("TableName", "");
			table.AddColumn(new Column("ID"));
			table.AddColumn(new Column("Speed"));
			table.AddColumn(new Column("Name"));
			table.AddColumn(new Column("Speed"));
			table.AddColumn(new Column("Discriminator"));
			table.AddColumn(new Column("Car_Make"));
			table.AddColumn(new Column("Bike_Model"));

			database.AddTable(table);
			loader.GetEntities(new[] {"nhtest.xml"}, database);
		}

		private void buttonRunNHTemplateTest_Click(object sender, RoutedEventArgs args)
		{
            // Setup log4net
            log4net.Config.XmlConfigurator.Configure();

			//hibernatemapping hm = Utility.Open("nhtest.xml");

            var project = new WorkbenchProject();
            project.Load(@"..\..\..\ArchAngel.Templates\NHibernate\Northwind\Northwind.wbproj",
                new NullVerificationIssueSolver(),
                false, @"..\..\..\ArchAngel.Templates\NHibernate\Template\NHibernate.AAT.DLL");
            SharedData.CurrentProject = project;
            
            MappingSet mappingSet = project.Providers[0].GetAllObjectsOfType(typeof(MappingSet).FullName).OfType<MappingSet>().First();
		    var entitySet = mappingSet.EntitySet;

			if (Directory.Exists("Output") == false)
			{
				Directory.CreateDirectory("Output");
			}

			foreach (var entity in entitySet.Entities)
			{
				if (Utility.SkipEntity(entity)) continue;

				try
				{
					string xml = Utility.CreateMappingXMLFrom(entity, "Test", "Test", true, "", DefaultCascadeTypes.none, true);
					File.WriteAllText(Path.Combine("Output", entity.Name + ".xml"), xml);
				}
				catch (Exception e)
				{
                    //string msg = string.Format("Exception: {0}\nMessage: {1}\nStack: {2}", e.GetType(), e.Message, e.StackTrace);

                    StringBuilder sb = new StringBuilder();
				    Exception currentEx = e.InnerException;
                    sb.AppendLine(e.ToString());
                    while(currentEx != null)
                    {
                        sb.AppendLine("Inner Exception:");
                        sb.AppendLine(currentEx.ToString());
                        currentEx = currentEx.InnerException;
                    }

					File.WriteAllText(Path.Combine("Output", entity.Name + ".xml"), sb.ToString());
				}
			}

			Process.Start("Output");

			Application.Current.Shutdown();
		}
	}
}
