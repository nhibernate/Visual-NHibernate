using System.Windows;
using log4net.Config;

namespace ArchAngel
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			XmlConfigurator.Configure();
		}
	}
}
