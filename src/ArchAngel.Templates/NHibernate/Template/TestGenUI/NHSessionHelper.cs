using NHibernate;
using NHibernate.Cfg;
using Project.Model;

namespace TestGenUI
{
	public class NHSessionHelper
	{
		private static ISessionFactory factory;

		public static ISession OpenSession()
		{
			if (factory == null)
			{
				var assembly = typeof(Order).Assembly;

				Configuration c = new Configuration();
				c.Configure("hibernate.cfg.xml");
				c.AddAssembly(assembly);
				factory = c.BuildSessionFactory();
			}
			return factory.OpenSession();
		}
		
	}

}
